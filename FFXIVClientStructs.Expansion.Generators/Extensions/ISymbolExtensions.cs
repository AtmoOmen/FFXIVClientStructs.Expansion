
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Extensions;

/// <summary>
///     Extension methods for <see cref="ISymbol" /> types.
/// </summary>
public static class ISymbolExtensions {

    private static readonly SymbolDisplayFormat ContainingTypesAndNamespaces = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
    public static string GetFullyQualifiedName(this ISymbol symbol) => symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    public static string GetNameWithContainingTypeAndNamespace(this ISymbol symbol) => symbol.ToDisplayString(ContainingTypesAndNamespaces);

   
   
   
   
   
   
    public static bool HasAttributeWithType(this ISymbol symbol, ITypeSymbol typeSymbol) => TryGetAttributeWithType(symbol, typeSymbol, out _);

   
   
   
   
   
   
   
    public static bool TryGetAttributeWithType(this ISymbol symbol, ITypeSymbol typeSymbol, [NotNullWhen(true)] out AttributeData? attributeData) {
        foreach (AttributeData? attribute in symbol.GetAttributes()) {
            if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, typeSymbol)) {
                attributeData = attribute;

                return true;
            }
        }

        attributeData = null;

        return false;
    }

   
   
   
   
   
   
   
    public static bool TryGetAttributeWithFullyQualifiedMetadataName(this ISymbol symbol, string name, [NotNullWhen(true)] out AttributeData? attributeData) {
        foreach (AttributeData? attribute in symbol.GetAttributes()) {
            if (attribute.AttributeClass is { } attributeSymbol &&
                attributeSymbol.HasFullyQualifiedMetadataName(name)) {
                attributeData = attribute;

                return true;
            }
        }

        attributeData = null;

        return false;
    }

   
   
   
   
   
   
   
   
    public static bool TryGetSyntaxNode<T>(this ISymbol symbol, CancellationToken token, [NotNullWhen(true)] out T? syntaxNode)
        where T : SyntaxNode {
        if (symbol.DeclaringSyntaxReferences is not [{ } syntaxReference, ..]) {
            syntaxNode = null;

            return false;
        }
        var candidateNode = syntaxReference.GetSyntax(token) as T;

        syntaxNode = candidateNode;

        return candidateNode is not null;
    }
}
