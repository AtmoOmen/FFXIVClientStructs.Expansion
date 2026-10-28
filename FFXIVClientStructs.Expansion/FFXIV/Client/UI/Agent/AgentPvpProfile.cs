using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     PvP 档案界面的代理。
///     负责汇总玩家对战等级、系列赛奖励、PvP 技能、职能技能、快捷聊天与 PvP 日程，
///     并把结果写入界面数值数组 105 与界面字符串数组 94。
/// </summary>
/// <remarks>
///     本类型重写的虚函数依次为：虚函数 0 回调处理、虚函数 2 析构、虚函数 3 打开界面、
///     虚函数 5 关闭界面、虚函数 7 每帧更新、虚函数 8 可激活判断、虚函数 10 游戏事件、
///     虚函数 11 等级变化、虚函数 12 职业变化。
/// </remarks>
[Agent(AgentId.PvpProfile)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x428)]
public unsafe partial struct AgentPvpProfile
{
    /// <summary>
    ///     界面数据待刷新标记。为 <see langword="true" /> 时下一次每帧更新会重新填充界面数值与字符串数组，
    ///     并关闭仍在显示的奖励领取确认界面。
    /// </summary>
    [FieldOffset(40)]
    public bool RequiresDataRefresh;

    /// <summary>
    ///     赛季奖励界面待刷新标记。为 <see langword="true" /> 时，若赛季奖励界面已打开并完成加载，
    ///     每帧更新会用 <see cref="RefreshRewardAddonData" /> 重新填充该界面。
    /// </summary>
    [FieldOffset(41)]
    public bool RequiresRewardAddonRefresh;

    /// <summary>
    ///     页签切换标记。为 <see langword="true" /> 时每帧更新会调用 <see cref="SwitchTab" />
    ///     切回 <see cref="PendingTab" />。
    /// </summary>
    [FieldOffset(42)]
    public bool RequiresTabSwitch;

    /// <summary>
    ///     当前职业在 PvP 技能页签中展示的技能行 ID，共 32 项，数量由 <see cref="ActionIDCount" /> 记录。
    /// </summary>
    [FieldOffset(44)]
    [FixedSizeArray]
    internal FixedSizeArray32<uint> ActionIDsStorage;

    /// <summary>
    ///     当前职业在 PvP 技能页签中展示的连击路线行 ID，共 32 项，数量由 <see cref="ActionComboRouteIDCount" /> 记录。
    /// </summary>
    [FieldOffset(172)]
    [FixedSizeArray]
    internal FixedSizeArray32<uint> ActionComboRouteIDsStorage;

    /// <summary>
    ///     <see cref="ActionIDsStorage" /> 中有效技能的数量。
    /// </summary>
    [FieldOffset(300)]
    public byte ActionIDCount;

    /// <summary>
    ///     <see cref="ActionComboRouteIDsStorage" /> 中有效连击路线的数量。
    /// </summary>
    [FieldOffset(301)]
    public byte ActionComboRouteIDCount;

    /// <summary>
    ///     客户端在构造时整体清零、此后不再读写的保留区域。
    /// </summary>
    [FieldOffset(304)]
    [FixedSizeArray]
    internal FixedSizeArray40<byte> Unknown304Storage;

    /// <summary>
    ///     职能技能槽位索引，共 2 项。第 0 项为当前生效的槽位，写入界面数值数组 166；
    ///     切换技能时客户端会交换这两个字节。
    /// </summary>
    [FieldOffset(344)]
    [FixedSizeArray]
    internal FixedSizeArray2<byte> RoleActionSlotsStorage;

    /// <summary>
    ///     快捷聊天页签展示的快捷聊天行 ID，共 40 项，有效数量写入界面数值数组 214。
    /// </summary>
    [FieldOffset(348)]
    [FixedSizeArray]
    internal FixedSizeArray40<uint> QuickChatIDsStorage;

    /// <summary>
    ///     PvP 档案界面上显示的当前职业名称，取自当前语言的职业名字典。
    /// </summary>
    [FieldOffset(512)]
    public Utf8String JobName;

    /// <summary>
    ///     由数据表 ClassJob 解析得到的当前职业名称，用于在界面文本中作为职业名占位参数。
    /// </summary>
    [FieldOffset(616)]
    public Utf8String JobNameFromSheet;

    /// <summary>
    ///     当前选中的页签索引。
    /// </summary>
    [FieldOffset(720)]
    public int SelectedTab;

    /// <summary>
    ///     待切回的页签索引。值为 -2 时表示放弃修改后直接关闭界面；
    ///     其余取值作为 <see cref="SwitchTab" /> 的参数使用。
    /// </summary>
    [FieldOffset(724)]
    public int PendingTab;

