using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     采集品数量说明代理，负责把若干采集品鉴定条目的名称、需求数量与持有数量
///     送入物品数量界面，并响应界面的行进入、离开与点击回调。
///     其 <c>ReceiveEvent</c>、<c>Dtor</c> 与 <c>Hide</c> 均由 <see cref="AgentInterface" /> 继承而来的虚函数槽位覆盖。
/// </summary>
[GenerateInterop]
[Agent(AgentId.HwdGathererInspectionItemCount)]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe partial struct AgentHwdGathererInspectionItemCount
{
    /// <summary>
    ///     运行时数据，首次载入时在界面内存空间中分配，界面关闭时释放。
    /// </summary>
    [FieldOffset(40)]
    public ItemCountData* Data;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <param name="uiModuleInterface">界面模块接口。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 C7 43 ?? ?? ?? ?? ?? 48 89 03 48 8B C3 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 57"
    )]
    public partial AgentHwdGathererInspectionItemCount* Ctor
    (
        UIModuleInterface* uiModuleInterface
    );

    /// <summary>
    ///     清理基类成员，不释放自身内存。
    /// </summary>
    [MemberFunction("48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 57 48 83 EC")]
    public partial void Destroy();

    /// <summary>
    ///     按给定物品 id 列表重建运行时数据，并启动主表行加载。
    /// </summary>
    /// <param name="itemIds">采集品物品 id 数组。</param>
    /// <param name="count">物品 id 数量，超过 6 时按 6 处理。</param>
    /// <param name="argument">打开界面时传入的整数参数。</param>
    [MemberFunction("40 53 57 48 83 EC ?? 48 89 6C 24 ?? 48 8B F9 48 8B 49")]
    public partial void LoadSheet
    (
        uint* itemIds,
        uint  count,
        int   argument
    );

    /// <summary>
    ///     主表每一行加载完成时的回调，把该行 79 个槽位中与给定物品匹配的鉴定产物 id 写入运行时数据。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    [MemberFunction("40 57 48 83 EC ?? 48 8B F9 45 0F B6 D9")]
    public partial void OnMainSheetRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast
    );
    
    /// <summary>
    ///     采集品数量说明代理在界面内存空间中分配的运行时数据。
    ///     它保存六组采集品鉴定条目的名称与数量，以及三个已完成加载的表等待器。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1976)]
    public unsafe struct ItemCountData
    {
        /// <summary>
        ///     第一个表等待器。
        /// </summary>
        [FieldOffset(0)]
        public ItemCountSheetWaiter SheetWaiterA;

        /// <summary>
        ///     第二个表等待器。
        /// </summary>
        [FieldOffset(160)]
        public ItemCountSheetWaiter SheetWaiterB;

        /// <summary>
        ///     第三个表等待器。
        /// </summary>
        [FieldOffset(320)]
        public ItemCountSheetWaiter SheetWaiterC;

        /// <summary>
        ///     六组采集品鉴定条目，每组对应主表中一个已填充槽位。
        /// </summary>
        [FieldOffset(488)]
        [FixedSizeArray]
        internal FixedSizeArray6<Entry> EntriesStorage;

        /// <summary>
        ///     当前有效的条目数量，最大为 6。
        /// </summary>
        [FieldOffset(1968)]
        public uint Count;

        /// <summary>
        ///     打开界面时由调用方传入的整数参数。
        /// </summary>
        [FieldOffset(1972)]
        public uint Argument;

        /// <summary>
        ///     一组采集品鉴定条目，保存条目名称、物品 id 与鉴定产物 id。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 248)]
        public struct Entry
        {
            /// <summary>
            ///     条目名称。
            /// </summary>
            [FieldOffset(0)]
            public Utf8String Name;

            /// <summary>
            ///     该条目对应的采集品物品 id。
            /// </summary>
            [FieldOffset(116)]
            public uint ItemID;

            /// <summary>
            ///     该条目对应的说明文本。
            /// </summary>
            [FieldOffset(128)]
            public Utf8String Description;

            /// <summary>
            ///     该条目对应的鉴定产物物品 id。
            /// </summary>
            [FieldOffset(236)]
            public uint ResultItemID;
        }
    }

    /// <summary>
    ///     采集品数量说明代理内嵌的多行表等待器。
    ///     它派生自 <see cref="ExcelSheetWaiter" />，除基类的单表等待能力外，
    ///     还维护一个待加载行号队列，并在每一行载入完成后调用代理注册的回调。
    /// </summary>
    [GenerateInterop]
    [Inherits<ExcelSheetWaiter>]
    [StructLayout(LayoutKind.Explicit, Size = 160)]
    public unsafe partial struct ItemCountSheetWaiter
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
        ///     布局保留字段，在全部已知代码路径中未被读写。
        /// </summary>
        [FieldOffset(96)]
        public nint Reserved60;

        /// <summary>
        ///     行号队列快照的队头索引。
        /// </summary>
        [FieldOffset(104)]
        public nint RowQueueSnapshotHead;

        /// <summary>
        ///     每加载一行时调用的回调。
        /// </summary>
        [FieldOffset(112)]
        public nint RowLoadedCallback;

        /// <summary>
        ///     传给 <see cref="RowLoadedCallback" /> 的参数相对回调上下文的字节偏移。
        /// </summary>
        [FieldOffset(120)]
        public nint RowLoadedCallbackArgumentOffset;

        /// <summary>
        ///     传给回调的上下文对象，指向所属代理。
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
        ///     置位时 <see cref="ExcelSheetWaiter.Prepare" /> 跳过完整重载，只设置队列中的第一行。
        /// </summary>
        [FieldOffset(153)]
        public byte SkipReload;
    }
}
