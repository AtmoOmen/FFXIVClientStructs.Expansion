using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     制作笔记的配方搜索上下文，按批扫描 Recipe 表并把命中的配方写入结果列表。
/// </summary>
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 608)]
public unsafe partial struct RecipeSearchContext
{
    /// <summary>
    ///     界面模块接口，用于取得文本模块与查询表。
    /// </summary>
    [FieldOffset(8)]
    public UIModuleInterface* UIModuleInterface;

    /// <summary>
    ///     原始搜索关键字。
    /// </summary>
    [FieldOffset(16)]
    public Utf8String SearchText;

    /// <summary>
    ///     转为小写后的搜索关键字，用于包含匹配。
    /// </summary>
    [FieldOffset(120)]
    public Utf8String SearchTextLowercase;

    /// <summary>
    ///     搜索模式，非零表示精确匹配，为零表示包含匹配。
    /// </summary>
    [FieldOffset(224)]
    public bool IsExactMatch;

    /// <summary>
    ///     搜索已完成时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(225)]
    public bool IsComplete;

    /// <summary>
    ///     仍有待扫描行时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(226)]
    public bool CanIterate;

    /// <summary>
    ///     Recipe 表的行数，作为扫描上界。
    /// </summary>
    [FieldOffset(228)]
    public uint RowEndIndex;

    /// <summary>
    ///     下一次扫描的起始行号。
    /// </summary>
    [FieldOffset(232)]
    public uint RowStartIndex;

    /// <summary>
    ///     Recipe 表等待器。
    /// </summary>
    [FieldOffset(240)]
    public RecipeSearchWaiter RecipeWaiter;

    /// <summary>
    ///     Item 表等待器。
    /// </summary>
    [FieldOffset(400)]
    public RecipeSearchWaiter ItemWaiter;

    /// <summary>
    ///     扫描 Recipe 表得到的中间结果，供 Item 表按物品名过滤。
    /// </summary>
    [FieldOffset(560)]
    public StdVector<RecipeSearchMatch> Matches;

    /// <summary>
    ///     过滤后的结果链表头节点。
    /// </summary>
    [FieldOffset(584)]
    public void* ResultListHead;

    /// <summary>
    ///     结果链表的元素数量。
    /// </summary>
    [FieldOffset(592)]
    public ulong ResultCount;

    /// <summary>
    ///     接收最终搜索结果的目标列表。
    /// </summary>
    [FieldOffset(600)]
    public StdVector<uint>* SearchResults;

    /// <summary>
    ///     析构搜索上下文。
    /// </summary>
    /// <param name="freeFlags">释放标志。</param>
    /// <returns>上下文自身。</returns>
    [VirtualFunction(0)]
    public partial RecipeSearchContext* Dtor
    (
        byte freeFlags
    );

    /// <summary>
    ///     搜索是否已完成。
    /// </summary>
    /// <returns>已完成时为 <see langword="true" />。</returns>
    [VirtualFunction(1)]
    public partial bool GetIsComplete();

    /// <summary>
    ///     推进一批扫描，每批最多 200 行。
    /// </summary>
    [VirtualFunction(2)]
    public partial void Iterate();

    /// <summary>
    ///     扫描一批 Recipe 表行，匹配物品名称并转入 Item 表查表。
    /// </summary>
    /// <param name="rowIndex">起始行号。</param>
    /// <param name="rowCount">本批行数。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 48 8D B9")]
    public partial void ScanRecipeRows
    (
        uint rowIndex,
        uint rowCount
    );

    /// <summary>
    ///     Recipe 表行加载完成回调，把行内物品 id 写入中间结果并转查 Item 表。
    /// </summary>
    /// <param name="context">搜索上下文。</param>
    /// <param name="rows">表行指针数组。</param>
    /// <param name="rowCount">表行数量。</param>
    /// <param name="isReload">是否为重新加载。</param>
    /// <param name="rowIds">表行 id 数组。</param>
    /// <returns>后续处理结果。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 4C 8B 02 48 8B D9 48 89 74 24")]
    public partial nint OnRecipeRowsLoaded
    (
        RecipeSearchContext* context,
        void**               rows,
        uint                 rowCount,
        bool                 isReload,
        uint*                rowIds
    );

    /// <summary>
    ///     Item 表行加载完成回调，按物品名称过滤命中项并转入结果链表。
    /// </summary>
    /// <param name="context">搜索上下文。</param>
    /// <param name="rows">表行指针数组。</param>
    /// <param name="rowCount">表行数量。</param>
    /// <param name="isReload">是否为重新加载。</param>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 1D")]
    public partial void OnItemRowsLoaded
    (
        RecipeSearchContext* context,
        void**               rows,
        uint                 rowCount,
        bool                 isReload
    );

