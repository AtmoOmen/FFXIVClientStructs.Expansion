using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record StructInfo
(
    string                              FullyQualifiedMetadataName,
    string                              Namespace,
    EquatableArray<string>              Hierarchy,
    EquatableArray<MemberFunctionInfo>  MemberFunctions,
    EquatableArray<VirtualFunctionInfo> VirtualFunctions,
    EquatableArray<StaticAddressInfo>   StaticAddresses,
    EquatableArray<StringOverloadInfo>  StringOverloads,
    SignatureInfo?                      StaticVirtualTableSignature,
    uint?                               VirtualTableFunctionCount,
    EquatableArray<FixedSizeArrayInfo>  FixedSizeArrays,
    EquatableArray<BitFieldInfo>        BitFields,
    EquatableArray<InheritanceInfo>     InheritedStructs,
    int?                                Size,
    EquatableArray<OwnMemberInfo>       OwnMembers,
    bool                                IsExternal,
    ExtraInheritedStructInfo?           ExtraInheritedStructInfo
)
{
    public string Name              => Hierarchy[0];
    public bool   HasSignatures()   => !MemberFunctions.IsEmpty  || !StaticAddresses.IsEmpty || StaticVirtualTableSignature is not null;
    public bool   HasVirtualTable() => !VirtualFunctions.IsEmpty || StaticVirtualTableSignature is not null;

    public bool NeedsRender() => Size.HasValue                           ||
                                 MemberFunctions.Any()                   ||
                                 VirtualFunctions.Any()                  ||
                                 StaticAddresses.Any()                   ||
                                 StringOverloads.Any()                   ||
                                 StaticVirtualTableSignature is not null ||
                                 FixedSizeArrays.Any()                   ||
                                 BitFields.Any();
}
