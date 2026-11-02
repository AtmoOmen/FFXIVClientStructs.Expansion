namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.MJI;

/// <summary>
///     屯货仓库的探索状态，补充普通开拓素材待领取数量的完整宽度。
/// </summary>
[GenerateInterop]
[Inherits<FFXIVClientStructs.FFXIV.Client.Game.MJI.MJIGranaryState>]
[StructLayout(LayoutKind.Explicit, Size = 72)]
public partial struct MJIGranaryState
{
    /// <summary>
    ///     按普通素材口袋编号顺序排列的待领取数量，每项为无符号 16 位整数。
    /// </summary>
    [FieldOffset(26)]
    [FixedSizeArray]
    internal FixedSizeArray20<ushort> NormalResourceAmountsStorage;
}
