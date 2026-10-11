using FFXIVClientStructs.Expansion.Generators.Helpers;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record ParameterInfo(
    string Name,
    string Type,
    string? DefaultValue,
    RefKind RefKind,
    EquatableArray<string>? InheritableAttributes) {
    public string GetDefaultValue() => DefaultValue is null ? string.Empty : $" = {DefaultValue}";
    public string GetAttributeString() => InheritableAttributes is null ? string.Empty : string.Join("", InheritableAttributes);
}
