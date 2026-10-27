using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     制作笔记界面，展示各工艺职业的配方分类树、配方列表与选中配方的材料详情。
/// </summary>
[Addon("RecipeNote")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 13440)]
public unsafe partial struct AddonRecipeNote
{
    /// <summary>
    ///     当前工艺职业的等级文本。
    /// </summary>
    [FieldOffset(568)]
    public AtkTextNode* CurrentJobLevel;

    /// <summary>
    ///     当前工艺职业的名称文本。
    /// </summary>
    [FieldOffset(576)]
    public AtkTextNode* CurrentJobName;

    /// <summary>
    ///     当前工艺职业的英文名称文本。
    /// </summary>
    [FieldOffset(584)]
    public AtkTextNode* CurrentJobNameEn;

    /// <summary>
    ///     当前工艺职业的图标。
    /// </summary>
    [FieldOffset(592)]
    public AtkImageNode* CurrentJobImage;

    /// <summary>
    ///     工艺职业图标的布局偏移，依次为横向偏移、纵向偏移与图标横向间距。
    /// </summary>
    [FieldOffset(600)]
    public short JobImageLayoutX;

    /// <summary>
    ///     工艺职业图标的布局纵向偏移。
    /// </summary>
    [FieldOffset(602)]
    public short JobImageLayoutY;

    /// <summary>
    ///     工艺职业图标的布局横向间距。
    /// </summary>
    [FieldOffset(604)]
    public short JobImageLayoutSpacing;

    /// <summary>
    ///     配方分类树与特性面板的父容器。
    /// </summary>
    [FieldOffset(608)]
    public AtkResNode* CategoryTreeContainer;

    /// <summary>
    ///     配方列表的父容器。
    /// </summary>
    [FieldOffset(616)]
    public AtkResNode* RecipeListContainer;

    /// <summary>
    ///     制作笔记主容器的背景节点。
    /// </summary>
    [FieldOffset(624)]
    public AtkResNode* BackgroundNode;

    /// <summary>
    ///     各工艺职业的页签单选按钮，共 9 项，最后一项固定隐藏。
    /// </summary>
    [FieldOffset(632)]
    [FixedSizeArray]
    internal FixedSizeArray9<Pointer<AtkComponentRadioButton>> TabButtonsStorage;

    /// <summary>
    ///     各工艺职业页签对应的工艺职业索引。
    /// </summary>
    [FieldOffset(704)]
    [FixedSizeArray]
    internal FixedSizeArray9<int> TabButtonCraftTypesStorage;

    /// <summary>
    ///     各工艺职业页签的图标节点，共 9 项。
    /// </summary>
    [FieldOffset(744)]
    [FixedSizeArray]
    internal FixedSizeArray9<Pointer<AtkImageNode>> TabButtonImagesStorage;

    /// <summary>
    ///     试作按钮。
    /// </summary>
    [FieldOffset(816)]
    public AtkComponentButton* TrialSynthesisButton;

    /// <summary>
    ///     简易制作按钮。
    /// </summary>
    [FieldOffset(824)]
    public AtkComponentButton* QuickSynthesisButton;

    /// <summary>
    ///     制作按钮。
    /// </summary>
    [FieldOffset(832)]
    public AtkComponentButton* SynthesizeButton;

    /// <summary>
    ///     上一分类按钮。
    /// </summary>
    [FieldOffset(840)]
    public AtkComponentButton* PreviousCategoryButton;

    /// <summary>
    ///     下一分类按钮。
    /// </summary>
    [FieldOffset(848)]
    public AtkComponentButton* NextCategoryButton;

    /// <summary>
    ///     上一页按钮。
    /// </summary>
    [FieldOffset(856)]
    public AtkComponentButton* PreviousPageButton;

    /// <summary>
    ///     下一页按钮。
    /// </summary>
    [FieldOffset(864)]
    public AtkComponentButton* NextPageButton;

    /// <summary>
    ///     筛选按钮。
    /// </summary>
    [FieldOffset(872)]
    public AtkComponentButton* RecipeFilterButton;

    /// <summary>
    ///     筛选按钮上的图标。
    /// </summary>
    [FieldOffset(880)]
    public AtkImageNode* RecipeFilterButtonImage;

    /// <summary>
    ///     分页文本，形如 1-32。
    /// </summary>
    [FieldOffset(888)]
    public AtkTextNode* PaginationText;

    /// <summary>
    ///     配方特性条目的容器组件，共 5 项。
    /// </summary>
    [FieldOffset(896)]
    [FixedSizeArray]
    internal FixedSizeArray5<Pointer<AtkComponentBase>> CharacteristicsComponentsStorage;

    /// <summary>
    ///     配方特性条目的文本节点，共 5 项。
    /// </summary>
    [FieldOffset(936)]
    [FixedSizeArray]
    internal FixedSizeArray5<Pointer<AtkTextNode>> CharacteristicsTextsStorage;

    /// <summary>
    ///     搜索框的提示文本。
    /// </summary>
    [FieldOffset(976)]
    public AtkTextNode* SearchHintText;

    /// <summary>
    ///     配方等级面板的容器节点。
    /// </summary>
    [FieldOffset(984)]
    public AtkResNode* RecipeLevelContainer;

    /// <summary>
    ///     配方等级字面量文本。
    /// </summary>
    [FieldOffset(992)]
    public AtkTextNode* RecipeLevelLiteral;

    /// <summary>
    ///     关闭界面的按钮。
    /// </summary>
    [FieldOffset(1000)]
    public AtkComponentButton* CloseButton;

    /// <summary>
    ///     关闭按钮的图标节点。
    /// </summary>
    [FieldOffset(1008)]
    public AtkResNode* CloseButtonNode;

    /// <summary>
    ///     备用关闭界面的按钮。
    /// </summary>
    [FieldOffset(1016)]
    public AtkComponentButton* CloseButtonAlternative;

    /// <summary>
    ///     备用关闭按钮的图标节点。
    /// </summary>
    [FieldOffset(1024)]
    public AtkResNode* CloseButtonAlternativeNode;

    /// <summary>
    ///     打开制作笔记设置界面的筛选按钮。
    /// </summary>
    [FieldOffset(1032)]
    public AtkComponentButton* FilterButton;

    /// <summary>
    ///     筛选按钮的图标节点。
    /// </summary>
    [FieldOffset(1040)]
    public AtkResNode* FilterButtonNode;

    /// <summary>
    ///     界面当前的焦点节点。
    /// </summary>
    [FieldOffset(1048)]
    public AtkResNode* FocusNode;

