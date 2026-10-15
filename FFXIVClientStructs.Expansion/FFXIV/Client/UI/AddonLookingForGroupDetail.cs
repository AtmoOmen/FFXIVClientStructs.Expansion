using NativeAddonLookingForGroupDetail = FFXIVClientStructs.FFXIV.Client.UI.AddonLookingForGroupDetail;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

[GenerateInterop]
[Inherits<NativeAddonLookingForGroupDetail>]
[Addon("LookingForGroupDetail")]
[StructLayout(LayoutKind.Explicit, Size = 1024)]
public unsafe partial struct AddonLookingForGroupDetail
{
    [FieldOffset(592)]
    public CStringPointer RawDescription;
}
