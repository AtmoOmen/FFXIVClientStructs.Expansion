namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.MJI;

/// <summary>
///     工房建筑状态，建筑数据从虚表指针之后开始排列。
/// </summary>
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe partial struct MJIWorkshops
{
    /// <summary>
    ///     各工房所在的建筑位置编号，零表示尚未建造。
    /// </summary>
    [FieldOffset(8)]
    [FixedSizeArray]
    internal FixedSizeArray4<byte> PlaceIDsStorage;

    /// <summary>
    ///     各工房的外观等级，从零开始编号。
    /// </summary>
    [FieldOffset(12)]
    [FixedSizeArray]
    internal FixedSizeArray4<byte> GlamourLevelsStorage;

    /// <summary>
    ///     各工房距离建造或升级完成的剩余小时数。
    /// </summary>
    [FieldOffset(16)]
    [FixedSizeArray]
    internal FixedSizeArray4<byte> HoursToCompletionStorage;

    /// <summary>
    ///     各工房的实际建筑等级，从零开始编号。
    /// </summary>
    [FieldOffset(20)]
    [FixedSizeArray]
    internal FixedSizeArray4<byte> BuildingLevelsStorage;

    /// <summary>
    ///     各工房的施工状态，非零表示正在建造、升级或等待确认竣工。
    /// </summary>
    [FieldOffset(24)]
    [FixedSizeArray]
    internal FixedSizeArray4<byte> UnderConstructionStorage;
}
