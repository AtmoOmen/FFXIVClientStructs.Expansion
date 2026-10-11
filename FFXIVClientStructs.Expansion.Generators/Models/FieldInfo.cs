using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record FieldInfo(
    string Name,
    string Type,
    int Offset,
    EquatableArray<string> InheritableAttributes,
    bool IsReadOnly
);
