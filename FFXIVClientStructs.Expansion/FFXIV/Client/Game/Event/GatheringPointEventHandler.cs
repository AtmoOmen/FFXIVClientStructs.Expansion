using NativeGatheringPointEventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.GatheringPointEventHandler;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

/// <summary>采集点事件处理器，补充交互期间使用的物品槽与快速采集状态。</summary>
[GenerateInterop(isInherited: true)]
[Inherits<NativeGatheringPointEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 1096)]
public unsafe partial struct GatheringPointEventHandler
{
    /// <summary>八个采集物品槽的原生数据地址，仅在采集点交互期间有效。</summary>
    [FieldOffset(1064)]
    public GatheringPointItem* Items;

    /// <summary>是否正在处理一次采集动作，期间不能再次提交物品选择。</summary>
    [FieldOffset(1087)]
    public bool IsGathering;

    /// <summary>是否正在执行快速采集。</summary>
    [FieldOffset(1092)]
    public bool IsQuickGathering;
}
