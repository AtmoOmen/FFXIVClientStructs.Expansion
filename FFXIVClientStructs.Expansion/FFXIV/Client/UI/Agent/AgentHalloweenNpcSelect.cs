using FFXIVClientStructs.FFXIV.Client.Game.Event;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     “选择变身角色”窗口（HalloweenNpcSelect）对应的代理。<br />
///     它加载 <c>HalloweenNpcSelect</c> 表，按任务完成状态与当前节日筛选可选项，并把结果写入窗口。
/// </summary>
[GenerateInterop]
[Inherits<AgentInterface>]
[Agent(AgentId.HalloweenNpcSelect)]
[StructLayout(LayoutKind.Explicit, Size = 272)]
public unsafe partial struct AgentHalloweenNpcSelect
{
    /// <summary>
    ///     内嵌的 <c>HalloweenNpcSelect</c> 表等待器，负责逐行加载表数据并回调该代理。
    /// </summary>
    [FieldOffset(40)]
    public HalloweenNpcSelectSheetWaiter SheetWaiter;

    /// <summary>
    ///     缓存自 <c>HalloweenNpcSelect</c> 表等待器的全部表行，按表行号加载顺序追加。
    /// </summary>
    [FieldOffset(200)]
    public StdVector<HalloweenNpcSelectEntry> CandidateRows;

    /// <summary>
    ///     置位时在下一个 <see cref="Update" /> 中重写窗口标题、页签与各版本图标。
    /// </summary>
    [FieldOffset(224)]
    public byte NeedsHeaderUpdate;

    /// <summary>
    ///     置位时在下一个 <see cref="Update" /> 中按当前节日重建可用行列表。
    /// </summary>
    [FieldOffset(225)]
    public byte NeedsListUpdate;

    /// <summary>
    ///     置位时在下一个 <see cref="Update" /> 中刷新预览图标。
    /// </summary>
    [FieldOffset(226)]
    public byte NeedsPreviewUpdate;

    /// <summary>
    ///     当前选中的页签按钮索引，取值 0 至 5，6 表示没有可用的页签。
    /// </summary>
    [FieldOffset(228)]
    public uint SelectedButtonIndex;

    /// <summary>
    ///     当前选中的表行号，0 表示尚未选中任何行。
    /// </summary>
    [FieldOffset(232)]
    public uint SelectedRowID;

    /// <summary>
    ///     同时满足任务完成状态与当前节日条件的行，供窗口列表显示。
    /// </summary>
    [FieldOffset(240)]
    public StdVector<HalloweenNpcSelectEntry> AvailableRows;

    /// <summary>
    ///     已解锁的版本位图，位 0 至 5 依次对应版本 0 至 5。
    /// </summary>
    [FieldOffset(264)]
    public uint UnlockedExpansionMask;

    /// <summary>
    ///     当前处于事件场景时，把选中的表行号推送给事件脚本的 Lua 线程。
    /// </summary>
    [VirtualFunction(15)]
    public partial void SendSelectionToEventFramework
    (
        EventFramework* eventFramework
    );

