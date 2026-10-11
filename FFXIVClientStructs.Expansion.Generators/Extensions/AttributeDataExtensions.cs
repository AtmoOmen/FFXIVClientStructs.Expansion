using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Extensions;

/// <summary>
///     Extensions methods for <see cref="AttributeData" /> type.
/// </summary>
public static class AttributeDataExtensions {
   
   
   
   
   
    public static Location? GetLocation(this AttributeData attributeData) {
        if (attributeData.ApplicationSyntaxReference is { } syntaxReference) {
            return syntaxReference.SyntaxTree.GetLocation(syntaxReference.Span);
        }

        return null;
    }

   
   
   
   
   
   
   
   
    public static bool TryGetConstructorArgument<T>(this AttributeData attributeData, int index, [NotNullWhen(true)] out T? result) {
        if (attributeData.ConstructorArguments.Length > index &&
            attributeData.ConstructorArguments[index].Value is T argument) {
            result = argument;

            return true;
        }

        result = default;

        return false;
    }

   
   
   
   
   
   
   
   
   
    public static bool TryGetMultiValueConstructorArgument<T>(this AttributeData attributeData, int index, [NotNullWhen(true)] out ImmutableArray<T>? result) {
        if (attributeData.ConstructorArguments.Length > index) {

            using ImmutableArrayBuilder<T> values = new();

            foreach (TypedConstant typedConstant in attributeData.ConstructorArguments[index].Values) {
                if (typedConstant.Value is not T value) {
                    result = default;
                    return false;
                }
                values.Add(value);
            }

            result = values.ToImmutable();
            return true;
        }

        result = default;

        return false;
    }

   
   
   
   
   
   
   
   
   
   
   
    public static bool TryGetNamedArgument<T>(this AttributeData attributeData, string name, out T? value) {
        foreach (KeyValuePair<string, TypedConstant> properties in attributeData.NamedArguments) {
            if (properties.Key == name) {
                value = (T?)properties.Value.Value;

                return true;
            }
        }

        value = default;

        return false;
    }
}