    /// <summary>
    ///     奖励领取确认界面的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(728)]
    public uint RewardClaimAddonID;

    /// <summary>
    ///     客户端仅在提交职能技能选择时写入 0 的保留字段，从不写入非零值。
    /// </summary>
    [FieldOffset(732)]
    public uint Unknown732;

    /// <summary>
    ///     PvP 日程界面的界面 ID，共 2 项：第 0 项由 PvP 档案界面打开，第 1 项由 PvP 队伍战绩界面打开。
    /// </summary>
    [FieldOffset(736)]
    [FixedSizeArray]
    internal FixedSizeArray2<uint> CalendarAddonIDsStorage;

    /// <summary>
    ///     赛季奖励界面的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(744)]
    public uint RewardAddonID;

    /// <summary>
    ///     界面状态位。第 0 位在界面打开时置位，第 1 位在访问过水晶冲突页签后置位；
    ///     两位置齐后会给界面数值数组 49 加上图标高亮位，随后整体清零。
    /// </summary>
    [BitField<bool>(nameof(IsAddonOpened),                    0)]
    [BitField<bool>(nameof(HasVisitedCrystallineConflictTab), 1)]
    [FieldOffset(748)]
    public uint Flags2EC;

    /// <summary>
    ///     客户端从未写入非零值的标志。仅在放弃职能技能修改时被读取，非零时跳过待处理页签的切回。
    /// </summary>
    [FieldOffset(752)]
    public byte Unknown752;

    /// <summary>
    ///     当前 PvP 职业在数据表 ClassJob 中的 JobIndex，用于定位角色的 PvP 技能配置；为 0 时表示尚未确定。
    /// </summary>
    [FieldOffset(756)]
    public int JobIndex;

    /// <summary>
    ///     触发 PvP 爆发标记的目标对象 ID，无目标时为 0xE0000000。
    /// </summary>
    [FieldOffset(760)]
    public uint BurstStateObjectID;

    /// <summary>
    ///     爆发标记的剩余计时，单位为秒，随帧时间递减。
    /// </summary>
    [FieldOffset(764)]
    public float BurstStateTimer;

    /// <summary>
    ///     两份 PvP 日程的逐日时段位掩码，按“日程索引 * 7 + 星期”排布，共 14 项。
    ///     每项的第 h 位表示该日第 h 小时存在日程。
    /// </summary>
    [FieldOffset(768)]
    [FixedSizeArray]
    internal FixedSizeArray14<uint> CalendarDayMasksStorage;

    /// <summary>
    ///     两份 PvP 日程数据的请求冷却计时，单位为秒，共 2 项。
    /// </summary>
    [FieldOffset(824)]
    [FixedSizeArray]
    internal FixedSizeArray2<float> CalendarRequestTimersStorage;

    /// <summary>
    ///     服务器时间换算得到的当前分钟数，用于给日程时段补上当前时刻的标记。
    /// </summary>
    [FieldOffset(832)]
    public byte CurrentMinute;

    /// <summary>
    ///     当前系列赛等级，取自 PvP 档案数据的当前系列赛等级。
    /// </summary>
    [FieldOffset(833)]
    public byte SeriesCurrentRank;

    /// <summary>
    ///     赛季奖励界面待打开标记，为 <see langword="true" /> 且赛季数据齐备时才会真正打开界面。
    /// </summary>
    [FieldOffset(834)]
    public bool IsRewardAddonOpenPending;

    /// <summary>
    ///     系列赛奖励行数据已加载完成标记。
    /// </summary>
    [FieldOffset(835)]
    public bool IsSeriesRewardDataLoaded;

    /// <summary>
    ///     奖励物品数据已加载完成标记，物品图标依赖该标记。
    /// </summary>
    [FieldOffset(836)]
    public bool IsRewardItemDataLoaded;

    /// <summary>
    ///     是否展示上一赛季的奖励：为 <see langword="true" /> 时读取奖励行数据的第二份缓存。
    /// </summary>
    [FieldOffset(837)]
    public bool ShowPreviousSeries;

    /// <summary>
    ///     系列赛等级经验表，共“等级数量 + 1”项，最后一项为数据已加载标记（1 表示已加载）。
    ///     前若干项为升到下一等级所需的经验值。
    /// </summary>
    [FieldOffset(840)]
    public ushort* SeriesLevelExperience;

    /// <summary>
    ///     <see cref="SeriesLevelExperience" /> 中有效等级的数量，取自数据表 PvPSeriesLevel 的行数。
    /// </summary>
    [FieldOffset(848)]
    public int SeriesLevelCount;

    /// <summary>
    ///     系列赛奖励行缓存，共 2 份，第 0 份对应当前赛季等级，第 1 份对应上一等级。
    /// </summary>
    [FieldOffset(856)]
    public PvpSeriesRewardRow* SeriesRewardRows;

