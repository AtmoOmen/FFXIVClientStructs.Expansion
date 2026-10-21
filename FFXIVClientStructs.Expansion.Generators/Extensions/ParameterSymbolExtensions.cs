using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Extensions;

/// <summary>
///     Extension methods for <see cref="IParameterSymbol" /> types.
/// </summary>
internal static class IParameterSymbolExtensions
{
    public static string? GetDefaultValueString
    (
        this IParameterSymbol symbol
    )
    {
        if (!symbol.HasExplicitDefaultValue)
            return null;

        var defaultValue = symbol.ExplicitDefaultValue;

        return defaultValue switch
        {
            null                                                                        => "null",
            bool boolValue                                                              => boolValue.ToLowercaseString(),
            float                                                                       => defaultValue + "f",
            _ when symbol.Type is INamedTypeSymbol { TypeKind: TypeKind.Enum } enumType => GetEnumDefaultValueString(enumType, defaultValue),
            _                                                                           => defaultValue.ToString()
        };
    }

    private static string GetEnumDefaultValueString
    (
        INamedTypeSymbol enumType,
        object           defaultValue
    )
    {
        var typeName = enumType.GetFullyQualifiedName();

        foreach (var field in enumType.GetMembers().OfType<IFieldSymbol>())
        {
            if (field.HasConstantValue && Equals(field.ConstantValue, defaultValue))
                return $"{typeName}.{field.Name}";
        }

        return $"({typeName})({defaultValue})";
    }
}
