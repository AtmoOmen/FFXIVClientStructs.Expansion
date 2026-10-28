using FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     打字游戏的 HUD 代理（AgentKTGHud）。<br />
///     代理负责在副本中打开并驱动全部 KTG 子窗口，维护敌人词条集合与四名玩家的名字条，
///     把词条数据写入数值数组 0xA1 与字符串数组 0x80，并把副本控制器与本地玩家的状态分发给各窗口。
/// </summary>
[Agent(AgentId.KTGHud)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 752)]
public unsafe partial struct AgentKTGHud
{
    /// <summary>
    ///     以敌人实体 id 为键的词条集合，驱动血条、法条与词条三个窗口的数据来源。
    /// </summary>
    [FieldOffset(40)]
    public StdMap<uint, EntityEntry> Entities;

    /// <summary>
    ///     四名队员的名字条数据。
    /// </summary>
    [FieldOffset(56)]
    [FixedSizeArray]
    internal FixedSizeArray4<PlayerNameEntry> PlayerNamesStorage;

    /// <summary>
    ///     每帧从副本控制器读取的第 1 个数值。
    /// </summary>
    [FieldOffset(600)]
    public uint DirectorValue0;

    /// <summary>
    ///     每帧从副本控制器读取的第 2 个数值。
    /// </summary>
    [FieldOffset(604)]
    public uint DirectorValue1;

    /// <summary>
    ///     每帧从副本控制器读取的第 3 个数值，作为得分窗口的总分。
    /// </summary>
    [FieldOffset(608)]
    public uint Score;

    /// <summary>
    ///     每帧从副本控制器读取的第 4 个数值，作为待办窗口的进度参数。
    /// </summary>
    [FieldOffset(612)]
    public uint TodoValue;

    /// <summary>
    ///     每帧从副本控制器读取的第 5 个数值，作为得分窗口的阶段参数。
    /// </summary>
    [FieldOffset(616)]
    public uint ScoreStage;

    /// <summary>
    ///     额外奖励剩余时间，毫秒。
    /// </summary>
    [FieldOffset(620)]
    public uint ScoreBonusTime;

    /// <summary>
    ///     副本控制器的关卡状态，取值 0 至 6，-1 表示尚未同步。
    /// </summary>
    [FieldOffset(624)]
    public int Stage;

    /// <summary>
    ///     KTGEWord 窗口 id。
    /// </summary>
    [FieldOffset(628)]
    public uint EWordAddonID;

    /// <summary>
    ///     KTGIGaugeHp 窗口 id。
    /// </summary>
    [FieldOffset(632)]
    public uint IGaugeHpAddonID;

    /// <summary>
    ///     KTGEnemyCastBar 窗口 id。
    /// </summary>
    [FieldOffset(636)]
    public uint EnemyCastBarAddonID;

    /// <summary>
    ///     KTGInput 窗口 id。
    /// </summary>
    [FieldOffset(640)]
    public uint InputAddonID;

    /// <summary>
    ///     KTGHudGaugeHp 窗口 id。
    /// </summary>
    [FieldOffset(644)]
    public uint HudGaugeHpAddonID;

    /// <summary>
    ///     KTGHudLog 窗口 id。
    /// </summary>
    [FieldOffset(648)]
    public uint HudLogAddonID;

    /// <summary>
    ///     KTGHudScore 窗口 id。
    /// </summary>
    [FieldOffset(652)]
    public uint HudScoreAddonID;

    /// <summary>
    ///     KTGHudTodo 窗口 id。
    /// </summary>
    [FieldOffset(656)]
    public uint HudTodoAddonID;

    /// <summary>
    ///     KTGPlayerName 窗口 id。
    /// </summary>
    [FieldOffset(660)]
    public uint PlayerNameAddonID;

    /// <summary>
    ///     KTGPlayGuide 窗口 id。
    /// </summary>
    [FieldOffset(664)]
    public uint PlayGuideAddonID;

    /// <summary>
    ///     KTGScreenTextFever 窗口 id。
    /// </summary>
    [FieldOffset(668)]
    public uint ScreenTextFeverAddonID;

    /// <summary>
    ///     本地玩家当前是否处于爆发状态，用于在状态变化时刷新爆发文字窗口。
    /// </summary>
    [FieldOffset(672)]
    public bool IsFever;

    /// <summary>
    ///     代理是否处于显示状态。
    /// </summary>
    [FieldOffset(673)]
    public bool IsShown;

