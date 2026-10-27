using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的结果窗口（KTGResult）。<br />
///     窗口显示四名队员的得分、八项奖励与统计信息、关卡剩余时间以及退出按钮。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGResult")]
[StructLayout(LayoutKind.Explicit, Size = 3456)]
public unsafe partial struct AddonKTGResult
{
    /// <summary>
    ///     窗口状态，小于等于 1 时进行倒计时动画。
    /// </summary>
    [FieldOffset(568)]
    public int State;

    /// <summary>
    ///     得分条目数量，取值 0 至 4。
    /// </summary>
    [FieldOffset(572)]
    public int ScoreEntryCount;

    /// <summary>
    ///     结果图标 id，加载到窗口图标节点上。
    /// </summary>
    [FieldOffset(576)]
    public int IconID;

    /// <summary>
    ///     窗口说明文本，显示在说明文本节点上。
    /// </summary>
    [FieldOffset(584)]
    public Utf8String Description;

    /// <summary>
    ///     四个得分条目。
    /// </summary>
    [FieldOffset(688)]
    [FixedSizeArray]
    internal FixedSizeArray4<ScoreEntry> ScoreEntriesStorage;

    /// <summary>
    ///     奖励条目数量，取值 0 至 8。
    /// </summary>
    [FieldOffset(1680)]
    public int StatEntryCount;

    /// <summary>
    ///     八个奖励条目，依次为金碟币、奖励与道具。
    /// </summary>
    [FieldOffset(1688)]
    [FixedSizeArray]
    internal FixedSizeArray8<StatEntry> StatEntriesStorage;

    /// <summary>
    ///     结果数据是否已经应用。
    /// </summary>
    [FieldOffset(2648)]
    public bool IsActive;

    /// <summary>
    ///     倒计时动画的累计秒数。
    /// </summary>
    [FieldOffset(2652)]
    public float CountdownTimer;

    /// <summary>
    ///     剩余秒数，显示为“剩余时间：分:秒”。
    /// </summary>
    [FieldOffset(2656)]
    public int RemainingSeconds;

    /// <summary>
    ///     实际显示的得分条目数量。
    /// </summary>
    [FieldOffset(2660)]
    public int VisibleScoreCount;

    /// <summary>
    ///     四个得分条目各自的显示标志。
    /// </summary>
    [FieldOffset(2664)]
    [FixedSizeArray]
    internal FixedSizeArray4<bool> ScoreVisibleStorage;

    /// <summary>
    ///     剩余时间文本节点（节点 0x2B）。
    /// </summary>
    [FieldOffset(2672)]
    public AtkTextNode* CountdownTextNode;

    /// <summary>
    ///     说明文本节点（节点 3）。
    /// </summary>
    [FieldOffset(2680)]
    public AtkTextNode* DescriptionTextNode;

    /// <summary>
    ///     结果图标节点（图片节点 0x18）。
    /// </summary>
    [FieldOffset(2688)]
    public AtkImageNode* IconNode;

    /// <summary>
    ///     退出按钮组件（组件按钮 0x30）。
    /// </summary>
    [FieldOffset(2696)]
    public AtkComponentButton* ExitButton;

    /// <summary>
    ///     四个得分条目的节点集合。
    /// </summary>
    [FieldOffset(2704)]
    [FixedSizeArray]
    internal FixedSizeArray4<ScoreRowNodes> ScoreRowsStorage;

    /// <summary>
    ///     结果时间线节点（节点 0x15），窗口未显示得分时播放时间线 103，否则播放 101。
    /// </summary>
    [FieldOffset(3344)]
    public AtkResNode* ResultTimelineNode;

    /// <summary>
    ///     节点 0x19，仅缓存不使用。
    /// </summary>
    [FieldOffset(3352)]
    public AtkResNode* Node0x19;

    /// <summary>
    ///     金碟币行组件（节点 0x24）内的文本九宫格节点。
    /// </summary>
    [FieldOffset(3360)]
    public AtkComponentTextNineGrid* MgpTextNineGrid;

    /// <summary>
    ///     奖励行组件（节点 0x25）内的文本九宫格节点。
    /// </summary>
    [FieldOffset(3368)]
    public AtkComponentTextNineGrid* RewardTextNineGrid;

    /// <summary>
    ///     金碟币行容器节点（节点 0x24）。
    /// </summary>
    [FieldOffset(3376)]
    public AtkResNode* MgpContainerNode;

    /// <summary>
    ///     奖励行容器节点（节点 0x25）。
    /// </summary>
    [FieldOffset(3384)]
    public AtkResNode* RewardContainerNode;

    /// <summary>
    ///     道具行容器节点（节点 0x26）。
    /// </summary>
    [FieldOffset(3392)]
    public AtkResNode* ItemContainerNode;

    /// <summary>
    ///     金碟币行组件内显示数量的文本九宫格节点（节点 8）。
    /// </summary>
    [FieldOffset(3400)]
    public AtkComponentTextNineGrid* MgpAmountNineGrid;

    /// <summary>
    ///     奖励行组件内显示数量的文本九宫格节点（节点 8）。
    /// </summary>
    [FieldOffset(3408)]
    public AtkComponentTextNineGrid* RewardAmountNineGrid;

    /// <summary>
    ///     道具行组件内的道具名节点（节点 3）。
    /// </summary>
    [FieldOffset(3416)]
    public AtkResNode* ItemNameNode;

    /// <summary>
    ///     道具行组件内的道具图标组件。
    /// </summary>
    [FieldOffset(3424)]
    public AtkComponentIcon* ItemIconComponent;

    /// <summary>
    ///     奖励确认复选框的节点（组件节点 0x2F 的节点 5）。
    /// </summary>
    [FieldOffset(3432)]
    public AtkResNode* CheckboxNode;

