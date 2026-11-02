using NativeMJIGranariesState = FFXIVClientStructs.FFXIV.Client.Game.MJI.MJIGranariesState;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.MJI;

/// <summary>
///     屯货仓库可用状态查询。
/// </summary>
[GenerateInterop]
[Inherits<NativeMJIGranariesState>]
[StructLayout(LayoutKind.Explicit, Size = 152)]
public unsafe partial struct MJIGranariesState
{
    /// <summary>
    ///     按仓库放置、建造及升级状态判断是否可以使用。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 0F B6 FA E8")]
    public partial bool IsGranaryAvailable(byte granaryIndex);
}
