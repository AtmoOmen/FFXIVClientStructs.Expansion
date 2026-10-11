using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record FixedSizeArrayInfo
(
    string                 FieldName,
    string                 Type,
    int                    Size,
    bool                   IsString,
    bool                   IsBitArray,
    int                    BitCount,
    EquatableArray<string> InheritableAttributes
)
{
    public string GetPublicFieldName() =>
        FieldName.EndsWith("Storage", StringComparison.Ordinal) ?
            FieldName[..^7] :
            char.ToUpperInvariant(FieldName[0]) + FieldName[1..];
}
