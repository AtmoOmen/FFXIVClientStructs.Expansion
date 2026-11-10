using NativeMJIManager = FFXIVClientStructs.FFXIV.Client.Game.MJI.MJIManager;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.MJI;

/// <summary>
///     开拓无人岛管理器，补充包含虚表指针的工房建筑状态。
/// </summary>
[GenerateInterop]
[Inherits<NativeMJIManager>]
[StructLayout(LayoutKind.Explicit, Size = 1088)]
public unsafe partial struct MJIManager
{
    /// <summary>
    ///     四个工房的建筑位置、等级及施工状态。
    /// </summary>
    [FieldOffset(208)]
    public MJIWorkshops Workshops;
}
