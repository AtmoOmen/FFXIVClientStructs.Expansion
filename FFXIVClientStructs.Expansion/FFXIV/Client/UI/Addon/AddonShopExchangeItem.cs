using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     物品兑换商店，按商品容量和各项兑换材料计算可兑换次数。
/// </summary>
[Addon("ShopExchangeItem")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 34312)]
public unsafe partial struct AddonShopExchangeItem
{
    /// <summary>
    ///     商品列表树（节点 20）。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentTreeList* ItemTreeList;

    /// <summary>
    ///     职业筛选下拉列表（节点 3）。
    /// </summary>
    [FieldOffset(576)]
    public AtkComponentDropDownList* ClassJobDropDown;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(584)]
    public ulong Unk248;

    /// <summary>
    ///     各列的商品名称文本节点所在组件（节点 7）的组件，共 3 列。
    /// </summary>
    [FieldOffset(592)]
    [FixedSizeArray]
    internal FixedSizeArray3<Pointer<AtkComponentBase>> ColumnComponentStorage;

    /// <summary>
    ///     各列的商品图标组件，共 3 列。
    /// </summary>
    [FieldOffset(616)]
    [FixedSizeArray]
    internal FixedSizeArray3<Pointer<AtkComponentIcon>> ColumnItemIconStorage;

    /// <summary>
    ///     各列的材料图标所在组件的组件（节点 4），共 3 列。
    /// </summary>
    [FieldOffset(640)]
    [FixedSizeArray]
    internal FixedSizeArray3<Pointer<AtkComponentBase>> ColumnMaterialComponentStorage;

    /// <summary>
    ///     各列的材料图标文本节点（节点 5），共 3 列。
    /// </summary>
    [FieldOffset(664)]
    [FixedSizeArray]
    internal FixedSizeArray3<Pointer<AtkTextNode>> ColumnMaterialTextStorage;

    /// <summary>
    ///     各列的材料数量文本节点（节点 6），共 3 列。
    /// </summary>
    [FieldOffset(688)]
    [FixedSizeArray]
    internal FixedSizeArray3<Pointer<AtkTextNode>> ColumnMaterialCountStorage;

    /// <summary>
    ///     按职业显示商品的复选框（节点 11）。
    /// </summary>
    [FieldOffset(712)]
    public AtkComponentCheckBox* ClassJobCheckBox;

    /// <summary>
    ///     兑换说明按钮（按钮 2）。
    /// </summary>
    [FieldOffset(720)]
    public AtkComponentButton* InfoButton;

    /// <summary>
    ///     商品数组，每行最多 2 项，共 122 项。
    /// </summary>
    [FieldOffset(728)]
    [FixedSizeArray]
    internal FixedSizeArray122<ExchangeItem> ItemsStorage;

    /// <summary>
    ///     商品列表每一行首个材料在 <see cref="Materials" /> 中的索引，共 61 行。
    /// </summary>
    [FieldOffset(11464)]
    [FixedSizeArray]
    internal FixedSizeArray61<uint> RowMaterialIndexStorage;

    /// <summary>
    ///     商品所需的兑换材料，每项商品最多 3 项，共 183 项。
    /// </summary>
    [FieldOffset(11712)]
    [FixedSizeArray]
    internal FixedSizeArray183<MaterialItem> MaterialsStorage;

    /// <summary>
    ///     商品列表每一行的材料是否齐备，共 61 行。
    /// </summary>
    [FieldOffset(27816)]
    [FixedSizeArray]
    internal FixedSizeArray61<bool> RowHasMaterialsStorage;

    /// <summary>
    ///     商品列表中一行的商品数量，取值 1 或 2。
    /// </summary>
    [FieldOffset(27880)]
    public uint ItemsPerRow;

    /// <summary>
    ///     当前选中的商品行索引，-1 表示未选中。
    /// </summary>
    [FieldOffset(27884)]
    public int SelectedRowIndex;

    /// <summary>
    ///     复选框的原始文本，未勾选按职业筛选时显示在界面标题处。
    /// </summary>
    [FieldOffset(27888)]
    public Utf8String DefaultCheckBoxText;

    /// <summary>
    ///     客户端未初始化的保留字段。
    /// </summary>
    [FieldOffset(27968)]
    public ulong Unk6D60;

    /// <summary>
    ///     客户端未初始化的保留字段。
    /// </summary>
    [FieldOffset(27976)]
    public ulong Unk6D68;

    /// <summary>
    ///     客户端未初始化的保留字段。
    /// </summary>
    [FieldOffset(27984)]
    public ulong Unk6D70;

    /// <summary>
    ///     供商品列表取得列表项渲染器的回调接口。
    /// </summary>
    [FieldOffset(27992)]
    public ItemListCallback CallbackInterface;

    /// <summary>
    ///     构造商品列表时使用的界面值暂存区，共 366 项，每项商品占 3 项。
    /// </summary>
    [FieldOffset(28016)]
    [FixedSizeArray]
    internal FixedSizeArray366<AtkValue> TreeListValueStorage;

    /// <summary>
    ///     职业筛选下拉列表中各项的名称，共 35 项。
    /// </summary>
    [FieldOffset(33872)]
    [FixedSizeArray]
    internal FixedSizeArray35<CStringPointer> ClassJobNameStorage;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(34152)]
    public ulong Unk8568;

    /// <summary>
    ///     职业筛选下拉列表中各项对应的 ClassJob 表行 id，共 35 项。
    /// </summary>
    [FieldOffset(34160)]
    [FixedSizeArray]
    internal FixedSizeArray35<uint> ClassJobIDStorage;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(34300)]
    public ulong Unk85FC;

    /// <summary>
    ///     商品列表只显示一列，为 <see langword="false" /> 时显示两列。
    /// </summary>
    [FieldOffset(34308)]
    public bool IsSingleColumn;

    /// <summary>
    ///     上一次刷新的商品列表为空。
    /// </summary>
    [FieldOffset(34309)]
    public bool WasEmpty;

    /// <summary>
    ///     材料未齐备的商品一律不可兑换。
    /// </summary>
    [FieldOffset(34310)]
    public bool RequiresAllMaterials;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 33 FF 48 89 03 48 8D 8B ?? ?? ?? ?? 89 BB ?? ?? ?? ?? E8"
    )]
    public partial AddonShopExchangeItem* Ctor();

    /// <summary>
    ///     拆离并隐藏本界面的悬浮提示后终结基类。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 0F B7 97 ?? ?? ?? ?? 41 B0 ?? 48 8B D8 48 8D 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F B7 97 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? 45 33 C0 E8 ?? ?? ?? ?? 48 8B CF 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 41 B9 ?? ?? ?? ?? 48 8D 15 ?? ?? ?? ?? 45 33 C0 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 44 8B E2"
    )]
    public partial void Deinitialize();

    /// <summary>
    ///     刷新商品、兑换材料及各商品的可兑换数量。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>刷新是否完成。</returns>
    [MemberFunction("4C 89 44 24 ?? 53 57 41 56 48 81 EC")]
    public partial bool Refresh
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     按商品行是否为第二列取得列表项使用的渲染器。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <returns>列表项渲染器。</returns>
    [MemberFunction
    (
        "4C 8B 89 ?? ?? ?? ?? 45 33 C0 48 8B 42 ?? 48 8B 08 44 39 41 ?? 49 8B C9 74 ?? 41 8D 50 ?? E9 ?? ?? ?? ?? 33 D2 E9 ?? ?? ?? ?? CC CC CC CC CC CC 4C 89 44 24"
    )]
    public partial AtkComponentListItemRenderer* GetItemListRenderer
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo
    );

    /// <summary>
    ///     渲染商品列表第一列的商品，包括图标、名称、成本与数量输入框。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项节点数组。</param>
    [MemberFunction("4C 89 44 24 ?? 48 89 4C 24 ?? 53 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 8B 02")]
    public partial void RenderFirstItem
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodes
    );

    /// <summary>
    ///     渲染商品列表第二列的商品，包括图标、名称与成本。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 8B 02 48 8B F9")]
    public partial void RenderSecondItem
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodes
    );

    /// <summary>
    ///     刷新界面顶部的货币与职业筛选区域，并按启用位图重建职业下拉列表。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction
    (
        "40 56 41 57 48 83 EC ?? 48 8B F1 4D 8B F8 49 8D 48 ?? E8 ?? ?? ?? ?? 49 8D 4F ?? 89 86 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 8E ?? ?? ?? ?? 88 86 ?? ?? ?? ?? 48 85 C9 0F 84 ?? ?? ?? ?? 84 C0 0F 84 ?? ?? ?? ?? 4C 8B 01 0F B6 D0 48 89 5C 24 ?? 41 FF 50 ?? 49 8D 4F ?? E8 ?? ?? ?? ?? 48 39 86 ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 48 89 6C 24 ?? 49 8D 4F ?? 4C 89 74 24 ?? E8 ?? ?? ?? ?? 45 33 F6 48 89 86 ?? ?? ?? ?? 41 8B EE 48 89 7C 24 ?? BB ?? ?? ?? ?? 48 85 9E ?? ?? ?? ?? 74 ?? 8D 0C 6D ?? ?? ?? ?? 48 C1 E1 ?? 49 03 CF E8 ?? ?? ?? ?? 8D 0C 6D ?? ?? ?? ?? 4A 89 84 F6 ?? ?? ?? ?? 48 C1 E1 ?? 49 03 CF E8 ?? ?? ?? ?? 42 89 84 B6 ?? ?? ?? ?? 41 FF C6 FF C5 48 D1 C3 83 FD ?? 7C ?? 48 8B 8E ?? ?? ?? ?? 4C 8D 86 ?? ?? ?? ?? 41 B1 ?? 41 0F B7 D6 48 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 7C 24 ?? 48 8B 6C 24 ?? 44 89 B6 ?? ?? ?? ?? 4C 8B 74 24 ?? 48 8B 8E ?? ?? ?? ?? 48 8B 5C 24 ?? 48 85 C9 74 ?? 48 8B 01 0F B6 96 ?? ?? ?? ?? 48 83 C4 ?? 41 5F 5E 48 FF 60 ?? 48 83 C4 ?? 41 5F 5E C3 CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BB ?? ?? ?? ?? 48 8B F1 39 99 ?? ?? ?? ?? 76 ?? 66 90 8B C3 48 8D 3C 86 8B 8F ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B 8E ?? ?? ?? ?? 39 8F ?? ?? ?? ?? 74 ?? 0F B6 40 ?? 3B C1 74 ?? FF C3 3B 9E ?? ?? ?? ?? 72 ?? 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 48 8B 10 FF 52 ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 4C 8D 5C 24 ?? 49 8B 5B ?? 49 8B 73 ?? 49 8B E3 5F C3 48 8B 8E ?? ?? ?? ?? 8B D3 E8 ?? ?? ?? ?? BF ?? ?? ?? ?? 48 8D 44 24 ?? 8B CF 33 D2 66 90 89 10 89 50 ?? 48 8D 40 ?? 48 83 E9 ?? 75 ?? 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 48 63 C3 48 8D 4C 24 ?? 89 5C 24 ?? BA ?? ?? ?? ?? 8B 9C 86 ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 5C 24 ?? 8B D7 48 8B CE E8 ?? ?? ?? ?? 48 8D 5C 24 ?? 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BF ?? ?? ?? ?? 48 63 DA 48 8B F1 48 8D 44 24 ?? 8B CF 33 D2 0F 1F 40 ?? 66 66 0F 1F 84 00 ?? ?? ?? ?? 89 10 89 50 ?? 48 8D 40 ?? 48 83 E9 ?? 75 ?? 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 89 5C 24 ?? 48 8D 4C 24 ?? 8B 9C 9E ?? ?? ?? ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 5C 24 ?? 8B D7 48 8B CE E8 ?? ?? ?? ?? 48 8D 5C 24 ?? 66 66 66 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 4C 8D 5C 24 ?? 49 8B 5B ?? 49 8B 73 ?? 49 8B E3 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 4C 8B C1"
    )]
    public partial void RefreshHeader
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     按当前选中的商品行更新各列的材料图标与数量显示。
    /// </summary>
    [MemberFunction("40 53 57 41 55 48 83 EC ?? 48 63 C2")]
    public partial void RefreshSelection();

    /// <summary>
    ///     供商品列表取得列表项渲染器的回调接口。
    /// </summary>
    [GenerateInterop]
    [Inherits<ListComponentCallBackInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe partial struct ItemListCallback
    {
        /// <summary>
        ///     回调接口的持有者界面。
        /// </summary>
        [FieldOffset(8)]
        public AtkUnitBase* Owner;

        /// <summary>
        ///     取得指定列表项使用的渲染器。
        /// </summary>
        [FieldOffset(16)]
        public delegate* unmanaged<AddonShopExchangeItem*, AtkComponentListItemPopulator.ListItemInfo*, AtkComponentListItemRenderer*> GetItemRenderer;
    }

    /// <summary>
    ///     物品兑换商店中的一项商品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 88)]
    public unsafe struct ExchangeItem
    {
        /// <summary>
        ///     商品的 Item 表行 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     商品的图标 id。
        /// </summary>
        [FieldOffset(4)]
        public int ItemIconID;

        /// <summary>
        ///     商品名称。
        /// </summary>
        [FieldOffset(16)]
        public CStringPointer Name;

        /// <summary>
        ///     商品的备用名称文本。
        /// </summary>
        [FieldOffset(40)]
        public CStringPointer AlternateName;

        /// <summary>
        ///     商品的说明文本。
        /// </summary>
        [FieldOffset(48)]
        public CStringPointer Description;

        /// <summary>
        ///     由界面值数组写入的持有数量。
        /// </summary>
        [FieldOffset(56)]
        public uint OwnedCount;

        /// <summary>
        ///     商品容量被每次产出整除后的可兑换次数。
        /// </summary>
        [FieldOffset(60)]
        public ushort StackMax;

        /// <summary>
        ///     当前行的兑换数量，取该行各商品可兑换次数的最小值。
        /// </summary>
        [FieldOffset(64)]
        public uint Quantity;

        /// <summary>
        ///     当前行的可兑换次数上限，上限为 99。
        /// </summary>
        [FieldOffset(68)]
        public uint MaxQuantity;

        /// <summary>
        ///     由界面值数组写入的商品索引。
        /// </summary>
        [FieldOffset(72)]
        public int Index;

        /// <summary>
        ///     界面状态位，第 0 位与第 1 位分别由界面值数组写入。
        /// </summary>
        [FieldOffset(76)]
        public uint StateFlags;

        /// <summary>
        ///     客户端是否为该商品启用数量输入。
        /// </summary>
        [FieldOffset(80)]
        public bool CanSpecifyQuantity;

        /// <summary>
        ///     该商品的数量输入框已初始化。
        /// </summary>
        [FieldOffset(81)]
        public bool IsInitialized;

        /// <summary>
        ///     该商品标记为特殊商品。
        /// </summary>
        [FieldOffset(82)]
        public bool IsSpecialItem;
    }

    /// <summary>
    ///     商品所需的一项兑换材料。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 88)]
    public unsafe struct MaterialItem
    {
        /// <summary>
        ///     材料的 Item 表行 id，为 0 表示该项未使用。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     材料的图标 id。
        /// </summary>
        [FieldOffset(4)]
        public int ItemIconID;

        /// <summary>
        ///     材料名称。
        /// </summary>
        [FieldOffset(8)]
        public CStringPointer Name;

        /// <summary>
        ///     材料的补充文本。
        /// </summary>
        [FieldOffset(16)]
        public CStringPointer AdditionalText;

        /// <summary>
        ///     材料的说明文本。
        /// </summary>
        [FieldOffset(40)]
        public CStringPointer Description;

        /// <summary>
        ///     玩家持有的该材料数量，上限为 999。
        /// </summary>
        [FieldOffset(56)]
        public uint OwnedCount;

        /// <summary>
        ///     兑换一次所需的该材料数量。
        /// </summary>
        [FieldOffset(60)]
        public ushort RequiredCount;

        /// <summary>
        ///     客户端是否为该材料启用数量输入。
        /// </summary>
        [FieldOffset(80)]
        public bool CanSpecifyQuantity;

        /// <summary>
        ///     该材料标记为特殊物品。
        /// </summary>
        [FieldOffset(82)]
        public bool IsSpecialItem;
    }
}
