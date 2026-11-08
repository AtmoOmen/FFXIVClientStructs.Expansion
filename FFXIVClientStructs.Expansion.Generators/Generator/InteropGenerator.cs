using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

[Generator]
public sealed partial class InteropGenerator : IIncrementalGenerator
{
    public void Initialize
    (
        IncrementalGeneratorInitializationContext context
    )
    {
        IncrementalValuesProvider<StructInfo> structInfos =
            context.SyntaxProvider.ForAttributeWithMetadataName
                   (
                       InteropTypeNames.GenerateInteropAttribute,
                       static (node, _) => node is StructDeclarationSyntax { AttributeLists.Count: > 0 },
                       static (context, token) => context.TargetSymbol is INamedTypeSymbol structSymbol ?
                                                      ParseStructInfo(structSymbol, context.Attributes[0], token) :
                                                      null
                   )
                   .Where(static si => si is not null)!;

        var generatorOptionsProvider = context.AnalyzerConfigOptionsProvider.Select
        (static (options, _) =>
            {
                if (!options.GlobalOptions.TryGetValue("build_property.InteropGenerator_InteropNamespace", out var interopNamespace))
                    interopNamespace = "FFXIVClientStructs.Expansion.Runtime.Generated";
                return new GeneratorOptions
                (
                    interopNamespace
                );
            }
        );

        context.RegisterSourceOutput
        (
            structInfos.Collect().Combine(generatorOptionsProvider),
            static (sourceContext, args) =>
            {
                var addressIndex = 0;

                foreach (var item in args.Left)
                {
                    if (item.NeedsRender())
                        sourceContext.AddSource
                        (
                            $"{item.FullyQualifiedMetadataName}.InteropGenerator.g.cs",
                            RenderStructInfo(item, addressIndex, args.Right.InteropNamespace, sourceContext.CancellationToken)
                        );

                    addressIndex += item.MemberFunctions.Length + item.StaticAddresses.Length;

                    if (item.StaticVirtualTableSignature is not null)
                        addressIndex++;
                }
            }
        );
        var structInfosWithInheritance = structInfos.Where(static sI => sI.InheritedStructs.Length  != 0).Collect();
        var structInfosInherited       = structInfos.Where(static sI => sI.ExtraInheritedStructInfo != null).Collect();

        var structInfosWithInheritedStructs =
            structInfosWithInheritance.Combine(structInfosInherited).Combine(context.CompilationProvider).SelectMany
            (static (structInfos, token) =>
                {
                    var structsWithInheritance = structInfos.Left.Left;
                    var structsInherited       = structInfos.Left.Right;

                    using ImmutableArrayBuilder<(StructInfo targetStruct, ImmutableArray<StructInfo> inheritedStructs)> structInheritanceGroupedBuilder = new();

                    Dictionary<string, ImmutableArray<StructInfo>> tempInheritanceMap = new();

                    foreach (var targetStruct in structsWithInheritance)
                    {
                        using ImmutableArrayBuilder<StructInfo> inheritedStructsBuilder = new();

                        foreach (var inheritedStruct in targetStruct.InheritedStructs)
                        {
                            CollectInheritedStructs
                                (inheritedStruct.InheritedTypeName, structsInherited, structInfos.Right, token, tempInheritanceMap, inheritedStructsBuilder);
                        }

                        structInheritanceGroupedBuilder.Add((targetStruct, inheritedStructsBuilder.ToImmutable()));
                        token.ThrowIfCancellationRequested();
                    }

                    return structInheritanceGroupedBuilder.ToImmutable();
                }
            );

        context.RegisterSourceOutput
        (
            structInfosWithInheritedStructs.Collect().Combine(generatorOptionsProvider),
            static (sourceContext, args) =>
            {
                Dictionary<string, string> sharedPointerBases = new(StringComparer.Ordinal);

                foreach (var item in args.Left)
                {
                    sourceContext.AddSource
                    (
                        $"{item.targetStruct.FullyQualifiedMetadataName}.Inheritance.InteropGenerator.g.cs",
                        RenderInheritedStructInfo
                        (
                            item.targetStruct,
                            item.inheritedStructs,
                            args.Right.InteropNamespace,
                            sharedPointerBases,
                            sourceContext.CancellationToken
                        )
                    );
                }

                if (sharedPointerBases.Count != 0)
                    sourceContext.AddSource
                    (
                        $"{args.Right.InteropNamespace}.InheritedMemberFunctionPointers.g.cs",
                        RenderInheritedMemberFunctionPointerBases(args.Right.InteropNamespace, sharedPointerBases)
                    );
            }
        );
        var structInfosWithAddresses = structInfos.Where(static sI => sI.HasSignatures()).Collect();

        context.RegisterSourceOutput
        (
            structInfosWithAddresses.Combine(generatorOptionsProvider),
            static (SourceProductionContext sourceContext, (ImmutableArray<StructInfo> StructInfos, GeneratorOptions GeneratorOptions) args) =>
            {
                if (!args.StructInfos.IsEmpty)
                    sourceContext.AddSource
                    (
                        $"{args.GeneratorOptions.InteropNamespace}.Addresses.g.cs",
                        RenderResolverInitializer(args.StructInfos, args.GeneratorOptions.InteropNamespace)
                    );
            }
        );
        var structInfosWithFixedArrays = structInfos.Where(static sI => !sI.FixedSizeArrays.IsEmpty).Collect();

        context.RegisterSourceOutput
        (
            structInfosWithFixedArrays.Combine(generatorOptionsProvider),
            static (SourceProductionContext sourceContext, (ImmutableArray<StructInfo> StructInfos, GeneratorOptions GeneratorOptions) args) =>
            {
                if (!args.StructInfos.IsEmpty)
                    sourceContext.AddSource
                    (
                        $"{args.GeneratorOptions.InteropNamespace}.FixedArrays.g.cs",
                        RenderFixedArrayTypes(args.StructInfos, args.GeneratorOptions.InteropNamespace)
                    );
            }
        );
    }