    /// <summary>
    ///     把结果链表中的配方行 id 写入目标列表并标记搜索完成。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 91 ?? ?? ?? ?? 48 8B F9 48 8B 02")]
    public partial void FinishSearch();

    /// <summary>
    ///     制作笔记搜索使用的 Recipe 表与 Item 表等待器。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 160)]
    public unsafe partial struct RecipeSearchWaiter
    {
        /// <summary>
        ///     当前已准备的行数量。
        /// </summary>
        [FieldOffset(8)]
        public uint RowDescriptorCount;

        /// <summary>
        ///     客户端未读写的保留字段。
        /// </summary>
        [FieldOffset(12)]
        public uint Unk0C;

        /// <summary>
        ///     当前请求的表行 id。
        /// </summary>
        [FieldOffset(16)]
        public uint RequestedRowID;

        /// <summary>
        ///     加载代次。
        /// </summary>
        [FieldOffset(24)]
        public ulong Generation;

        /// <summary>
        ///     行描述符数组。
        /// </summary>
        [FieldOffset(32)]
        public void* RowDescriptors;

        /// <summary>
        ///     行指针数组。
        /// </summary>
        [FieldOffset(40)]
        public void* Rows;

        /// <summary>
        ///     当前批次已载入的行 id 与行指针的对应序列。
        /// </summary>
        [FieldOffset(48)]
        public StdDeque<StdPair<ulong, ulong>> Results;

        /// <summary>
        ///     当前迭代到的链表节点。
        /// </summary>
        [FieldOffset(88)]
        public void* CurrentRow;

        /// <summary>
        ///     客户端未读写的保留字段。
        /// </summary>
        [FieldOffset(96)]
        public ulong Unk60;

        /// <summary>
        ///     当前迭代位置。
        /// </summary>
        [FieldOffset(104)]
        public ulong CurrentRowIndex;

        /// <summary>
        ///     表加载完成后的回调。
        /// </summary>
        [FieldOffset(112)]
        public delegate* unmanaged<RecipeSearchContext*, void**, uint, bool, nint> OnLoaded;

        /// <summary>
        ///     表加载完成回调的上下文。
        /// </summary>
        [FieldOffset(120)]
        public void* OnLoadedContext;

        /// <summary>
        ///     等待器的持有者上下文。
        /// </summary>
        [FieldOffset(128)]
        public RecipeSearchContext* Owner;

        /// <summary>
        ///     等待的表索引。
        /// </summary>
        [FieldOffset(136)]
        public uint SheetIndex;

        /// <summary>
        ///     已加载的行数量。
        /// </summary>
        [FieldOffset(140)]
        public uint LoadedRowCount;

        /// <summary>
        ///     加载尝试次数。
        /// </summary>
        [FieldOffset(144)]
        public uint LoadAttempt;

        /// <summary>
        ///     等待器状态：0 表示空闲，1 表示加载中。
        /// </summary>
        [FieldOffset(148)]
        public uint State;

        /// <summary>
        ///     表仍保持注册时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(152)]
        public bool KeepRegistered;

        /// <summary>
        ///     行已准备完成时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(153)]
        public bool RowsPrepared;
    }

    /// <summary>
    ///     扫描 Recipe 表得到的中间匹配结果。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct RecipeSearchMatch
    {
        /// <summary>
        ///     配方行 id。
        /// </summary>
        [FieldOffset(0)]
        public uint RecipeID;

        /// <summary>
        ///     配方成品对应的物品 id。
        /// </summary>
        [FieldOffset(4)]
        public uint ItemID;

        /// <summary>
        ///     排序键，取自配方行的制作难度等级。
        /// </summary>
        [FieldOffset(8)]
        public ushort SortKey;
    }

    /// <summary>
    ///     配方搜索的结果链表节点。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe struct RecipeSearchResultNode
    {
        /// <summary>
        ///     下一个节点。
        /// </summary>
        [FieldOffset(0)]
        public RecipeSearchResultNode* Next;

        /// <summary>
        ///     上一个节点。
        /// </summary>
        [FieldOffset(8)]
        public RecipeSearchResultNode* Previous;

        /// <summary>
        ///     配方行 id。
        /// </summary>
        [FieldOffset(16)]
        public uint RecipeID;

        /// <summary>
        ///     配方成品对应的物品 id。
        /// </summary>
        [FieldOffset(20)]
        public uint ItemID;

        /// <summary>
        ///     排序键。
        /// </summary>
        [FieldOffset(24)]
        public ushort SortKey;
    }
}
