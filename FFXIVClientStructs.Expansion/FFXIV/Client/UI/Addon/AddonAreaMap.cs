using NativeAddonAreaMap = FFXIVClientStructs.FFXIV.Client.UI.AddonAreaMap;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

[GenerateInterop]
[Inherits<NativeAddonAreaMap>]
[Addon("AreaMap")]
[StructLayout(LayoutKind.Explicit, Size = 2016)]
public unsafe partial struct AddonAreaMap
{
    [FieldOffset(1864)]
    public int HoveredMarkerIndex;

    [FieldOffset(1976)]
    public bool IsResizingMap;

    [FieldOffset(1978)]
    public bool IsDraggingMap;

    [FieldOffset(1989)]
    public bool IsUsingController;

    [StructLayout(LayoutKind.Explicit, Size = 56)]
    public struct MapMarker
    {
        [FieldOffset(16)]
        public uint TooltipContext;

        [FieldOffset(50)]
        public byte InteractionFlags;
    }
}
