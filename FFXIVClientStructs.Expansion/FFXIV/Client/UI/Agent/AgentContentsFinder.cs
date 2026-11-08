using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Sound;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     任务搜索器代理，维护可选任务列表、选中状态、奖励预览与排队流程。
/// </summary>
[Agent(AgentId.ContentsFinder)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 9304)]
public unsafe partial struct AgentContentsFinder
{
    /// <summary>
    ///     详情面板与奖励预览共用的内容搜索器接口子对象。
    /// </summary>
    [FieldOffset(40)]
    public AgentContentsFinderInterface InterfaceSub;

    /// <summary>
    ///     奖励预览子对象，保存普通奖励、加成奖励与道具明细。
    /// </summary>
    [FieldOffset(2216)]
    public AgentContentsFinderReward RewardSub;

    /// <summary>
    ///     奖励图标右键菜单的事件处理器，记录目标奖励并响应菜单项。
    /// </summary>
    [FieldOffset(7792)]
    public RewardContextMenuEventHandler RewardContextMenuHandler;

    /// <summary>
    ///     内容条目存储区，指向 155 个 <see cref="Contents" /> 组成的数组，数组首地址前 8 字节为元素数量。
    /// </summary>
    [FieldOffset(7824)]
    public Contents* ContentStorage;

    /// <summary>
    ///     当前标签下的可选内容列表。
    /// </summary>
    [FieldOffset(7832)]
    public StdVector<Pointer<Contents>> ContentList;

    /// <summary>
    ///     已勾选待申请的内容标识列表，最多 5 项。
    /// </summary>
    [FieldOffset(7856)]
    public StdVector<ContentsID> SelectedContent;

    /// <summary>
    ///     当前在列表中高亮的内容标识。
    /// </summary>
    [FieldOffset(7880)]
    public ContentsID SelectedDuty;

    /// <summary>
    ///     待切换到列表中的目标内容标识，由打开指定任务时写入，界面刷新时消费并清零。
    /// </summary>
    [FieldOffset(7888)]
    public ContentsID PendingSelection;

    /// <summary>
    ///     当前内容已领取的奖励数量。
    /// </summary>
    [FieldOffset(7896)]
    public byte NumCollectedRewards;

    /// <summary>
    ///     已勾选内容中是否存在随机任务，存在时禁止再勾选其它随机任务。
    /// </summary>
    [FieldOffset(7897)]
    public bool HasRouletteSelected;

    /// <summary>
    ///     规则选择对话框中勾选的规则索引，用于组队内容申请。
    /// </summary>
    [FieldOffset(7898)]
    public byte CheckBoxDialogSelection;

    /// <summary>
    ///     置位时跳过规则选择对话框，直接提交申请。
    /// </summary>
    [FieldOffset(7899)]
    public bool SkipCheckBoxDialog;

    /// <summary>
    ///     当前高亮内容所属的界面分类索引，与 <see cref="Contents" /> 中的分类索引比较以判断能否同时申请。
    /// </summary>
    [FieldOffset(7900)]
    public byte SelectedTerritoryTypeIndex;

    /// <summary>
    ///     招募与跨服招募菜单项的事件处理器，响应菜单项时按参数打开招募界面或剧情辅助器。
    /// </summary>
    [FieldOffset(7904)]
    public ContentsFinderContextMenuEventHandler ContextMenuHandler;

    /// <summary>
    ///     底部主按钮与次按钮的状态，共 2 项。
    /// </summary>
    [FieldOffset(7928)]
    [FixedSizeArray]
    internal FixedSizeArray2<ContentsFinderButtonState> ButtonStatesStorage;

    /// <summary>
    ///     主界面关闭时接收事件的处理子对象，同时持有详情面板所需的数据对象。
    /// </summary>
    [FieldOffset(7944)]
    public ContentsFinderAddonEventHandler AddonEventHandler;

    /// <summary>
    ///     各标签页标题，按 <see cref="SelectedTab" /> 索引。
    /// </summary>
    [FieldOffset(7976)]
    [FixedSizeArray]
    internal FixedSizeArray10<Utf8String> CategoryTitlesStorage;

    /// <summary>
    ///     内容分类名称数组，长度见 <see cref="CategoryCount" />，数组首地址前 8 字节为元素数量。
    /// </summary>
    [FieldOffset(9016)]
    public Utf8String* CategoryNames;

    /// <summary>
    ///     内容分类数量，取自 ContentUICategory 表的行数。
    /// </summary>
    [FieldOffset(9024)]
    public byte CategoryCount;

    /// <summary>
    ///     各标签页当前是否存在可申请内容，按标签索引。
    /// </summary>
    [FieldOffset(9025)]
    [FixedSizeArray]
    internal FixedSizeArray10<byte> TabHasContentsStorage;

    /// <summary>
    ///     各标签页下按资料片分组的勾选状态，外层按标签索引、内层按 ExVersion 索引，共 10 组 6 项。
    /// </summary>
    [FieldOffset(9035)]
    [FixedSizeArray]
    internal FixedSizeArray60<byte> TabFilterSelectionsStorage;

    /// <summary>
    ///     各标签页是否已完成首次填充，按标签索引。
    /// </summary>
    [FieldOffset(9095)]
    [FixedSizeArray]
    internal FixedSizeArray10<byte> TabInitializedStorage;

    /// <summary>
    ///     各角色定位的随机任务奖励加成类型，共 11 项，索引对应 ContentRouletteRoleBonus 行。
    /// </summary>
    [FieldOffset(9105)]
    [FixedSizeArray]
    internal FixedSizeArray11<ContentsRouletteRole> ContentRouletteRoleBonusesStorage;

    /// <summary>
    ///     两类任务搜索封印的剩余分钟数，索引 0 与 1 分别对应两种封印来源。
    /// </summary>
    [FieldOffset(9116)]
    [FixedSizeArray]
    internal FixedSizeArray2<uint> DutyPenaltyMinutesStorage;

    /// <summary>
    ///     计时音效对象，用于出发确认阶段播放剩余时间提示。
    /// </summary>
    [FieldOffset(9128)]
    public SoundData* TimeCountSoundDataPad;

    /// <summary>
    ///     计时音效对象，用于出发确认阶段播放倒计时提示。
    /// </summary>
    [FieldOffset(9136)]
    public SoundData* TimeCountSoundData;

    /// <summary>
    ///     排队状态界面的插件标识，取值 146 至 149，分别对应 ContentsFinderStatus、ContentsFinderConfirm、ContentsFinderReady 与 ContentsFinderSupply。
    /// </summary>
    [FieldOffset(9144)]
    public uint QueueStatusAddonID;

