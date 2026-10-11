
using FFXIVClientStructs.Expansion.Generators.Helpers;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Extensions;

/// <summary>
///     Extension methods for <see cref="ITypeSymbol" /> types.
/// </summary>
internal static class ITypeSymbolExtensions {
   
   
   
   
   
   
    public static bool HasFullyQualifiedMetadataName(this ITypeSymbol symbol, string name) {
        using ImmutableArrayBuilder<char> builder = new();

        symbol.AppendFullyQualifiedMetadataName(in builder);

        return builder.WrittenSpan.SequenceEqual(name.AsSpan());
    }

   
   
   
   
   
    public static string GetFullyQualifiedMetadataName(this ITypeSymbol symbol) {
        using ImmutableArrayBuilder<char> builder = new();

        symbol.AppendFullyQualifiedMetadataName(in builder);

        return builder.ToString();
    }

   
   
   
   
   
    public static void AppendFullyQualifiedMetadataName(this ITypeSymbol symbol, ref readonly ImmutableArrayBuilder<char> builder) {
        static void BuildFrom(ISymbol? symbol, ref readonly ImmutableArrayBuilder<char> builder) {
            switch (symbol) {
                case INamespaceSymbol { ContainingNamespace.IsGlobalNamespace: false }:
                    BuildFrom(symbol.ContainingNamespace, in builder);
                    builder.Add('.');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
                case INamespaceSymbol { IsGlobalNamespace: false }:
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol { IsGlobalNamespace: true } }:
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
                case ITypeSymbol { ContainingSymbol: INamespaceSymbol namespaceSymbol }:
                    BuildFrom(namespaceSymbol, in builder);
                    builder.Add('.');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
                case ITypeSymbol { ContainingSymbol: ITypeSymbol typeSymbol }:
                    BuildFrom(typeSymbol, in builder);
                    builder.Add('+');
                    builder.AddRange(symbol.MetadataName.AsSpan());
                    break;
            }
        }

        BuildFrom(symbol, in builder);
    }
}
