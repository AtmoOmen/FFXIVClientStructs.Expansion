using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     采集品交易界面，展示各职业类别下的可提交采集品，并支持选择物品与提交。
/// </summary>
[Addon("CollectablesShop")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 736)]
public unsafe partial struct AddonCollectablesShop
{
    /// <summary>
    ///     各职业类别对应的单选按钮，共 11 项。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray11<Pointer<AtkComponentRadioButton>> CategoryButtonsStorage;

    /// <summary>
    ///     职业类别树形列表。
    /// </summary>
    [FieldOffset(656)]
    public AtkComponentTreeList* CategoryList;

    /// <summary>
    ///     职业类别列表项的渲染器。
    /// </summary>
    [FieldOffset(664)]
    public AtkComponentListItemRenderer* CategoryItemRenderer;

    /// <summary>
    ///     物品列表。
    /// </summary>
    [FieldOffset(672)]
    public AtkComponentList* ItemList;

    /// <summary>
    ///     供物品列表获取列表项渲染器的回调接口。
    /// </summary>
    [FieldOffset(680)]
    public CollectablesShopListCallback CallbackInterface;

    /// <summary>
    ///     提交按钮。
    /// </summary>
    [FieldOffset(704)]
    public AtkComponentButton* SubmitButton;

    /// <summary>
    ///     界面布局类型：0 与 1 分别对应两套节点编号与列表列配置。
    /// </summary>
    [FieldOffset(712)]
    public int LayoutType;

    /// <summary>
    ///     当前选中类别的图标 id，用于物品列表项的图标显示。
    /// </summary>
    [FieldOffset(716)]
    public int CategoryIconID;

    /// <summary>
    ///     当前选中的职业类别索引，11 表示无选中类别。
    /// </summary>
    [FieldOffset(720)]
    public int SelectedCategoryIndex;

    /// <summary>
    ///     当前类别的奖励类型：0 表示普通货币，1 表示特殊货币。
    /// </summary>
    [FieldOffset(724)]
    public int RewardType;

    /// <summary>
    ///     界面状态位。
    /// </summary>
    [FieldOffset(728)]
    public StateFlags Flags;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 33 C9 48 8D 05 ?? ?? ?? ?? 48 89 03 0F 57 C0 0F 11 83 ?? ?? ?? ?? 33 C0 0F 11 83")]
    public partial AddonCollectablesShop* Ctor();

    /// <summary>
    ///     析构前重置虚表并销毁基类。
    /// </summary>
    [MemberFunction
    (
        "48 8D 05 ?? ?? ?? ?? 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 81 ?? ?? ?? ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 57 41 56"
    )]
    public partial void PrepareDestructor();

    /// <summary>
    ///     切换选中的职业类别。
    /// </summary>
    /// <param name="categoryIndex">类别索引。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B"
    )]
    public partial void SelectCategory
    (
        int categoryIndex
    );

    /// <summary>
    ///     向代理发送当前选中物品。
    /// </summary>
    /// <param name="itemIndex">物品索引。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B"
    )]
    public partial void NotifyItemSelected
    (
        int itemIndex
    );

    /// <summary>
    ///     向代理发送物品选择变更事件。
    /// </summary>
    /// <param name="itemIndex">物品索引。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F1 44 8B C3 8B FA 33 C9 0F 1F 40 ?? 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 49 83 E8 ?? 75 ?? 41 8D 50 ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? BA ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? 89 7C 24 ?? 8B D3 48 8B CE E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 0F 1F 40 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B"
    )]
    public partial void NotifyItemSelectedSecondary
    (
        int itemIndex
    );

    /// <summary>
    ///     按界面值重建类别树形列表并更新禁用状态。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("48 89 5C 24 ?? 89 54 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B F1")]
    public partial void RefreshCategoryList
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     刷新货币显示行并重建物品列表项标签。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否刷新成功。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B FA 8B 89 ?? ?? ?? ?? 85 C9 74 ?? 83 F9 ?? 75 ?? 48 8B CB")]
    public partial bool RefreshCurrencyRows
    (
        AtkValue* values
    );

    /// <summary>
    ///     按布局类型 0 刷新货币显示行。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("40 53 55 56 41 55 41 56 41 57 48 83 EC")]
    public partial void RefreshCurrencyRowLayout0
    (
        AtkValue* values
    );

    /// <summary>
    ///     按布局类型 1 刷新货币显示行。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("40 53 55 56 57 41 55 41 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B E9")]
    public partial void RefreshCurrencyRowLayout1
    (
        AtkValue* values
    );

    /// <summary>
    ///     渲染类别列表项的公共部分：类别图标、名称、星级与三段说明文本。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodeList">列表项节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 49 8B F0 49 8B 08 48 8B EA")]
    public partial void RenderListItemCommon
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodeList
    );

    /// <summary>
    ///     按布局类型 0 渲染类别列表项的货币与数量。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodeList">列表项节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 49 8B D8 48 8B FA E8")]
    public partial void RenderCategoryItemLayout0
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodeList
    );

    /// <summary>
    ///     按布局类型 1 渲染类别列表项的货币与数量。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodeList">列表项节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 49 8B F8 48 8B DA E8 ?? ?? ?? ?? 48 8B 4F")]
    public partial void RenderCategoryItemLayout1
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodeList
    );

    /// <summary>
    ///     渲染类别列表项的名称文本。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodeList">列表项节点数组。</param>
    [MemberFunction
        ("40 53 48 83 EC ?? 49 8B 08 48 8B DA E8 ?? ?? ?? ?? 48 8B 4B ?? 48 8B 51 ?? 48 8B C8 48 8B 12 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC 48 8B 42")]
    public partial void RenderCategoryName
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo,
        AtkResNode**                                nodeList
    );

    /// <summary>
    ///     取得物品列表项使用的渲染器。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <returns>列表项渲染器。</returns>
    [MemberFunction("48 8B 42 ?? 48 8B 10 80 3A")]
    public partial AtkComponentListItemRenderer* GetItemListRenderer
    (
        AtkComponentListItemPopulator.ListItemInfo* itemInfo
    );

    /// <summary>
    ///     渲染物品列表项的图标、名称与数量。
    /// </summary>
    /// <param name="itemIndex">列表项索引。</param>
    /// <param name="nodeList">列表项节点数组。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 66 83 B9 ?? ?? ?? ?? ?? 49 8B F0 8B EA")]
    public partial void RenderItemListItem
    (
        int          itemIndex,
        AtkResNode** nodeList
    );

    /// <summary>
    ///     界面内嵌的列表回调接口，供物品列表获取列表项渲染器。
    /// </summary>
    [GenerateInterop]
    [Inherits<ListComponentCallBackInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe partial struct CollectablesShopListCallback
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
        public delegate* unmanaged<AddonCollectablesShop*, AtkComponentListItemPopulator.ListItemInfo*, AtkComponentListItemRenderer*> GetItemRenderer;
    }

    /// <summary>
    ///     界面状态位。
    /// </summary>
    [Flags]
    public enum StateFlags : byte
    {
        /// <summary>
        ///     无待处理状态。
        /// </summary>
        None = 0,

        /// <summary>
        ///     物品列表需要重新获得焦点。
        /// </summary>
        FocusItemList = 1 << 0,

        /// <summary>
        ///     物品列表项正处于双击处理中。
        /// </summary>
        ItemDoubleClick = 1 << 1
    }
}
