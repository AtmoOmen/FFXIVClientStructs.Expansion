using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏中的敌人词条窗口（KTGEWord）。<br />
///     窗口持有二十个词条槽位，每个槽位是一个复制出来的组件节点，显示某个敌人对应的名字与称号，
///     并按数值数组下发的坐标、缩放、状态播放对应的动画。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGEWord")]
[StructLayout(LayoutKind.Explicit, Size = 4640)]
public unsafe partial struct AddonKTGEWord
{
    /// <summary>
    ///     二十个词条槽位，每个槽位 200 字节。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray20<WordEntry> WordEntriesStorage;

    /// <summary>
    ///     词条内容节点的基准宽度，取自第 0 个槽位内容节点（节点 4）的宽度。
    /// </summary>
    [FieldOffset(4568)]
    public float BaseWidth;

    /// <summary>
    ///     词条内容节点的基准高度，取自第 0 个槽位内容节点（节点 4）的高度。
    /// </summary>
    [FieldOffset(4572)]
    public float BaseHeight;

    /// <summary>
    ///     词条整体布局的缩放系数，默认 1。
    /// </summary>
    [FieldOffset(4576)]
    public float LayoutScale;

    /// <summary>
    ///     词条内容节点相对名字文本节点的宽度余量，初始化时等于基准宽度减去名字文本宽度，无名字文本节点时为 64。
    /// </summary>
    [FieldOffset(4580)]
    public float WidthPadding;

    /// <summary>
    ///     第 0 个槽位装饰图片容器节点（节点 0x0A）的宽度。
    /// </summary>
    [FieldOffset(4584)]
    public float FrameContainerWidth;

    /// <summary>
    ///     词条带第二行文本时使用的文本缩放系数，默认 0.85，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4588)]
    public float TextScaleWithTitle;

    /// <summary>
    ///     词条不带第二行文本时使用的文本缩放系数，默认 0.85，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4592)]
    public float TextScaleWithoutTitle;

    /// <summary>
    ///     词条文本的额外缩放系数，默认 1，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4596)]
    public float TextScaleMultiplier;

    /// <summary>
    ///     词条首次出现时闪烁间隔的下限秒数，默认 0.1，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4600)]
    public float InitialBlinkIntervalMin;

    /// <summary>
    ///     词条首次出现时闪烁间隔的上限秒数，默认 0.5，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4604)]
    public float InitialBlinkIntervalMax;

    /// <summary>
    ///     词条重复闪烁间隔的下限秒数，默认 0.2，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4608)]
    public float RepeatBlinkIntervalMin;

    /// <summary>
    ///     词条重复闪烁间隔的上限秒数，默认 0.8，可由刷新参数改写。
    /// </summary>
    [FieldOffset(4612)]
    public float RepeatBlinkIntervalMax;

    /// <summary>
    ///     当前订阅的数值数组（索引 0xA1），每帧由请求更新回调缓存。
    /// </summary>
    [FieldOffset(4616)]
    public NumberArrayData* NumberArray;

    /// <summary>
    ///     已处理过的敌人实体 id 集合，用于阻止同一个敌人对应的词条重复出现。
    /// </summary>
    [FieldOffset(4624)]
    public StdSet<int> HandledEntityIDs;

