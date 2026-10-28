using FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.InstanceContent;

/// <summary>
///     打字游戏的副本控制器（InstanceContentKeyboundBrawler）。<br />
///     控制器维护五条内容文本条目、三条阶段图像参数与当前阶段，接收导演更新以显示波次、开始、清空、警告、
///     结束与倒计时等屏幕文字，并把玩家提交的文本解析为目标与数值后交给副本逻辑判定。
/// </summary>
[GenerateInterop]
[Inherits<InstanceContentDirector>]
[StructLayout(LayoutKind.Explicit, Size = 8184)]
public unsafe partial struct InstanceContentKeyboundBrawler
{
    /// <summary>
    ///     内容文本条目，共 5 条，每条 12 字节。<br />
    ///     条目的文本行 id、参数与显示类型决定屏幕文字的来源与格式，条件字段决定该条目在哪些副本状态下显示。
    /// </summary>
    [FieldOffset(8088)]
    [FixedSizeArray]
    internal FixedSizeArray5<ContentTextEntry> ContentTextEntriesStorage;

    /// <summary>
    ///     阶段图像参数，共 3 条，每条 8 字节。<br />
    ///     图像行 id 对应界面图像表，参数作为图像的附加整数参数。
    /// </summary>
    [FieldOffset(8148)]
    [FixedSizeArray]
    internal FixedSizeArray3<ScreenImageEntry> ScreenImageEntriesStorage;

    /// <summary>
    ///     构造与重置时清零、本类型的方法均未读写的保留值。
    /// </summary>
    [FieldOffset(8172)]
    public uint Unk1FEC;

    /// <summary>
    ///     副本的阶段状态，4、5、6 表示游戏正在进行中，其余值表示可以退出；输入代理据此决定是否允许打开退出确认窗口。<br />
    ///     取值位于对象分配大小（8176 字节）之外的尾部填充区，由客户端直接读取。
    /// </summary>
    [FieldOffset(8176)]
    public int GamePhase;

    /// <summary>
    ///     副本的阶段图像编号，取值 0 至 2，对应界面图像 1620 至 1622；输入代理据此切换阶段图像。<br />
    ///     取值位于对象分配大小（8176 字节）之外的尾部填充区，由客户端直接读取。
    /// </summary>
    [FieldOffset(8180)]
    public int PhaseImageIndex;

    /// <summary>
    ///     构造控制器并把副本类型设为 23（打字游戏），同时通知界面模块打开打字游戏界面。
    /// </summary>
    /// <returns>控制器自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 41 B0 ?? 48 89 03 B2 ?? 48 8D 05 ?? ?? ?? ?? 48 8B CB 48 89 83 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 0D"
    )]
    public partial InstanceContentKeyboundBrawler* Ctor();

    /// <summary>
    ///     释放控制器并通知界面模块关闭打字游戏界面。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 81 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 81 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B 10 48 8B C8 FF 52"
    )]
    public partial void Finalizer();

    /// <summary>
    ///     处理玩家在聊天输入框中提交的文本：要求文本由十个数字字符或十个数字词逐位组成，最多十位。<br />
    ///     解析成功时输出当前选中的目标与解析出的数值，解析失败或没有选中目标时返回假。
    /// </summary>
    /// <param name="text">玩家提交的文本。</param>
    /// <param name="target">输出：当前选中的目标。</param>
    /// <param name="value">输出：解析出的数值。</param>
    /// <returns>是否解析成功。</returns>
    [VirtualFunction(30)]
    public partial bool ProcessChatInput
    (
        Utf8String*  text,
        GameObject** target,
        uint*        value
    );

    /// <summary>
    ///     把事件类型设为 23（打字游戏）、记录事件参数与事件对象，并同步当前事件状态。<br />
    ///     事件对象的类型为 7 时，额外以状态 4 派发到事件场景模块。
    /// </summary>
    /// <param name="gameObject">事件对象。</param>
    /// <param name="eventState">事件状态。</param>
    [VirtualFunction(31)]
    public partial void ProcessEventObject
    (
        GameObject* gameObject,
        int         eventState
    );

    /// <summary>
    ///     初始化五条内容文本条目：写入文本行 id、参数、显示类型、条件字段与文本来源，
    ///     并把条目数组指针、数量与阶段状态写入基类的文本控件。
    /// </summary>
    /// <returns>固定返回真。</returns>
    [VirtualFunction(321)]
    public partial bool InitializeContentTextEntries();

    /// <summary>
    ///     按副本的进度字节刷新第四至第六条文本条目（即索引 2 至 4）的条件字段与文本行 id，
    ///     并根据当前阶段决定第二条与第三条阶段图像参数，最后交给基类刷新文本控件。
    /// </summary>
    [VirtualFunction(404)]
    public partial void UpdateContentTextEntries();

    /// <summary>
    ///     单条内容文本条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public unsafe struct ContentTextEntry
    {
        /// <summary>
        ///     文本行 id，作为 InstanceContentText 表或内嵌文本数组的索引。
        /// </summary>
        [FieldOffset(0)]
        public ushort TextRowID;

        /// <summary>
        ///     文本的整数参数，随文本一起下发给文本控件。
        /// </summary>
        [FieldOffset(2)]
        public ushort TextArgument;

        /// <summary>
        ///     显示类型：0 表示无参数文本，1 至 4 表示控件支持的四种带参数文本格式。
        /// </summary>
        [FieldOffset(4)]
        public byte DisplayType;

        /// <summary>
        ///     条件位掩码，与条件索引取出的值按位与后决定条目是否显示；255 表示不检查。
        /// </summary>
        [FieldOffset(5)]
        public byte ConditionMask;

        /// <summary>
        ///     条件索引，0 至 11，对应基类进度字节中第索引项。
        /// </summary>
        [FieldOffset(6)]
        public byte ConditionIndex;

        /// <summary>
        ///     条件值：254 表示无条件跳过，255 表示条件索引的值为 0 时隐藏，其余值作为位掩码参与判定。
        /// </summary>
        [FieldOffset(7)]
        public byte ConditionValue;

        /// <summary>
        ///     第二个条件索引，与 <see cref="ConditionIndex" /> 不同时以其值参与判定。
        /// </summary>
        [FieldOffset(8)]
        public byte SecondaryConditionIndex;

        /// <summary>
        ///     文本来源：0 表示没有文本，1 表示 InstanceContentText 表，2 表示基类的内嵌文本数组。
        /// </summary>
        [FieldOffset(9)]
        public byte TextSource;

        /// <summary>
        ///     条目标志，最低位为 1 时条目只显示无参数文本。
        /// </summary>
        [FieldOffset(10)]
        public ushort Flags;
    }

    /// <summary>
    ///     单条阶段图像参数。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe struct ScreenImageEntry
    {
        /// <summary>
        ///     界面图像行 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ScreenImageRowID;

        /// <summary>
        ///     图像的附加整数参数，作为界面图像命令的参数之一下发。
        /// </summary>
        [FieldOffset(4)]
        public uint Argument;
    }
}