    /// <summary>
    ///     工艺职业列表组件。
    /// </summary>
    [FieldOffset(1056)]
    public AtkComponentList* CraftTypeList;

    /// <summary>
    ///     配方分类树组件。
    /// </summary>
    [FieldOffset(1064)]
    public AtkComponentTreeList* CategoryTreeList;

    /// <summary>
    ///     配方分类树的列表项渲染回调接口。
    /// </summary>
    [FieldOffset(1072)]
    public ListItemRendererInterface CategoryTreeRenderer;

    /// <summary>
    ///     配方列表组件。
    /// </summary>
    [FieldOffset(1096)]
    public AtkComponentTreeList* RecipeList;

    /// <summary>
    ///     配方列表的列表项渲染回调接口。
    /// </summary>
    [FieldOffset(1104)]
    public ListItemRendererInterface RecipeListRenderer;

    /// <summary>
    ///     配方分类下拉列表组件。
    /// </summary>
    [FieldOffset(1128)]
    public AtkComponentDropDownList* CategoryDropDown;

    /// <summary>
    ///     配方详情面板的根节点。
    /// </summary>
    [FieldOffset(1136)]
    public AtkResNode* DetailPanelRoot;

    /// <summary>
    ///     选中配方的图标按钮。
    /// </summary>
    [FieldOffset(1144)]
    public AtkComponentButton* SelectedRecipeIconButton;

    /// <summary>
    ///     选中配方的图标。
    /// </summary>
    [FieldOffset(1152)]
    public AtkComponentIcon* SelectedRecipeIcon;

    /// <summary>
    ///     选中配方的名称文本。
    /// </summary>
    [FieldOffset(1160)]
    public AtkTextNode* SelectedRecipeName;

    /// <summary>
    ///     选中配方的耐久文本。
    /// </summary>
    [FieldOffset(1168)]
    public AtkTextNode* SelectedRecipeDurability;

    /// <summary>
    ///     选中配方的难度文本。
    /// </summary>
    [FieldOffset(1176)]
    public AtkTextNode* SelectedRecipeDifficulty;

    /// <summary>
    ///     选中配方当前填充数量的文本。
    /// </summary>
    [FieldOffset(1184)]
    public AtkTextNode* SelectedRecipeQuantity;

    /// <summary>
    ///     选中配方的初始品质文本。
    /// </summary>
    [FieldOffset(1192)]
    public AtkTextNode* SelectedRecipeStartingQuality;

    /// <summary>
    ///     选中配方的初始品质上限提示文本。
    /// </summary>
    [FieldOffset(1200)]
    public AtkTextNode* SelectedRecipeHighestStartingQuality;

    /// <summary>
    ///     背包字面量文本。
    /// </summary>
    [FieldOffset(1208)]
    public AtkTextNode* BagLiteral;

    /// <summary>
    ///     选中配方成品在背包中的数量文本。
    /// </summary>
    [FieldOffset(1216)]
    public AtkTextNode* SelectedRecipeResultQuantityInInventory;

    /// <summary>
    ///     可制作数量字面量文本。
    /// </summary>
    [FieldOffset(1224)]
    public AtkTextNode* CraftableLiteral;

    /// <summary>
    ///     选中配方可制作数量的文本。
    /// </summary>
    [FieldOffset(1232)]
    public AtkTextNode* SelectedRecipeCraftableQuantity;

    /// <summary>
    ///     选中配方图标按钮的节点。
    /// </summary>
    [FieldOffset(1240)]
    public AtkResNode* SelectedRecipeIconButtonNode;

    /// <summary>
    ///     进度计算说明文本。
    /// </summary>
    [FieldOffset(1248)]
    public AtkTextNode* ProgressCalculationSentence;

    /// <summary>
    ///     品质计算说明文本。
    /// </summary>
    [FieldOffset(1256)]
    public AtkTextNode* QualityCalculationSentence;

    /// <summary>
    ///     普通品质材料填充按钮。
    /// </summary>
    [FieldOffset(1264)]
    public AtkComponentButton* NormalQualityFillButton;

    /// <summary>
    ///     优质材料填充按钮。
    /// </summary>
    [FieldOffset(1272)]
    public AtkComponentButton* HighQualityFillButton;

    /// <summary>
    ///     水晶材料槽，共 2 项，分别对应灵砂与晶簇。
    /// </summary>
    [FieldOffset(1280)]
    [FixedSizeArray]
    internal FixedSizeArray2<CrystalNodes> CrystalsStorage;

    /// <summary>
    ///     配方材料槽，共 6 项。
    /// </summary>
    [FieldOffset(1344)]
    [FixedSizeArray]
    internal FixedSizeArray6<IngredientNodes> IngredientsStorage;

    /// <summary>
    ///     配方详情面板的补间动画。
    /// </summary>
    [FieldOffset(2208)]
    public AtkSimpleTween DetailPanelTween;

    /// <summary>
    ///     材料填充数量的数值。
    /// </summary>
    [FieldOffset(2288)]
    public uint FillQuantity;

    /// <summary>
    ///     制作笔记界面自身。
    /// </summary>
    [FieldOffset(2296)]
    public AddonRecipeNote* Self;

    /// <summary>
    ///     界面所属的界面舞台，用于文本转换与提示框。
    /// </summary>
    [FieldOffset(2304)]
    public AtkStage* AtkStage;

    /// <summary>
    ///     配方搜索输入框。
    /// </summary>
    [FieldOffset(2312)]
    public AtkComponentTextInput* SearchTextInput;

    /// <summary>
    ///     制作笔记界面自身。
    /// </summary>
    [FieldOffset(2320)]
    public AddonRecipeNote* SelfPtr;

    /// <summary>
    ///     职业类别列表与配方分类树共用的名称缓存，共 66 项。
    /// </summary>
    [FieldOffset(2328)]
    [FixedSizeArray]
    internal FixedSizeArray66<TreeEntry> TreeEntriesStorage;

    /// <summary>
    ///     配方分类下拉列表的条目缓存，共 50 项。
    /// </summary>
    [FieldOffset(9720)]
    [FixedSizeArray]
    internal FixedSizeArray50<DropDownEntry> DropDownEntriesStorage;

    /// <summary>
    ///     配方列表项的渲染数据，共 100 项。
    /// </summary>
    [FieldOffset(10520)]
    [FixedSizeArray]
    internal FixedSizeArray100<RecipeListItemData> RecipeListItemsStorage;

    /// <summary>
    ///     界面右侧操作指引的显示状态。
    /// </summary>
    [FieldOffset(13320)]
    public uint OperationGuideState;

