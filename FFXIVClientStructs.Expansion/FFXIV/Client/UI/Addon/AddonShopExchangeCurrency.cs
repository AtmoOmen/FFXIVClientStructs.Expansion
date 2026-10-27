using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     货币兑换商店，按可收纳数量、持有货币及兑换成本计算数量。
/// </summary>
[Addon("ShopExchangeCurrency")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 15200)]
public unsafe partial struct AddonShopExchangeCurrency
{
    /// <summary>
    ///     显示持有货币数量的文本节点（节点 6）。
    /// </summary>
    [FieldOffset(568)]
    public AtkTextNode* CurrencyCountTextNode;

    /// <summary>
    ///     显示货币图标的图片节点（节点 5）。
    /// </summary>
    [FieldOffset(576)]
    public AtkImageNode* CurrencyIconNode;

    /// <summary>
    ///     商品分类单选按钮，共 4 项。
    /// </summary>
    [FieldOffset(584)]
    [FixedSizeArray]
    internal FixedSizeArray4<Pointer<AtkComponentRadioButton>> CategoryButtonStorage;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(616)]
    public ulong Unk268;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(624)]
    public ulong Unk270;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(632)]
    public ulong Unk278;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(640)]
    public ulong Unk280;

    /// <summary>
    ///     按职业显示商品的复选框（节点 12）。
    /// </summary>
    [FieldOffset(648)]
    public AtkComponentCheckBox* ClassJobCheckBox;

    /// <summary>
    ///     商品列表树（节点 20）。
    /// </summary>
    [FieldOffset(656)]
    public AtkComponentTreeList* ItemTreeList;

    /// <summary>
    ///     职业筛选下拉列表（节点 3）。
    /// </summary>
    [FieldOffset(664)]
    public AtkComponentDropDownList* ClassJobDropDown;

    /// <summary>
    ///     显示职业筛选结果的文本节点（节点 21）。
    /// </summary>
    [FieldOffset(672)]
    public AtkTextNode* ClassJobTextNode;

    /// <summary>
    ///     货币说明按钮（按钮 2）。
    /// </summary>
    [FieldOffset(680)]
    public AtkComponentButton* InfoButton;

    /// <summary>
    ///     复选框的原始文本，未勾选按职业筛选时显示在 <see cref="ClassJobTextNode" /> 上。
    /// </summary>
    [FieldOffset(688)]
    public Utf8String DefaultClassJobText;

    /// <summary>
    ///     当前选中的分类索引。
    /// </summary>
    [FieldOffset(792)]
    public byte SelectedCategoryIndex;

    /// <summary>
    ///     分类单选按钮数量，固定为 4。
    /// </summary>
    [FieldOffset(793)]
    public byte CategoryButtonCount;

    /// <summary>
    ///     由界面值数组第 0 项写入的分类标志字节。
    /// </summary>
    [FieldOffset(794)]
    public byte Unk31A;

    /// <summary>
    ///     商品数组，每行最多 2 项，共 122 项。
    /// </summary>
    [FieldOffset(800)]
    [FixedSizeArray]
    internal FixedSizeArray122<ExchangeItem> ItemsStorage;

    /// <summary>
    ///     商品列表每一行对应到 <see cref="Items" /> 的索引，共 61 行。
    /// </summary>
    [FieldOffset(8608)]
    [FixedSizeArray]
    internal FixedSizeArray61<uint> VisibleItemIndexStorage;

    /// <summary>
    ///     职业筛选下拉列表中各项的名称，共 35 项。
    /// </summary>
    [FieldOffset(8856)]
    [FixedSizeArray]
    internal FixedSizeArray35<CStringPointer> ClassJobNameStorage;

    /// <summary>
    ///     职业筛选下拉列表中各项对应的 ClassJob 表行 id，共 35 项。
    /// </summary>
    [FieldOffset(9136)]
    [FixedSizeArray]
    internal FixedSizeArray35<uint> ClassJobIDStorage;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(9264)]
    public ulong Unk2430;

    /// <summary>
    ///     客户端仅在构造时清零，其他函数未读写的保留字段。
    /// </summary>
    [FieldOffset(9272)]
    public uint Unk2438;

    /// <summary>
    ///     构造商品列表时使用的界面值暂存区，共 366 项，每项商品占 3 项。
    /// </summary>
    [FieldOffset(9280)]
    [FixedSizeArray]
    internal FixedSizeArray366<AtkValue> TreeListValueStorage;

    /// <summary>
    ///     供商品列表取得列表项渲染器的回调接口。
    /// </summary>
    [FieldOffset(15136)]
    public ItemListCallback CallbackInterface;

    /// <summary>
    ///     商品列表中一行的商品数量，取值 1 或 2。
    /// </summary>
    [FieldOffset(15160)]
    public uint ItemsPerRow;

    /// <summary>
    ///     玩家当前持有的货币总数。
    /// </summary>
    [FieldOffset(15164)]
    public uint TotalCurrency;

    /// <summary>
    ///     货币图标 id。
    /// </summary>
    [FieldOffset(15168)]
    public uint CurrencyIconID;

    /// <summary>
    ///     玩家当前的 ClassJob 表行 id，用于自动选中职业筛选下拉列表中的对应项。
    /// </summary>
    [FieldOffset(15172)]
    public uint PlayerClassJobID;

    /// <summary>
    ///     职业筛选下拉列表中的有效项数量。
    /// </summary>
    [FieldOffset(15176)]
    public uint ClassJobEntryCount;

    /// <summary>
    ///     职业筛选下拉列表中有效项的启用位图，第 n 位对应第 n 个职业。
    /// </summary>
    [FieldOffset(15184)]
    public ulong ClassJobEnableMask;

    /// <summary>
    ///     职业筛选下拉列表已启用。
    /// </summary>
    [FieldOffset(15192)]
    public bool IsClassJobSelectionEnabled;

    /// <summary>
    ///     按职业筛选的复选框已勾选。
    /// </summary>
    [FieldOffset(15193)]
    public bool IsClassJobCheckBoxChecked;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? 48 89 03 E8 ?? ?? ?? ?? 48 8D 83 ?? ?? ?? ?? 66 C7 83")]
    public partial AddonShopExchangeCurrency* Ctor();

    /// <summary>
    ///     拆离并隐藏本界面的悬浮提示后终结基类。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 0F B7 97 ?? ?? ?? ?? 41 B0 ?? 48 8B D8 48 8D 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 0F B7 97 ?? ?? ?? ?? 48 8D 8B ?? ?? ?? ?? 45 33 C0 E8 ?? ?? ?? ?? 48 8B CF 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 41 B9 ?? ?? ?? ?? 48 8D 15 ?? ?? ?? ?? 45 33 C0 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 44 8B F2"
    )]
    public partial void Deinitialize();

    /// <summary>
    ///     刷新商品、货币余额和可兑换数量。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>刷新是否完成。</returns>
    [MemberFunction("4C 89 44 24 ?? 53 55 56 57 41 56")]
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
        "4C 8B 89 ?? ?? ?? ?? 45 33 C0 48 8B 42 ?? 48 8B 08 44 39 41 ?? 49 8B C9 74 ?? 41 8D 50 ?? E9 ?? ?? ?? ?? 33 D2 E9 ?? ?? ?? ?? CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial AtkComponentListItemRenderer* GetItemListRenderer
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo
    );

    /// <summary>
    ///     渲染商品列表第一列的商品，包括图标、名称、成本、持有数量与数量输入框。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B 02 48 8B F1")]
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
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B 02 48 8B F9")]
    public partial void RenderSecondItem
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodes
    );

    /// <summary>
    ///     按启用位图重建职业筛选下拉列表的项，并把当前职业对应的项设为选中。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction
    (
        "40 56 41 57 48 83 EC ?? 48 8B F1 4D 8B F8 49 8D 48 ?? E8 ?? ?? ?? ?? 49 8D 4F ?? 89 86 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 8E ?? ?? ?? ?? 88 86 ?? ?? ?? ?? 48 85 C9 0F 84 ?? ?? ?? ?? 84 C0 0F 84 ?? ?? ?? ?? 4C 8B 01 0F B6 D0 48 89 5C 24 ?? 41 FF 50 ?? 49 8D 4F ?? E8 ?? ?? ?? ?? 48 39 86 ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 48 89 6C 24 ?? 49 8D 4F ?? 4C 89 74 24 ?? E8 ?? ?? ?? ?? 45 33 F6 48 89 86 ?? ?? ?? ?? 41 8B EE 48 89 7C 24 ?? BB ?? ?? ?? ?? 48 85 9E ?? ?? ?? ?? 74 ?? 8D 0C 6D ?? ?? ?? ?? 48 C1 E1 ?? 49 03 CF E8 ?? ?? ?? ?? 8D 0C 6D ?? ?? ?? ?? 4A 89 84 F6 ?? ?? ?? ?? 48 C1 E1 ?? 49 03 CF E8 ?? ?? ?? ?? 42 89 84 B6 ?? ?? ?? ?? 41 FF C6 FF C5 48 D1 C3 83 FD ?? 7C ?? 48 8B 8E ?? ?? ?? ?? 4C 8D 86 ?? ?? ?? ?? 41 B1 ?? 41 0F B7 D6 48 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 7C 24 ?? 48 8B 6C 24 ?? 44 89 B6 ?? ?? ?? ?? 4C 8B 74 24 ?? 48 8B 8E ?? ?? ?? ?? 48 8B 5C 24 ?? 48 85 C9 74 ?? 48 8B 01 0F B6 96 ?? ?? ?? ?? 48 83 C4 ?? 41 5F 5E 48 FF 60 ?? 48 83 C4 ?? 41 5F 5E C3 CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BB ?? ?? ?? ?? 48 8B F1 39 99 ?? ?? ?? ?? 76 ?? 66 90 8B C3 48 8D 3C 86 8B 8F ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B 8E ?? ?? ?? ?? 39 8F ?? ?? ?? ?? 74 ?? 0F B6 40 ?? 3B C1 74 ?? FF C3 3B 9E ?? ?? ?? ?? 72 ?? 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 48 8B 10 FF 52 ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 4C 8D 5C 24 ?? 49 8B 5B ?? 49 8B 73 ?? 49 8B E3 5F C3 48 8B 8E ?? ?? ?? ?? 8B D3 E8 ?? ?? ?? ?? BF ?? ?? ?? ?? 48 8D 44 24 ?? 8B CF 33 D2 66 90 89 10 89 50 ?? 48 8D 40 ?? 48 83 E9 ?? 75 ?? 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 48 63 C3 48 8D 4C 24 ?? 89 5C 24 ?? BA ?? ?? ?? ?? 8B 9C 86 ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 5C 24 ?? 8B D7 48 8B CE E8 ?? ?? ?? ?? 48 8D 5C 24 ?? 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BF ?? ?? ?? ?? 48 63 DA 48 8B F1 48 8D 44 24 ?? 8B CF 33 D2 0F 1F 40 ?? 66 66 0F 1F 84 00 ?? ?? ?? ?? 89 10 89 50 ?? 48 8D 40 ?? 48 83 E9 ?? 75 ?? 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 89 5C 24 ?? 48 8D 4C 24 ?? 8B 9C 9E ?? ?? ?? ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 5C 24 ?? 8B D7 48 8B CE E8 ?? ?? ?? ?? 48 8D 5C 24 ?? 66 66 66 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 4C 8D 5C 24 ?? 49 8B 5B ?? 49 8B 73 ?? 49 8B E3 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53"
    )]
    public partial void RefreshClassJobList
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     选中职业筛选下拉列表中的指定项，并把该项的 ClassJob 表行 id 回传给打开者。
    /// </summary>
    /// <param name="dropdownIndex">下拉列表项索引。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BF ?? ?? ?? ?? 48 63 DA 48 8B F1 48 8D 44 24 ?? 8B CF 33 D2 0F 1F 40 ?? 66 66 0F 1F 84 00 ?? ?? ?? ?? 89 10 89 50 ?? 48 8D 40 ?? 48 83 E9 ?? 75 ?? 8D 51 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 89 5C 24 ?? 48 8D 4C 24 ?? 8B 9C 9E ?? ?? ?? ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 5C 24 ?? 8B D7 48 8B CE E8 ?? ?? ?? ?? 48 8D 5C 24 ?? 66 66 66 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 4C 8D 5C 24 ?? 49 8B 5B ?? 49 8B 73 ?? 49 8B E3 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53"
    )]
    public partial void SelectClassJob
    (
        int dropdownIndex
    );

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
        public delegate* unmanaged<AddonShopExchangeCurrency*, AtkComponentListItemPopulator.ListItemInfo*, AtkComponentListItemRenderer*> GetItemRenderer;
    }

    /// <summary>
    ///     货币兑换商店中的一项商品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
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
        [FieldOffset(8)]
        public CStringPointer Name;

        /// <summary>
        ///     商品的备用名称文本，界面在 <see cref="HasAlternateName" /> 为 <see langword="true" /> 时作为悬浮提示显示。
        /// </summary>
        [FieldOffset(16)]
        public CStringPointer AlternateName;

        /// <summary>
        ///     兑换一件所需的主货币数量，界面显示时乘以兑换数量。
        /// </summary>
        [FieldOffset(24)]
        public uint CostPerItem;

        /// <summary>
        ///     由界面值数组写入并以数量形式显示的数值。
        /// </summary>
        [FieldOffset(28)]
        public uint DisplayQuantity;

        /// <summary>
        ///     兑换一件所需的副货币数量，为 0 时不参与可兑换数量的计算。
        /// </summary>
        [FieldOffset(32)]
        public uint SecondaryCostPerItem;

        /// <summary>
        ///     大于 1 时界面额外显示该倍率。
        /// </summary>
        [FieldOffset(36)]
        public uint SetSize;

        /// <summary>
        ///     界面与代理均未读写的保留字段。
        /// </summary>
        [FieldOffset(40)]
        public uint Unk28;

        /// <summary>
        ///     本次兑换的数量。
        /// </summary>
        [FieldOffset(44)]
        public uint Quantity;

        /// <summary>
        ///     按持有货币与成本算出的可兑换数量上限，上限为 99。
        /// </summary>
        [FieldOffset(48)]
        public uint MaxQuantity;

        /// <summary>
        ///     界面状态位，第 0 位与第 1 位分别由界面值数组写入。
        /// </summary>
        [FieldOffset(52)]
        public uint StateFlags;

        /// <summary>
        ///     客户端是否为该商品启用数量输入。
        /// </summary>
        [FieldOffset(56)]
        public bool CanSpecifyQuantity;

        /// <summary>
        ///     该商品的数量输入框已初始化。
        /// </summary>
        [FieldOffset(57)]
        public bool IsInitialized;

        /// <summary>
        ///     使用 <see cref="AlternateName" /> 作为悬浮提示文本。
        /// </summary>
        [FieldOffset(58)]
        public bool HasAlternateName;

        /// <summary>
        ///     持有货币不足以兑换该商品。
        /// </summary>
        [FieldOffset(59)]
        public bool IsCurrencyInsufficient;
    }
}