    /// <summary>
    ///     系列赛货币（战利水晶）的物品图标 ID，取得后不再变化。
    /// </summary>
    [FieldOffset(864)]
    public uint SeriesCurrencyIconID;

    /// <summary>
    ///     系列赛货币（战利水晶）的物品名称。
    /// </summary>
    [FieldOffset(872)]
    public Utf8String SeriesCurrencyName;

    /// <summary>
    ///     数据表 PvPSeriesLevel 的实例。
    /// </summary>
    [FieldOffset(976)]
    public ExcelSheet* SeriesLevelSheet;

    /// <summary>
    ///     用于异步载入数据表 PvPSeriesLevel 全部行的等待器。
    /// </summary>
    [FieldOffset(984)]
    public ExcelSheetWaiter* SeriesLevelSheetWaiter;

    /// <summary>
    ///     数据表 PvPSeries 的实例。
    /// </summary>
    [FieldOffset(992)]
    public ExcelSheet* SeriesSheet;

    /// <summary>
    ///     用于异步载入数据表 PvPSeries 中当前等级与上一等级两行的等待器。
    /// </summary>
    [FieldOffset(1000)]
    public ExcelSheetWaiter* SeriesSheetWaiter;

    /// <summary>
    ///     数据表 Item 的实例。
    /// </summary>
    [FieldOffset(1008)]
    public ExcelSheet* ItemSheet;

    /// <summary>
    ///     用于异步载入奖励物品行的等待器。
    /// </summary>
    [FieldOffset(1016)]
    public ExcelSheetWaiter* ItemSheetWaiter;

    /// <summary>
    ///     奖励行中出现过的去重物品 ID 列表，用于向数据表 Item 查询图标与名称。
    /// </summary>
    [FieldOffset(1024)]
    public StdVector<uint> RewardItemIDs;

    /// <summary>
    ///     赛季提示的显示状态：0 表示待处理，1 表示正在等待 <see cref="SeriesNoticeTimer" /> 倒计时，
    ///     2 表示已处理完毕（提示已打开或无需提示）。
    /// </summary>
    [FieldOffset(1048)]
    public int SeriesNoticeState;

    /// <summary>
    ///     赛季提示确认界面的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(1052)]
    public uint SeriesNoticeAddonID;

    /// <summary>
    ///     赛季提示确认界面的打开延迟计时，单位为秒。
    /// </summary>
    [FieldOffset(1056)]
    public float SeriesNoticeTimer;

    /// <summary>
    ///     客户端从未读写的保留字段。
    /// </summary>
    [FieldOffset(1060)]
    public uint Unknown1060;

    /// <summary>
    ///     刷新快捷栏，并把界面数据与赛季奖励界面标记为待刷新。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 E8 ?? ?? ?? ?? 66 C7 43")]
    public partial void RefreshHotbar();

    /// <summary>
    ///     按职业类别查出当前职业所属的 PvP 动作集索引。
    /// </summary>
    /// <param name="playerState">玩家状态。</param>
    /// <returns>PvP 动作集索引，未匹配到任何职业类别时返回 0。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 4C 8D 35")]
    public partial uint GetActionSetIndex
    (
        PlayerState* playerState
    );

    /// <summary>
    ///     把职能技能槽位设为指定索引，写入角色保存的 PvP 技能配置，
    ///     界面激活时同时更新本地缓存与界面数值数组 166。
    /// </summary>
    /// <param name="index">职能技能索引。</param>
    [MemberFunction("4C 8B DC 53 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B D9")]
    public partial void SetRoleActionIndex
    (
        byte index
    );

    /// <summary>
    ///     打开 PvP 日程界面，并向服务器请求该日程的数据。
    /// </summary>
    /// <param name="parentAddonID">父界面 ID。</param>
    /// <param name="calendarIndex">日程索引：0 为 PvP 档案界面的日程，1 为队伍战绩界面的日程。</param>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4D 63 F0")]
    public partial void OpenCalendar
    (
        ushort parentAddonID,
        int    calendarIndex
    );

    /// <summary>
    ///     关闭指定日程的界面。
    /// </summary>
    /// <param name="calendarIndex">日程索引。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 63 C2 8B 9C 81")]
    public partial void CloseCalendar
    (
        int calendarIndex
    );

    /// <summary>
    ///     用服务器下发的日程数据填充指定日程的时段掩码并刷新对应界面。
    /// </summary>
    /// <param name="data">日程数据，首字节为日程类型。</param>
    [MemberFunction("40 53 56 57 41 54 41 55 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 33 FF")]
    public partial void ApplyCalendarData
    (
        byte* data
    );