    /// <summary>
    ///     各工艺职业的名称缓存，共 9 项。
    /// </summary>
    [FieldOffset(13328)]
    [FixedSizeArray]
    internal FixedSizeArray9<Pointer<byte>> CraftTypeNamesStorage;

    /// <summary>
    ///     已解锁的工艺职业位掩码。
    /// </summary>
    [FieldOffset(13400)]
    public uint UnlockedCraftTypes;

    /// <summary>
    ///     当前选中的工艺职业索引。
    /// </summary>
    [FieldOffset(13404)]
    public int SelectedCraftType;

    /// <summary>
    ///     当前选中的配方笔记分类 id，初始为 -1。
    /// </summary>
    [FieldOffset(13408)]
    public int SelectedCategoryID;

    /// <summary>
    ///     当前选中的配方行 id。
    /// </summary>
    [FieldOffset(13412)]
    public int SelectedRecipeID;

    /// <summary>
    ///     配方列表当前使用的页码。
    /// </summary>
    [FieldOffset(13416)]
    public int PageIndex;

    /// <summary>
    ///     当前物品分类筛选的索引。
    /// </summary>
    [FieldOffset(13420)]
    public int ItemCategoryFilter;

    /// <summary>
    ///     当前配方使用的材料槽数量。
    /// </summary>
    [FieldOffset(13424)]
    public uint MaterialSlotCount;

