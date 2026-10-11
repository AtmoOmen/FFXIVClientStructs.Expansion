using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Extensions;

/// <summary>
///     Extension methods for <see cref="IParameterSymbol" /> types.
/// </summary>
internal static class IParameterSymbolExtensions {
    public static string? GetDefaultValueString(this IParameterSymbol symbol) {
        if (!symbol.HasExplicitDefaultValue)
            return null;

        object? defaultValue = symbol.ExplicitDefaultValue;

        if (defaultValue is null)
            return "null";

        if (defaultValue is bool boolValue)
            return boolValue.ToLowercaseString();

        if (defaultValue is float floatValue)
            return defaultValue + "f";

        return defaultValue.ToString();
    }
}