    /// <summary>
    ///     设置爆发标记的目标对象并启动爆发计时与音效。
    /// </summary>
    /// <param name="objectID">目标对象 ID，0xE0000000 表示无目标。</param>
    [MemberFunction("81 FA ?? ?? ?? ?? 74 ?? 89 91")]
    public partial void SetBurstState
    (
        uint objectID
    );

    /// <summary>
    ///     按爆发计时与对象归属计算要展示的 PvP 爆发技能 ID。
    /// </summary>
    /// <param name="gameObject">目标对象。</param>
    /// <returns>爆发技能 ID，不属于爆发目标时返回 0。</returns>
    [MemberFunction("48 85 D2 74 ?? 8B 81 ?? ?? ?? ?? 39 42 ?? 75 ?? 3D ?? ?? ?? ?? 74 ?? F3 0F 10 81 ?? ?? ?? ?? 4C 8D 05")]
    public partial uint GetBurstActionID
    (
        void* gameObject
    );

    /// <summary>
    ///     按爆发计时与对象归属计算要展示的标记图标 ID。
    /// </summary>
    /// <param name="gameObject">目标对象。</param>
    /// <returns>爆发技能对应的图标 ID，非爆发目标时返回对象自身的标记图标。</returns>
    [MemberFunction("48 83 EC ?? 48 85 D2 74 ?? 8B 81")]
    public partial uint GetBurstMarkerIconID
    (
        void* gameObject
    );