    private static void CollectInheritedStructs
    (
        string                                         inheritedTypeName,
        ImmutableArray<StructInfo>                     validInheritedTypes,
        Compilation                                    compilation,
        CancellationToken                              token,
        Dictionary<string, ImmutableArray<StructInfo>> processedInheritanceMap,
        ImmutableArrayBuilder<StructInfo>              inheritedStructsBuilder
    )
    {
        if (processedInheritanceMap.TryGetValue(inheritedTypeName, out var processedInheritance))
        {
            inheritedStructsBuilder.AddRange(processedInheritance.AsSpan());
            return;
        }

        var targetInheritedStruct = validInheritedTypes.FirstOrDefault(sInfo => sInfo.FullyQualifiedMetadataName == inheritedTypeName);

        if (targetInheritedStruct == null)
        {
            var inheritedSymbol = compilation.GetTypeByMetadataName(inheritedTypeName);
            if (inheritedSymbol == null || SymbolEqualityComparer.Default.Equals(inheritedSymbol.ContainingAssembly, compilation.Assembly))
                return;
            targetInheritedStruct = ParseExternalInheritedStructInfo(inheritedSymbol, compilation, token);
        }

        using ImmutableArrayBuilder<StructInfo> inheritanceHierarchyBuilder = new();
        inheritanceHierarchyBuilder.Add(targetInheritedStruct);

        if (targetInheritedStruct.InheritedStructs.Length != 0)
        {
            foreach (var inheritanceInfo in targetInheritedStruct.InheritedStructs)
            {
                CollectInheritedStructs
                    (inheritanceInfo.InheritedTypeName, validInheritedTypes, compilation, token, processedInheritanceMap, inheritanceHierarchyBuilder);
            }
        }

        var inheritanceHierarchy = inheritanceHierarchyBuilder.ToImmutable();
        inheritedStructsBuilder.AddRange(inheritanceHierarchy.AsSpan());
        processedInheritanceMap.Add(inheritedTypeName, inheritanceHierarchy);
    }
}
