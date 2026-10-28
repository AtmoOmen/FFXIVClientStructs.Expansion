using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     赛季奖励界面，按系列赛等级展示每一级的奖励物品、晋升进度与系列赛货币持有量。
/// </summary>
/// <remarks>
///     本类型重写的虚函数依次为：虚函数 2 事件处理、虚函数 5 打开界面、虚函数 6 关闭界面、
///     虚函数 16 触发关闭回调后隐藏、虚函数 42 卸载、虚函数 46 载入布局资源、
///     虚函数 49 初始化、虚函数 51 刷新。
/// </remarks>
[Addon("PvpReward")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x438)]
public unsafe partial struct AddonPvpReward
{
    /// <summary>
    ///     各级奖励的物品 ID，共 31 项，索引与奖励等级一一对应。
    ///     每项记录该等级两件奖励的物品 ID，供物品提示使用。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray31<PvpSeriesRewardItem> RewardItemsStorage;

    /// <summary>
    ///     各级奖励物品的鼠标悬停节点，共 31 项，与 <see cref="RewardItemsStorage" /> 一一对应。
    ///     首次悬停时由界面写入，用于挂载物品提示。
    /// </summary>
    [FieldOffset(816)]
    [FixedSizeArray]
    internal FixedSizeArray31<Pointer<AtkResNode>> TooltipNodesStorage;

    /// <summary>
    ///     晋升进度条的填充节点，刷新时按进度改写其宽度。
    /// </summary>
    [FieldOffset(1064)]
    public AtkResNode* ProgressBarFillNode;

    /// <summary>
    ///     晋升进度条填充节点的最大宽度，用于把进度换算为像素宽度。
    /// </summary>
    [FieldOffset(1072)]
    public ushort ProgressBarMaxWidth;

    /// <summary>
    ///     构造界面并初始化奖励列表与进度条字段。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 FF 48 89 03 B8 ?? ?? ?? ?? 48 89 BB ?? ?? ?? ?? 48 8D 8B"
    )]
    public partial AddonPvpReward* Ctor();

    /// <summary>
    ///     按界面参数刷新单个奖励项：显示奖励图标、数量与未解锁状态，并挂载物品提示。
    /// </summary>
    /// <param name="button">奖励项对应的按钮组件。</param>
    /// <param name="values">界面参数。</param>
    /// <param name="index">奖励等级索引。</param>
    [MemberFunction("48 89 5C 24 ?? 44 89 4C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B E9")]
    public partial void UpdateRewardSlot
    (
        AtkComponentButton* button,
        AtkValue*           values,
        uint                index
    );

    /// <summary>
    ///     向界面代理发出奖励项点击回调，并关闭界面。
    /// </summary>
    /// <param name="index">奖励等级索引。</param>
    /// <param name="rewardKind">奖励类型：1 为等级奖励，2 为最终奖励。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B E9 44 8B CB 41 8B F8 8B F2 33 C9 66 0F 1F 44 00 ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E9 ?? 75 ?? 41 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 89 7C 24"
    )]
    public partial void FireItemClickCallback
    (
        int index,
        int rewardKind
    );

    /// <summary>
    ///     单个奖励等级的两件奖励物品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe struct PvpSeriesRewardItem
    {
        /// <summary>
        ///     第一件奖励的物品 ID。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     第二件奖励的物品 ID，无第二件奖励时为 0。
        /// </summary>
        [FieldOffset(4)]
        public uint AdditionalItemID;
    }
}