    /// <summary>
    ///     排队状态相关确认对话框的插件标识。
    /// </summary>
    [FieldOffset(9148)]
    public uint QueueStatusYesNoAddonID;

    /// <summary>
    ///     排队操作相关确认对话框的插件标识。
    /// </summary>
    [FieldOffset(9152)]
    public uint QueueYesNoAddonID;

    /// <summary>
    ///     出发警告确认对话框的插件标识，键盘检测与游戏时间不足两种警告共用此字段。
    /// </summary>
    [FieldOffset(9156)]
    public uint CommenceWarningYesNoAddonID;

    /// <summary>
    ///     跨进度进入警告确认对话框的插件标识。
    /// </summary>
    [FieldOffset(9160)]
    public uint CrossProgressWarningYesNoAddonID;

    /// <summary>
    ///     招募中无法申请时的结束招募确认对话框插件标识。
    /// </summary>
    [FieldOffset(9164)]
    public uint RecruitYesNoAddonID;

    /// <summary>
    ///     规则选择对话框的插件标识。
    /// </summary>
    [FieldOffset(9168)]
    public uint CheckBoxDialogAddonID;

    /// <summary>
    ///     当前应显示的排队状态界面种类，取值 146 至 149，与 <see cref="QueueStatusAddonID" /> 一致。
    /// </summary>
    [FieldOffset(9172)]
    public uint QueueStatusKind;

    /// <summary>
    ///     上次刷新时记录的服务器时间戳，用于限制每秒仅刷新一次。
    /// </summary>
    [FieldOffset(9176)]
    public int CurrentTimestamp;

    /// <summary>
    ///     待推送到界面的更新位标志，位 0 表示列表数据、位 1 表示详情数据。
    /// </summary>
    [FieldOffset(9180)]
    public uint PendingAddonUpdateFlags;

    /// <summary>
    ///     满足条件的招募队伍数量，-1 表示尚未获取，且大于 99 时按 99 显示。
    /// </summary>
    [FieldOffset(9184)]
    public int RecruitingParties;

    /// <summary>
    ///     当前选中的标签页索引。
    /// </summary>
    [FieldOffset(9188)]
    public byte SelectedTab;

    /// <summary>
    ///     分类标题加载游标，按行索引递增填充 <see cref="CategoryTitles" />，加载完毕时置为 -1。
    /// </summary>
    [FieldOffset(9189)]
    public sbyte CategoryTitleLoadIndex;

    /// <summary>
    ///     出发警告流程状态，0 表示未开始，1 表示已开始，2 表示已完成。
    /// </summary>
    [FieldOffset(9190)]
    public byte CommenceWarningState;

    /// <summary>
    ///     上次刷新警告时角色的昏迷状态，用于检测昏迷状态变化并触发界面更新。
    /// </summary>
    [FieldOffset(9191)]
    public bool WasUnconscious;

    /// <summary>
    ///     请求刷新列表的位标志，位 0 表示已请求、位 1 表示等待处理。
    /// </summary>
    [FieldOffset(9192)]
    public byte ListRefreshRequested;

    /// <summary>
    ///     列表刷新流程中等待处理的位标志，与 <see cref="ListRefreshRequested" /> 配合使用。
    /// </summary>
    [FieldOffset(9193)]
    public byte ListRefreshPending;

    /// <summary>
    ///     列表刷新流程的启用标志，界面打开时置位、招募代理刷新完成时清零。
    /// </summary>
    [FieldOffset(9194)]
    public byte ListRefreshEnabled;

    /// <summary>
    ///     招募代理当前是否处于招募状态。
    /// </summary>
    [FieldOffset(9195)]
    public byte LookingForGroupActive;

    /// <summary>
    ///     分类标题加载完成后的首次全量刷新是否已完成。
    /// </summary>
    [FieldOffset(9196)]
    public byte InitialRefreshCompleted;

    /// <summary>
    ///     标签页内容已变化，需要重建列表。
    /// </summary>
    [FieldOffset(9197)]
    public bool TabChanged;

    /// <summary>
    ///     列表数据已变化，需要重建列表。
    /// </summary>
    [FieldOffset(9198)]
    public bool ListChanged;

    /// <summary>
    ///     详情数据已变化，需要重建详情面板。
    /// </summary>
    [FieldOffset(9199)]
    public bool DetailsChanged;

    /// <summary>
    ///     加载结束后需要补刷排队状态界面。
    /// </summary>
    [FieldOffset(9200)]
    public byte PendingQueueStatusUpdate;

    /// <summary>
    ///     需要重新计算出发警告状态。
    /// </summary>
    [FieldOffset(9201)]
    public byte NeedsCommenceWarningUpdate;

    /// <summary>
    ///     副本奖励加成事件监听器，监听 CSBonusManager 的奖励变动事件。
    /// </summary>
    [FieldOffset(9208)]
    public ContentsFinderCSBonusEventHandler CSBonusEventHandler;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <param name="uiModuleInterface">界面模块接口。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F2 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05")]
    public partial AgentContentsFinder* Ctor
    (
        UIModuleInterface* uiModuleInterface
    );

    /// <summary>
    ///     释放列表存储、详情数据与奖励子对象，并把虚表复位为基类。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 01 33 ED 48 8B 89 ?? ?? ?? ?? 48 85 C9")]
    public partial void PrepareDestructor();

    /// <summary>
    ///     释放全部子对象，并在指定时释放自身内存。
    /// </summary>
    /// <param name="freeFlags">位 0 置位时释放自身内存。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 01 45 33 F6")]
    public partial AgentContentsFinder* Destroy
    (
        byte freeFlags
    );

    /// <summary>
    ///     响应界面与确认对话框事件，驱动标签切换、内容勾选、申请提交与右键菜单。
    /// </summary>
    /// <param name="returnValue">返回值容器。</param>
    /// <param name="values">事件参数数组。</param>
    /// <param name="valueCount">事件参数数量。</param>
    /// <param name="eventKind">事件类型。</param>
    /// <returns>返回值容器。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 45")]
    public partial AtkValue* ProcessEvent
    (
        AtkValue* returnValue,
        AtkValue* values,
        uint      valueCount,
        ulong     eventKind
    );

    /// <summary>
    ///     每秒刷新一次排队状态与封印剩余时间，并同步列表与详情。
    /// </summary>
    /// <param name="frameCount">帧计数。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 8B C8")]
    public partial void Tick
    (
        uint frameCount
    );