    /// <summary>
    ///     当前是否处于副本中，即是否存在副本控制器。
    /// </summary>
    [FieldOffset(674)]
    public bool HasDirector;

    /// <summary>
    ///     是否已经把词条窗口的参数（界面值 1028 至 1031）下发过一次。
    /// </summary>
    [FieldOffset(675)]
    public bool EWordParamsSent;

    /// <summary>
    ///     是否已经把缩放与高度参数（界面值 1034 至 1038）读取并应用过一次。
    /// </summary>
    [FieldOffset(676)]
    public bool ScaleParamsSent;

    /// <summary>
    ///     是否已经隐藏常规界面。
    /// </summary>
    [FieldOffset(677)]
    public bool HudHidden;

    /// <summary>
    ///     是否已经隐藏聊天窗口。
    /// </summary>
    [FieldOffset(678)]
    public bool ChatLogHidden;

    /// <summary>
    ///     是否已经接管目标系统。
    /// </summary>
    [FieldOffset(679)]
    public bool TargetSystemTakenOver;

    /// <summary>
    ///     是否已经计算界面缩放与屏幕偏移。
    /// </summary>
    [FieldOffset(680)]
    public bool LayoutInitialized;

    /// <summary>
    ///     代理显示以来的累计秒数，每帧累加帧间隔，并作为新词条的时间戳。
    /// </summary>
    [FieldOffset(684)]
    public float TotalTime;

    /// <summary>
    ///     界面缩放系数，按屏幕高度除以 1080 得到。
    /// </summary>
    [FieldOffset(688)]
    public float UIScale;

    /// <summary>
    ///     界面在屏幕上的横向偏移。
    /// </summary>
    [FieldOffset(692)]
    public short ScreenOffsetX;

    /// <summary>
    ///     界面在屏幕上的纵向偏移。
    /// </summary>
    [FieldOffset(694)]
    public short ScreenOffsetY;

    /// <summary>
    ///     显示时写入的保留值，与全局计数变量交换得到。
    /// </summary>
    [FieldOffset(696)]
    public uint Unk696;

    /// <summary>
    ///     构造时清零且未在客户端代码中读写的保留指针。
    /// </summary>
    [FieldOffset(704)]
    public void* Unk704;

    /// <summary>
    ///     词条窗口文本的主缩放系数，由界面值 1034 除以 1000 得到。
    /// </summary>
    [FieldOffset(712)]
    public float EWordScale;

    /// <summary>
    ///     词条窗口文本的次缩放系数，由界面值 1035 除以 1000 得到。
    /// </summary>
    [FieldOffset(716)]
    public float EWordSecondaryScale;

    /// <summary>
    ///     血条窗口的缩放系数，由界面值 1036 除以 1000 得到。
    /// </summary>
    [FieldOffset(720)]
    public float IGaugeHpScale;

    /// <summary>
    ///     敌方法条窗口的缩放系数，由界面值 1037 除以 1000 得到。
    /// </summary>
    [FieldOffset(724)]
    public float EnemyCastBarScale;

    /// <summary>
    ///     是否按屏幕高度自动计算界面缩放。
    /// </summary>
    [FieldOffset(728)]
    public bool AutoScale;

    /// <summary>
    ///     目标投影时使用的高度偏移，由界面值 1038 减去 100000 再除以 10000 得到。
    /// </summary>
    [FieldOffset(732)]
    public float HeightOffset;

    /// <summary>
    ///     构造时清零且未在客户端代码中读写的保留值。
    /// </summary>
    [FieldOffset(736)]
    public ulong Unk736;

    /// <summary>
    ///     当前跟踪的敌人实体 id，0 表示尚未获取；优先跟踪基础 id 为 20146 的敌人。
    /// </summary>
    [FieldOffset(740)]
    public uint TrackedEntityID;

    /// <summary>
    ///     爆发状态（状态 0x14E3）在状态管理器中的索引缓存，-1 表示尚未查询或状态不存在。
    /// </summary>
    [FieldOffset(744)]
    public int FeverStatusIndex;

