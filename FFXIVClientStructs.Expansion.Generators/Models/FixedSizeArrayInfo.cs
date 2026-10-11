using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record FixedSizeArrayInfo(
    string FieldName,
    string Type,
    int Size,
    bool IsString,
    bool IsBitArray,
    int BitCount,
    EquatableArray<string> InheritableAttributes) {
    public string GetPublicFieldName() =>
        FieldName[1].ToString().ToUpper() + FieldName[2..];
}