    /// <summary>
    ///     构造窗口并把全部槽位与阈值初始化为默认值。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 D2 48 8D 9F ?? ?? ?? ?? 48 89 07 48 8B CB 41 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 81 C3"
    )]
    public partial AddonKTGEWord* Ctor();

    /// <summary>
    ///     按次文本是否存在更新词条时间线：视觉障碍期间只记录待应用状态，否则立即播放时间线 17 或 101。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="hasTitle">次文本是否存在。</param>
    [MemberFunction("4C 8B C9 83 FA ?? 77 ?? 48 63 C2 48 69 D0")]
    public partial void SetTitleState
    (
        uint index,
        bool hasTitle
    );

    /// <summary>
    ///     以指定文本与状态显示词条：设置名字与称号、更新宽度、切换状态并播放出现动画。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="name">名字文本。</param>
    /// <param name="title">称号文本。</param>
    /// <param name="state">内容状态。</param>
    [MemberFunction("40 55 56 57 41 54 48 83 EC ?? 48 63 F2")]
    public partial void ShowWordEntry
    (
        uint           index,
        int            entityID,
        CStringPointer name,
        CStringPointer title,
        uint           state
    );

    /// <summary>
    ///     设置指定槽位节点及其全部子节点的深度排序标志。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="enable">是否启用按深度决定绘制优先级。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 55 48 83 EC ?? 48 63 C2 41 0F B6 E8 48 69 D0")]
    public partial void SetEntryDepthBasedPriority
    (
        uint index,
        bool enable
    );

    /// <summary>
    ///     清空指定词条的状态字段：实体 id 置为无效值，状态、闪烁与时间线字段全部归零。
    /// </summary>
    /// <param name="entry">词条。</param>
    [MemberFunction("33 C0 C7 82 ?? ?? ?? ?? ?? ?? ?? ?? 48 89 82 ?? ?? ?? ?? 66 89 82")]
    public partial void ClearWordEntry
    (
        WordEntry* entry
    );

    /// <summary>
    ///     重新计算词条布局所需的基准宽度、高度、装饰容器宽度与宽度余量。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 89 ?? ?? ?? ?? 48 85 C9 0F 84 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 8B")]
    public partial void UpdateBaseSizes();

    /// <summary>
    ///     按数值数组 0xA1 中缓存的坐标与缩放重新摆放全部词条。
    /// </summary>
    [MemberFunction
    (
        "41 56 48 83 EC ?? 48 8B 81 ?? ?? ?? ?? 4C 8B F1 48 85 C0 0F 84 ?? ?? ?? ?? 48 8B 40 ?? 48 63 10 85 D2 0F 8E ?? ?? ?? ?? 48 89 7C 24 ?? 33 FF 4C 89 7C 24 ?? 4C 8B FA 85 D2 0F 8E ?? ?? ?? ?? 48 89 5C 24 ?? 8D 5F ?? 48 89 6C 24 ?? 48 89 74 24 ?? 33 F6 33 ED 83 FF ?? 0F 8D ?? ?? ?? ?? 49 8B 86 ?? ?? ?? ?? 33 D2 48 8B 48 ?? 8B 44 19 ?? 44 8B 44 29 ?? 44 8B 4C 19 ?? 89 44 24 ?? 49 8D 86 ?? ?? ?? ?? 44 39 00 74 ?? FF C2 48 05"
    )]
    public partial void LayoutAllWordEntries();

    /// <summary>
    ///     推进全部词条的闪烁计时，并在计时结束时在两个随机区间与时间线之间切换。
    /// </summary>
    /// <param name="delta">距上一帧的秒数。</param>
    [MemberFunction("4C 8B DC 57 48 83 EC ?? 8B 81")]
    public partial void UpdateBlinkTimers
    (
        float delta
    );

    /// <summary>
    ///     在词条未被读条状态占用且时间线状态为 104 时复位其时间线。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="unknown">保留参数，必须为 0。</param>
    /// <param name="castProgress">读条进度百分比。</param>
    /// <param name="castParam">读条状态参数。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 63 C2 48 69 D8")]
    public partial void ResetWordIfUnchanged
    (
        uint index,
        int  entityID,
        int  unknown,
        int  castProgress,
        int  castParam
    );

    /// <summary>
    ///     处理刷新命令 100：按第二项切换全部词条的显示与隐藏。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("83 FA ?? 7C ?? 53 48 83 EC ?? 48 8B D9 49 8D 48")]
    public partial void HandleVisibilityCommand
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     处理刷新命令 200：改写带次文本与不带次文本时的文本缩放系数，以及额外缩放系数。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("83 FA ?? 0F 8C ?? ?? ?? ?? 48 89 5C 24 ?? 48 89 74 24")]
    public partial void ApplyTextScaleCommand
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     处理刷新命令 6：改写首次出现与重复闪烁的四个间隔秒数。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("83 FA ?? 0F 8C ?? ?? ?? ?? 48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9")]
    public partial void ApplyBlinkIntervalsCommand
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     处理刷新命令 2 至 5：按第二项给出的实体 id 切换对应词条的状态。
    /// </summary>
    /// <param name="state">命令编号，取值 2 至 5。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 8B FA 49 8D 48")]
    public partial void HandleStateCommand
    (
        int       state,
        AtkValue* values
    );

    /// <summary>
    ///     按视觉障碍状态设置指定词条：进入障碍时启动闪烁计时，退出时恢复时间线并清零计时。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="obstructed">是否处于视觉障碍状态。</param>
    /// <param name="immediate">词条已存在时为真，走延迟应用分支。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 48 89 6C 24 ?? 57")]
    public partial void SetEntryObstructed
    (
        uint index,
        bool obstructed,
        bool immediate
    );

    /// <summary>
    ///     按词条次文本是否存在播放时间线，并复位其状态节点的时间线。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entry">词条。</param>
    [MemberFunction("40 53 48 83 EC ?? 49 8B D8 4C 8B C1 44 0F B6 8B")]
    public partial void ApplyTitleTimeline
    (
        uint       index,
        WordEntry* entry
    );

    /// <summary>
    ///     应用词条的内容状态与时间线状态：按旧状态播放过渡动画，写入新状态并触发时间线。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entry">词条。</param>
    /// <param name="state">新的内容状态。</param>
    /// <param name="timelineState">新的时间线状态。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 41 8B 80")]
    public partial void ApplyEntryState
    (
        uint       index,
        WordEntry* entry,
        uint       state,
        uint       timelineState
    );

    /// <summary>
    ///     按词条当前的内容状态与时间线状态刷新动画：状态 2 时初始化三段时间线，其余情况按状态切换。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 8B FA 48 8B F1 48 69 DF")]
    public partial void RefreshEntryTimeline
    (
        uint index
    );

    /// <summary>
    ///     在指定区间内取一个随机秒数。
    /// </summary>
    /// <param name="min">区间下限。</param>
    /// <param name="max">区间上限。</param>
    /// <returns>随机秒数，区间无效时返回下限。</returns>
    [MemberFunction("48 83 EC ?? 0F 29 74 24 ?? 0F 57 C0 0F 29 7C 24 ?? 0F 28 F2")]
    public partial float GetRandomRange
    (
        float min,
        float max
    );

    /// <summary>
    ///     播放词条出现效果：播放音效并启动状态节点与时间线节点。
    /// </summary>
    /// <param name="entry">词条。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B DA 45 33 C9 33 D2 45 33 C0")]
    public partial void PlayEntryAppear
    (
        WordEntry* entry
    );

    /// <summary>
    ///     按状态播放指定词条的时间线动画。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="state">动画状态，0 表示无动画，1 至 5 分别对应时间线标签 101 至 105。</param>
    [MemberFunction("48 83 EC ?? 83 FA ?? 77 ?? 48 63 C2 41 8B D0")]
    public partial void SetWordState
    (
        uint index,
        uint state
    );

    /// <summary>
    ///     复制指定槽位的组件节点，缓存该组件内全部节点的指针并注册时间线事件。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("48 89 6C 24 ?? 56 48 83 EC ?? 45 33 C9 8B EA")]
    public partial void SetupWordEntry
    (
        uint index
    );

    /// <summary>
    ///     把数值数组与字符串数组下发的数据写入指定槽位：设置名字与称号文本、切换状态并播放对应动画。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entityID">词条所属敌人的实体 id。</param>
    /// <param name="name">名字文本。</param>
    /// <param name="title">称号文本。</param>
    /// <param name="state">内容状态。</param>
    /// <param name="timelineState">时间线状态，取值 0、101、104、105。</param>
    [MemberFunction("40 56 41 54 41 56 41 57 48 83 EC ?? 48 63 F2")]
    public partial void ApplyWordData
    (
        uint           index,
        uint           entityID,
        CStringPointer name,
        CStringPointer title,
        uint           state,
        uint           timelineState
    );

    /// <summary>
    ///     读取数值数组与字符串数组中指定下标的词条数据并写入对应槽位。
    /// </summary>
    /// <param name="numberArray">数值数组（索引 0xA1）。</param>
    /// <param name="stringArray">字符串数组（索引 0x80）。</param>
    /// <param name="index">词条下标。</param>
    /// <param name="entityID">输出：解析到的敌人实体 id。</param>
    /// <returns>是否处理成功。</returns>
    [MemberFunction("40 53 56 57 48 81 EC ?? ?? ?? ?? 48 8B 42")]
    public partial bool UpdateWordFromArrays
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        uint             index,
        int*             entityID
    );

    /// <summary>
    ///     复位指定槽位：清空文本、隐藏节点、并把状态 2 的已处理敌人加入已处理集合。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 53 48 83 EC ?? 48 63 C2 48 8B D9 48 89 7C 24 ?? 48 69 F8 ?? ?? ?? ?? 48 83 BC 0F")]
    public partial void ResetWordEntry
    (
        uint index
    );

    /// <summary>
    ///     复位全部槽位并清空已处理集合。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 33 DB 8B D3")]
    public partial void ResetAllWordEntries();

    /// <summary>
    ///     按坐标、缩放百分比与优先级摆放指定槽位，并同步调整各节点尺寸。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="x">屏幕横坐标。</param>
    /// <param name="y">屏幕纵坐标。</param>
    /// <param name="scalePercent">缩放百分比，取值下限为 60。</param>
    /// <param name="priority">优先级系数，取值 0 至 1。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 53 48 83 EC ?? 0F 29 7C 24 ?? 48 8B D9 44 0F 29 44 24 ?? 0F 28 FA 48 63 C2 44 0F 28 C3 48 89 7C 24 ?? 48 69 F8")]
    public partial void LayoutWordEntry
    (
        uint  index,
        float x,
        float y,
        int   scalePercent,
        float priority
    );

    /// <summary>
    ///     按名字与称号文本宽度同步指定槽位各节点的宽度。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 53 48 83 EC ?? 48 63 C2 48 8B D9 48 89 7C 24 ?? 48 69 F8 ?? ?? ?? ?? 48 8B 8C 0F")]
    public partial void UpdateWordWidth
    (
        uint index
    );

    /// <summary>
    ///     播放指定槽位当前时间线状态的动画，并把槽位标记为已应用。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("48 83 EC ?? 83 FA ?? 77 ?? 48 89 5C 24 ?? 48 63 C2 48 69 D8")]
    public partial void ApplyWordTimeline
    (
        uint index
    );

    /// <summary>
    ///     按槽位的内容状态在 1 与 4 之间切换时间线。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 63 FA 48 8B F1 83 FF")]
    public partial void ToggleWordTimeline
    (
        uint index
    );

    /// <summary>
    ///     单个词条槽位。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 200)]
    public unsafe partial struct WordEntry
    {
        /// <summary>
        ///     复制出来的组件节点（节点 id 为 20000 + 槽位索引）。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     背景九宫格节点（节点 2）。
        /// </summary>
        [FieldOffset(8)]
        public AtkResNode* BackgroundNode;

        /// <summary>
        ///     背景高亮九宫格节点（节点 3）。
        /// </summary>
        [FieldOffset(16)]
        public AtkResNode* BackgroundHighlightNode;

        /// <summary>
        ///     内容根节点（节点 4），闪烁计时与位置变化都作用于该节点。
        /// </summary>
        [FieldOffset(24)]
        public AtkResNode* ContentNode;

        /// <summary>
        ///     装饰图片容器节点（节点 0x0A）。
        /// </summary>
        [FieldOffset(32)]
        public AtkResNode* FrameContainerNode;

        /// <summary>
        ///     装饰边框图片节点（节点 0x0B 至 0x12 共八个）。
        /// </summary>
        [FieldOffset(40)]
        [FixedSizeArray]
        internal FixedSizeArray8<Pointer<AtkResNode>> FrameImageNodesStorage;

        /// <summary>
        ///     名字文本容器节点（节点 0x13）。
        /// </summary>
        [FieldOffset(104)]
        public AtkResNode* NameTextContainerNode;

        /// <summary>
        ///     名字文本节点（节点 0x14）。
        /// </summary>
        [FieldOffset(112)]
        public AtkTextNode* NameTextNode;

        /// <summary>
        ///     称号文本容器节点（节点 0x15）。
        /// </summary>
        [FieldOffset(120)]
        public AtkResNode* TitleTextContainerNode;

        /// <summary>
        ///     称号文本节点（节点 0x16）。
        /// </summary>
        [FieldOffset(128)]
        public AtkTextNode* TitleTextNode;

        /// <summary>
        ///     高亮边框容器节点（节点 0x17）。
        /// </summary>
        [FieldOffset(136)]
        public AtkResNode* HighlightContainerNode;

        /// <summary>
        ///     高亮边框九宫格节点（节点 0x18 至 0x1A 共三个）。
        /// </summary>
        [FieldOffset(144)]
        [FixedSizeArray]
        internal FixedSizeArray3<Pointer<AtkResNode>> HighlightNodesStorage;

        /// <summary>
        ///     词条所属敌人的实体 id，无效时为 -536870912。
        /// </summary>
        [FieldOffset(168)]
        public int EntityID;

        /// <summary>
        ///     用于播放时间线动画的状态，0 对应无动画，1 至 5 对应时间线标签 101 至 105。
        /// </summary>
        [FieldOffset(172)]
        public int AnimationState;

        /// <summary>
        ///     内容状态，与 <see cref="AnimationState"/> 同步，但只在应用词条数据时更新。
        /// </summary>
        [FieldOffset(176)]
        public int State;

        /// <summary>
        ///     本地玩家是否处于视觉障碍状态，为真时词条进入闪烁计时。
        /// </summary>
        [FieldOffset(180)]
        public bool Obstructed;

        /// <summary>
        ///     闪烁阶段标志，在两次随机间隔之间交替。
        /// </summary>
        [FieldOffset(181)]
        public bool BlinkPhase;

        /// <summary>
        ///     距离下次闪烁切换的剩余秒数。
        /// </summary>
        [FieldOffset(184)]
        public float BlinkTimer;

        /// <summary>
        ///     称号文本节点当前是否带有文本，决定闪烁时使用时间线标签 101 还是 17。
        /// </summary>
        [FieldOffset(188)]
        public bool HasTitleText;

        /// <summary>
        ///     当前时间线状态，取值 0、101、104、105。
        /// </summary>
        [FieldOffset(192)]
        public int TimelineState;

        /// <summary>
        ///     时间线是否已经应用，应用后不再重复播放。
        /// </summary>
        [FieldOffset(196)]
        public bool TimelineApplied;
    }
}