    /// <summary>
    ///     奖励确认复选框组件（组件 0x2F）。
    /// </summary>
    [FieldOffset(3440)]
    public AtkComponentCheckBox* Checkbox;

    /// <summary>
    ///     奖励确认复选框是否被勾选。
    /// </summary>
    [FieldOffset(3448)]
    public bool CheckboxChecked;

    /// <summary>
    ///     构造窗口并初始化四个得分条目、八个奖励条目与全部节点字段。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 45 33 F6 48 8D 8F")]
    public partial AddonKTGResult* Ctor();

    /// <summary>
    ///     按节点 id 缓存窗口内全部节点与组件，并注册退出按钮与复选框的点击事件。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 83 B9 ?? ?? ?? ?? ?? 48 8B D9 48 89 6C 24")]
    public partial void CacheNodes();

    /// <summary>
    ///     按得分条目刷新四个队员行的名次动画、分数文本、名牌提示与是否可见。
    /// </summary>
    [MemberFunction("40 55 48 83 EC ?? 48 8B E9 E8 ?? ?? ?? ?? 80 BD")]
    public partial void UpdateScoreRows();

    /// <summary>
    ///     按奖励条目刷新金碟币、奖励与道具三行的数量、图标与提示。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 48 89 6C 24")]
    public partial void UpdateRewardRows();

    /// <summary>
    ///     把剩余秒数格式化为“剩余时间：分:秒”并写入剩余时间文本节点。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 48 83 BB")]
    public partial void UpdateCountdownText();

    /// <summary>
    ///     按当前显示的条目数量设置得分行、奖励行与复选框节点的可聚焦状态，必要时重新设置焦点。
    /// </summary>
    [MemberFunction("40 53 57 48 83 EC ?? 8B 81 ?? ?? ?? ?? 48 8D 99")]
    public partial void UpdateFocusableNodes();

    /// <summary>
    ///     应用一份结果数据：把数据块中的状态、说明、四个得分条目与八个奖励条目复制到窗口字段，并刷新界面。
    /// </summary>
    /// <param name="data">与窗口 568 至 2648 字段块同布局的结果数据。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 8B 02")]
    public partial void ApplyResultData
    (
        void* data
    );

    /// <summary>
    ///     把说明文本与结果图标刷新到对应节点。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 74 ?? 48 89 7C 24")]
    public partial void UpdateDescription();

    /// <summary>
    ///     单个队员得分条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 248)]
    public unsafe struct ScoreEntry
    {
        /// <summary>
        ///     显示状态，为 1 时显示名次图标并播放时间线 105。
        /// </summary>
        [FieldOffset(0)]
        public int DisplayState;

        /// <summary>
        ///     由界面值第 6、7 项组合而成的 64 位数据。
        /// </summary>
        [FieldOffset(8)]
        public long Value1;

        /// <summary>
        ///     由界面值第 8、9 项组合而成的 64 位数据。
        /// </summary>
        [FieldOffset(16)]
        public long Value2;

        /// <summary>
        ///     队员得分。
        /// </summary>
        [FieldOffset(24)]
        public int Score;

        /// <summary>
        ///     名次索引，用于播放时间线 101 至 104。
        /// </summary>
        [FieldOffset(28)]
        public int Rank;

        /// <summary>
        ///     是否不显示得分，为真时显示占位文本。
        /// </summary>
        [FieldOffset(32)]
        public bool HideScore;

        /// <summary>
        ///     队员名。
        /// </summary>
        [FieldOffset(40)]
        public Utf8String Name;

        /// <summary>
        ///     队员详细信息，用于名牌提示。
        /// </summary>
        [FieldOffset(144)]
        public Utf8String Detail;
    }

    /// <summary>
    ///     单个奖励条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 120)]
    public unsafe struct StatEntry
    {
        /// <summary>
        ///     奖励类型。
        /// </summary>
        [FieldOffset(0)]
        public int Type;

        /// <summary>
        ///     奖励数量。
        /// </summary>
        [FieldOffset(4)]
        public int Amount;

        /// <summary>
        ///     道具图标 id，仅道具行使用。
        /// </summary>
        [FieldOffset(8)]
        public int IconID;

        /// <summary>
        ///     提示文本。
        /// </summary>
        [FieldOffset(16)]
        public Utf8String Text;
    }

    /// <summary>
    ///     单个得分条目的节点集合。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 160)]
    public unsafe struct ScoreRowNodes
    {
        /// <summary>
        ///     行组件节点。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     队员名文本节点（组件节点 9）。
        /// </summary>
        [FieldOffset(8)]
        public AtkTextNode* NameTextNode;

        /// <summary>
        ///     得分文本节点（组件节点 0x0B）。
        /// </summary>
        [FieldOffset(16)]
        public AtkTextNode* ScoreTextNode;

        /// <summary>
        ///     名牌提示目标节点（组件节点 0x0C）。
        /// </summary>
        [FieldOffset(24)]
        public AtkResNode* TooltipTargetNode;

        /// <summary>
        ///     名次动画节点（组件节点 2），按名次播放时间线 101 至 104。
        /// </summary>
        [FieldOffset(32)]
        public AtkResNode* RankNode;

        /// <summary>
        ///     名次图标节点（组件节点 3），显示时播放时间线 105。
        /// </summary>
        [FieldOffset(40)]
        public AtkResNode* RankIconNode;

        /// <summary>
        ///     显示状态节点（组件节点 0x0A），按是否有得分播放时间线 17 或 20。
        /// </summary>
        [FieldOffset(48)]
        public AtkResNode* StateNode;

        /// <summary>
        ///     名牌提示文本。
        /// </summary>
        [FieldOffset(56)]
        public Utf8String TooltipText;
    }
}
