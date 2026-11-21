using FFXIVClientStructs.STD;
using NativeCraftLeveClientEventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.CraftLeveClientEventHandler;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

/// <summary>生产理符委托人的事件处理器，提供当前任务和按委托人筛选的任务集合。</summary>
[GenerateInterop]
[Inherits<NativeCraftLeveClientEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 1256)]
public unsafe partial struct CraftLeveClientEventHandler
{
    /// <summary>当前处理的 Leve 表行标识。</summary>
    [FieldOffset(728)]
    public ushort LeveID;

    /// <summary>当前任务的基础奖励和追加奖励，内部字符串由事件处理器负责初始化及释放。</summary>
    [FieldOffset(776)]
    public LeveRewards Rewards;

    /// <summary>当前委托人能够受理的已接取任务行标识集合。</summary>
    [FieldOffset(1056)]
    public StdVector<ushort> AvailableLeveIDs;

    /// <summary>任务选择菜单中的理符任务行标识，顺序与菜单条目一致。</summary>
    [FieldOffset(1080)]
    public StdVector<ushort> MenuLeveIDs;

    /// <summary>根据已接取任务的奖励种子、职业等级及高品质交付标记计算实际奖励，不打开领奖界面。</summary>
    /// <param name="leveID">需要计算奖励的理符任务行标识。</param>
    /// <param name="rewards">已初始化的奖励存储区，可传入当前处理器的奖励字段地址。</param>
    [MemberFunction("40 53 56 57 41 55 48 83 EC ?? 4D 8B E8 0F B7 FA")]
    public partial void CalculateRewards(ushort leveID, LeveRewards* rewards);

    /// <summary>理符奖励计算结果中与金币消息有关的字段，保留完整原生存储区大小。</summary>
    [StructLayout(LayoutKind.Explicit, Size = 280)]
    public struct LeveRewards
    {
        /// <summary>基础金币奖励。</summary>
        [FieldOffset(4)]
        public uint Gil;

        /// <summary>高品质等条件产生的追加金币奖励。</summary>
        [FieldOffset(32)]
        public uint BonusGil;
    }
}