    /// <summary>
    ///     在赛季奖励界面未打开时打开它，已打开时关闭它。
    /// </summary>
    /// <param name="parentAddonID">父界面 ID。</param>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 8B 99 ?? ?? ?? ?? 85 DB 74 ?? C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B D3 48 8B C8 4C 8B 00 48 83 C4 ?? 5B 49 FF A0 ?? ?? ?? ?? C6 81"
    )]
    public partial void ToggleRewardAddon
    (
        ushort parentAddonID
    );

    /// <summary>
    ///     关闭赛季奖励界面。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 8B 99 ?? ?? ?? ?? 85 DB 74 ?? C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B D3 48 8B C8 4C 8B 00 48 83 C4 ?? 5B 49 FF A0 ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 6C 24 ?? 56"
    )]
    public partial void CloseRewardAddon();

    /// <summary>
    ///     构造 PvP 档案界面所需的 9 个界面参数。
    /// </summary>
    /// <param name="values">接收参数的目标数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 57 41 54 41 56 41 57 48 83 EC ?? 4C 8B F2")]
    public partial void BuildProfileAddonValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     切换页签并请求界面按新页签重新填充数据。
    /// </summary>
    /// <param name="tabIndex">目标页签索引，负值不会写入 <see cref="SelectedTab" />。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F9 8B F2 48 8B 49")]
    public partial void SwitchTab
    (
        int tabIndex
    );

    /// <summary>
    ///     重新填充界面的数值数组 105 与字符串数组 94。
    /// </summary>
    /// <param name="skipActionList">为 <see langword="true" /> 时跳过技能列表的刷新。</param>
    /// <returns>界面数据已完整填充时返回 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 48 8B F9 44 0F B6 F2")]
    public partial bool RefreshAddonArrays
    (
        bool skipActionList
    );

    /// <summary>
    ///     填充对战等级区块：等级、经验与晋升进度。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="playerState">玩家状态。</param>
    /// <param name="profile">PvP 档案数据。</param>
    /// <returns>等级相关数据齐备时返回 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 4C 8B F2 4D 8B F8")]
    public partial bool UpdateRankSection
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PlayerState*     playerState,
        PvPProfile*      profile
    );

    /// <summary>
    ///     填充战绩区块的标题文本、场地记录与赛季信息。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="profile">PvP 档案数据。</param>
    /// <returns>场地记录数据齐备时返回 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 57 41 54 41 56 41 57 48 83 EC ?? 48 8B E9 4D 8B F9")]
    public partial bool UpdateRecordSection
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PvPProfile*      profile
    );

    /// <summary>
    ///     填充纷争前线的累计与本周战绩。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="profile">PvP 档案数据。</param>
    [MemberFunction("4C 8B DC 53 55 56 57 41 54 41 55 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 49")]
    public partial void UpdateFrontlineSummary
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PvPProfile*      profile
    );

    /// <summary>
    ///     填充水晶冲突的累计与本周战绩及胜率。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="profile">PvP 档案数据。</param>
    [MemberFunction("40 53 55 56 57 41 54 41 55 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1")]
    public partial void UpdateCrystallineConflictSummary
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PvPProfile*      profile
    );

    /// <summary>
    ///     按当前职业的 PvP 技能排序表填充技能行与连击路线行，并记录到
    ///     <see cref="ActionIDsStorage" /> 与 <see cref="ActionComboRouteIDsStorage" />。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="playerState">玩家状态。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("4C 89 44 24 ?? 53 55 41 54")]
    public partial bool UpdateActionList
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PlayerState*     playerState
    );

    /// <summary>
    ///     填充职能技能列表，并同步角色保存的职能技能槽位。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="playerState">玩家状态。</param>
    /// <param name="profile">PvP 档案数据。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("40 55 41 54 41 55 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B BC 24")]
    public partial bool UpdateRoleActionList
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PlayerState*     playerState,
        PvPProfile*      profile
    );

    /// <summary>
    ///     填充快捷聊天列表，并把有效的快捷聊天行 ID 记录到 <see cref="QuickChatIDsStorage" />。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("4C 8B DC 4D 89 43 ?? 49 89 53 ?? 49 89 4B ?? 55")]
    public partial bool UpdateQuickChatList
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray
    );

    /// <summary>
    ///     在界面数值数组中写入一个图标项：相邻两项分别为数值与带高亮位的图标 ID。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="index">写入的起始下标。</param>
    /// <param name="value">写入第一项的数值。</param>
    /// <param name="iconID">写入第二项的图标 ID。</param>
    /// <param name="highlighted">为 <see langword="true" /> 时给图标加上第 24 位高亮标记。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 41 8B D8 48 8B FA 8B D3 48 8B CF 45 8B C1 E8 ?? ?? ?? ?? 44 8B 44 24 ?? 8D 53 ?? 41 C1 E0")]
    public partial void SetIconValue
    (
        NumberArrayData* numberArray,
        int              index,
        int              value,
        int              iconID,
        bool             highlighted
    );

    /// <summary>
    ///     处理日程条目的鼠标事件：按条目构造提示文本并刷新日程界面。
    /// </summary>
    /// <param name="values">界面回调参数。</param>
    /// <param name="valueCount">参数数量。</param>
    /// <param name="calendarIndex">日程索引。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("40 55 53 57 41 54 41 55 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 4C 8B E9")]
    public partial bool HandleCalendarEntryEvent
    (
        AtkValue* values,
        uint      valueCount,
        int       calendarIndex
    );

    /// <summary>
    ///     处理赛季奖励界面的回调：关闭界面、领取奖励或切换上一赛季视图。
    /// </summary>
    /// <param name="values">界面回调参数。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B FA 48 8B CA E8 ?? ?? ?? ?? 44 8D 40 ?? 41 83 F8 ?? 76 ?? 85 C0 74 ?? 83 F8 ?? 74")]
    public partial bool HandleRewardAddonEvent
    (
        AtkValue* values
    );

    /// <summary>
    ///     在“当前赛季”与“上一赛季”两种视图之间切换，并刷新赛季奖励界面。
    /// </summary>
    /// <param name="values">界面回调参数。</param>
    /// <returns>恒为 <see langword="true" />。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? 85 C0 75 ?? 0F B6 93")]
    public partial bool TogglePreviousSeriesView
    (
        AtkValue* values
    );

    /// <summary>
    ///     在地图上定位赛季奖励发放员，并打开对应的地图标记。
    /// </summary>
    /// <param name="values">界面回调参数。</param>
    /// <returns>地图数据齐备时返回 <see langword="false" />。</returns>
    [MemberFunction("40 55 41 56 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B F1 48 8B CA")]
    public partial bool OpenSeasonRewardNPCMap
    (
        AtkValue* values
    );

    /// <summary>
    ///     打开 PvP 档案界面的右键上下文菜单。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8B C8 48 8B D8 E8 ?? ?? ?? ?? 48 8B 4F")]
    public partial void OpenContextMenu();

    /// <summary>
    ///     填充一场对局统计行的文本与进度。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="ruleIndex">统计行索引，5 与 6 为特殊说明行。</param>
    /// <param name="value">统计数值。</param>
    /// <param name="maxValue">统计上限。</param>
    /// <param name="isWeekly">为 <see langword="true" /> 时使用本周文本。</param>
    [MemberFunction("40 53 55 56 57 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 44 8B A4 24")]
    public partial void UpdateRuleRow
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        int              ruleIndex,
        int              value,
        uint             maxValue,
        bool             isWeekly
    );

    /// <summary>
    ///     填充段位行：段位积分、段位星级与文本。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="currentCredit">当前段位积分。</param>
    /// <param name="currentRank">当前段位等级。</param>
    /// <param name="highestCredit">最高段位积分。</param>
    /// <param name="highestRank">最高段位等级。</param>
    /// <param name="rankRowIndex">段位行在数值数组中的基址下标。</param>
    /// <param name="seriesLevel">系列赛等级。</param>
    /// <param name="valueIndex">写入数值数组的起始下标。</param>
    /// <param name="index">写入段位文本的字符串数组下标。</param>
    /// <returns>段位文本数据齐备时返回 <see langword="true" />。</returns>
    [MemberFunction("40 55 57 41 54 41 55 41 56 48 8D 6C 24")]
    public partial bool UpdateMatchRankRow
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        ushort           currentCredit,
        byte             currentRank,
        ushort           highestCredit,
        byte             highestRank,
        byte             rankRowIndex,
        ushort           seriesLevel,
        int              valueIndex,
        int              index
    );

    /// <summary>
    ///     填充赛季标题、段位星级与本季是否已经开赛。
    /// </summary>
    /// <param name="numberArray">界面数值数组 105。</param>
    /// <param name="stringArray">界面字符串数组 94。</param>
    /// <param name="profile">PvP 档案数据。</param>
    /// <returns>段位数据齐备时返回 <see langword="true" />。</returns>
    [MemberFunction("40 55 56 57 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 49 8B 40")]
    public partial bool UpdateSeasonLabels
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        PvPProfile*      profile
    );

    /// <summary>
    ///     对当前目标使用界面数值数组 100 起始的槽位中的 PvP 技能。
    /// </summary>
    /// <param name="slotIndex">技能槽位索引。</param>
    /// <returns>技能使用请求的返回值。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 8B DA 48 8B 01 FF 50 ?? BA ?? ?? ?? ?? 48 8B 08 4C 8B 41 ?? 48 8B C8 41 FF D0 8D 0C 5D")]
    public partial bool UseActionFromSlot
    (
        int slotIndex
    );

    /// <summary>
    ///     对当前目标使用界面数值数组 200 起始的槽位中的 PvP 连击技能。
    /// </summary>
    /// <param name="slotIndex">技能槽位索引。</param>
    /// <returns>连击技能使用请求的返回值。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 8B DA 48 8B 01 FF 50 ?? BA ?? ?? ?? ?? 48 8B 08 4C 8B 41 ?? 48 8B C8 41 FF D0 C1 E3")]
    public partial int UseComboActionFromSlot
    (
        int slotIndex
    );

    /// <summary>
    ///     对当前目标使用界面数值数组 52 起始的网格中的 PvP 技能。
    /// </summary>
    /// <param name="cellIndex">网格单元索引。</param>
    /// <returns>技能使用请求的返回值。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 8B DA 48 8B 01 FF 50 ?? BA ?? ?? ?? ?? 48 8B 08 4C 8B 41 ?? 48 8B C8 41 FF D0 4C 8B C0")]
    public partial bool UseActionFromGrid
    (
        int cellIndex
    );

    /// <summary>
    ///     选择界面数值数组 860 起始的网格中的职能技能。
    /// </summary>
    /// <param name="cellIndex">网格单元索引。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 8B DA 48 8B 49 ?? 48 8B 01 FF 50")]
    public partial void SelectRoleAction
    (
        int cellIndex
    );

    /// <summary>
    ///     打开 PvP 玩法介绍网页。
    /// </summary>
    [MemberFunction("B9 ?? ?? ?? ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC 84 D2")]
    public partial void OpenGuideWebPage();

    /// <summary>
    ///     显示技能列表中指定项的技能提示。
    /// </summary>
    /// <param name="index">技能索引。</param>
    [MemberFunction("84 D2 78 ?? 53")]
    public partial void ShowActionTooltip
    (
        byte index
    );

    /// <summary>
    ///     显示连击路线列表中指定项的连击路线提示。
    /// </summary>
    /// <param name="index">连击路线索引。</param>
    [MemberFunction("84 D2 0F 88 ?? ?? ?? ?? 53")]
    public partial void ShowComboRouteTooltip
    (
        byte index
    );

    /// <summary>
    ///     显示技能网格中指定单元的技能提示。
    /// </summary>
    /// <param name="cellIndex">网格单元索引。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 0F BE DA")]
    public partial void ShowActionGridTooltip
    (
        byte cellIndex
    );

    /// <summary>
    ///     设置职能技能槽位，同时写回角色保存的 PvP 技能配置。
    /// </summary>
    /// <param name="index">职能技能索引。</param>
    [MemberFunction("40 53 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B F9 41 B0")]
    public partial void SetRoleAction
    (
        byte index
    );

    /// <summary>
    ///     交换两个职能技能槽位的索引，并把新的第 0 项写入界面数值数组 166。
    /// </summary>
    /// <param name="first">第一个槽位索引。</param>
    /// <param name="second">第二个槽位索引。</param>
    [MemberFunction("84 D2 0F 85 ?? ?? ?? ?? 57")]
    public partial void SwapRoleActionSlots
    (
        byte first,
        byte second
    );

    /// <summary>
    ///     清除待处理的界面 ID；未被取消且启用了交叉快捷栏时额外显示坐骑技能提示。
    /// </summary>
    /// <param name="canceled">为 <see langword="true" /> 时只做清理。</param>
    [MemberFunction("84 D2 0F 85 ?? ?? ?? ?? 53")]
    public partial void OnRoleActionSelectionEnded
    (
        bool canceled
    );

    /// <summary>
    ///     对当前目标使用 PvP 坐骑技能。
    /// </summary>
    /// <param name="canceled">为 <see langword="true" /> 时不执行。</param>
    [MemberFunction("48 83 EC ?? 84 D2 75 ?? 48 8D 0D")]
    public partial void UseMountAction
    (
        bool canceled
    );

    /// <summary>
    ///     显示指定快捷聊天的提示。
    /// </summary>
    /// <param name="index">快捷聊天索引。</param>
    /// <returns>对应的快捷聊天行，索引无效时返回空指针。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 48 0F BE DA")]
    public partial void* ShowQuickChatTooltip
    (
        byte index
    );

    /// <summary>
    ///     把职能技能槽位与修改标记写入界面数值数组 166 与 5。
    /// </summary>
    /// <param name="edited">为 <see langword="true" /> 时表示修改尚未提交。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 8B F2 48 8B 49 ?? 48 8B 01 FF 50")]
    public partial void SetRoleActionEdited
    (
        bool edited
    );

    /// <summary>
    ///     判断当前是否具备查看 PvP 日程的权限：需要特定权限、已解锁的 PvP 状态与网络连接。
    /// </summary>
    /// <returns>具备权限时返回 <see langword="true" />。</returns>
    [MemberFunction("40 53 48 83 EC ?? 45 33 C9 48 8B D9 48 8D 0D")]
    public partial bool CanUsePvPCalendar();

    /// <summary>
    ///     为指定 PvP 动作集写入角色的技能配置，并刷新快捷栏。
    /// </summary>
    /// <param name="setIndex">动作集索引。</param>
    /// <returns>动作集已切换时返回 <see langword="true" />。</returns>
    [MemberFunction("40 53 55 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B E9 48 63 DA")]
    public partial bool SelectActionSet
    (
        int setIndex
    );

    /// <summary>
    ///     把当前服务器时间对应的时段写入给定的日程掩码。
    /// </summary>
    /// <param name="mask">接收时段标记的掩码数组，长度为 7。</param>
    /// <returns>写入结果。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 0F B6 44 24")]
    public partial int ApplyCurrentTimeToMask
    (
        uint* mask
    );

    /// <summary>
    ///     取得当前服务器时间在日程中对应的星期与小时。
    /// </summary>
    /// <param name="dayIndex">接收星期索引。</param>
    /// <param name="hourIndex">接收小时索引。</param>
    /// <returns>星期基准偏移，供换算使用。</returns>
    [MemberFunction("40 53 56 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 0F B6 9C 24")]
    public partial int GetCurrentCalendarTime
    (
        byte* dayIndex,
        byte* hourIndex
    );

    /// <summary>
    ///     推进爆发标记计时，并在阈值处播放提示音、到期时清除标记。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 0F 29 74 24 ?? 48 8B D9 F3 0F 10 B1 ?? ?? ?? ?? 0F 29 7C 24 ?? 0F 57 FF")]
    public partial void UpdateBurstTimer();

    /// <summary>
    ///     在赛季提示状态为“延迟中”时打开赛季提示确认界面。
    /// </summary>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 0F 85 ?? ?? ?? ?? C7 81")]
    public partial void OpenSeriesNoticeDialog();

    /// <summary>
    ///     在赛季数据齐备时打开赛季奖励界面。
    /// </summary>
    /// <param name="parentAddonID">父界面 ID。</param>
    [MemberFunction("4C 8B DC 56 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 80 B9")]
    public partial void OpenRewardAddon
    (
        ushort parentAddonID
    );

    /// <summary>
    ///     用当前赛季数据重新填充已打开的赛季奖励界面。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? BB ?? ?? ?? ?? 48 8D 84 24")]
    public partial void RefreshRewardAddonData();

    /// <summary>
    ///     构造赛季奖励界面所需的 202 个界面参数。
    /// </summary>
    /// <param name="values">接收参数的目标数组。</param>
    /// <param name="seriesName">接收系列赛名称的字符串。</param>
    /// <param name="progressText">接收晋升进度的字符串。</param>
    /// <param name="currencyText">接收系列赛货币数量的字符串。</param>
    /// <returns>数据齐备时返回 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 4C 8B F9 49 8B D9")]
    public partial bool BuildRewardAddonValues
    (
        AtkValue*   values,
        Utf8String* seriesName,
        Utf8String* progressText,
        Utf8String* currencyText
    );

    /// <summary>
    ///     领取指定等级的系列赛奖励，先校验背包与货币上限，再弹出确认界面。
    /// </summary>
    /// <param name="rank">要领取奖励的等级。</param>
    [MemberFunction("40 53 41 57 48 81 EC ?? ?? ?? ?? 4C 8B F9 8B DA")]
    public partial void ClaimReward
    (
        uint rank
    );

    /// <summary>
    ///     首次使用时载入数据表 PvPSeriesLevel 的全部行。
    /// </summary>
    [MemberFunction("40 56 48 83 EC ?? 48 83 B9 ?? ?? ?? ?? ?? 48 8B F1 0F 85 ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 89 5C 24 ?? 4C 89 74 24")]
    public partial void EnsureSeriesLevelData();

    /// <summary>
    ///     重新申请数据表 PvPSeries 中当前等级与上一等级两行，并清空旧的奖励行缓存。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 C6 81 ?? ?? ?? ?? ?? 48 8B 89")]
    public partial void ReloadSeriesRewardData();

    /// <summary>
    ///     依据奖励行缓存重建去重后的奖励物品列表，并申请物品数据表查询。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8D B1 ?? ?? ?? ?? 45 33 ED")]
    public partial void RebuildRewardItemList();

    /// <summary>
    ///     数据表 PvPSeriesLevel 全部行载入完成后的回调，把每行经验写入 <see cref="SeriesLevelExperience" />。
    /// </summary>
    /// <param name="rows">行数据指针数组。</param>
    /// <param name="count">行数量。</param>
    [MemberFunction("4C 8B C9 4C 8B D2 48 8B 89 ?? ?? ?? ?? 48 85 C9")]
    public partial void OnSeriesLevelLoaded
    (
        void** rows,
        ulong  count
    );

    /// <summary>
    ///     数据表 PvPSeries 行载入完成后的回调，复制奖励行并申请物品数据表查询。
    /// </summary>
    /// <param name="rows">行数据指针数组。</param>
    /// <param name="count">行数量。</param>
    /// <returns>回调返回值。</returns>
    [MemberFunction("48 8B C4 55 48 83 EC ?? 48 83 B9")]
    public partial bool OnSeriesRewardLoaded
    (
        void** rows,
        ulong  count
    );

    /// <summary>
    ///     物品数据表行载入完成后的回调，把物品图标填入奖励行并记录系列赛货币信息。
    /// </summary>
    /// <param name="rows">行数据指针数组。</param>
    /// <param name="count">行数量。</param>
    [MemberFunction("40 53 55 41 54 41 55 48 83 EC ?? 33 ED")]
    public partial void OnRewardItemsLoaded
    (
        void** rows,
        ulong  count
    );

    /// <summary>
    ///     数据表 PvPSeries 的一行奖励数据，由当前赛季等级与上一等级两份缓存共用。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 772)]
    public unsafe partial struct PvpSeriesRewardRow
    {
        /// <summary>
        ///     各等级的奖励内容，共 32 项，索引与系列赛等级一一对应。
        /// </summary>
        [FieldOffset(0)]
        [FixedSizeArray]
        internal FixedSizeArray32<PvpSeriesLevelReward> LevelRewardsStorage;

        /// <summary>
        ///     数据表 PvPSeries 行末尾的标量字段。
        /// </summary>
        [FieldOffset(512)]
        public byte Unknown512;

        /// <summary>
        ///     各等级两项奖励的物品图标 ID，共 32 组，每组依次为第一件与第二件奖励的图标。
        /// </summary>
        [FieldOffset(516)]
        [FixedSizeArray]
        internal FixedSizeArray32<PvpSeriesRewardItemIcons> ItemIconsStorage;
    }

    /// <summary>
    ///     系列赛单个等级的奖励内容。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public unsafe struct PvpSeriesLevelReward
    {
        /// <summary>
        ///     第一件奖励的物品 ID。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     第二件奖励的物品 ID。
        /// </summary>
        [FieldOffset(4)]
        public uint AdditionalItemID;

        /// <summary>
        ///     数据表 PvPSeries 中该等级的标量字段。
        /// </summary>
        [FieldOffset(8)]
        public byte Unknown8;

        /// <summary>
        ///     第一件奖励的数量。
        /// </summary>
        [FieldOffset(12)]
        public ushort Count;

        /// <summary>
        ///     第二件奖励的数量。
        /// </summary>
        [FieldOffset(14)]
        public ushort AdditionalCount;
    }

    /// <summary>
    ///     系列赛单个等级两项奖励的物品图标 ID。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe struct PvpSeriesRewardItemIcons
    {
        /// <summary>
        ///     第一件奖励的物品图标 ID。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemIconID;

        /// <summary>
        ///     第二件奖励的物品图标 ID。
        /// </summary>
        [FieldOffset(4)]
        public uint AdditionalItemIconID;
    }
}
