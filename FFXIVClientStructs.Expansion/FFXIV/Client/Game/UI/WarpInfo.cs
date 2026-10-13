using NativeWarpInfo = FFXIVClientStructs.FFXIV.Client.Game.UI.WarpInfo;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine.Layer;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.UI;

[GenerateInterop]
[Inherits<NativeWarpInfo>]
[StructLayout(LayoutKind.Explicit, Size = 0xE8)]
public unsafe partial struct WarpInfo
{
    [StaticAddress("48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 75 ?? 48 8B D7", 3)]
    public static partial WarpInfo* Instance();

    [FieldOffset(0x00)]
    public uint State;

    [FieldOffset(0x10)]
    public ExitRangeLayoutInstance* ExitRange;
}