    /// <summary>
    ///     每加载一行 HalloweenNpcSelect 表时由表等待器调用，把该行的描述、图标与条件字段追加到 CandidateRows。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 55 56 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 9C 24 ?? ?? ?? ?? 48 8B E9 48 8B 3A 48 8D 4C 24 ?? 33 C0 41 0F B6 F1 48 89 44 24 ?? E8 ?? ?? ?? ?? 33 C0"
    )]
    public partial void OnRowLoaded
    (
        byte** row,
        nint   unused,
        bool   isLastRow,
        uint*  rowID
    );

    /// <summary>
    ///     把窗口标题、页签文本与各扩展版本的图标写入 AtkValue 数组。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 54 24 ?? 56 57 41 54 41 55 41 56 48 83 EC")]
    public partial bool BuildHeaderValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     按当前节日筛选可用行并写入 AtkValue 数组，返回列表是否发生变化。
    /// </summary>
    [MemberFunction("48 89 54 24 ?? 48 89 4C 24 ?? 53 55 57 41 54")]
    public partial bool BuildOptionList
    (
        AtkValue* values
    );

    /// <summary>
    ///     统计当前已解锁的扩展版本数量，同时刷新 UnlockedExpansionMask。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 45 33 E4 C7 44 24")]
    public partial uint CountUnlockedExpansions();

    /// <summary>
    ///     判断 CandidateRows 中指定索引的行是否在当前活动列表中开放。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B EA 48 B8")]
    public partial bool IsOptionAvailable
    (
        uint index
    );

    /// <summary>
    ///     内嵌在 <see cref="AgentHalloweenNpcSelect" /> 中的表等待器。<br />
    ///     它派生自 <see cref="ExcelSheetWaiter" />，把 <c>HalloweenNpcSelect</c> 表的每一行送入该代理。
    /// </summary>
    [GenerateInterop]
    [Inherits<ExcelSheetWaiter>]
    [StructLayout(LayoutKind.Explicit, Size = 160)]
    public unsafe partial struct HalloweenNpcSelectSheetWaiter
    {
        /// <summary>
        ///     行号队列的哨兵节点。
        /// </summary>
        [FieldOffset(48)]
        public nint RowQueueNode;

        /// <summary>
        ///     行号队列的桶数组。
        /// </summary>
        [FieldOffset(56)]
        public nint RowQueueBuckets;

        /// <summary>
        ///     行号队列的桶数量。
        /// </summary>
        [FieldOffset(64)]
        public nint RowQueueCapacity;

        /// <summary>
        ///     行号队列的队头索引。
        /// </summary>
        [FieldOffset(72)]
        public nint RowQueueHead;

        /// <summary>
        ///     队列中的行号数量。
        /// </summary>
        [FieldOffset(80)]
        public nint RowQueueCount;

        /// <summary>
        ///     行号队列快照的哨兵节点。
        /// </summary>
        [FieldOffset(88)]
        public nint RowQueueSnapshotNode;

        /// <summary>
        ///     队列快照与队头之间的保留字段。
        /// </summary>
        [FieldOffset(96)]
        public nint Reserved60;

        /// <summary>
        ///     行号队列快照的队头索引。
        /// </summary>
        [FieldOffset(104)]
        public nint RowQueueSnapshotHead;

        /// <summary>
        ///     每加载一行 <c>HalloweenNpcSelect</c> 表时调用的回调。
        /// </summary>
        [FieldOffset(112)]
        public nint RowLoadedCallback;

        /// <summary>
        ///     表缺失时调用的次要回调。
        /// </summary>
        [FieldOffset(120)]
        public nint SecondaryCallback;

        /// <summary>
        ///     传给 <see cref="RowLoadedCallback" /> 的参数。
        /// </summary>
        [FieldOffset(128)]
        public nint CallbackArgument;

        /// <summary>
        ///     该等待器加载的 Excel 表索引。
        /// </summary>
        [FieldOffset(136)]
        public uint SheetIndex;

        /// <summary>
        ///     当前正在加载的行索引。
        /// </summary>
        [FieldOffset(140)]
        public uint CurrentRowIndex;

        /// <summary>
        ///     每次重置与准备时递增，用于丢弃过期的行回调。
        /// </summary>
        [FieldOffset(144)]
        public uint RequestGeneration;

        /// <summary>
        ///     表数据就绪后置为 1。
        /// </summary>
        [FieldOffset(148)]
        public uint LoadState;

        /// <summary>
        ///     处理行回调期间置位。
        /// </summary>
        [FieldOffset(152)]
        public byte IsProcessing;

        /// <summary>
        ///     置位时跳过完整重载，只设置队列中的第一行。
        /// </summary>
        [FieldOffset(153)]
        public byte SkipReload;

        /// <summary>
        ///     Excel 表请求的行加载完成时调用。
        /// </summary>
        [VirtualFunction(1)]
        public partial void Finish
        (
            int  rowCount,
            nint rows,
            uint requestGeneration
        );
    }

    /// <summary>
    ///     <see cref="AgentHalloweenNpcSelect" /> 为窗口缓存的一行 <c>HalloweenNpcSelect</c> 表数据。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 136)]
    public unsafe struct HalloweenNpcSelectEntry
    {
        /// <summary>
        ///     表行号。
        /// </summary>
        [FieldOffset(0)]
        public uint RowID;

        /// <summary>
        ///     选项描述文本。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String Description;

        /// <summary>
        ///     表定义中的未知列，客户端用作可用行列表的排序键。
        /// </summary>
        [FieldOffset(112)]
        public uint Unknown1;

        /// <summary>
        ///     解锁该选项所需完成的任务。
        /// </summary>
        [FieldOffset(116)]
        public uint RequiredQuest;

        /// <summary>
        ///     预览图标 ID。
        /// </summary>
        [FieldOffset(120)]
        public uint PreviewIcon;

        /// <summary>
        ///     变身（Transformation）表行号。
        /// </summary>
        [FieldOffset(124)]
        public ushort Transformation;

        /// <summary>
        ///     该选项开放的活动 ID。
        /// </summary>
        [FieldOffset(126)]
        public ushort Festival;

        /// <summary>
        ///     该选项所属的扩展版本。
        /// </summary>
        [FieldOffset(128)]
        public byte Expansion;
    }
}