    /// <summary>
    ///     构造代理并初始化词条集合、四名队员的名字条与全部数值字段。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction
        ("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 33 ED 48 8D 05 ?? ?? ?? ?? 48 89 03 B9 ?? ?? ?? ?? 48 89 6B")]
    public partial AgentKTGHud* Ctor();

    /// <summary>
    ///     收集副本内全部可选中敌人的词条并写入集合，同时把实体 id 汇总到 <paramref name="visibleEntities"/>。
    /// </summary>
    /// <param name="director">当前副本控制器。</param>
    /// <param name="visibleEntities">本次采样到的实体 id 集合。</param>
    [MemberFunction("4C 8B DC 55 53 56 41 54 41 55 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05")]
    public partial void CollectEntities
    (
        InstanceContentDirector* director,
        StdSet<int>*             visibleEntities
    );

    /// <summary>
    ///     删除本次采样中不再出现的词条，并销毁其文本资源。
    /// </summary>
    /// <param name="visibleEntities">本次采样到的实体 id 集合。</param>
    /// <param name="unknown">保留参数，由调用方传入 0。</param>
    [MemberFunction("48 89 74 24 ?? 41 56 48 83 EC ?? 48 8D 71")]
    public partial void PruneEntities
    (
        StdSet<int>* visibleEntities,
        nint         unknown
    );

    /// <summary>
    ///     隐藏常规界面中与打字游戏冲突的窗口，并隐藏 Hud 代理的界面。
    /// </summary>
    [MemberFunction
    (
        "40 55 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B E9 0F 85 ?? ?? ?? ?? 48 8B 49 ?? 48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 48 8B 01 FF 50 ?? 48 8D 1D ?? ?? ?? ?? BE ?? ?? ?? ?? 48 8B F8 66 90 8B 4B ?? 85 C9 74 ?? 83 E9 ?? 74 ?? 83 E9 ?? 74 ?? 83 F9 ?? 75 ?? 8B 13 44 0F B6 C1 48 8B CF E8 ?? ?? ?? ?? EB ?? 8B 13 45 33 C0 48 8B CF E8 ?? ?? ?? ?? EB ?? 8B 13 48 8B CF E8 ?? ?? ?? ?? 48 83 C3 ?? 48 83 EE ?? 75 ?? 8D 56 ?? 48 8B CD E8 ?? ?? ?? ?? 48 8B 74 24"
    )]
    public partial void HideHudAddons();

    /// <summary>
    ///     隐藏聊天窗口并记录已经隐藏。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? B2")]
    public partial void HideChatLog();

    /// <summary>
    ///     隐藏推荐列表、任务通知与玩法指南三个代理。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? BA ?? ?? ?? ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 74 ?? 48 8B 10 48 8B C8 FF 52 ?? 84 C0 74 ?? 48 8B 13 48 8B CB FF 52 ?? BA"
    )]
    public partial void HideOverlayAgents();

    /// <summary>
    ///     按屏幕尺寸计算界面缩放与偏移，并发送一次基础界面值。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 0F 85 ?? ?? ?? ?? C6 81 ?? ?? ?? ?? ?? 48 8B 49 ?? 48 89 7C 24")]
    public partial void InitializeLayout();

    /// <summary>
    ///     更新输入窗口与目标跟踪：优先跟踪基础 id 为 20146 的敌人，并刷新输入窗口坐标。
    /// </summary>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 0F 84 ?? ?? ?? ?? 48 8B 49")]
    public partial void UpdateInputTracking();

    /// <summary>
    ///     复位指定词条：写入新的时间戳、把文本置空、清空血量与读条状态，并通知血条窗口。
    /// </summary>
    /// <param name="entry">词条。</param>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="resetHealth">是否把血量百分比清零。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 8B 81 ?? ?? ?? ?? 33 ED 89 82")]
    public partial void ResetEntity
    (
        EntityEntry* entry,
        uint         entityID,
        bool         resetHealth
    );

    /// <summary>
    ///     按敌人基础 id 查询其投影高度偏移，取副本控制器参数值的十分之一。
    /// </summary>
    /// <param name="entityID">敌人实体 id。</param>
    /// <returns>高度偏移。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 8B FA E8 ?? ?? ?? ?? 48 8B C8")]
    public partial float GetEntityHeightOffset
    (
        uint entityID
    );

    /// <summary>
    ///     把词条的实体投影到屏幕坐标，写入横纵坐标与绘制优先级；投影失败时把词条标记为不可见。
    /// </summary>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="entry">词条。</param>
    /// <param name="heightOffset">投影高度偏移。</param>
    /// <returns>是否投影成功。</returns>
    [MemberFunction("40 53 48 83 EC ?? F3 0F 11 5C 24")]
    public partial bool ProjectEntity
    (
        uint         entityID,
        EntityEntry* entry,
        float        heightOffset
    );

    /// <summary>
    ///     遍历词条集合，按实体是否可选中更新其屏幕坐标与可见标志。
    /// </summary>
    [MemberFunction("40 53 41 56 41 57 48 83 EC ?? 4C 8B F9 48 8D 0D")]
    public partial void UpdateEntityPositions();

    /// <summary>
    ///     遍历词条集合，按敌人的读条状态更新技能名、读条进度、重要施法与状态参数。
    /// </summary>
    [MemberFunction("48 8B C4 53 48 83 EC ?? 48 8B 59")]
    public partial void UpdateEntityCastState();

    /// <summary>
    ///     把世界坐标投影为屏幕坐标，可指定高度取样点与额外高度偏移。
    /// </summary>
    /// <param name="entityID">实体 id。</param>
    /// <param name="outX">输出：屏幕横坐标。</param>
    /// <param name="outY">输出：屏幕纵坐标。</param>
    /// <param name="heightType">高度取样点，-1 表示直接取实体坐标。</param>
    /// <param name="outPriority">输出：屏幕深度，用作绘制优先级。</param>
    /// <param name="heightOffset">额外高度偏移。</param>
    /// <returns>是否投影成功。</returns>
    [MemberFunction("40 53 55 56 57 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 8B B4 24")]
    public partial bool ProjectWorldToScreen
    (
        uint   entityID,
        float* outX,
        float* outY,
        uint   heightType,
        float* outPriority,
        float  heightOffset
    );

    /// <summary>
    ///     把当前词条集合写入数值数组 0xA1 与字符串数组 0x80，并清空多余槽位。
    /// </summary>
    [MemberFunction("40 55 41 55 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 4C 8B E9 48 8B 49")]
    public partial void UpdateArrays();

    /// <summary>
    ///     打开全部 KTG 子窗口并记录窗口 id。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B F1 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 BA"
    )]
    public partial void OpenAddons();

    /// <summary>
    ///     重置代理状态：清空词条集合、关闭全部子窗口并复位关卡状态。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 59 ?? 48 8D 51")]
    public partial void Reset();

    /// <summary>
    ///     查询指定窗口是否已经加载完成。
    /// </summary>
    /// <param name="addonID">窗口 id。</param>
    /// <returns>窗口为空或已加载完成时返回真。</returns>
    [MemberFunction("40 53 48 83 EC ?? 8B DA 85 D2 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B D3 48 8B 08 4C 8B 81 ?? ?? ?? ?? 48 8B C8 41 FF D0")]
    public partial bool CheckAddonReady
    (
        uint addonID
    );

    /// <summary>
    ///     按当前队伍成员填充四名队员的名字条数据，名字附带在线状态图标文本。
    /// </summary>
    [MemberFunction("40 53 56 57 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1")]
    public partial void UpdatePlayerNames();

    /// <summary>
    ///     把四名队员的名字条坐标与显示标志下发给名字条窗口。
    /// </summary>
    [MemberFunction("40 55 53 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B D9")]
    public partial void UpdatePlayerNameAddon();

    /// <summary>
    ///     隐藏名字条窗口中指定槽位。
    /// </summary>
    /// <param name="index">槽位索引，取值 0 至 3。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 8B F2 48 8B F9 0F 84")]
    public partial void HidePlayerNameAddon
    (
        uint index
    );

    /// <summary>
    ///     把输入窗口摆放到指定坐标与缩放。
    /// </summary>
    /// <param name="x">屏幕横坐标。</param>
    /// <param name="y">屏幕纵坐标。</param>
    /// <param name="scale">界面缩放。</param>
    [MemberFunction("48 8B C4 48 89 58 ?? 57 48 81 EC ?? ?? ?? ?? 0F 29 70 ?? 0F 29 78 ?? 44 0F 29 40 ?? 48 8B 05")]
    public partial void LayoutInputAddon
    (
        float x,
        float y,
        float scale
    );

    /// <summary>
    ///     按屏幕尺寸为血条、日志、待办与指南等子窗口下发布局参数。
    /// </summary>
    [MemberFunction("48 8B C4 48 89 70 ?? 48 89 78 ?? 55 48 8D 68 ?? 48 81 EC ?? ?? ?? ?? 0F 29 70 ?? 0F 29 78")]
    public partial void LayoutSubAddons();

    /// <summary>
    ///     通过界面值 3 把子窗口摆放到指定坐标与缩放。
    /// </summary>
    /// <param name="addonID">子窗口 id。</param>
    /// <param name="offsetX">横坐标偏移。</param>
    /// <param name="offsetY">纵坐标偏移。</param>
    /// <param name="originX">原点横坐标。</param>
    /// <param name="originY">原点纵坐标。</param>
    /// <param name="scale">界面缩放。</param>
    [MemberFunction("85 D2 0F 84 ?? ?? ?? ?? 48 8B C4 48 89 58 ?? 57 48 81 EC ?? ?? ?? ?? 0F 29 70 ?? 0F 29 78")]
    public partial void LayoutAddon
    (
        uint  addonID,
        float offsetX,
        float offsetY,
        float originX,
        float originY,
        float scale
    );

    /// <summary>
    ///     把总分、阶段进度、奖励倍率与爆发状态下发给得分窗口。
    /// </summary>
    [MemberFunction("40 55 41 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 83 B9 ?? ?? ?? ?? ?? 4C 8B F1 0F 84")]
    public partial void UpdateScoreAddon();

    /// <summary>
    ///     把目标类型与进度下发给待办窗口。
    /// </summary>
    [MemberFunction("40 55 41 55 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 83 B9")]
    public partial void UpdateTodoAddon();

    /// <summary>
    ///     检查血条、日志、得分、待办、名字条、指南与爆发文字窗口是否全部就绪，并同步关卡状态。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 8B B9 ?? ?? ?? ?? 48 8B D9 85 FF 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B D7 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? 84 C0 0F 84 ?? ?? ?? ?? 8B BB ?? ?? ?? ?? 85 FF 74 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B D7 48 8B 08 4C 8B 81 ?? ?? ?? ?? 48 8B C8 41 FF D0 84 C0 0F 84 ?? ?? ?? ?? 8B BB ?? ?? ?? ?? 85 FF 74 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B D7 48 8B 08 4C 8B 81 ?? ?? ?? ?? 48 8B C8 41 FF D0 84 C0 0F 84 ?? ?? ?? ?? 8B BB ?? ?? ?? ?? 85 FF 74 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B D7 48 8B 08 4C 8B 81 ?? ?? ?? ?? 48 8B C8 41 FF D0 84 C0 0F 84"
    )]
    public partial void CheckAddonsReady();

    /// <summary>
    ///     按关卡配置的可见性标志显示或隐藏各子窗口。
    /// </summary>
    /// <param name="flags">八个可见性标志，依次对应血条、得分、待办、日志、词条类窗口、输入、名字条与指南。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 55 57 41 54 41 55 41 57 48 8B EC")]
    public partial void ApplyVisibilityFlags
    (
        byte* flags
    );

    /// <summary>
    ///     把界面缩放与三档窗口缩放系数分别下发给词条、血条与敌方法条窗口。
    /// </summary>
    [MemberFunction("48 8B C4 48 89 58 ?? 48 89 70 ?? 48 89 78 ?? 55 41 56 41 57 48 8D 68 ?? 48 81 EC ?? ?? ?? ?? 0F 29 70 ?? 0F 29 78 ?? 48 8B 05")]
    public partial void UpdateAddonScales();

    /// <summary>
    ///     从副本控制器读取并下发一次词条窗口的闪烁区间与各窗口缩放参数。
    /// </summary>
    /// <param name="director">当前副本控制器。</param>
    [MemberFunction("40 55 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 80 B9")]
    public partial void InitializeAddonParams
    (
        InstanceContentDirector* director
    );

    /// <summary>
    ///     递归销毁词条集合节点及其三个文本资源。
    /// </summary>
    /// <param name="unknown">保留参数，由调用方传入 0。</param>
    /// <param name="node">集合节点。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 56 48 83 EC ?? 41 80 78 ?? ?? 49 8B D8 48 8B F2 48 8B E9 75 ?? 48 89 7C 24 ?? 0F 1F 40 ?? 0F 1F 84 00 ?? ?? ?? ?? 4C 8B 43 ?? 48 8B D6 48 8B CD E8 ?? ?? ?? ?? 48 8B FB 48 8B 1B 48 8D 8F ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 8F"
    )]
    public partial void DestroyEntities
    (
        nint  unknown,
        void* node
    );

    /// <summary>
    ///     单个敌人词条，也被写入数值数组 0xA1 与字符串数组 0x80 供各窗口读取。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 376)]
    public unsafe struct EntityEntry
    {
        /// <summary>
        ///     敌人实体 id，与集合的键相同，同时作为数值数组每项的第 0 个整数。
        /// </summary>
        [FieldOffset(0)]
        public uint EntityID;

        /// <summary>
        ///     词条类型，按敌人基础 id 映射为 0 至 4，同时作为数值数组每项的第 10 个整数。
        /// </summary>
        [FieldOffset(4)]
        public uint Type;

        /// <summary>
        ///     第一行文本，即敌人名，写入字符串数组的第 3i 项。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String Name;

        /// <summary>
        ///     第二行文本，即敌人称号，写入字符串数组的第 3i+1 项。
        /// </summary>
        [FieldOffset(112)]
        public Utf8String Title;

        /// <summary>
        ///     文本内容已变化，需要在本次刷新中重新写入字符串数组。
        /// </summary>
        [FieldOffset(216)]
        public bool TextDirty;

        /// <summary>
        ///     屏幕横坐标，作为数值数组每项的第 1 个整数下发。
        /// </summary>
        [FieldOffset(220)]
        public float X;

        /// <summary>
        ///     屏幕纵坐标，作为数值数组每项的第 2 个整数下发。
        /// </summary>
        [FieldOffset(224)]
        public float Y;

        /// <summary>
        ///     绘制优先级，作为数值数组每项的第 3 个整数按位下发。
        /// </summary>
        [FieldOffset(228)]
        public float Priority;

        /// <summary>
        ///     缩放百分比，作为数值数组每项的第 9 个整数下发。
        /// </summary>
        [FieldOffset(232)]
        public int ScalePercent;

        /// <summary>
        ///     词条是否有效可见，作为数值数组每项的第 4 个整数下发。
        /// </summary>
        [FieldOffset(236)]
        public bool IsVisible;

        /// <summary>
        ///     写入数值数组第 6 个整数但三个窗口均未读取的保留值。
        /// </summary>
        [FieldOffset(240)]
        public int Unk240;

        /// <summary>
        ///     血量百分比，作为数值数组每项的第 5 个整数下发。
        /// </summary>
        [FieldOffset(244)]
        public float HealthPercent;

        /// <summary>
        ///     读条进度，取值 0 至 1，乘以 100 后作为数值数组每项的第 7 个整数下发。
        /// </summary>
        [FieldOffset(248)]
        public float CastProgress;

        /// <summary>
        ///     当前读条技能名，写入字符串数组的第 3i+2 项。
        /// </summary>
        [FieldOffset(256)]
        public Utf8String CastActionName;

        /// <summary>
        ///     读条是否为需要躲避的重要技能，作为数值数组每项的第 8 个整数下发。
        /// </summary>
        [FieldOffset(360)]
        public bool IsImportantCast;

        /// <summary>
        ///     读条状态参数，作为数值数组每项的第 11 个整数下发。
        /// </summary>
        [FieldOffset(362)]
        public short CastParam;

        /// <summary>
        ///     词条的时间戳，取 <see cref="AgentKTGHud.TotalTime"/>，负值表示不计时。
        /// </summary>
        [FieldOffset(364)]
        public float Timestamp;

        /// <summary>
        ///     词条是否已经处理完毕。
        /// </summary>
        [FieldOffset(368)]
        public bool Disposed;
    }

    /// <summary>
    ///     单个队员的名字条数据。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 136)]
    public unsafe struct PlayerNameEntry
    {
        /// <summary>
        ///     队员实体 id，0 表示槽位空闲。
        /// </summary>
        [FieldOffset(0)]
        public int EntityID;

        /// <summary>
        ///     队员名，附带在线状态图标文本。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String Name;

        /// <summary>
        ///     名字条投影后的屏幕横坐标，同时作为左右错开的排序依据。
        /// </summary>
        [FieldOffset(112)]
        public float X;

        /// <summary>
        ///     名字条投影后的屏幕纵坐标。
        /// </summary>
        [FieldOffset(116)]
        public float Y;

        /// <summary>
        ///     名字条投影后的屏幕深度，作为绘制优先级。
        /// </summary>
        [FieldOffset(120)]
        public float Priority;

        /// <summary>
        ///     名字条是否已经显示。
        /// </summary>
        [FieldOffset(124)]
        public bool IsShown;

        /// <summary>
        ///     该队员是否为本地玩家。
        /// </summary>
        [FieldOffset(125)]
        public bool IsLocalPlayer;

        /// <summary>
        ///     在队伍中的序号，-1 表示不在队伍中。
        /// </summary>
        [FieldOffset(128)]
        public int PartyIndex;
    }
}