    /// <summary>
    ///     材料槽已刷新完成时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(13428)]
    public bool IsMaterialSlotsInitialized;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(13429)]
    public bool Unk3475;

    /// <summary>
    ///     界面数据已初始化时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(13430)]
    public bool IsDataInitialized;

    /// <summary>
    ///     操作指引已初始化时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(13431)]
    public bool IsOperationGuideInitialized;

    /// <summary>
    ///     配方列表已填充完成时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(13432)]
    public bool IsRecipeListFilled;

    /// <summary>
    ///     正在刷新界面数据时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(13433)]
    public bool IsRefreshing;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(13434)]
    public ushort Unk347A;

    /// <summary>
    ///     关闭界面后需要重新聚焦的界面 id，无待聚焦界面时为 0。
    /// </summary>
    [FieldOffset(13436)]
    public uint FocusAddonID;

    /// <summary>
    ///     构造界面并初始化节点缓存与列表容器。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
        ("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 33 ED 48 8D 05 ?? ?? ?? ?? 48 89 03 48 8D 8B ?? ?? ?? ?? 48 8D 05")]
    public partial AddonRecipeNote* Ctor();


    /// <summary>
    ///     处理界面的 16 号刷新事件所携带的操作指引参数。
    /// </summary>
    /// <param name="valueCount">参数数量。</param>
    /// <param name="values">参数。</param>
    [VirtualFunction(50)]
    public partial void UpdateOperationGuide
    (
        uint      valueCount,
        AtkValue* values
    );


    /// <summary>
    ///     重建界面主区域的全部节点引用与按钮事件。
    /// </summary>
    /// <param name="addon">界面自身。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 48 8B DA 48 89 91")]
    public partial void BuildMainNodes
    (
        AddonRecipeNote* addon
    );

    /// <summary>
    ///     重建配方详情面板的节点引用与按钮事件。
    /// </summary>
    /// <param name="addon">界面自身。</param>
    /// <param name="atkStage">界面舞台。</param>
    [MemberFunction("48 8B C4 53 48 83 EC ?? 48 89 68 ?? 48 8B D9 48 89 70 ?? 48 89 78 ?? 48 8B FA")]
    public partial void BuildDetailPanelNodes
    (
        AddonRecipeNote* addon,
        AtkStage*        atkStage
    );

    /// <summary>
    ///     清空配方详情面板的文本、图标与提示框。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B E9 48 8D 15 ?? ?? ?? ?? 48 8B 49")]
    public partial void ResetDetailPanel();

    /// <summary>
    ///     刷新配方详情面板，载入图标、提示框与数量文本。
    /// </summary>
    /// <param name="refreshFlags">刷新内容位掩码。</param>
    /// <param name="value">数值来源集合。</param>
    /// <param name="stringValue">文本来源集合。</param>
    /// <param name="index">配方在列表中的索引。</param>
    /// <returns>面板已刷新时为 <see langword="true" />。</returns>
    [MemberFunction("4C 89 4C 24 ?? 89 54 24 ?? 53 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 44 8B E2")]
    public partial bool RefreshDetailPanel
    (
        byte  refreshFlags,
        void* value,
        void* stringValue,
        int   index
    );

    /// <summary>
    ///     把材料填充数量写入详情面板的数量文本。
    /// </summary>
    [MemberFunction("48 83 EC ?? 48 8B 81 ?? ?? ?? ?? 48 85 C0 74 ?? 8B 91")]
    public partial void UpdateFillQuantityText();

    /// <summary>
    ///     刷新职业类别列表、配方分类树与配方列表。
    /// </summary>
    /// <param name="numberArray">界面数值数组。</param>
    /// <param name="stringArray">界面字符串数组。</param>
    /// <param name="reserved">保留参数。</param>
    [MemberFunction
        ("40 55 53 57 41 56 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 89 B4 24")]
    public partial void RefreshAllLists
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray,
        nint             reserved
    );

    /// <summary>
    ///     处理职业类别列表项的点击。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 33 D2 49 8B C8 E8 ?? ?? ?? ?? 48 8B 8B ?? ?? ?? ?? 48 8B 01")]
    public partial void OnCraftTypeListItemClick
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     处理配方分类树项的选中。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 55 53 56 57 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B F9")]
    public partial void OnCategoryItemClick
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     处理配方列表项的点击。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 53 56 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B C2 48 8B F9 48 8B C8 33 D2")]
    public partial void OnRecipeListItemClick
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     处理材料槽的点击，打开材料选择菜单。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction
    (
        "40 53 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B DA 48 8B F9 48 8B CB 33 D2 E8 ?? ?? ?? ?? 48 8B 4B ?? 48 85 C9 0F 84 ?? ?? ?? ?? 48 89 B4 24 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B F0 48 85 C0 0F 84 ?? ?? ?? ?? 48 8B 88"
    )]
    public partial void OnIngredientClick
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     处理制作按钮的点击。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction
    (
        "40 53 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B DA 48 8B F9 48 8B CB 33 D2 E8 ?? ?? ?? ?? 48 8B 4B ?? 48 85 C9 0F 84 ?? ?? ?? ?? 48 89 B4 24 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B F0 48 85 C0 0F 84 ?? ?? ?? ?? 48 3B B7"
    )]
    public partial void OnSynthesizeClick
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     切换配方列表到上一页或下一页，仅在对应翻页按钮可用时生效。
    /// </summary>
    /// <param name="nextPage">为 <see langword="true" /> 时翻到下一页，否则翻到上一页。</param>
    /// <returns>翻页请求已发出时返回 1。</returns>
    [MemberFunction
    (
        "48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B F9 84 D2 74 ?? 48 8B 89 ?? ?? ?? ?? 48 85 C9 0F 84 ?? ?? ?? ?? 48 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 0F 84 ?? ?? ?? ?? BE ?? ?? ?? ?? EB ?? 48 8B 89 ?? ?? ?? ?? 48 85 C9 0F 84 ?? ?? ?? ?? 48 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 0F 84 ?? ?? ?? ?? BE ?? ?? ?? ?? 48 89 9C 24 ?? ?? ?? ?? 48 8D 44 24 ?? BB ?? ?? ?? ?? 33 C9 8B D3"
    )]
    public partial byte ChangePage
    (
        bool nextPage
    );

    /// <summary>
    ///     向游戏发出关闭制作笔记界面的请求。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F9 8B D3 33 C9 66 0F 1F 44 00 ?? 89 08 89 48 ?? 48 8D 40 ?? 48 83 EA ?? 75 ?? BA ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? C7 44 24 ?? ?? ?? ?? ?? 8B D3 48 8B CF E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 66 0F 1F 44 00 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 48 8B 9C 24 ?? ?? ?? ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B F9"
    )]
    public partial void RequestClose();

    /// <summary>
    ///     向游戏发出打开制作笔记设置界面的请求，并附带当前物品分类筛选是否为空。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B F9 48 8D 44 24")]
    public partial void RequestOpenSettings();

    /// <summary>
    ///     为配方分类树取得指定列表项使用的渲染器。
    /// </summary>
    /// <param name="addon">界面自身。</param>
    /// <param name="info">列表项信息。</param>
    /// <returns>列表项渲染器。</returns>
    [MemberFunction
        ("48 8B 42 ?? 45 33 C0 4C 8B 89 ?? ?? ?? ?? 48 8B 08 41 8D 50 ?? 44 39 41 ?? 49 8B C9 74 ?? 41 8D 50 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC 48 8B 42")]
    public partial AtkComponentListItemRenderer* GetCategoryTreeItemRenderer
    (
        AddonRecipeNote*                            addon,
        AtkComponentListItemPopulator.ListItemInfo* info
    );

    /// <summary>
    ///     为配方列表取得指定列表项使用的渲染器。
    /// </summary>
    /// <param name="addon">界面自身。</param>
    /// <param name="info">列表项信息。</param>
    /// <returns>列表项渲染器。</returns>
    [MemberFunction
    (
        "48 8B 42 ?? 45 33 C0 4C 8B 89 ?? ?? ?? ?? 48 8B 08 41 8D 50 ?? 44 39 41 ?? 49 8B C9 74 ?? 41 8D 50 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 8B C2"
    )]
    public partial AtkComponentListItemRenderer* GetRecipeListItemRenderer
    (
        AddonRecipeNote*                            addon,
        AtkComponentListItemPopulator.ListItemInfo* info
    );

    /// <summary>
    ///     按索引选择工艺职业并刷新界面。
    /// </summary>
    /// <param name="craftType">工艺职业索引。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 8B F2 48 8B F9 39 91")]
    public partial void SelectCraftType
    (
        int craftType
    );

    /// <summary>
    ///     恢复上次选中的工艺职业并刷新界面。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 80 B9 ?? ?? ?? ?? ?? 48 8B F9 0F 85 ?? ?? ?? ?? 48 8B 89")]
    public partial void SelectStoredCraftType();

    /// <summary>
    ///     按分类 id 在配方分类树中选中对应节点。
    /// </summary>
    /// <param name="categoryID">配方笔记分类 id。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 63 FA 48 8B D9 48 8B 89")]
    public partial void SelectCategory
    (
        int categoryID
    );

    /// <summary>
    ///     隐藏并清空两个水晶材料槽的节点与提示框。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B E9 48 8D 99 ?? ?? ?? ?? BE")]
    public partial void ClearCrystalSlots();

    /// <summary>
    ///     卸载配方详情面板上的成品图标与提示框。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 85 C9 74 ?? E8 ?? ?? ?? ?? 48 8B BB")]
    public partial void UnloadDetailPanelIcon();

    /// <summary>
    ///     为全部材料槽的数量增加按钮注册点击事件，并写入事件用户数据。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 45 33 FF 48 8D 99")]
    public partial void AddIngredientButtonEvents
    (
        AtkEventType eventType,
        uint         eventParam
    );

    /// <summary>
    ///     移除全部材料槽数量增加按钮的点击事件。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 41 8B E8 48 8D 99")]
    public partial void RemoveIngredientButtonEvents
    (
        AtkEventType eventType,
        uint         eventParam
    );

    /// <summary>
    ///     重置两个水晶材料槽的可见性，并在索引有效时清空数量文本。
    /// </summary>
    /// <param name="visibleCount">要保持可用的水晶槽数量。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B EA 48 8D 99 ?? ?? ?? ?? 4C 8B F1 33 F6")]
    public partial void ResetCrystalSlots
    (
        int visibleCount
    );

    /// <summary>
    ///     重置全部材料槽的可见性、提示框与显示状态。
    /// </summary>
    /// <param name="visibleCount">要保持可见的材料槽数量。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 41 56 41 57 48 83 EC ?? 45 33 FF 48 89 7C 24")]
    public partial void ResetIngredientSlots
    (
        uint visibleCount
    );

    /// <summary>
    ///     设置指定材料槽的光标导航关系，使图标按钮与两个数量增加按钮可以互相切换。
    /// </summary>
    /// <param name="ingredientIndex">材料槽索引。</param>
    [MemberFunction("40 55 57 48 83 EC ?? 48 63 FA")]
    public partial void UpdateIngredientCursorNavigation
    (
        int ingredientIndex
    );

    /// <summary>
    ///     清空配方详情面板的全部节点引用与缓存。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8D 05 ?? ?? ?? ?? 48 89 39")]
    public partial void ResetDetailPanelState();

    /// <summary>
    ///     设置制作、简易制作与试作三个按钮的可用状态。
    /// </summary>
    /// <param name="enabled">为 <see langword="true" /> 时可用。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 0F B6 FA 48 8B 89 ?? ?? ?? ?? 48 8B 01 FF 50 ?? 48 8B 8B ?? ?? ?? ?? 40 0F B6 D7 48 8B 01 FF 50")]
    public partial void SetSynthesisButtonsEnabled
    (
        byte enabled
    );

    /// <summary>
    ///     设置当前工艺职业的名称、英文名、图标与职业图标。
    /// </summary>
    /// <param name="name">职业名称。</param>
    /// <param name="iconID">职业图标 id。</param>
    /// <param name="nameEn">职业英文名称。</param>
    /// <param name="jobName">职业类别名称。</param>
    [MemberFunction("48 8B C4 48 89 48 ?? 53 55 57 48 83 EC")]
    public partial void SetCurrentJobDisplay
    (
        CStringPointer name,
        uint           iconID,
        CStringPointer nameEn,
        CStringPointer jobName
    );

    /// <summary>
    ///     初始化九个工艺职业页签按钮的事件、选中状态与动画，并返回选中页签的节点。
    /// </summary>
    /// <param name="selectedIndex">选中的页签索引。</param>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <returns>选中页签按钮的节点，未选中任何页签时为 0。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 89 54 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 33 ED")]
    public partial AtkResNode* InitTabButtons
    (
        int          selectedIndex,
        AtkEventType eventType,
        uint         eventParam
    );

    /// <summary>
    ///     移除九个工艺职业页签按钮的点击事件。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B EA 48 8D 59")]
    public partial void RemoveTabButtonEvents
    (
        AtkEventType eventType
    );

    /// <summary>
    ///     从页签按钮节点取得其对应的工艺职业索引。
    /// </summary>
    /// <param name="node">页签按钮节点。</param>
    /// <returns>工艺职业索引，节点不是单选按钮时返回 -1。</returns>
    [MemberFunction("48 83 EC ?? 48 8B CA E8 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B 80")]
    public partial int GetTabCraftType
    (
        AtkResNode* node
    );

    /// <summary>
    ///     按位掩码设置前八个工艺职业页签的可用状态。
    /// </summary>
    /// <param name="craftTypeMask">工艺职业位掩码。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? BB ?? ?? ?? ?? 48 8D 79")]
    public partial void SetTabButtonsEnabled
    (
        int craftTypeMask
    );

    /// <summary>
    ///     从界面数值数组依次为前八个工艺职业页签载入图标。
    /// </summary>
    /// <param name="values">界面参数数组。</param>
    /// <param name="startIndex">起始参数索引。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 41 56 48 83 EC ?? 41 8B E8")]
    public partial void LoadTabButtonIcons
    (
        AtkValue* values,
        uint      startIndex
    );

    /// <summary>
    ///     清空配方详情面板的节点缓存。
    /// </summary>
    [MemberFunction
    (
        "48 81 C1 ?? ?? ?? ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9"
    )]
    public partial void ClearDetailPanel();

    /// <summary>
    ///     销毁界面的列表渲染回调、补间动画与字符串缓存。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F1 48 89 01 48 8D 99 ?? ?? ?? ?? BF ?? ?? ?? ?? 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8D 8E ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 8E ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8D 05"
    )]
    public partial void Destructor();

    /// <summary>
    ///     把焦点设置到指定节点。
    /// </summary>
    /// <param name="node">目标节点。</param>
    /// <returns>设置成功时返回非零值。</returns>
    [MemberFunction("45 33 C9 41 B0 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC E9")]
    public partial byte SetFocusToNode
    (
        AtkResNode* node
    );

    /// <summary>
    ///     从被点击的页签节点取得工艺职业索引并切换工艺职业，索引为 8 时恢复上次选择。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B DA 48 8B F9 48 8B CB 33 D2")]
    public partial void SelectCraftTypeFromNode
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     恢复上次选中的工艺职业。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 56"
    )]
    public partial void SelectStoredCraftTypeForNode
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     处理工艺职业列表项的选中，切换选中分类并通知游戏。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B C2 48 8B F1 48 8B C8 33 D2 49 8B D8 E8 ?? ?? ?? ?? 48 63 43")]
    public partial void OnCraftTypeListItemSelect
    (
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     工艺职业列表获得焦点时更新操作指引。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 0F 85 ?? ?? ?? ?? 48 8B 8B")]
    public partial void OnCraftTypeListFocus
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     处理配方分类树项的选中，切换选中分类并通知游戏。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B C2 48 8B F1 48 8B C8 33 D2 49 8B D8 E8 ?? ?? ?? ?? 80 BE")]
    public partial void OnCategoryItemSelect
    (
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );

    /// <summary>
    ///     配方分类树获得焦点时更新操作指引。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>操作指引已更新时返回非零值。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 48 8B 8B")]
    public partial byte OnCategoryTreeFocus
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     处理配方分类树项的鼠标移入，悬停到对应分类。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 53 55 56 57 41 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B C2 48 8B F1")]
    public partial void OnCategoryListItemRollOver
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理配方分类树项的鼠标移出，清除操作指引并恢复上次选择。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>恢复结果。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? B2 ?? C6 83")]
    public partial nint OnCategoryListItemRollOut
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     处理配方分类树项的高亮，展开其父分组并切换分类。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 53 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B C2")]
    public partial void OnCategoryListItemHighlight
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理配方列表项的双击，向游戏请求打开对应配方。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 0F 85 ?? ?? ?? ?? 48 89 7C 24")]
    public partial void OnRecipeListItemDoubleClick
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     处理配方列表项的鼠标移入，显示配方成品提示框。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B C2")]
    public partial void OnRecipeListItemRollOver
    (
        AtkEvent* atkEvent,
        int*      atkEventData
    );

    /// <summary>
    ///     处理配方列表项的鼠标移出，移除配方成品提示框。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>移除结果。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 33 D2 48 8D 88")]
    public partial nint OnRecipeListItemRollOut
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     在配方列表拖动时高亮可作为目标的材料槽。
    /// </summary>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    /// <param name="isHighlighted">为 <see langword="true" /> 时高亮，否则取消高亮。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B C2 48 8B F9")]
    public partial void OnRecipeListItemDragDrop
    (
        int       eventParam,
        AtkEvent* atkEvent,
        nint      atkEventData,
        bool      isHighlighted
    );

    /// <summary>
    ///     处理材料槽的鼠标移出，停止数量文本的闪烁动画。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="quality">品质标志：0 表示普通品质，1 表示优质品质。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B C2 48 8B F1 48 8B C8 33 D2 E8")]
    public partial void OnIngredientMouseOut
    (
        AtkEvent* atkEvent,
        int       quality
    );

    /// <summary>
    ///     处理关闭按钮的点击，收起焦点节点的提示并通知游戏关闭界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    /// <returns>关闭请求结果。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B FA 48 8B D9 48 8B 91 ?? ?? ?? ?? 48 85 D2 74 ?? 45 33 C9 41 B0 ?? E8 ?? ?? ?? ?? 33 D2 48 8B CF E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 57 48 83 EC ?? 48 8B FA 48 8B D9 48 8B 91 ?? ?? ?? ?? 48 85 D2 74 ?? 45 33 C9 41 B0 ?? E8 ?? ?? ?? ?? 33 D2 48 8B CF E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial byte OnCloseButtonClick
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理备用关闭按钮的点击，收起焦点节点的提示并通知游戏关闭界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    /// <returns>关闭请求结果。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B FA 48 8B D9 48 8B 91 ?? ?? ?? ?? 48 85 D2 74 ?? 45 33 C9 41 B0 ?? E8 ?? ?? ?? ?? 33 D2 48 8B CF E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 57 48 83 EC ?? 48 8B FA 48 8B D9 48 8B 91 ?? ?? ?? ?? 48 85 D2 74 ?? 45 33 C9 41 B0 ?? E8 ?? ?? ?? ?? 33 D2 48 8B CF E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 40 55"
    )]
    public partial byte OnCloseButtonAlternativeClick
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理筛选按钮的点击，通知游戏打开制作笔记设置界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    /// <returns>设置界面请求结果。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B FA 48 8B D9 48 8B 91 ?? ?? ?? ?? 48 85 D2 74 ?? 45 33 C9 41 B0 ?? E8 ?? ?? ?? ?? 33 D2 48 8B CF E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 8B 5C 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 40 55"
    )]
    public partial byte OnFilterButtonClick
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理材料槽数量按钮的点击，向游戏请求选择该材料。
    /// </summary>
    /// <param name="ingredientIndex">材料槽索引。</param>
    /// <returns>请求已发出时返回 1。</returns>
    [MemberFunction("40 55 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 8B EA 48 8B F9 83 FA ?? 0F 83")]
    public partial byte OnIngredientQuantityClick
    (
        uint ingredientIndex
    );

    /// <summary>
    ///     处理配方成品图标按钮的点击，向游戏请求打开配方笔记。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction
    (
        "40 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 41 80 78 ?? ?? 48 8B C2 48 8B F9 0F 85 ?? ?? ?? ?? 33 D2 48 89 9C 24 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 8B D3 33 C9 66 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 48 83 EA ?? 75 ?? BA ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? C7 44 24 ?? ?? ?? ?? ?? 8B D3 48 8B CF E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 66 0F 1F 44 00 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 9C 24 ?? ?? ?? ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 74 24"
    )]
    public partial void OnRecipeIconButtonClick
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理材料填充按钮的点击，向游戏请求按指定品质填充材料。
    /// </summary>
    /// <param name="quality">品质标志：0 表示普通品质，1 表示优质品质。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 41 80 79 ?? ?? 8B FA 48 8B F1 0F 85 ?? ?? ?? ?? 33 D2 48 89 9C 24")]
    public partial void OnFillButtonClick
    (
        int       quality,
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理配方成品图标的焦点进入，向游戏请求打开配方笔记。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction
    (
        "40 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 41 80 78 ?? ?? 48 8B C2 48 8B F9 0F 85 ?? ?? ?? ?? 33 D2 48 89 9C 24 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 8B D3 33 C9 66 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 48 83 EA ?? 75 ?? BA ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? C7 44 24 ?? ?? ?? ?? ?? 8B D3 48 8B CF E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 66 0F 1F 44 00 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 9C 24 ?? ?? ?? ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 55"
    )]
    public partial void OnRecipeIconFocus
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理材料槽数量按钮的点击事件，根据按钮的事件用户数据取得材料索引与品质。
    /// </summary>
    /// <param name="eventParam">事件参数。</param>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("40 55 53 56 57 41 54 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 44 8B E2")]
    public partial void OnIngredientButtonClick
    (
        uint      eventParam,
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     处理配方分类下拉列表的高亮项，切换分类树中对应节点的选中状态。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    /// <returns>处理结果。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B C2 48 8B F9 48 8B C8 33 D2 49 8B D8 E8 ?? ?? ?? ?? 8B 53")]
    public partial byte OnCategoryDropDownChanged
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     沿配方分类树向前滚动一项。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>滚动结果。</returns>
    [MemberFunction
        ("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 40 53")]
    public partial nint ScrollCategoryBackward
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     沿配方分类树向后滚动一项。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>滚动结果。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial nint ScrollCategoryForward
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     向游戏请求打开配方树界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B C2 48 8B F9 48 8B C8 33 D2 E8 ?? ?? ?? ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 8B D3 33 C9 66 0F 1F 84 00 ?? ?? ?? ?? 89 08 89 48 ?? 48 8D 40 ?? 48 83 EA ?? 75 ?? BA ?? ?? ?? ?? 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 4C 8D 44 24 ?? C7 44 24 ?? ?? ?? ?? ?? 8B D3 48 8B CF E8 ?? ?? ?? ?? 48 8D 7C 24 ?? 66 0F 1F 44 00 ?? 48 83 EF ?? 48 8B CF E8 ?? ?? ?? ?? 48 83 EB ?? 75 ?? 48 8B 4C 24 ?? 48 33 CC E8 ?? ?? ?? ?? 48 8B 9C 24 ?? ?? ?? ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B C2"
    )]
    public partial void OnOpenRecipeTree
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     切换到下一个配方分类分页。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>切换结果。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? B2 ?? 48 8B CB")]
    public partial nint OnNextCategoryPage
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     切换到上一个配方分类分页。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>切换结果。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 33 D2 48 8B CB")]
    public partial nint OnPreviousCategoryPage
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     请求关闭界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>关闭请求结果。</returns>
    [MemberFunction
        ("40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53")]
    public partial nint OnCloseRequest
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     请求打开制作笔记设置界面。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <returns>设置界面请求结果。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B C2 48 8B D9 48 8B C8 33 D2 E8 ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 57"
    )]
    public partial nint OnOpenSettingsRequest
    (
        AtkEvent* atkEvent
    );

    /// <summary>
    ///     开始把配方列表项拖放到材料槽。
    /// </summary>
    /// <param name="atkEvent">事件对象。</param>
    /// <param name="atkEventData">事件数据。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B C2 48 8B F9 48 8B C8 33 D2 49 8B D8 E8 ?? ?? ?? ?? 48 8B 8F")]
    public partial void OnRecipeListItemDragDropBegin
    (
        AtkEvent* atkEvent,
        nint      atkEventData
    );

    /// <summary>
    ///     按偏移量切换配方分类下拉列表的选中项。
    /// </summary>
    /// <param name="offset">相对当前选中项的偏移量。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 8B F2 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 83 B9")]
    public partial void ScrollCategoryDropDown
    (
        int offset
    );

    /// <summary>
    ///     按索引选中配方分类下拉列表的项并刷新配方列表。
    /// </summary>
    /// <param name="index">下拉列表项的索引。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? BB ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B F9 44 8B C3")]
    public partial void SelectCategoryDropDownItem
    (
        int index
    );

    /// <summary>
    ///     为配方分类树项写入名称文本。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项的节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 42 ?? 48 8B F1 49 8B 08 48 8B 10 48 63 5A ?? E8")]
    public partial void RenderCategoryTreeItemName
    (
        nint         itemInfo,
        AtkResNode** nodes
    );

    /// <summary>
    ///     为配方分类树项写入名称文本并按分类状态播放动画。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项的节点数组。</param>
    /// <returns>动画播放结果。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 42 ?? 49 8B F8 48 8B 10")]
    public partial double RenderCategoryTreeItemNameWithTimeline
    (
        nint itemInfo,
        nint nodes
    );

    /// <summary>
    ///     为配方分类树项写入等级文本。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项的节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 42 ?? 48 8B D9 49 8B 08")]
    public partial void RenderCategoryTreeItemLevel
    (
        nint         itemInfo,
        AtkResNode** nodes
    );

    /// <summary>
    ///     为配方列表项写入名称、等级与星级，并载入成品图标与各项动画。
    /// </summary>
    /// <param name="itemInfo">列表项信息。</param>
    /// <param name="nodes">列表项的节点数组。</param>
    /// <returns>最后一个节点的动画播放结果。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B 42 ?? 48 8D B9")]
    public partial double RenderRecipeListItem
    (
        nint itemInfo,
        nint nodes
    );

    /// <summary>
    ///     为配方分类下拉列表项写入名称与数量文本。
    /// </summary>
    /// <param name="index">下拉列表项的索引。</param>
    /// <param name="nodes">列表项的节点数组。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 8B DA 49 8B F8 48 C1 E3")]
    public partial void RenderCategoryDropDownItem
    (
        uint         index,
        AtkResNode** nodes
    );

    /// <summary>
    ///     取得指定分类树项显示的名称。
    /// </summary>
    /// <param name="index">分类树项的索引。</param>
    /// <param name="buffer">接收名称的缓冲。</param>
    /// <returns>名称所在的缓冲。</returns>
    [MemberFunction("40 53 48 83 EC ?? 8B C2 48 81 C1")]
    public partial nint GetTreeEntryName
    (
        uint index,
        nint buffer
    );

    /// <summary>
    ///     搜索输入框的回调，处理文本提交与搜索框关闭。
    /// </summary>
    /// <param name="eventKind">回调事件类型。</param>
    /// <param name="text">输入的文本。</param>
    /// <returns>固定返回 0。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 85 D2 74 ?? 83 FA ?? 75 ?? 80 B9")]
    public partial nint OnSearchInput
    (
        int  eventKind,
        nint text
    );

    /// <summary>
    ///     提交搜索框中的关键字并发出配方搜索请求。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? BE ?? ?? ?? ?? 48 8D 44 24 ?? 48 8B D9"
    )]
    public partial void SubmitSearch();

    /// <summary>
    ///     通知游戏关闭当前界面。
    /// </summary>
    /// <returns>关闭请求结果。</returns>
    [MemberFunction("BA ?? ?? ?? ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC 44 8B 89")]
    public partial byte OnCloseAction();

    /// <summary>
    ///     按界面状态更新右侧操作指引。
    /// </summary>
    /// <param name="state">操作指引状态。</param>
    /// <param name="useFocusNode">为 <see langword="true" /> 时以当前焦点节点为指引目标。</param>
    [MemberFunction("40 53 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 0F 85 ?? ?? ?? ?? 48 89 7C 24 ?? 33 FF")]
    public partial void UpdateOperationGuideForState
    (
        int  state,
        nint useFocusNode
    );

    /// <summary>
    ///     按参数设置或清除制作操作指引。
    /// </summary>
    /// <param name="show">为 <see langword="true" /> 时设置制作指引，否则恢复默认指引。</param>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 84 D2 0F 84 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 48 8B 10 48 8B 42 ?? 83 78 ?? ?? 0F 84 ?? ?? ?? ?? 4C 8B 8B ?? ?? ?? ?? 4D 85 C9 74"
    )]
    public partial void UpdateSynthesisOperationGuide
    (
        bool show
    );

    /// <summary>
    ///     根据当前焦点所在的区域更新右侧操作指引。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 55 48 8B EC 48 83 EC ?? 48 8B D9 E8")]
    public partial void UpdateOperationGuideFromFocus();

    /// <summary>
    ///     展开配方分类树中当前选中项的父分组。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 48 8B 89 ?? ?? ?? ?? 8B 99")]
    public partial void ExpandSelectedCategoryGroup();

    /// <summary>
    ///     判断配方详情面板的根节点是否可见。
    /// </summary>
    /// <returns>根节点及其全部父节点均可见时返回 1。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 99 ?? ?? ?? ?? 48 85 DB 74 ?? 48 8B CB")]
    public partial byte IsDetailPanelVisible();

    /// <summary>
    ///     水晶材料槽的节点集合。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct CrystalNodes
    {
        /// <summary>
        ///     水晶图标按钮。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentButton* Button;

        /// <summary>
        ///     水晶图标。
        /// </summary>
        [FieldOffset(8)]
        public AtkImageNode* Image;

        /// <summary>
        ///     制作所需数量文本。
        /// </summary>
        [FieldOffset(16)]
        public AtkTextNode* QuantityRequiredForCraft;

        /// <summary>
        ///     背包中的持有数量文本。
        /// </summary>
        [FieldOffset(24)]
        public AtkTextNode* QuantityInInventory;
    }

    /// <summary>
    ///     配方材料槽的节点集合。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 144)]
    public struct IngredientNodes
    {
        /// <summary>
        ///     材料槽的组件。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentBase* Component;

        /// <summary>
        ///     材料槽组件的节点。
        /// </summary>
        [FieldOffset(8)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     材料名称的容器节点。
        /// </summary>
        [FieldOffset(16)]
        public AtkResNode* NameContainer;

        /// <summary>
        ///     材料名称文本。
        /// </summary>
        [FieldOffset(24)]
        public AtkTextNode* Name;

        /// <summary>
        ///     制作所需数量文本。
        /// </summary>
        [FieldOffset(32)]
        public AtkTextNode* QuantityRequiredForCraft;

        /// <summary>
        ///     材料图标按钮。
        /// </summary>
        [FieldOffset(40)]
        public AtkComponentButton* IconButton;

        /// <summary>
        ///     材料图标。
        /// </summary>
        [FieldOffset(48)]
        public AtkComponentIcon* Icon;

        /// <summary>
        ///     初始品质加成图标的容器节点。
        /// </summary>
        [FieldOffset(56)]
        public AtkResNode* InitialQualityBoostIconContainer;

        /// <summary>
        ///     背包中普通品质的持有数量文本。
        /// </summary>
        [FieldOffset(64)]
        public AtkTextNode* QuantityInInventoryNormalQuality;

        /// <summary>
        ///     背包中优质品质的持有数量文本。
        /// </summary>
        [FieldOffset(72)]
        public AtkTextNode* QuantityInInventoryHighQuality;

        /// <summary>
        ///     未选择材料时的数量文本。
        /// </summary>
        [FieldOffset(80)]
        public AtkTextNode* QuantityUnselectedText;

        /// <summary>
        ///     普通品质材料的数量增加按钮。
        /// </summary>
        [FieldOffset(88)]
        public AtkComponentButton* QuantityIncrementButtonNormalQuality;

        /// <summary>
        ///     优质品质材料的数量增加按钮。
        /// </summary>
        [FieldOffset(96)]
        public AtkComponentButton* QuantityIncrementButtonHighQuality;

        /// <summary>
        ///     普通品质数量增加按钮的点击事件数据。
        /// </summary>
        [FieldOffset(104)]
        public IngredientEventData NormalQualityEventData;

        /// <summary>
        ///     优质品质数量增加按钮的点击事件数据。
        /// </summary>
        [FieldOffset(108)]
        public IngredientEventData HighQualityEventData;

        /// <summary>
        ///     普通品质数量增加按钮的文本节点。
        /// </summary>
        [FieldOffset(112)]
        public AtkTextNode* QuantityIncrementButtonNormalQualityText;

        /// <summary>
        ///     优质品质数量增加按钮的文本节点。
        /// </summary>
        [FieldOffset(120)]
        public AtkTextNode* QuantityIncrementButtonHighQualityText;

        /// <summary>
        ///     材料槽对应的物品 id。
        /// </summary>
        [FieldOffset(128)]
        public uint ItemID;

        /// <summary>
        ///     材料槽已被选中的材料时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(132)]
        public bool IsSelected;

        /// <summary>
        ///     材料槽选中的是优质品质时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(133)]
        public bool IsHighQuality;

        /// <summary>
        ///     材料槽可用的普通品质数量。
        /// </summary>
        [FieldOffset(136)]
        public int NormalQualityAvailableCount;

        /// <summary>
        ///     材料槽可用的优质品质数量。
        /// </summary>
        [FieldOffset(140)]
        public int HighQualityAvailableCount;
    }

    /// <summary>
    ///     材料槽数量增加按钮的点击事件数据，作为按钮的事件用户数据被传入。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct IngredientEventData
    {
        /// <summary>
        ///     材料槽在配方中的索引。
        /// </summary>
        [FieldOffset(0)]
        public ushort MaterialIndex;

        /// <summary>
        ///     品质标志：0 表示普通品质，1 表示优质品质。
        /// </summary>
        [FieldOffset(2)]
        public byte Quality;
    }

    /// <summary>
    ///     职业类别列表与配方分类树共用的条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 112)]
    public struct TreeEntry
    {
        /// <summary>
        ///     分类或列表项对应的行 id。
        /// </summary>
        [FieldOffset(0)]
        public int ID;

        /// <summary>
        ///     条目是否为可展开的父分类。
        /// </summary>
        [FieldOffset(4)]
        public bool IsGroup;

        /// <summary>
        ///     条目当前是否处于展开状态。
        /// </summary>
        [FieldOffset(5)]
        public bool IsExpanded;

        /// <summary>
        ///     条目显示的名称。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String Name;
    }

    /// <summary>
    ///     配方分类下拉列表的条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct DropDownEntry
    {
        /// <summary>
        ///     条目显示的名称。
        /// </summary>
        [FieldOffset(0)]
        public CStringPointer Name;

        /// <summary>
        ///     条目对应的配方笔记分类 id。
        /// </summary>
        [FieldOffset(8)]
        public int ID;
    }

    /// <summary>
    ///     配方列表项的渲染数据。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct RecipeListItemData
    {
        /// <summary>
        ///     配方列表项对应的配方行 id。
        /// </summary>
        [FieldOffset(0)]
        public int RecipeID;

        /// <summary>
        ///     配方列表项对应的物品 id。
        /// </summary>
        [FieldOffset(4)]
        public int ItemID;

        /// <summary>
        ///     配方列表项对应的配方笔记分类 id。
        /// </summary>
        [FieldOffset(8)]
        public int CategoryID;

        /// <summary>
        ///     配方列表项图标使用的动画编号。
        /// </summary>
        [FieldOffset(12)]
        public ushort IconTimelineID;

        /// <summary>
        ///     配方列表项背景使用的动画编号。
        /// </summary>
        [FieldOffset(14)]
        public ushort BackgroundTimelineID;

        /// <summary>
        ///     配方列表项名称使用的动画编号。
        /// </summary>
        [FieldOffset(16)]
        public ushort NameTimelineID;

        /// <summary>
        ///     配方列表项等级文本使用的动画编号。
        /// </summary>
        [FieldOffset(18)]
        public ushort LevelTimelineID;

        /// <summary>
        ///     配方列表项品质文本使用的动画编号。
        /// </summary>
        [FieldOffset(20)]
        public ushort QualityTimelineID;

        /// <summary>
        ///     配方列表项在列表中的索引。
        /// </summary>
        [FieldOffset(22)]
        public ushort Index;

        /// <summary>
        ///     配方列表项是否为可展开的父分类。
        /// </summary>
        [FieldOffset(24)]
        public bool IsGroup;

        /// <summary>
        ///     配方列表项是否已解锁。
        /// </summary>
        [FieldOffset(25)]
        public bool IsUnlocked;
    }

    /// <summary>
    ///     列表项的渲染回调接口，含持有者界面与取得渲染器的回调。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct ListItemRendererInterface
    {
        /// <summary>
        ///     回调接口的持有者界面。
        /// </summary>
        [FieldOffset(8)]
        public AddonRecipeNote* Owner;

        /// <summary>
        ///     取得指定列表项使用的渲染器。
        /// </summary>
        [FieldOffset(16)]
        public delegate* unmanaged<AddonRecipeNote*, AtkComponentListItemPopulator.ListItemInfo*, AtkComponentListItemRenderer*> GetItemRenderer;
    }
}
