using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

[Generator]
public sealed partial class InteropGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        IncrementalValuesProvider<StructInfo> structInfos =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                    InteropTypeNames.GenerateInteropAttribute,
                    static (node, _) => node is StructDeclarationSyntax { AttributeLists.Count: > 0 },
                    static (context, token) => {
                        if (context.TargetSymbol is not INamedTypeSymbol structSymbol)
                            return null;
                        return ParseStructInfo(structSymbol, context.Attributes[0], token);
                    })
                .Where(static si => si is not null)!;

        context.RegisterSourceOutput(structInfos,
            static (sourceContext, item) => {
                if (item.NeedsRender())
                    sourceContext.AddSource($"{item.FullyQualifiedMetadataName}.InteropGenerator.g.cs", RenderStructInfo(item, sourceContext.CancellationToken));
            });
        IncrementalValueProvider<ImmutableArray<StructInfo>> structInfosWithInheritance = structInfos.Where(static sI => sI.InheritedStructs.Length != 0).Collect();
        IncrementalValueProvider<ImmutableArray<StructInfo>> structInfosInherited = structInfos.Where(static sI => sI.ExtraInheritedStructInfo != null).Collect();

        IncrementalValuesProvider<(StructInfo targetStruct, ImmutableArray<StructInfo> inheritedStructs)> structInfosWithInheritedStructs = structInfosWithInheritance.Combine(structInfosInherited).SelectMany(static (structInfos, token) => {
            ImmutableArray<StructInfo> structsWithInheritance = structInfos.Left;
            ImmutableArray<StructInfo> structsInherited = structInfos.Right;

            using ImmutableArrayBuilder<(StructInfo targetStruct, ImmutableArray<StructInfo> inheritedStructs)> structInheritanceGroupedBuilder = new();

            Dictionary<string, ImmutableArray<StructInfo>> tempInheritanceMap = new();

            foreach (StructInfo targetStruct in structsWithInheritance) {
                using ImmutableArrayBuilder<StructInfo> inheritedStructsBuilder = new();

                foreach (InheritanceInfo inheritedStruct in targetStruct.InheritedStructs) {
                    CollectInheritedStructs(inheritedStruct.InheritedTypeName, structsInherited, tempInheritanceMap, inheritedStructsBuilder);
                }

                structInheritanceGroupedBuilder.Add((targetStruct, inheritedStructsBuilder.ToImmutable()));
                token.ThrowIfCancellationRequested();
            }

            return structInheritanceGroupedBuilder.ToImmutable();
        });

        context.RegisterSourceOutput(structInfosWithInheritedStructs,
            static (sourceContext, item) => { sourceContext.AddSource($"{item.targetStruct.FullyQualifiedMetadataName}.Inheritance.InteropGenerator.g.cs", RenderInheritedStructInfo(item.targetStruct, item.inheritedStructs, sourceContext.CancellationToken)); });
        IncrementalValueProvider<GeneratorOptions> generatorOptionsProvider = context.AnalyzerConfigOptionsProvider.Select(static (options, _) => {
            if (!options.GlobalOptions.TryGetValue("build_property.InteropGenerator_InteropNamespace", out string? interopNamespace))
                interopNamespace = "FFXIVClientStructs.Expansion.Runtime.Generated";
            return new GeneratorOptions(
                interopNamespace);
        });
        IncrementalValueProvider<ImmutableArray<StructInfo>> structInfosWithAddresses = structInfos.Where(static sI => sI.HasSignatures()).Collect();

        context.RegisterSourceOutput(structInfosWithAddresses.Combine(generatorOptionsProvider),
            static (SourceProductionContext sourceContext, (ImmutableArray<StructInfo> StructInfos, GeneratorOptions GeneratorOptions) args) => {
                if (!args.StructInfos.IsEmpty)
                    sourceContext.AddSource($"{args.GeneratorOptions.InteropNamespace}.Addresses.g.cs", RenderResolverInitializer(args.StructInfos, args.GeneratorOptions.InteropNamespace));
            });
        IncrementalValueProvider<ImmutableArray<StructInfo>> structInfosWithFixedArrays = structInfos.Where(static sI => !sI.FixedSizeArrays.IsEmpty).Collect();

        context.RegisterSourceOutput(structInfosWithFixedArrays.Combine(generatorOptionsProvider),
            static (SourceProductionContext sourceContext, (ImmutableArray<StructInfo> StructInfos, GeneratorOptions GeneratorOptions) args) => {
                if (!args.StructInfos.IsEmpty)
                    sourceContext.AddSource($"{args.GeneratorOptions.InteropNamespace}.FixedArrays.g.cs", RenderFixedArrayTypes(args.StructInfos, args.GeneratorOptions.InteropNamespace));
            });
    }

    private static void CollectInheritedStructs(string inheritedTypeName, ImmutableArray<StructInfo> validInheritedTypes, Dictionary<string, ImmutableArray<StructInfo>> processedInheritanceMap, ImmutableArrayBuilder<StructInfo> inheritedStructsBuilder) {
        if (processedInheritanceMap.TryGetValue(inheritedTypeName, out ImmutableArray<StructInfo> processedInheritance)) {
            inheritedStructsBuilder.AddRange(processedInheritance.AsSpan());
            return;
        }
        StructInfo? targetInheritedStruct = validInheritedTypes.FirstOrDefault(sInfo => sInfo.FullyQualifiedMetadataName == inheritedTypeName);
        if (targetInheritedStruct == null)
            return;

        using ImmutableArrayBuilder<StructInfo> inheritanceHierarchyBuilder = new();
        inheritanceHierarchyBuilder.Add(targetInheritedStruct);
        if (targetInheritedStruct.InheritedStructs.Length != 0) {
            foreach (InheritanceInfo inheritanceInfo in targetInheritedStruct.InheritedStructs) {
                CollectInheritedStructs(inheritanceInfo.InheritedTypeName, validInheritedTypes, processedInheritanceMap, inheritanceHierarchyBuilder);
            }
        }

        ImmutableArray<StructInfo> inheritanceHierarchy = inheritanceHierarchyBuilder.ToImmutable();
        inheritedStructsBuilder.AddRange(inheritanceHierarchy.AsSpan());
        processedInheritanceMap.Add(inheritedTypeName, inheritanceHierarchy);
    }
}
