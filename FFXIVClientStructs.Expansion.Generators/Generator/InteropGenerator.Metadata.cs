using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Extensions;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

public sealed partial class InteropGenerator
{
    private static StructInfo ParseExternalInheritedStructInfo
    (
        INamedTypeSymbol  symbol,
        Compilation       compilation,
        CancellationToken token
    )
    {
        var methods          = ImmutableArray.CreateBuilder<MethodInfo>();
        var virtualFunctions = ImmutableArray.CreateBuilder<VirtualFunctionInfo>();
        var fields           = ImmutableArray.CreateBuilder<FieldInfo>();
        var properties       = ImmutableArray.CreateBuilder<PropertyInfo>();
        var reference = compilation.References.OfType<PortableExecutableReference>()
                                   .Single
                                   (candidate => SymbolEqualityComparer.Default.Equals
                                        (compilation.GetAssemblyOrModuleSymbol(candidate), symbol.ContainingAssembly)
                                   );
        var metadata = (AssemblyMetadata)reference.GetMetadata();
        var reader   = metadata.GetModules()[0].GetMetadataReader();
        var definition = reader.TypeDefinitions.Select(reader.GetTypeDefinition)
                               .Single
                               (candidate => reader.GetString(candidate.Name)      == symbol.MetadataName &&
                                             reader.GetString(candidate.Namespace) == symbol.ContainingNamespace.ToDisplayString()
                               );
        var fieldOffsets = definition.GetFields().Select(reader.GetFieldDefinition)
                                     .ToDictionary(field => reader.GetString(field.Name), field => field.GetOffset());
        var virtualOffsets = new Dictionary<string, int>();

        foreach (var nestedHandle in definition.GetNestedTypes())
        {
            var nested = reader.GetTypeDefinition(nestedHandle);
            if (reader.GetString(nested.Name) != symbol.Name + "VirtualTable")
                continue;

            foreach (var fieldHandle in nested.GetFields())
            {
                var field = reader.GetFieldDefinition(fieldHandle);
                virtualOffsets.Add(reader.GetString(field.Name), field.GetOffset());
            }
        }

        foreach (var method in symbol.GetMembers().OfType<IMethodSymbol>())
        {
            token.ThrowIfCancellationRequested();
            if (method.DeclaredAccessibility != Accessibility.Public || method.MethodKind != MethodKind.Ordinary)
                continue;

            var methodInfo = new MethodInfo
            (
                method.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                method.IsStatic ?
                    "public static" :
                    "public",
                method.ReturnType.GetFullyQualifiedName(),
                string.Empty,
                method.IsStatic,
                method.Parameters.Select(parameter => ParseParameter(parameter, false, token)).ToImmutableArray(),
                ParseMetadataAttributes(method),
                method.TypeParameters.Select(parameter => parameter.Name).ToImmutableArray()
            );

            if (!method.IsStatic && virtualOffsets.TryGetValue(method.Name, out var offset))
            {
                virtualFunctions.Add(new VirtualFunctionInfo(methodInfo, (uint)(offset / 8)));
            }
            else
            {
                methods.Add(methodInfo);
            }
        }

        foreach (var field in symbol.GetMembers().OfType<IFieldSymbol>())
        {
            token.ThrowIfCancellationRequested();
            if (field.DeclaredAccessibility != Accessibility.Public ||
                field.IsStatic                                      ||
                field.IsFixedSizeBuffer                             ||
                (field.Name == "VirtualTable" && virtualFunctions.Count != 0))
                continue;
            if (!fieldOffsets.TryGetValue(field.MetadataName, out var offset) || offset < 0)
                continue;

            fields.Add(new FieldInfo(field.Name, field.Type.GetFullyQualifiedName(), offset, ParseMetadataAttributes(field), field.IsReadOnly));
        }

        foreach (var property in symbol.GetMembers().OfType<IPropertySymbol>())
        {
            token.ThrowIfCancellationRequested();
            if (property.DeclaredAccessibility != Accessibility.Public || property.IsIndexer || property.IsStatic)
                continue;

            properties.Add
            (
                new PropertyInfo
                (
                    property.Name,
                    property.Type.GetFullyQualifiedName(),
                    property.RefKind,
                    property.GetMethod?.DeclaredAccessibility == Accessibility.Public,
                    property.SetMethod?.DeclaredAccessibility == Accessibility.Public,
                    ParseMetadataAttributes(property)
                )
            );
        }


        return new StructInfo
        (
            symbol.GetNameWithContainingTypeAndNamespace(),
            symbol.ContainingNamespace.GetNameWithContainingTypeAndNamespace(),
            ImmutableArray.Create(symbol.Name),
            ImmutableArray<MemberFunctionInfo>.Empty,
            virtualFunctions.ToImmutable(),
            ImmutableArray<StaticAddressInfo>.Empty,
            ImmutableArray<StringOverloadInfo>.Empty,
            null,
            null,
            ImmutableArray<FixedSizeArrayInfo>.Empty,
            ImmutableArray<BitFieldInfo>.Empty,
            ImmutableArray<InheritanceInfo>.Empty,
            definition.GetLayout().Size,
            ImmutableArray<OwnMemberInfo>.Empty,
            true,
            new ExtraInheritedStructInfo(fields.ToImmutable(), methods.ToImmutable(), properties.ToImmutable())
        );
    }

    private static ImmutableArray<string> ParseMetadataAttributes
    (
        ISymbol symbol
    ) =>
    [
        .. symbol.GetAttributes()
                 .Where
                 (attribute => attribute.AttributeClass?.ToDisplayString() is
                                   "System.ObsoleteAttribute" or "System.Diagnostics.CodeAnalysis.UnscopedRefAttribute"
                 )
                 .Select(attribute => $"[global::{attribute}]")
    ];
}
