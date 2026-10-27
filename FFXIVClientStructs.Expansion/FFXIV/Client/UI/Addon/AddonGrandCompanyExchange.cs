using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     军票兑换商店的商品及数量计算。
/// </summary>
[Addon("GrandCompanyExchange")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 1048)]
public unsafe partial struct AddonGrandCompanyExchange
{
    /// <summary>
    ///     商品列表组件（节点 57）。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentList* ItemList;

    /// <summary>
    ///     由字符串数组 33 的第 1 项刷新的文本节点（节点 8）。
    /// </summary>
    [FieldOffset(576)]
    public AtkTextNode* StatusTextNode;

    /// <summary>
    ///     显示当前军队图标的图片节点（节点 9）。
    /// </summary>
    [FieldOffset(584)]
    public AtkImageNode* GrandCompanyIconNode;

    /// <summary>
    ///     内嵌的按钮组件，构造时初始化，但紧随其后的清零会覆盖该子对象，当前版本未被使用。
    /// </summary>
    [FieldOffset(592)]
    public AtkComponentButton Button;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(832)]
    public ulong Unk340;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(840)]
    public ulong Unk348;

    /// <summary>
    ///     各军衔对应的显示文本，共 19 项，索引 0 对应军衔 1。
    /// </summary>
    [FieldOffset(848)]
    [FixedSizeArray]
    internal FixedSizeArray19<CStringPointer> RankNameStorage;

    /// <summary>
    ///     当前页面的商品数组，长度由 <see cref="ItemCount" /> 决定，打开界面时在 UI 内存空间中分配。
    /// </summary>
    [FieldOffset(1008)]
    public ExchangeItem* Items;

    /// <summary>
    ///     当前页面的有效商品数量。
    /// </summary>
    [FieldOffset(1016)]
    public int ItemCount;

    /// <summary>
    ///     子分类按钮 501 对应的子分类编号。
    /// </summary>
    [FieldOffset(1020)]
    public uint SubCategory1;

    /// <summary>
    ///     子分类按钮 502 对应的子分类编号。
    /// </summary>
    [FieldOffset(1024)]
    public uint SubCategory2;

    /// <summary>
    ///     子分类按钮 503 对应的子分类编号。
    /// </summary>
    [FieldOffset(1028)]
    public uint SubCategory3;

    /// <summary>
    ///     子分类按钮 504 对应的子分类编号。
    /// </summary>
    [FieldOffset(1032)]
    public uint SubCategory4;

    /// <summary>
    ///     子分类按钮 505 对应的子分类编号。
    /// </summary>
    [FieldOffset(1036)]
    public uint SubCategory5;

    /// <summary>
    ///     玩家所属军队与界面显示的军队一致，为 <see langword="false" /> 时全部商品不可兑换。
    /// </summary>
    [FieldOffset(1040)]
    public bool IsSameGrandCompany;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? 48 89 03 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 8B")]
    public partial AddonGrandCompanyExchange* Ctor();

    /// <summary>
    ///     重置虚表并销毁基类。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 01 48 81 C1 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 83 EC ?? 4C 8D 44 24 ?? C7 44 24 ?? ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 48 83 C4 ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC E9"
    )]
    public partial void PrepareDestructor();

    /// <summary>
    ///     拆离并隐藏本界面的悬浮提示，释放商品数组后终结基类与数组订阅。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 0F B7 93 ?? ?? ?? ?? 41 B0 ?? 48 8D 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 0F B7 93")]
    public partial void Deinitialize();

    /// <summary>
    ///     根据军票余额和商品单价刷新可兑换数量。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>刷新是否完成。</returns>
    [MemberFunction("4C 89 44 24 ?? 55 57 48 83 EC ?? 48 89 5C 24")]
    public partial bool Refresh
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     切换子分类，并把该子分类编号回传给打开者。
    /// </summary>
    /// <param name="subCategoryButtonID">子分类按钮 id，取值 501 至 505。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? BE ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B E9"
    )]
    public partial void SelectSubCategory
    (
        int subCategoryButtonID
    );

    /// <summary>
    ///     渲染商品列表项的名称、图标、单价、持有数量与数量输入框。
    /// </summary>
    /// <param name="itemIndex">商品索引。</param>
    /// <param name="nodes">列表项节点数组。</param>
    /// <param name="renderer">列表项渲染器。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B C2")]
    public partial void RenderItemListItem
    (
        int                           itemIndex,
        AtkResNode**                  nodes,
        AtkComponentListItemRenderer* renderer
    );

    /// <summary>
    ///     军票兑换商店中的一项商品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public unsafe struct ExchangeItem
    {
        /// <summary>
        ///     商品名称。
        /// </summary>
        [FieldOffset(0)]
        public CStringPointer Name;

        /// <summary>
        ///     悬浮提示文本，指向 <see cref="RankNameStorage" /> 中对应军衔的一项。
        /// </summary>
        [FieldOffset(8)]
        public CStringPointer RankName;

        /// <summary>
        ///     兑换一件商品所需的军票数。
        /// </summary>
        [FieldOffset(16)]
        public uint Cost;

        /// <summary>
        ///     由界面值数组第 117 项写入，界面与代理均未读取的保留数值。
        /// </summary>
        [FieldOffset(20)]
        public uint Unk14;

        /// <summary>
        ///     商品图标 id。
        /// </summary>
        [FieldOffset(24)]
        public uint ItemIconID;

        /// <summary>
        ///     所需军衔的图标 id，随军衔升高而增大，用于判定玩家当前军衔是否满足要求。
        /// </summary>
        [FieldOffset(28)]
        public uint RequiredRankIconID;

        /// <summary>
        ///     玩家持有该商品的数量，超过 99999 时以格式化文本显示。
        /// </summary>
        [FieldOffset(32)]
        public uint OwnedCount;

        /// <summary>
        ///     本次兑换的数量。
        /// </summary>
        [FieldOffset(36)]
        public uint Quantity;

        /// <summary>
        ///     按军票余额与单价算出的可兑换数量上限，上限为 99。
        /// </summary>
        [FieldOffset(40)]
        public uint MaxQuantity;

        /// <summary>
        ///     玩家当前军衔低于商品所需军衔。
        /// </summary>
        [FieldOffset(44)]
        public bool IsRankInsufficient;

        /// <summary>
        ///     该商品的数量输入框已初始化。
        /// </summary>
        [FieldOffset(45)]
        public bool IsInitialized;

        /// <summary>
        ///     客户端是否为该商品启用数量输入。
        /// </summary>
        [FieldOffset(46)]
        public bool CanSpecifyQuantity;

        /// <summary>
        ///     该商品带有军衔名称悬浮提示。
        /// </summary>
        [FieldOffset(47)]
        public bool HasRankName;
    }
}