    /// <summary>
    ///     打开任务搜索器；受副本限制时改为打开任务搜索器菜单。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 84 C0 74 ?? 48 8B 4B ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 48 8B 10 FF 52"
    )]
    public partial void Open();

    /// <summary>
    ///     关闭任务搜索器，重置选中内容、对话框标识与警告状态。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 48 8D 0D")]
    public partial void Close();

    /// <summary>
    ///     响应游戏事件：登录结束时重置标签与筛选状态，加载结束时补刷排队状态界面。
    /// </summary>
    /// <param name="gameEvent">游戏事件类型。</param>
    [MemberFunction("85 D2 74 ?? 83 FA ?? 75 ?? 80 B9 ?? ?? ?? ?? ?? 74 ?? 83 B9")]
    public partial void HandleGameEvent
    (
        AgentGameEvent gameEvent
    );

    /// <summary>
    ///     等级变化时的空实现。
    /// </summary>
    /// <param name="classJobID">职业 id。</param>
    /// <param name="level">等级。</param>
    [MemberFunction("E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 48 83 EC ?? BA")]
    public partial void HandleLevelChange
    (
        byte   classJobID,
        ushort level
    );

    /// <summary>
    ///     职业变化时的空实现。
    /// </summary>
    /// <param name="classJobID">职业 id。</param>
    [MemberFunction("E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 48 83 EC ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? BA ?? ?? ?? ?? 48 8B C8 E8")]
    public partial void HandleClassJobChange
    (
        byte classJobID
    );

    /// <summary>
    ///     判断主命令「任务搜索器」是否可用。
    /// </summary>
    /// <returns>可用时返回 true。</returns>
    [MemberFunction
    (
        "48 83 EC ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? BA ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 84 C0 75 ?? 48 83 C4 ?? C3 B0 ?? 48 83 C4 ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial bool CanActivate();

    /// <summary>
    ///     打开指定普通任务的详情；高级任务改由任务搜索器菜单代理打开。
    /// </summary>
    /// <param name="contentsFinderCondition">ContentFinderCondition 行 id。</param>
    /// <param name="hideIfShown">已显示同一任务时是否改为关闭界面。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B F9 41 0F B6 E8")]
    public partial void OpenRegularDuty
    (
        uint contentsFinderCondition,
        bool hideIfShown = false
    );

    /// <summary>
    ///     打开指定随机任务的详情。
    /// </summary>
    /// <param name="roulette">ContentRoulette 行 id。</param>
    /// <param name="hideIfShown">已显示同一任务时是否改为关闭界面。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 01 41 0F B6 E8")]
    public partial void OpenRouletteDuty
    (
        byte roulette,
        bool hideIfShown = false
    );

    /// <summary>
    ///     清空当前选中内容并重建界面。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 01 48 8B D9 FF 50 ?? 84 C0 74 ?? 48 8B CB C6 83")]
    public partial void Refresh();

    /// <summary>
    ///     按随机任务的奖励加成表更新各角色定位的加成类型。
    /// </summary>
    /// <param name="roleIDs">角色定位数组起始地址。</param>
    /// <param name="roleCount">角色定位数量，超过 11 时按 11 处理。</param>
    [MemberFunction("48 89 4C 24 ?? 55 41 56 48 83 EC ?? 48 8B 01")]
    public partial void SetContentRouletteRoleBonus
    (
        byte* roleIDs,
        uint  roleCount
    );

    /// <summary>
    ///     检查出发条件：角色资料已加载、当前职业与排队职业一致、角色未处于无法战斗状态。
    /// </summary>
    /// <returns>0 表示满足；1 表示职业不符；3 表示无法战斗；5 表示角色资料未加载。</returns>
    [MemberFunction("48 83 EC ?? 80 3D ?? ?? ?? ?? ?? 75 ?? B8")]
    public static partial int CheckCommenceRequirements();

    /// <summary>
    ///     提交当前勾选内容的参加申请；勾选多项时校验能否同时申请，并处理招募与规则选择对话框。
    /// </summary>
    /// <returns>成功提交申请时返回 true。</returns>
    [MemberFunction
        ("48 89 5C 24 ?? 55 57 41 54 41 55 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B A9")]
    public partial bool QueueSelectedContent();

    /// <summary>
    ///     提交任务出发响应：接受时校验出发条件与参加许可，成功后发送接受响应；拒绝时按参数撤回。
    /// </summary>
    /// <param name="accept">是否接受本次任务出发。</param>
    /// <param name="immediateWithdraw">拒绝时是否缩短退出等待时间。</param>
    /// <returns>是否成功处理出发响应。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 41 0F B6 F0 48 8D 0D ?? ?? ?? ?? 0F B6 DA")]
    public partial bool RespondToDuty
    (
        bool accept,
        bool immediateWithdraw = false
    );

    /// <summary>
    ///     按内容标识加载详情面板数据；随机任务与普通任务分别读取对应数据表。
    /// </summary>
    /// <param name="contentsID">内容标识。</param>
    /// <returns>加载成功时返回 true；失败时清空奖励预览。</returns>
    [MemberFunction("40 53 48 83 EC ?? 44 0F B6 02 48 8B D9")]
    public partial bool UpdateDescription
    (
        ContentsID* contentsID
    );

    /// <summary>
    ///     按变化标志重建标签、列表与详情，并推送到界面。
    /// </summary>
    [MemberFunction("40 56 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1")]
    public partial void UpdateAddon();

    /// <summary>
    ///     依次处理出发前的附加提示：未检测到键盘、剩余游戏时间不足、跨进度进入。
    /// </summary>
    /// <returns>存在需要用户确认的提示时返回 true。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 4C 8B F1 48 8D 0D")]
    public partial bool ShowCommenceWarnings();

    /// <summary>
    ///     重建当前标签下的内容列表，刷新条目名称、等级、分类、版本与可参加标志。
    /// </summary>
    [MemberFunction("48 8B C4 55 56 48 8D 68 ?? 48 81 EC ?? ?? ?? ?? 80 B9")]
    public partial void RebuildContentList();

    /// <summary>
    ///     刷新列表条目的筛选与可参加状态，并按当前排序设置重排列表。
    /// </summary>
    [MemberFunction("48 8B C4 57 48 81 EC ?? ?? ?? ?? 48 89 58 ?? 48 8B F9 48 8B 49 ?? 48 89 70 ?? 4C 89 60")]
    public partial void RefreshContentList();

    /// <summary>
    ///     构建列表界面参数，填充条目文本、状态位与底部按钮状态。
    /// </summary>
    /// <param name="values">界面参数数组。</param>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F1 4C 8B EA")]
    public partial void BuildListValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     构建详情面板参数，填充等级、名称、奖励、惩罚与限制说明。
    /// </summary>
    /// <param name="values">界面参数数组。</param>
    [MemberFunction("40 55 53 56 41 56 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 80 B9")]
    public partial void BuildDetailValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     构建标签与公共参数，填充标签标题、排序设置与提示文本。
    /// </summary>
    /// <param name="values">界面参数数组。</param>
    [MemberFunction("40 55 41 57 48 83 EC ?? 80 3D")]
    public partial void BuildCommonValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     消费待切换内容标识，把目标内容切换到列表并选中。
    /// </summary>
    [MemberFunction("48 89 74 24 ?? 41 56 48 81 EC ?? ?? ?? ?? 80 B9")]
    public partial void ApplyPendingSelection();

    /// <summary>
    ///     重新计算各标签页是否存在可申请内容。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 4C 8B E1")]
    public partial void RefreshTabAvailability();

    /// <summary>
    ///     打开任务搜索器主界面，分配内容存储与分类名称数组并推送初始参数。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B F1 48 8D 0D"
    )]
    public partial void OpenAddon();

    /// <summary>
    ///     重置代理状态：关闭对话框、清空列表存储、详情数据与奖励预览。
    /// </summary>
    [MemberFunction("48 83 EC ?? 48 89 5C 24 ?? 48 8D 91")]
    public partial void Reset();

    /// <summary>
    ///     关闭全部排队相关对话框。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8B D9 39 B9 ?? ?? ?? ?? 76 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? 89 BB ?? ?? ?? ?? 39 BB ?? ?? ?? ?? 76 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? 89 BB ?? ?? ?? ?? 39 BB"
    )]
    public partial void CloseDialogs();

    /// <summary>
    ///     打开任务搜索器菜单。
    /// </summary>
    [MemberFunction("48 83 EC ?? 44 0F B7 41 ?? 45 33 C9")]
    public partial void OpenContentsFinderMenu();

    /// <summary>
    ///     按当前排队状态刷新排队界面，未打开时关闭已有对话框。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 82 ?? ?? ?? ?? 8B 93 ?? ?? ?? ?? 41 FF D0 83 F8"
    )]
    public partial void UpdateQueueStatusAddon();

    /// <summary>
    ///     按指定状态刷新排队界面，并更新出发警告状态。
    /// </summary>
    /// <param name="statusKind">排队状态界面种类。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8B 4B ?? 48 8B 01 FF 90 ?? ?? ?? ?? 84 C0")]
    public partial void UpdateQueueStatus
    (
        uint statusKind
    );

    /// <summary>
    ///     更新出发警告状态；界面未就绪时标记为需要更新。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 0F 84 ?? ?? ?? ?? 48 8B 49 ?? 48 8B 01 FF 50")]
    public partial void UpdateCommenceWarning();

    /// <summary>
    ///     刷新排队状态界面，填充当前排队名次与剩余时间。
    /// </summary>
    /// <param name="refreshOnly">是否仅刷新已有界面而不新建。</param>
    /// <returns>界面已就绪并完成刷新时返回 true。</returns>
    [MemberFunction("4C 8B DC 55 53 56 57 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4D 89 63 ?? 48 8D 44 24")]
    public partial bool RefreshQueueStatusAddon
    (
        bool refreshOnly
    );

    /// <summary>
    ///     刷新出发确认界面，填充任务名、等级、限制说明与已领取奖励。
    /// </summary>
    /// <param name="refreshOnly">是否仅刷新已有界面而不新建。</param>
    /// <returns>界面已就绪并完成刷新时返回 true。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 41 BE"
    )]
    public partial bool RefreshConfirmAddon
    (
        bool refreshOnly
    );

    /// <summary>
    ///     刷新已接受界面，填充任务名与各职能人数。
    /// </summary>
    /// <param name="refreshOnly">是否仅刷新已有界面而不新建。</param>
    /// <returns>界面已就绪并完成刷新时返回 true。</returns>
    [MemberFunction
        ("4C 8B DC 55 53 56 57 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4D 89 63 ?? 48 8D 45 ?? 4D 89 73")]
    public partial bool RefreshReadyAddon
    (
        bool refreshOnly
    );

    /// <summary>
    ///     刷新补充奖励界面，填充任务名与奖励条目。
    /// </summary>
    /// <param name="refreshOnly">是否仅刷新已有界面而不新建。</param>
    /// <returns>界面已就绪并完成刷新时返回 true。</returns>
    [MemberFunction("4C 8B DC 55 53 56 57 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 4D 89 63 ?? 48 8D 45")]
    public partial bool RefreshSupplyAddon
    (
        bool refreshOnly
    );

    /// <summary>
    ///     刷新排队剩余时间文本与对应界面数值。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 48 8D 0D")]
    public partial void UpdateQueueTimer();

    /// <summary>
    ///     刷新出发确认阶段的剩余时间文本。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 48 8B 4F")]
    public partial void UpdateConfirmTimer();

    /// <summary>
    ///     刷新界面上的当前职业图标。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? BA ?? ?? ?? ?? 48 8B 08 4C 8B 41 ?? 48 8B C8 41 FF D0 80 3D")]
    public partial void UpdateJobIcon();

    /// <summary>
    ///     停止计时音效并清空音效对象。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 01 BA ?? ?? ?? ?? FF 90 ?? ?? ?? ?? 48 C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B 8B ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 01 BA ?? ?? ?? ?? FF 90 ?? ?? ?? ?? 48 C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B 02"
    )]
    public partial void StopTimeCountSounds();

    /// <summary>
    ///     按内容类型与资料片计算所属标签页索引。
    /// </summary>
    /// <param name="contentType">内容类型。</param>
    /// <param name="exVersion">资料片索引。</param>
    /// <returns>标签页索引；无匹配标签时返回 0。</returns>
    [MemberFunction("44 8B D2 48 8D 05")]
    public static partial uint GetTabIndex
    (
        int  contentType,
        byte exVersion
    );

    /// <summary>
    ///     取得当前高亮内容已领取的奖励数量。
    /// </summary>
    /// <returns>已领取数量；无选中内容时返回 0。</returns>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 48 8D 99")]
    public partial byte GetCollectedRewardCount();

    /// <summary>
    ///     按内容标识在内容列表中查找条目。
    /// </summary>
    /// <param name="contentsID">内容标识。</param>
    /// <returns>找到时返回条目指针，否则返回空指针。</returns>
    [MemberFunction("4C 8B 91 ?? ?? ?? ?? 33 C0 4C 8B 81")]
    public partial Contents* FindContent
    (
        ContentsID* contentsID
    );

    /// <summary>
    ///     奖励图标右键菜单的事件处理器。
    /// </summary>
    [GenerateInterop]
    [Inherits<AtkModuleInterface.AtkEventInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe partial struct RewardContextMenuEventHandler
    {
        /// <summary>
        ///     打开菜单时所属界面的插件标识，菜单项执行后清零。
        /// </summary>
        [FieldOffset(16)]
        public uint AddonID;

        /// <summary>
        ///     打开菜单时选中的奖励条目，菜单项执行后清零。
        /// </summary>
        [FieldOffset(24)]
        public AgentContentsFinderReward.ItemWrap* SelectedReward;

        /// <summary>
        ///     按菜单项类型打开对比、试穿、检索或链接道具。
        /// </summary>
        /// <param name="returnValue">返回值容器。</param>
        /// <param name="values">事件参数数组。</param>
        /// <param name="valueCount">事件参数数量。</param>
        /// <param name="eventKind">菜单项索引。</param>
        /// <returns>返回值容器。</returns>
        [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 48 8B F2 48 8B F9 75 ?? C7 02")]
        public partial AtkValue* ReceiveEvent
        (
            AtkValue* returnValue,
            AtkValue* values,
            uint      valueCount,
            ulong     eventKind
        );
    }

    /// <summary>
    ///     招募菜单的事件处理器，按菜单项参数打开招募界面或剧情辅助器。
    /// </summary>
    [GenerateInterop]
    [Inherits<AtkModuleInterface.AtkEventInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe partial struct ContentsFinderContextMenuEventHandler
    {
        /// <summary>
        ///     打开菜单时选中的内容标识，供菜单项加载对应内容。
        /// </summary>
        [FieldOffset(16)]
        public ContentsID SelectedContent;
    }

    /// <summary>
    ///     底部按钮状态。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct ContentsFinderButtonState
    {
        /// <summary>
        ///     按钮种类：5 表示参加，6 表示取消参加，7 表示查看状况，13 表示解除选择。
        /// </summary>
        [FieldOffset(0)]
        public uint Kind;

        /// <summary>
        ///     按钮禁用标志，置位时按钮按禁用状态显示。
        /// </summary>
        [FieldOffset(4)]
        public bool IsDisabled;
    }

    /// <summary>
    ///     主界面关闭事件的处理子对象，同时持有详情面板所需的数据对象。
    /// </summary>
    [GenerateInterop]
    [Inherits<AtkModuleInterface.AtkEventInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe partial struct ContentsFinderAddonEventHandler
    {
        /// <summary>
        ///     界面模块接口，用于查询与关闭界面。
        /// </summary>
        [FieldOffset(16)]
        public UIModuleInterface* UIModuleInterface;

        /// <summary>
        ///     详情面板数据对象，界面关闭时释放。
        /// </summary>
        [FieldOffset(24)]
        public ContentsFinderDetailData* DetailData;

        /// <summary>
        ///     响应界面关闭事件并释放详情数据。
        /// </summary>
        /// <param name="returnValue">返回值容器。</param>
        /// <param name="values">事件参数数组。</param>
        /// <param name="valueCount">事件参数数量。</param>
        /// <param name="eventKind">事件类型。</param>
        /// <returns>返回值容器。</returns>
        [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 48 8B DA 49 8B C8 40 32 FF E8 ?? ?? ?? ?? 83 C0 ?? 83 F8 ?? 77 ?? 48 8B CE")]
        public partial AtkValue* ReceiveEvent
        (
            AtkValue* returnValue,
            AtkValue* values,
            uint      valueCount,
            ulong     eventKind
        );
    }

    /// <summary>
    ///     详情面板数据对象，保存道具表监听器与道具名称数组。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 80)]
    public unsafe struct ContentsFinderDetailData
    {
        /// <summary>
        ///     虚表指针。
        /// </summary>
        [FieldOffset(0)]
        public void** VirtualTable;

        /// <summary>
        ///     道具名称数组，数组首地址前 8 字节为元素数量。
        /// </summary>
        [FieldOffset(16)]
        public Utf8String* ItemNames;

        /// <summary>
        ///     道具图标数据指针。
        /// </summary>
        [FieldOffset(32)]
        public void* ItemIconData;

        /// <summary>
        ///     道具表。
        /// </summary>
        [FieldOffset(56)]
        public void* ItemSheet;

        /// <summary>
        ///     道具表监听器。
        /// </summary>
        [FieldOffset(64)]
        public void* ItemSheetWaiter;

        /// <summary>
        ///     详情面板所属界面的插件标识。
        /// </summary>
        [FieldOffset(72)]
        public uint AddonID;
    }

    /// <summary>
    ///     副本奖励加成事件监听器。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 96)]
    public unsafe partial struct ContentsFinderCSBonusEventHandler
    {
        /// <summary>
        ///     虚表指针。
        /// </summary>
        [FieldOffset(0)]
        public void** VirtualTable;

        /// <summary>
        ///     事件列表的节点数据。
        /// </summary>
        [FieldOffset(8)]
        public ulong EventListNode;

        /// <summary>
        ///     事件列表的尾部节点数据。
        /// </summary>
        [FieldOffset(16)]
        public ulong EventListTail;

        /// <summary>
        ///     内嵌的奖励事件订阅器虚表指针。
        /// </summary>
        [FieldOffset(24)]
        public void** SubscriberVirtualTable;

        /// <summary>
        ///     订阅管理器指针。
        /// </summary>
        [FieldOffset(32)]
        public void* SubscriberManager;

        /// <summary>
        ///     订阅锁指针。
        /// </summary>
        [FieldOffset(40)]
        public void* SubscriberLock;

        /// <summary>
        ///     所属任务搜索器代理。
        /// </summary>
        [FieldOffset(48)]
        public AgentContentsFinder* Agent;

        /// <summary>
        ///     事件类型参数。
        /// </summary>
        [FieldOffset(56)]
        public ulong EventKind;

        /// <summary>
        ///     奖励数据对象，事件触发时创建、释放时销毁。
        /// </summary>
        [FieldOffset(64)]
        public void* RewardData;

        /// <summary>
        ///     奖励内容标识列表，数组首地址前 8 字节为元素数量。
        /// </summary>
        [FieldOffset(72)]
        public uint* ContentIDs;

        /// <summary>
        ///     奖励内容标识列表的尾部指针。
        /// </summary>
        [FieldOffset(80)]
        public uint* ContentIDsEnd;

        /// <summary>
        ///     奖励内容标识列表的容量指针。
        /// </summary>
        [FieldOffset(88)]
        public uint* ContentIDsCapacity;
    }

    /// <summary>
    ///     任务搜索器详情面板与奖励预览共用的内容搜索器接口。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 2176)]
    public unsafe partial struct AgentContentsFinderInterface
    {
        /// <summary>
        ///     所属代理，用于查询界面模块与插件标识。
        /// </summary>
        [FieldOffset(0)]
        public AgentInterface* AgentInterface;

        /// <summary>
        ///     触发加载的事件类型，随加载调用写入并传给回调。
        /// </summary>
        [FieldOffset(8)]
        public ulong EventKind;

        /// <summary>
        ///     内容说明文本，由当前加载的内容数据填充。
        /// </summary>
        [FieldOffset(16)]
        public Utf8String Description;

        /// <summary>
        ///     普通奖励、随机任务角色加成奖励与额外奖励的数据容器。
        /// </summary>
        [FieldOffset(120)]
        public RewardsContainer Rewards;

        /// <summary>
        ///     InstanceContent 数据表。
        /// </summary>
        [FieldOffset(2112)]
        public ExcelSheet* InstanceContentSheet;

        /// <summary>
        ///     ContentFinderConditionTransient 数据表。
        /// </summary>
        [FieldOffset(2120)]
        public ExcelSheet* ContentFinderConditionTransientSheet;

        /// <summary>
        ///     InstanceContentRewardItem 数据表。
        /// </summary>
        [FieldOffset(2128)]
        public ExcelSheet* InstanceContentRewardItemSheet;

        /// <summary>
        ///     InstanceContent 数据表的异步加载器。
        /// </summary>
        [FieldOffset(2136)]
        public ExcelSheetWaiter* InstanceContentSheetWaiter;

        /// <summary>
        ///     ContentFinderConditionTransient 数据表的异步加载器。
        /// </summary>
        [FieldOffset(2144)]
        public ExcelSheetWaiter* ContentFinderConditionTransientSheetWaiter;

        /// <summary>
        ///     InstanceContentRewardItem 数据表的异步加载器。
        /// </summary>
        [FieldOffset(2152)]
        public ExcelSheetWaiter* InstanceContentRewardItemSheetWaiter;

        /// <summary>
        ///     当前加载的任务行 id，随机任务对应 ContentRoulette，普通任务对应 ContentFinderCondition。
        /// </summary>
        [FieldOffset(2160)]
        public int SelectedDutyID;

        /// <summary>
        ///     各奖励类型已领取次数的上限值，非 -1 的项计入已领取奖励总数。
        /// </summary>
        [FieldOffset(2164)]
        [FixedSizeArray]
        internal FixedSizeArray4<sbyte> MaxReceivedRewardValuesStorage;

        /// <summary>
        ///     列表刷新期间置位，用于抑制代理的事件回调。
        /// </summary>
        [FieldOffset(2168)]
        public bool IsRefreshing;

        /// <summary>
        ///     加载期间置位，用于抑制代理的事件回调。
        /// </summary>
        [FieldOffset(2169)]
        public bool IsLoading;

        /// <summary>
        ///     数据变更期间置位，用于抑制代理的事件回调。
        /// </summary>
        [FieldOffset(2170)]
        public bool IsUpdating;

        /// <summary>
        ///     构造接口，初始化说明文本与奖励容器。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8B D9 48 89 39 48 89 79 ?? 48 83 C1")]
        public partial AgentContentsFinderInterface* Ctor();

        /// <summary>
        ///     取消全部异步数据表加载并释放加载器。
        /// </summary>
        [MemberFunction
        (
            "40 53 48 83 EC ?? 48 8B D9 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 83 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B D1 48 8B C8 E8 ?? ?? ?? ?? 48 8B 8B ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 01 BA ?? ?? ?? ?? FF 10 48 C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B 8B"
        )]
        public partial void CancelWaiters();

        /// <summary>
        ///     设置所属代理与事件类型。
        /// </summary>
        /// <param name="agent">所属代理。</param>
        /// <param name="eventKind">事件类型。</param>
        [MemberFunction("48 89 11 4C 89 41 ?? C3")]
        public partial void SetAgent
        (
            AgentInterface* agent,
            ulong           eventKind
        );

        /// <summary>
        ///     按 ContentFinderCondition 行加载详情，并填充奖励与说明文本。
        /// </summary>
        /// <param name="rowID">ContentFinderCondition 行 id。</param>
        /// <returns>加载成功时返回 true。</returns>
        [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 8B D9 8B F2 8B CA")]
        public partial bool LoadContentFinderCondition
        (
            uint rowID
        );

        /// <summary>
        ///     按 ContentRoulette 行加载详情，并填充奖励与说明文本。
        /// </summary>
        /// <param name="rowID">ContentRoulette 行 id。</param>
        /// <returns>加载成功时返回 true。</returns>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B F9 0F B6 DA")]
        public partial bool LoadContentRoulette
        (
            byte rowID
        );

        /// <summary>
        ///     按 InstanceContent 行加载详情，并填充奖励与说明文本。
        /// </summary>
        /// <param name="rowID">InstanceContent 行 id。</param>
        /// <returns>加载成功时返回 true。</returns>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B D9")]
        public partial bool LoadInstanceContent
        (
            byte rowID
        );

        /// <summary>
        ///     取得当前内容已领取的奖励数量。
        /// </summary>
        /// <returns>已领取数量。</returns>
        [MemberFunction("4C 8B 1D ?? ?? ?? ?? 45 33 C0 4C 8B D1")]
        public partial int GetReceivedRewardCount();

        /// <summary>
        ///     取得当前内容可领取的奖励数量上限。
        /// </summary>
        /// <returns>奖励数量上限。</returns>
        [MemberFunction("33 C0 48 81 C1 ?? ?? ?? ?? 0F 1F 80 ?? ?? ?? ?? 80 39")]
        public partial int GetMaxReceivedRewardCount();

        /// <summary>
        ///     奖励数据容器，按普通奖励、角色加成奖励与额外奖励分组保存。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 1992)]
        public unsafe partial struct RewardsContainer
        {
            /// <summary>
            ///     内容本身的奖励数据。
            /// </summary>
            [FieldOffset(0)]
            public RewardsData ContentRewards;

            /// <summary>
            ///     内容奖励对应的道具实例。
            /// </summary>
            [FieldOffset(544)]
            [FixedSizeArray]
            internal FixedSizeArray5<InventoryItem> ItemsStorage;

            /// <summary>
            ///     随机任务按角色定位发放的加成奖励数据。
            /// </summary>
            [FieldOffset(904)]
            public RewardsData ContentRouletteRoleBonusRewards;

            /// <summary>
            ///     内容额外的奖励数据。
            /// </summary>
            [FieldOffset(1448)]
            public RewardsData ExtraRewards;

            /// <summary>
            ///     构造容器，初始化三组奖励数据。
            /// </summary>
            [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 48 8D BE")]
            public partial RewardsContainer* Ctor();

            /// <summary>
            ///     释放三组奖励数据及其道具实例。
            /// </summary>
            [MemberFunction
            (
                "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? BF ?? ?? ?? ?? 48 8D 99 ?? ?? ?? ?? 8B F7 48 8D AB ?? ?? ?? ?? 4C 8B F1 48 83 ED ?? 48 8B CD E8 ?? ?? ?? ?? 48 83 EE ?? 75 ?? 48 8B F7 48 8D AB ?? ?? ?? ?? 66 90 48 83 ED ?? 48 8B CD E8 ?? ?? ?? ?? 48 83 EE ?? 75 ?? BE ?? ?? ?? ?? 49 8D AE ?? ?? ?? ?? 66 90 48 83 ED ?? 48 8B CD E8 ?? ?? ?? ?? 48 83 EE ?? 75 ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B 5C 24"
            )]
            public partial void PrepareDestructor();

            /// <summary>
            ///     清空三组奖励数据。
            /// </summary>
            [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B E9 E8 ?? ?? ?? ?? 48 8D 9D")]
            public partial void Clear();

            /// <summary>
            ///     从另一个容器复制三组奖励数据。
            /// </summary>
            /// <param name="source">来源容器。</param>
            [MemberFunction("4C 8B C2 48 8D 41")]
            public partial void CopyFrom
            (
                RewardsContainer* source
            );
        }

        /// <summary>
        ///     单组奖励数据，包含道具列表与各类货币数值。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 544)]
        public unsafe partial struct RewardsData
        {
            /// <summary>
            ///     奖励道具实例，共 7 项。
            /// </summary>
            [FieldOffset(0)]
            [FixedSizeArray]
            internal FixedSizeArray7<InventoryItem> ItemsStorage;

            /// <summary>
            ///     经验值。
            /// </summary>
            [FieldOffset(504)]
            public int Experience;

            /// <summary>
            ///     金币数量。
            /// </summary>
            [FieldOffset(508)]
            public int Gil;

            /// <summary>
            ///     军票数量。
            /// </summary>
            [FieldOffset(512)]
            public int GrandCompanySeals;

            /// <summary>
            ///     第一类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(516)]
            public int CurrencyA;

            /// <summary>
            ///     第二类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(520)]
            public int CurrencyB;

            /// <summary>
            ///     第三类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(524)]
            public int CurrencyC;

            /// <summary>
            ///     对战场次经验值。
            /// </summary>
            [FieldOffset(528)]
            public int PvPExperience;

            /// <summary>
            ///     系列赛经验值。
            /// </summary>
            [FieldOffset(532)]
            public int SeriesExperience;

            /// <summary>
            ///     战狼印记数量。
            /// </summary>
            [FieldOffset(536)]
            public int WolfMarks;

            /// <summary>
            ///     存在限时奖励加成。
            /// </summary>
            [FieldOffset(540)]
            public bool LimitedTimeBonus;

            /// <summary>
            ///     构造奖励数据并清空全部字段。
            /// </summary>
            [MemberFunction
            (
                "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B F9 BE ?? ?? ?? ?? 66 0F 1F 44 00 ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 C7 ?? 48 83 EE ?? 75 ?? 48 8B 74 24 ?? 33 C0 48 89 83"
            )]
            public partial RewardsData* Ctor();

            /// <summary>
            ///     清空全部奖励道具与货币数值。
            /// </summary>
            [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B F9")]
            public partial void Clear();

            /// <summary>
            ///     从另一个奖励数据复制全部字段。
            /// </summary>
            /// <param name="source">来源奖励数据。</param>
            [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 4C 8B FA 4C 8B F2")]
            public partial void CopyFrom
            (
                RewardsData* source
            );
        }
    }

    /// <summary>
    ///     任务搜索器的奖励预览数据，保存普通奖励、加成奖励与逐条奖励显示项。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 5576)]
    public unsafe partial struct AgentContentsFinderReward
    {
        /// <summary>
        ///     内容本身的奖励数据。
        /// </summary>
        [FieldOffset(0)]
        public RewardWrap NormalItems;

        /// <summary>
        ///     内容奖励对应的道具实例，共 5 项。
        /// </summary>
        [FieldOffset(544)]
        [FixedSizeArray]
        internal FixedSizeArray5<InventoryItem> ItemsStorage;

        /// <summary>
        ///     随机任务按角色定位发放的加成奖励数据。
        /// </summary>
        [FieldOffset(904)]
        public RewardWrap BonusItems;

        /// <summary>
        ///     内容额外的奖励数据。
        /// </summary>
        [FieldOffset(1448)]
        public RewardWrap ExtraItems;

        /// <summary>
        ///     道具表加载完成后的回调处理器。
        /// </summary>
        [FieldOffset(1992)]
        public AtkModuleInterface.AtkEventInterface* EventHandler;

        /// <summary>
        ///     Item 数据表。
        /// </summary>
        [FieldOffset(2000)]
        public ExcelSheet* ItemSheet;

        /// <summary>
        ///     Item 数据表的异步加载器。
        /// </summary>
        [FieldOffset(2008)]
        public ExcelSheetWaiter* ItemSheetWaiter;

        /// <summary>
        ///     奖励数据已就绪，无需等待道具表加载。
        /// </summary>
        [FieldOffset(2016)]
        public bool IsReady;

        /// <summary>
        ///     奖励数据复制时传入的事件类型，加载完成后回传给处理器。
        /// </summary>
        [FieldOffset(2024)]
        public ulong EventKind;

        /// <summary>
        ///     普通奖励的显示项，共 7 项。
        /// </summary>
        [FieldOffset(2032)]
        [FixedSizeArray]
        internal FixedSizeArray7<ItemWrap> NormalRewardsStorage;

        /// <summary>
        ///     额外奖励的显示项，共 5 项。
        /// </summary>
        [FieldOffset(3432)]
        [FixedSizeArray]
        internal FixedSizeArray5<ItemWrap> ExtraRewardsStorage;

        /// <summary>
        ///     加成奖励的显示项，共 5 项。
        /// </summary>
        [FieldOffset(4432)]
        [FixedSizeArray]
        internal FixedSizeArray5<ItemWrap> BonusRewardsStorage;

        /// <summary>
        ///     奖励显示项对应的道具实例指针，共 16 项。
        /// </summary>
        [FieldOffset(5432)]
        [FixedSizeArray]
        internal FixedSizeArray16<Pointer<InventoryItem>> ItemRewardsStorage;

        /// <summary>
        ///     保留字段，构造与清理时清零，当前版本无其他读写。
        /// </summary>
        [FieldOffset(5560)]
        public ulong Reserved;

        /// <summary>
        ///     奖励显示项数量，不含经验值等纯数值项。
        /// </summary>
        [FieldOffset(5568)]
        public uint ItemRewardCount;

        /// <summary>
        ///     构造奖励预览，初始化三组奖励数据与全部显示项。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 4C 8D B7")]
        public partial AgentContentsFinderReward* Ctor();

        /// <summary>
        ///     释放道具表加载器与全部显示项。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B E9 48 8B 89")]
        public partial void PrepareDestructor();

        /// <summary>
        ///     清空全部显示项与奖励数值，并注销道具表加载器。
        /// </summary>
        [MemberFunction
        (
            "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B F9 48 8D 99 ?? ?? ?? ?? BE ?? ?? ?? ?? 45 33 F6 0F 1F 40 ?? 48 8B 43"
        )]
        public partial void Clear();

        /// <summary>
        ///     单组奖励数据，包含道具列表与各类货币数值。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 544)]
        public unsafe partial struct RewardWrap
        {
            /// <summary>
            ///     奖励道具实例，共 7 项。
            /// </summary>
            [FieldOffset(0)]
            [FixedSizeArray]
            internal FixedSizeArray7<InventoryItem> ItemsStorage;

            /// <summary>
            ///     经验值。
            /// </summary>
            [FieldOffset(504)]
            public int Experience;

            /// <summary>
            ///     金币数量。
            /// </summary>
            [FieldOffset(508)]
            public int Gil;

            /// <summary>
            ///     军票数量。
            /// </summary>
            [FieldOffset(512)]
            public int GrandCompanySeals;

            /// <summary>
            ///     第一类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(516)]
            public int CurrencyA;

            /// <summary>
            ///     第二类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(520)]
            public int CurrencyB;

            /// <summary>
            ///     第三类亚拉戈神典石数量。
            /// </summary>
            [FieldOffset(524)]
            public int CurrencyC;

            /// <summary>
            ///     对战场次经验值。
            /// </summary>
            [FieldOffset(528)]
            public int PvPExperience;

            /// <summary>
            ///     系列赛经验值。
            /// </summary>
            [FieldOffset(532)]
            public int SeriesExperience;

            /// <summary>
            ///     战狼印记数量。
            /// </summary>
            [FieldOffset(536)]
            public int WolfMarks;

            /// <summary>
            ///     存在限时奖励加成。
            /// </summary>
            [FieldOffset(540)]
            public bool LimitedTimeBonus;
        }

        /// <summary>
        ///     奖励预览中的单个显示项，可以是道具也可以是纯数值条目。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 200)]
        public unsafe struct ItemWrap
        {
            /// <summary>
            ///     条目对应的道具实例。
            /// </summary>
            [FieldOffset(0)]
            public InventoryItem Item;

            /// <summary>
            ///     条目类型，2 表示纯数值条目，257 表示道具条目。
            /// </summary>
            [FieldOffset(72)]
            public uint ItemType;

            /// <summary>
            ///     条目对应的道具行 id。
            /// </summary>
            [FieldOffset(76)]
            public uint ItemID;

            /// <summary>
            ///     条目显示的数值，纯数值条目为经验值或货币数量，道具条目为堆叠数量。
            /// </summary>
            [FieldOffset(80)]
            public int Value;

            /// <summary>
            ///     条目显示的图标 id。
            /// </summary>
            [FieldOffset(84)]
            public uint IconID;

            /// <summary>
            ///     条目显示的文本，道具条目为道具名。
            /// </summary>
            [FieldOffset(88)]
            public Utf8String String;

            /// <summary>
            ///     条目可通过右键菜单与已装备道具进行属性对比。
            /// </summary>
            [FieldOffset(192)]
            public bool CanCompare;

            /// <summary>
            ///     条目可通过右键菜单试穿。
            /// </summary>
            [FieldOffset(193)]
            public bool CanTryOn;
        }
    }

    /// <summary>
    ///     任务搜索器列表中的内容条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 240)]
    public unsafe struct Contents
    {
        /// <summary>
        ///     列表左侧显示的所需等级文本。
        /// </summary>
        [FieldOffset(0)]
        public Utf8String RequiredLevel;

        /// <summary>
        ///     列表右侧显示的副本名称。
        /// </summary>
        [FieldOffset(104)]
        public Utf8String Name;

        /// <summary>
        ///     内容标识。
        /// </summary>
        [FieldOffset(208)]
        public ContentsID ID;

        /// <summary>
        ///     排序键，非首个标签页时由资料片索引左移 16 位后与内容排序键合并，并可按排序设置取反。
        /// </summary>
        [FieldOffset(216)]
        public int SortKey;

        /// <summary>
        ///     不可参加原因位标志，位 11 表示任务搜索封印，位 4 表示需要显示操作指南。
        /// </summary>
        [FieldOffset(220)]
        public uint UnavailableReasons;

        /// <summary>
        ///     满足条件的招募队伍数量，-1 表示尚未获取，-2 表示无权限查看，大于 99 时按 99 显示。
        /// </summary>
        [FieldOffset(224)]
        public int RecruitingPartyCount;

        /// <summary>
        ///     所属的界面分类索引，对应 ContentUICategory 行 id。
        /// </summary>
        [FieldOffset(228)]
        public byte CategoryIndex;

        /// <summary>
        ///     内容所属资料片索引，对应 ExVersion 行 id。
        /// </summary>
        [FieldOffset(229)]
        public byte ExVersion;

        /// <summary>
        ///     随机任务的角色奖励加成类型。
        /// </summary>
        [FieldOffset(230)]
        public ContentsRouletteRole Role;

        /// <summary>
        ///     所属地域对应的界面索引，由 TerritoryType 查表得到，-1 表示无对应项。
        /// </summary>
        [FieldOffset(231)]
        public sbyte TerritoryTypeIndex;

        /// <summary>
        ///     当前小队人数满足参加要求。
        /// </summary>
        [FieldOffset(232)]
        public bool CanEnterAsParty;

        /// <summary>
        ///     内容可被申请参加。
        /// </summary>
        [FieldOffset(233)]
        public bool IsQueueable;

        /// <summary>
        ///     内容存在品级同步限制。
        /// </summary>
        [FieldOffset(234)]
        public bool HasItemLevelSync;

        /// <summary>
        ///     内容关联的副本存在于奖励加成表，可显示额外奖励。
        /// </summary>
        [FieldOffset(235)]
        public bool HasCSBonus;

        /// <summary>
        ///     内容当前存在限时奖励。
        /// </summary>
        [FieldOffset(236)]
        public bool IsLimitedTimeBonus;

        /// <summary>
        ///     内容包含水晶冲突玩法特性。
        /// </summary>
        [FieldOffset(237)]
        public bool HasCrystallineConflictFeatures;
    }

    /// <summary>
    ///     内容标识，由内容类型与对应数据表的行 id 组成。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct ContentsID
    {
        /// <summary>
        ///     内容类型。
        /// </summary>
        [FieldOffset(0)]
        public ContentsType ContentType;

        /// <summary>
        ///     内容行 id，随机任务指向 ContentRoulette，普通任务指向 ContentFinderCondition。
        /// </summary>
        [FieldOffset(4)]
        public uint ID;
    }
}
