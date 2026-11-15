namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

/// <summary>采集点交互期间使用的物品槽数据，包含采集物标识和实时成功率。</summary>
[StructLayout(LayoutKind.Explicit, Size = 184)]
public struct GatheringPointItem
{
    /// <summary>当前物品对应的 GatheringItem 表行 ID，零表示空槽。</summary>
    [FieldOffset(144)]
    public uint GatheringItemID;

    /// <summary>当前物品对应的 Item 表行 ID。</summary>
    [FieldOffset(148)]
    public uint ItemID;

    /// <summary>当前采集成功率，取值为百分比整数。</summary>
    [FieldOffset(160)]
    public byte SuccessRate;
}
