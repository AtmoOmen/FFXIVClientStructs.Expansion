using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     制作笔记代理，负责制作笔记界面的配方检索、分类与分页切换、材料选择、配方收藏同步以及制作发起。
/// </summary>
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 1392)]
public unsafe partial struct AgentRecipeNote
{
    /// <summary>
    ///     监听登录与登出事件、并在登出时重置全部界面状态的回调子对象。
    /// </summary>
    [FieldOffset(40)]
    public GameEventCallback GameEventCallback;

    /// <summary>
    ///     材料选择器，维护当前配方各材料在背包中的可用位置与图标缓存。
    /// </summary>
    [FieldOffset(48)]
    public MaterialSelector MaterialSelection;

    /// <summary>
    ///     各工艺职业上次退出制作笔记时保存的分类、分页与筛选状态，共 8 项。
    /// </summary>
    [FieldOffset(368)]
    [FixedSizeArray]
    internal FixedSizeArray8<CraftTypeState> CraftTypeStatesStorage;

    /// <summary>
    ///     当前配方的所需职业与等级文本。
    /// </summary>
    [FieldOffset(544)]
    public Utf8String CraftTypeLevelText;

    /// <summary>
    ///     当前配方的制作条件说明文本。
    /// </summary>
    [FieldOffset(648)]
    public Utf8String CraftConditionText;

    /// <summary>
    ///     当前配方的特性说明文本。
    /// </summary>
    [FieldOffset(752)]
    public Utf8String RecipeCharacteristicsText;

    /// <summary>
    ///     物品分类筛选列表，第 0 项固定为空表示全部，其后为各 ItemUICategory 行 id。
    /// </summary>
    [FieldOffset(856)]
    [FixedSizeArray(isString: true)]
    internal FixedSizeArray50<byte> ItemCategoryFiltersStorage;

    /// <summary>
    ///     物品分类筛选列表中的条目数量，包含表示全部的第 0 项。
    /// </summary>
    [FieldOffset(906)]
    public byte ItemCategoryFilterCount;

    /// <summary>
    ///     当前选中的物品分类筛选索引，0 表示全部。
    /// </summary>
    [FieldOffset(907)]
    public byte SelectedItemCategoryFilter;

    /// <summary>
    ///     由制作笔记配方右键菜单打开的上下文图标菜单界面 id，未打开时为 0。
    /// </summary>
    [FieldOffset(908)]
    public uint ContextIconMenuAddonID;

    /// <summary>
    ///     由制作笔记打开的简易制作确认对话框界面 id，未打开时为 0。
    /// </summary>
    [FieldOffset(912)]
    public uint SynthesisSimpleDialogAddonID;

    /// <summary>
    ///     已解锁的工艺职业位掩码，第 n 位对应第 n 个工艺职业。
    /// </summary>
    [FieldOffset(916)]
    public uint UnlockedCraftTypeFlags;

    /// <summary>
    ///     右键菜单所指向的物品 id。
    /// </summary>
    [FieldOffset(920)]
    public uint ContextMenuResultItemID;

    /// <summary>
    ///     右键菜单所指向的配方行 id。
    /// </summary>
    [FieldOffset(924)]
    public uint ContextMenuRecipeID;

    /// <summary>
    ///     右键菜单所指向的材料槽位索引。
    /// </summary>
    [FieldOffset(928)]
    public uint ContextMenuMaterialIndex;

    /// <summary>
    ///     右键菜单所指向的物品名称。
    /// </summary>
    [FieldOffset(936)]
    public CStringPointer ContextMenuItemName;

    /// <summary>
    ///     当前选中的工艺职业索引。
    /// </summary>
    [FieldOffset(944)]
    public int SelectedCraftType;

    /// <summary>
    ///     当前选中的配方分页索引：0 表示普通配方，1 表示秘籍配方，2 表示收藏配方。
    /// </summary>
    [FieldOffset(948)]
    public int SelectedRecipeCategoryPage;

    /// <summary>
    ///     当前选中的配方分类，取值 5000 表示收藏配方，取值 5001 表示搜索结果，其余为配方笔记分类行 id。
    /// </summary>
    [FieldOffset(952)]
    public int SelectedRecipeCategory;

    /// <summary>
    ///     当前选中的配方在当前可见配方列表中的索引。
    /// </summary>
    [FieldOffset(956)]
    public int SelectedRecipeIndex;

    /// <summary>
    ///     当前配方列表的页码，每页固定 100 条配方。
    /// </summary>
    [FieldOffset(960)]
    public int PageIndex;

    /// <summary>
    ///     当前工艺职业存在普通配方时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(964)]
    public bool HasNormalRecipes;

    /// <summary>
    ///     当前工艺职业存在秘籍配方时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(965)]
    public bool HasMasterRecipes;

    /// <summary>
    ///     按 NotebookDivisionCategory 索引记录的分类展开状态，共 13 项。
    /// </summary>
    [FieldOffset(966)]
    [FixedSizeArray(isString: true)]
    internal FixedSizeArray13<byte> NotebookDivisionCategoryStatesStorage;

    /// <summary>
    ///     已标记默认展开分类时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(979)]
    public bool DefaultCategoryMarked;

    /// <summary>
    ///     正在制作中的配方行 id，未在制作时为 0。
    /// </summary>
    [FieldOffset(980)]
    public uint ActiveCraftRecipeID;

    /// <summary>
    ///     待处理的配方行 id，由打开配方或搜索物品的请求写入。
    /// </summary>
    [FieldOffset(984)]
    public uint PendingRecipeID;

    /// <summary>
    ///     待处理请求的状态：0 表示空闲，1 表示等待定位配方，2 表示已定位，3 表示等待物品搜索，4 表示已搜索物品，5 表示等待练习制作设置。
    /// </summary>
    [FieldOffset(988)]
    public uint PendingState;

    /// <summary>
    ///     待搜索的物品 id，由按物品搜索配方的请求写入。
    /// </summary>
    [FieldOffset(992)]
    public uint PendingItemID;

    /// <summary>
    ///     配方列表数据可用时为 <see langword="true" />，作为读取可用分类数量与选中配方的前置条件。
    /// </summary>
    [FieldOffset(996)]
    public bool IsRecipeListAvailable;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(997)]
    public bool Unk3E5;

    /// <summary>
    ///     正在关闭制作笔记界面的子界面时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(998)]
    public bool IsClosingAddon;

    /// <summary>
    ///     配方列表刷新函数的返回值为零时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(999)]
    public bool IsRecipeListEmpty;

    /// <summary>
    ///     需要在下一次刷新时重算配方列表时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(1000)]
    public bool NeedsRecipeListRefresh;

    /// <summary>
    ///     当前处于搜索结果浏览状态时非零：0 表示浏览配方笔记分类，1 表示浏览物品搜索结果，2 表示浏览配方搜索结果。
    /// </summary>
    [FieldOffset(1004)]
    public int RecipeSearchOpen;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1008)]
    public ushort Unk3F0;

    /// <summary>
    ///     搜索结果浏览状态下当前选中的配方索引。
    /// </summary>
    [FieldOffset(1010)]
    public ushort SelectedRecipeIndexInSearch;

    /// <summary>
    ///     搜索结果浏览状态下当前选中的物品分类筛选。
    /// </summary>
    [FieldOffset(1012)]
    public byte ItemCategoryFilterInSearch;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1013)]
    public byte Unk3F5;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1014)]
    public ulong Unk3F6;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1022)]
    public uint Unk3FE;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1026)]
    public ushort Unk402;

    /// <summary>
    ///     客户端未读写的保留字段。
    /// </summary>
    [FieldOffset(1028)]
    public byte Unk404;

    /// <summary>
    ///     配方搜索尚未完成时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(1030)]
    public bool RecipeSearchProcessing;

    /// <summary>
    ///     当前配方搜索的关键字。
    /// </summary>
    [FieldOffset(1032)]
    public Utf8String RecipeSearch;

    /// <summary>
    ///     本次搜索已完成时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(1136)]
    public bool IsRecipeSearchComplete;

    /// <summary>
    ///     按物品名称搜索配方时使用的查表等待器。
    /// </summary>
    [FieldOffset(1144)]
    public RecipeLookupWaiter* RecipeLookup;

    /// <summary>
    ///     配方搜索命中的配方行 id 列表。
    /// </summary>
    [FieldOffset(1152)]
    public StdVector<uint> SearchResults;

    /// <summary>
    ///     当前选中的搜索历史项索引。
    /// </summary>
    [FieldOffset(1176)]
    public byte SelectedSearchHistoryIndex;

    /// <summary>
    ///     搜索历史模式：0 表示按关键字重新搜索，1 表示直接载入历史中的配方列表。
    /// </summary>
    [FieldOffset(1180)]
    public int SearchHistoryMode;

    /// <summary>
    ///     按关键字保存的搜索历史，最多保留 10 项。
    /// </summary>
    [FieldOffset(1184)]
    public StdDeque<Utf8String> RecipeSearchHistory;

    /// <summary>
    ///     带配方行 id 的搜索历史，最多保留 10 项。
    /// </summary>
    [FieldOffset(1224)]
    public StdDeque<RecipeSearchHistoryEntry> RecipeSearchHistoryEntries;

    /// <summary>
    ///     制作笔记设置界面当前使用的筛选选项位掩码。
    /// </summary>
    [FieldOffset(1264)]
    public uint RecipeFilterOptions;

    /// <summary>
    ///     制作笔记设置界面 id，未打开时为 0。
    /// </summary>
    [FieldOffset(1268)]
    public uint RecipeNoteSettingAddonID;

    /// <summary>
    ///     查看所需材料列表界面 id，未打开时为 0。
    /// </summary>
    [FieldOffset(1272)]
    public uint MaterialListAddonID;

    /// <summary>
    ///     练习制作使用的初始品质上限。
    /// </summary>
    [FieldOffset(1276)]
    public int PracticeStartingQuality;

    /// <summary>
    ///     练习制作设置中启用初始品质上限时为 <see langword="true" />。
    /// </summary>
    [FieldOffset(1280)]
    public bool PracticeUseStartingQuality;

    /// <summary>
    ///     当前练习制作设置对应的配方行 id。
    /// </summary>
    [FieldOffset(1282)]
    public ushort PracticeRecipeID;

    /// <summary>
    ///     练习制作设置界面 id，未打开时为 0。
    /// </summary>
    [FieldOffset(1284)]
    public uint RecipeNotePracticeSettingAddonID;

    /// <summary>
    ///     收藏配方同步器，负责把收藏模块中的配方校验后载入制作笔记。
    /// </summary>
    [FieldOffset(1288)]
    public FavoriteRecipeSync FavoriteSync;

    /// <summary>
    ///     构造代理并初始化全部子对象与容器。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 4C 8B F2 48 8B F9 E8")]
    public partial AgentRecipeNote* Ctor();

    /// <summary>
    ///     重置虚表、销毁全部子容器并调用基类终结函数。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F9 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 81 C1")]
    public partial void Finalizer();


    /// <summary>
    ///     打开制作笔记界面；界面已打开时按参数重新载入配方列表。
    /// </summary>
    /// <param name="practiceMode">是否为练习制作模式。</param>
    /// <param name="resetState">是否重置界面状态。</param>
    /// <param name="craftType">初始工艺职业索引，取值 8 表示按当前职业推断。</param>
    /// <param name="openerAddonID">发起打开的界面 id。</param>
    [MemberFunction("40 53 56 41 55 41 56 48 81 EC")]
    public partial void OpenAddon
    (
        byte practiceMode,
        bool resetState,
        int  craftType,
        int  openerAddonID
    );

    /// <summary>
    ///     响应制作笔记界面的 16 号刷新事件，按事件参数重算界面数据。
    /// </summary>
    /// <param name="eventKind">触发刷新的原因：0 表示收藏列表变化，1 表示界面重建，3 与 6 表示重绘，5 表示切换分类，7 表示重算可制作数量。</param>
    [MemberFunction("48 89 5C 24 ?? 57 41 55 41 57")]
    public partial void RefreshAddon
    (
        int eventKind
    );

    /// <summary>
    ///     按内部配方行 id 打开配方；内部 id 为配方行 id 加上 0x10000。
    /// </summary>
    /// <param name="internalRecipeID">内部配方行 id。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 8B FA 48 8B D9 0F 85 ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 0F 85 ?? ?? ?? ?? 83 B9")]
    public partial void OpenRecipeByRecipeIDInternal
    (
        uint internalRecipeID
    );

    /// <summary>
    ///     按配方行 id 打开配方。
    /// </summary>
    /// <param name="recipeID">配方行 id。</param>
    public void OpenRecipeByRecipeID
    (
        uint recipeID
    ) => OpenRecipeByRecipeIDInternal(recipeID + 0x10000);

    /// <summary>
    ///     以练习制作模式打开指定配方。
    /// </summary>
    /// <param name="recipeID">配方行 id。</param>
    /// <param name="openPracticeSetting">是否在打开后立即弹出练习制作设置。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 0F B7 C2 41 0F B6 D8")]
    public partial void OpenRecipeInPracticeMode
    (
        ushort recipeID,
        bool   openPracticeSetting
    );

    /// <summary>
    ///     按物品 id 搜索使用该物品的配方。
    /// </summary>
    /// <param name="itemID">物品 id。</param>
    /// <param name="openerAddonID">发起打开的界面 id。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 01 41 8B F8 0F B7 F2")]
    public partial void SearchRecipeByItemID
    (
        uint itemID,
        uint openerAddonID
    );

    /// <summary>
    ///     按关键字搜索配方。
    /// </summary>
    /// <param name="text">搜索关键字。</param>
    /// <param name="searchMode">搜索模式：0 表示包含匹配，非 0 表示精确匹配。</param>
    /// <param name="pushHistory">是否把关键字写入搜索历史。</param>
    [MemberFunction("40 55 56 57 48 83 EC ?? 80 B9")]
    public partial void SearchRecipe
    (
        Utf8String* text,
        byte        searchMode,
        bool        pushHistory
    );

    /// <summary>
    ///     按物品名称搜索配方，并把搜索结果填入界面搜索栏。
    /// </summary>
    /// <param name="itemID">物品 id。</param>
    /// <param name="itemName">物品名称。</param>
    [MemberFunction("85 D2 0F 84 ?? ?? ?? ?? 4C 8B DC 55 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 49 8B C0")]
    public partial void SearchRecipeByItemName
    (
        uint           itemID,
        CStringPointer itemName
    );

    /// <summary>
    ///     以指定文本发起精确搜索并刷新界面搜索栏。
    /// </summary>
    /// <param name="text">搜索文本。</param>
    [MemberFunction("48 85 D2 0F 84 ?? ?? ?? ?? 57 48 81 EC")]
    public partial void SearchRecipeByText
    (
        CStringPointer text
    );

    /// <summary>
    ///     判断指定配方笔记分类在指定工艺职业下是否已解锁。
    /// </summary>
    /// <param name="noteBookDivisionID">配方笔记分类 id。</param>
    /// <param name="craftType">工艺职业索引。</param>
    /// <returns>已解锁时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 4C 8B 15")]
    public partial bool IsNoteBookDivisionUnlocked
    (
        uint noteBookDivisionID,
        int  craftType
    );

    /// <summary>
    ///     判断指定配方笔记分类在当前工艺职业下是否已解锁。
    /// </summary>
    /// <param name="noteBookDivisionID">配方笔记分类 id。</param>
    /// <returns>已解锁时为 <see langword="true" />。</returns>
    [MemberFunction("44 8B 81 ?? ?? ?? ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC")]
    public partial bool IsNoteBookDivisionUnlockedForSelectedCraftType
    (
        uint noteBookDivisionID
    );

    /// <summary>
    ///     判断当前选中的材料是否已全部选定，并在失败时输出提示。
    /// </summary>
    /// <param name="recipeNote">制作笔记实例。</param>
    /// <param name="showError">失败时是否输出错误提示。</param>
    /// <returns>可以开始制作时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 41 0F B6 F8 48 8B CA E8 ?? ?? ?? ?? 4C 8B C8")]
    public partial bool CanCraft
    (
        RecipeNote* recipeNote,
        bool        showError
    );

    /// <summary>
    ///     按当前职业重新推断选中的工艺职业。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 48 8B F9 48 8B 0D ?? ?? ?? ?? 48 85 C9 74 ?? 48 89 5C 24")]
    public partial void SelectDefaultCraftType();

    /// <summary>
    ///     取得指定分页类型下的配方分类页数。
    /// </summary>
    /// <param name="pageType">分页类型：0 表示普通配方，1 与 2 表示按秘籍与收藏筛选的配方，3 表示搜索结果。</param>
    /// <returns>该分页类型下的页数。</returns>
    [MemberFunction("41 56 41 57 48 83 EC ?? 44 8B FA 4C 8B F1 44 8B C2")]
    public partial int GetRecipeCategoryPageCount
    (
        int pageType
    );

    /// <summary>
    ///     在配方列表按顺序查找指定配方行 id 的可见索引，跳过被筛选条件排除的配方。
    /// </summary>
    /// <param name="recipeID">配方行 id。</param>
    /// <returns>可见索引，未找到时返回 -1。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 80 3D ?? ?? ?? ?? ?? 44 0F B7 FA")]
    public partial int FindVisibleRecipeIndex
    (
        ushort recipeID
    );

    /// <summary>
    ///     重新统计各工艺职业的解锁状态并写入界面数值数组。
    /// </summary>
    /// <param name="recipeNote">制作笔记实例。</param>
    /// <param name="numberArray">界面数值数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 33 FF 4D 8B F0 48 8B EA")]
    public partial void UpdateUnlockedCraftTypes
    (
        RecipeNote*      recipeNote,
        NumberArrayData* numberArray
    );

    /// <summary>
    ///     刷新界面各工艺职业页签的名称文本。
    /// </summary>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 42 ?? BA ?? ?? ?? ?? 41 FF D0 48 8B F8")]
    public partial void RefreshCraftTypeNames();

    /// <summary>
    ///     刷新配方分类下拉列表与相关数值数组。
    /// </summary>
    [MemberFunction("41 54 41 55 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F1 48 89 4C 24 ?? 48 8B 49 ?? 48 8B 01")]
    public partial void RefreshCategoryList();

    /// <summary>
    ///     刷新搜索历史列表与相关数值数组。
    /// </summary>
    [MemberFunction("40 55 41 54 41 57 48 83 EC ?? 4C 8B F9")]
    public partial void RefreshSearchHistory();

    /// <summary>
    ///     把当前选中的配方分类写入界面数值数组。
    /// </summary>
    [MemberFunction
        ("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 42 ?? BA ?? ?? ?? ?? 41 FF D0 48 85 C0 75 ?? 48 83 C4 ?? 5B C3 83 BB")]
    public partial void UpdateSelectedCategoryNumberArray();

    /// <summary>
    ///     重建当前可见的配方列表，计算分页文本并刷新列表项。
    /// </summary>
    [MemberFunction("40 53 41 55 41 57 48 81 EC ?? ?? ?? ?? 48 8B D9")]
    public partial void RefreshRecipeList();

    /// <summary>
    ///     刷新选中配方的详情面板，包含材料、耐久、难度与品质等信息。
    /// </summary>
    /// <param name="refreshFlags">刷新内容位掩码：第 0 位表示基础信息，第 1 位表示数量与品质，第 2 位表示制作条件。</param>
    /// <returns>本次刷新写入的界面数据位掩码。</returns>
    [MemberFunction("48 89 5C 24 ?? 55 56 41 54 41 55 41 57 48 8D 6C 24")]
    public partial int RefreshRecipeDetail
    (
        int refreshFlags
    );

    /// <summary>
    ///     刷新当前配方在材料选择器中的可制作状态。
    /// </summary>
    /// <param name="recipeNote">制作笔记实例。</param>
    /// <param name="numberArray">界面数值数组。</param>
    /// <param name="stringArray">界面字符串数组。</param>
    /// <returns>本次刷新写入的界面数据位掩码。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 49 8B D9 48 8B 49 ?? 49 8B E8 48 8B F2")]
    public partial int RefreshCraftableState
    (
        RecipeNote*      recipeNote,
        NumberArrayData* numberArray,
        StringArrayData* stringArray
    );

    /// <summary>
    ///     重建物品分类筛选列表并刷新对应下拉框。
    /// </summary>
    /// <returns>本次刷新写入的界面数据位掩码。</returns>
    [MemberFunction("40 53 55 41 55 48 81 EC")]
    public partial int RefreshItemCategoryFilters();

    /// <summary>
    ///     刷新当前配方的特性说明文本，包含制作条件、专家配方与高质量限制等。
    /// </summary>
    /// <returns>配方特性可用时为 <see langword="true" />。</returns>
    [MemberFunction("40 53 41 54 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B D9")]
    public partial bool RefreshRecipeCharacteristics();

    /// <summary>
    ///     把工艺职业等级与名称写入界面值数组，用于打开界面时的初始参数。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 57 41 56 48 83 EC ?? 48 8D B1 ?? ?? ?? ?? 4C 8B F2")]
    public partial void FillCraftTypeLevelValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     打开材料选择的上下文图标菜单，并把材料列表写入菜单参数。
    /// </summary>
    [MemberFunction("41 54 41 55 48 81 EC ?? ?? ?? ?? 4C 8D 61")]
    public partial void OpenContextIconMenu();

    /// <summary>
    ///     打开物品右键菜单。
    /// </summary>
    /// <param name="itemID">物品 id。</param>
    /// <param name="itemName">物品名称。</param>
    /// <param name="menuFlags">菜单项启用位掩码。</param>
    /// <param name="recipeID">所属配方行 id。</param>
    /// <param name="materialIndex">所属材料槽位索引。</param>
    /// <param name="recipeCraftType">配方工艺职业索引。</param>
    /// <param name="addonID">发起菜单的界面 id。</param>
    [MemberFunction("40 53 55 56 57 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 44 8B B4 24")]
    public partial void OpenItemContextMenu
    (
        uint           itemID,
        CStringPointer itemName,
        int            menuFlags,
        int            recipeID,
        int            materialIndex,
        int            recipeCraftType,
        int            addonID
    );

    /// <summary>
    ///     打开当前配方成品材料的右键菜单。
    /// </summary>
    /// <param name="materialIndex">材料槽位索引。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 83 3D ?? ?? ?? ?? ?? 48 8B F9 48 63 F2")]
    public partial void OpenMaterialContextMenu
    (
        int materialIndex
    );

    /// <summary>
    ///     打开当前配方水晶材料的右键菜单。
    /// </summary>
    /// <param name="crystalIndex">水晶槽位索引。</param>
    [MemberFunction("48 89 5C 24 ?? 56 48 83 EC ?? 48 83 3D ?? ?? ?? ?? ?? 48 8B F1")]
    public partial void OpenIngredientContextMenu
    (
        int crystalIndex
    );

    /// <summary>
    ///     按物品 id 填充材料选择列表。
    /// </summary>
    /// <param name="itemID">物品 id。</param>
    /// <param name="materialSlotIndex">材料槽位索引。</param>
    [MemberFunction("40 53 55 57 41 55 48 83 EC ?? 80 B9")]
    public partial void SelectMaterialByItemID
    (
        int  itemID,
        byte materialSlotIndex
    );

    /// <summary>
    ///     选择物品分类筛选。
    /// </summary>
    /// <param name="filterIndex">筛选索引，0 表示全部。</param>
    [MemberFunction("40 57 48 83 EC ?? 0F B6 81 ?? ?? ?? ?? 48 8B F9 3B D0")]
    public partial void SelectItemCategoryFilter
    (
        int filterIndex
    );

    /// <summary>
    ///     按可见索引选择配方。
    /// </summary>
    /// <param name="visibleIndex">可见索引。</param>
    [MemberFunction("40 53 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 80 3D")]
    public partial void SelectRecipeByVisibleIndex
    (
        int visibleIndex
    );

    /// <summary>
    ///     按配方行 id 选择配方。
    /// </summary>
    /// <param name="recipeID">配方行 id。</param>
    /// <returns>找到并选中时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 8B F8 85 C0")]
    public partial bool SelectRecipeByRecipeID
    (
        ushort recipeID
    );

    /// <summary>
    ///     按可见索引在指定页码内查找配方的列表索引。
    /// </summary>
    /// <param name="pageIndex">页码。</param>
    /// <param name="indexInPage">页内索引。</param>
    /// <returns>列表索引，未找到时返回 -1。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 80 3D ?? ?? ?? ?? ?? 44 8B FA")]
    public partial int FindRecipeIndexInPage
    (
        int pageIndex,
        int indexInPage
    );

    /// <summary>
    ///     取得指定配方在当前可见列表中的页内序号。
    /// </summary>
    /// <param name="recipeIndex">配方索引。</param>
    /// <returns>页内序号。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC ?? 80 3D")]
    public partial int GetVisibleIndexInPage
    (
        int recipeIndex
    );

    /// <summary>
    ///     取出当前选中配方所属的分页类型。
    /// </summary>
    /// <returns>0 表示普通分类，1 表示分级配方，2 表示秘籍配方，3 表示搜索结果或收藏。</returns>
    [MemberFunction
    (
        "48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 75 ?? 8B 89 ?? ?? ?? ?? 81 F9 ?? ?? ?? ?? 7D ?? 81 F9 ?? ?? ?? ?? 7C ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 80 78 ?? ?? 75 ?? B8 ?? ?? ?? ?? 48 83 C4"
    )]
    public partial int GetRecipeCategoryPageType();

    /// <summary>
    ///     取出指定配方分类对应的分页类型。
    /// </summary>
    /// <param name="recipeCategory">配方分类。</param>
    /// <returns>0 表示普通分类，1 表示分级配方，2 表示秘籍配方，3 表示搜索结果或收藏。</returns>
    [MemberFunction("48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 75 ?? 81 FA")]
    public partial int GetRecipeCategoryPageTypeByCategory
    (
        int recipeCategory
    );

    /// <summary>
    ///     处理待打开的配方请求，定位到对应分类与配方。
    /// </summary>
    [MemberFunction("40 53 56 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 44 8B B1")]
    public partial void ProcessPendingRecipe();

    /// <summary>
    ///     处理待搜索的物品请求，把物品的配方搜索结果填入列表。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 8B 89 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 8B 8B")]
    public partial void ProcessPendingItemSearch();

    /// <summary>
    ///     在练习制作设置界面关闭后处理结果。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 83 3D ?? ?? ?? ?? ?? 48 8B D9 74 ?? 48 8B 49")]
    public partial void CheckPracticeSettingClosed();

    /// <summary>
    ///     清空待处理的配方与物品请求。
    /// </summary>
    /// <returns>固定返回 0。</returns>
    [MemberFunction("33 C0 48 89 81 ?? ?? ?? ?? 89 81 ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53")]
    public partial int ResetPendingRequest();

    /// <summary>
    ///     按当前待处理状态推进一次请求处理。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 8B 89 ?? ?? ?? ?? 85 C9 0F 84 ?? ?? ?? ?? 83 E9 ?? 0F 84")]
    public partial void ProcessPendingState();

    /// <summary>
    ///     清空当前的分类与分页选择。
    /// </summary>
    /// <returns>固定返回 0。</returns>
    [MemberFunction("33 C0 C6 81 ?? ?? ?? ?? ?? 48 89 81 ?? ?? ?? ?? C3")]
    public partial int ResetCategorySelection();

    /// <summary>
    ///     把当前界面状态写入对应工艺职业的槽位。
    /// </summary>
    /// <returns>写入的状态值。</returns>
    [MemberFunction("83 B9 ?? ?? ?? ?? ?? 48 8B D1 74 ?? 0F B7 81")]
    public partial int SaveCraftTypeState();

    /// <summary>
    ///     从对应工艺职业的槽位恢复界面状态。
    /// </summary>
    /// <returns>恢复的分类值。</returns>
    [MemberFunction("40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 0F BF 81")]
    public partial int LoadCraftTypeState();

    /// <summary>
    ///     把关键字写入搜索历史，超过上限时淘汰最早的条目。
    /// </summary>
    /// <param name="text">搜索关键字。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 57 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 99")]
    public partial void PushSearchHistory
    (
        Utf8String* text
    );

    /// <summary>
    ///     把带配方行 id 的关键字写入搜索历史，超过上限时淘汰最早的条目。
    /// </summary>
    /// <param name="text">搜索关键字。</param>
    /// <param name="recipeID">对应的配方行 id。</param>
    [MemberFunction("48 89 5C 24 ?? 56 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B D9")]
    public partial void PushSearchHistoryEntry
    (
        Utf8String* text,
        int         recipeID
    );

    /// <summary>
    ///     判断指定工艺职业是否已解锁。
    /// </summary>
    /// <param name="craftType">工艺职业索引。</param>
    /// <returns>已解锁时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 0F B6 DA 80 FB")]
    public partial bool IsCraftTypeUnlocked
    (
        byte craftType
    );

    /// <summary>
    ///     判断指定配方是否被当前的筛选条件排除。
    /// </summary>
    /// <param name="recipe">配方数据。</param>
    /// <returns>被排除时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B DA 48 8B F9 E8 ?? ?? ?? ?? 84 C0 74 ?? 8B 87")]
    public partial bool IsRecipeFiltered
    (
        RecipeNote.RecipeData* recipe
    );

    /// <summary>
    ///     判断当前的分类筛选条件是否生效。
    /// </summary>
    /// <returns>条件生效时为 <see langword="true" />。</returns>
    [MemberFunction
    (
        "48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 75 ?? 8B 89 ?? ?? ?? ?? 81 F9 ?? ?? ?? ?? 7D ?? 81 F9 ?? ?? ?? ?? 7C ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 80 78 ?? ?? 75 ?? B8 ?? ?? ?? ?? 83 F8"
    )]
    public partial bool IsFilterEnabled();

    /// <summary>
    ///     打开练习制作设置界面。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 8B 91")]
    public partial void OpenPracticeSetting();

    /// <summary>
    ///     关闭全部由制作笔记打开的子界面。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? C6 81 ?? ?? ?? ?? ?? 48 8B D9 48 8B 49 ?? 48 8B 01")]
    public partial void CloseChildAddons();

    /// <summary>
    ///     通知打开者与关联代理关闭界面。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 48 8B 49 ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 74 ?? 48 8B 17 48 8B CF FF 52 ?? 8B D0 48 8B CB E8 ?? ?? ?? ?? 48 8B 4F ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B D8 48 85 C0 74 ?? 48 8B 17 48 8B CF FF 52 ?? 85 C0"
    )]
    public partial void CloseOpenerAgents();

    /// <summary>
    ///     材料选择器，维护当前配方各材料在背包中的可用位置与图标缓存。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 320)]
    public unsafe partial struct MaterialSelector
    {
        /// <summary>
        ///     当前配方各材料在背包中的可用位置，元素数量上限为 140。
        /// </summary>
        [FieldOffset(0)]
        public StdVector<InventoryItemRef> Items;

        /// <summary>
        ///     材料选择列表中当前高亮的背包位置。
        /// </summary>
        [FieldOffset(24)]
        public InventoryItemRef SelectedItem;

        /// <summary>
        ///     普通品质材料的物品缓存，用于材料选择菜单的图标与名称显示。
        /// </summary>
        [FieldOffset(40)]
        public RaptureAtkModule.ItemCache NormalQualityCache;

        /// <summary>
        ///     优质材料的物品缓存，用于材料选择菜单的图标与名称显示。
        /// </summary>
        [FieldOffset(176)]
        public RaptureAtkModule.ItemCache HighQualityCache;

        /// <summary>
        ///     材料选择列表对应的物品 id。
        /// </summary>
        [FieldOffset(312)]
        public uint ItemID;

        /// <summary>
        ///     当前选中材料在配方中的槽位索引。
        /// </summary>
        [FieldOffset(316)]
        public byte SlotIndex;

        /// <summary>
        ///     材料选择列表中可用品质的位掩码：第 0 位表示存在普通品质，第 1 位表示存在优质品质。
        /// </summary>
        [FieldOffset(317)]
        public byte QualityFlags;

        /// <summary>
        ///     材料选择列表已填充完成时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(318)]
        public bool IsInitialized;

        /// <summary>
        ///     构造材料选择器并预分配可用位置列表。
        /// </summary>
        /// <returns>材料选择器自身。</returns>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 33 ED C7 41")]
        public partial MaterialSelector* Ctor();

        /// <summary>
        ///     销毁物品缓存并释放可用位置列表。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 48 8D 99 ?? ?? ?? ?? BF ?? ?? ?? ?? 48 8D 2D")]
        public partial void Dtor();

        /// <summary>
        ///     标记材料选择列表已填充完成。
        /// </summary>
        [MemberFunction("C6 81 ?? ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 C6 81")]
        public partial void SetInitialized();

        /// <summary>
        ///     清空材料选择列表、高亮位置与物品缓存。
        /// </summary>
        [MemberFunction("40 53 48 83 EC ?? 48 8B D9 C6 81 ?? ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 43")]
        public partial void Reset();

        /// <summary>
        ///     清空材料选择列表与物品缓存，但保留已填充标记。
        /// </summary>
        [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8B 43 ?? 48 8D 4B")]
        public partial void ResetQuiet();

        /// <summary>
        ///     按物品 id 填充材料选择列表；优质品质会额外计入高品质位。
        /// </summary>
        /// <param name="itemID">物品 id，优质物品会带上 1000000 的偏移。</param>
        /// <param name="slotIndex">材料槽位索引。</param>
        /// <returns>找到可用位置时为 <see langword="true" />。</returns>
        [MemberFunction("40 53 55 57 48 83 EC ?? 80 B9")]
        public partial bool SelectByItemID
        (
            int  itemID,
            byte slotIndex
        );

        /// <summary>
        ///     按待填充的品质位更新普通品质与优质品质的物品缓存。
        /// </summary>
        /// <returns>全部缓存均已填充时为 <see langword="true" />。</returns>
        [MemberFunction("41 56 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 4C 8B F1 0F 84 ?? ?? ?? ?? 0F B6 81")]
        public partial bool UpdateItemCaches();

        /// <summary>
        ///     清空可用品质位掩码。
        /// </summary>
        [MemberFunction("C6 81 ?? ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC 48 8B 41")]
        public partial void ClearQualityFlags();

        /// <summary>
        ///     把材料列表写入上下文图标菜单的界面值数组。
        /// </summary>
        /// <param name="values">界面值数组。</param>
        /// <param name="maxValueCount">界面值数组容量。</param>
        /// <returns>写入成功时为 <see langword="true" />。</returns>
        [MemberFunction("48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 80 B9")]
        public partial bool BuildContextMenuValues
        (
            AtkValue* values,
            int       maxValueCount
        );

        /// <summary>
        ///     清除当前高亮位置并解除对背包格的锁定。
        /// </summary>
        [MemberFunction("40 53 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 83 79 ?? ?? 76")]
        public partial void ClearHighlighted();

        /// <summary>
        ///     清空可用位置列表并解除对相应背包格的锁定。
        /// </summary>
        [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 09 48 8B 43 ?? 48 2B C1 48 C1 F8 ?? 85 C0")]
        public partial void ClearBlockedSlots();

        /// <summary>
        ///     按列表索引选择材料位置，并锁定对应背包格。
        /// </summary>
        /// <param name="itemID">物品 id。</param>
        /// <param name="index">列表索引，负数表示不选中任何位置。</param>
        [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 8B F2 49 63 F8")]
        public partial void SelectByIndex
        (
            int itemID,
            int index
        );

        /// <summary>
        ///     把背包物品加入可用位置列表并锁定对应背包格。
        /// </summary>
        /// <param name="item">背包物品。</param>
        [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B 41 ?? 48 8B F2 48 2B 01")]
        public partial void AddItem
        (
            InventoryItem* item
        );
    }

    /// <summary>
    ///     单一工艺职业在制作笔记中保存的界面状态。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 22)]
    public struct CraftTypeState
    {
        /// <summary>
        ///     上次选中的配方分类。
        /// </summary>
        [FieldOffset(0)]
        public ushort RecipeCategory;

        /// <summary>
        ///     上次选中的配方索引。
        /// </summary>
        [FieldOffset(2)]
        public ushort RecipeIndex;

        /// <summary>
        ///     上次选中的物品分类筛选索引。
        /// </summary>
        [FieldOffset(4)]
        public byte ItemCategoryFilter;

        /// <summary>
        ///     上次选中的配方分页索引。
        /// </summary>
        [FieldOffset(5)]
        public byte RecipeCategoryPage;

        /// <summary>
        ///     上次保存的配方笔记分类展开状态。
        /// </summary>
        [FieldOffset(6)]
        [FixedSizeArray(isString: true)]
        internal FixedSizeArray13<byte> NotebookDivisionCategoryStatesStorage;

        /// <summary>
        ///     上次已标记默认展开分类时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(19)]
        public bool DefaultCategoryMarked;

        /// <summary>
        ///     该槽位已写入过状态时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(20)]
        public bool IsInitialized;
    }

    /// <summary>
    ///     搜索历史条目，记录关键字与命中的配方行 id。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 112)]
    public struct RecipeSearchHistoryEntry
    {
        /// <summary>
        ///     搜索关键字。
        /// </summary>
        [FieldOffset(0)]
        public Utf8String Text;

        /// <summary>
        ///     该次搜索对应的配方行 id。
        /// </summary>
        [FieldOffset(104)]
        public uint RecipeID;
    }

    /// <summary>
    ///     收藏配方同步器，把收藏模块中的配方逐条校验后载入制作笔记。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public unsafe partial struct FavoriteRecipeSync
    {
        /// <summary>
        ///     同步器的持有者代理。
        /// </summary>
        [FieldOffset(0)]
        public AgentRecipeNote* Owner;

        /// <summary>
        ///     收藏配方模块。
        /// </summary>
        [FieldOffset(8)]
        public RecipeFavoriteModule* FavoriteModule;

        /// <summary>
        ///     同步状态：0 表示空闲，1 表示校验中，2 表示已完成。
        /// </summary>
        [FieldOffset(16)]
        public uint State;

        /// <summary>
        ///     正在等待的 Recipe 表。
        /// </summary>
        [FieldOffset(24)]
        public void* RecipeSheet;

        /// <summary>
        ///     Recipe 表的查表等待器。
        /// </summary>
        [FieldOffset(32)]
        public ExcelSheetWaiter* Waiter;

        /// <summary>
        ///     当前校验到的收藏槽位索引，取值范围为 0 至 79。
        /// </summary>
        [FieldOffset(40)]
        public uint Index;

        /// <summary>
        ///     已校验完成的收藏数量。
        /// </summary>
        [FieldOffset(44)]
        public uint LoadedCount;

        /// <summary>
        ///     收藏配方总数。
        /// </summary>
        [FieldOffset(48)]
        public uint TotalCount;

        /// <summary>
        ///     本批次待查表的收藏配方。
        /// </summary>
        [FieldOffset(56)]
        public StdVector<FavoriteRecipeEntry> PendingEntries;

        /// <summary>
        ///     校验通过的收藏配方。
        /// </summary>
        [FieldOffset(80)]
        public StdVector<FavoriteRecipeEntry> ValidEntries;

        /// <summary>
        ///     构造同步器并绑定持有者与收藏模块。
        /// </summary>
        /// <param name="owner">持有者代理。</param>
        /// <param name="favoriteModule">收藏配方模块。</param>
        /// <returns>同步器自身。</returns>
        [MemberFunction("33 C0 48 89 11 89 41 ?? 48 89 41")]
        public partial FavoriteRecipeSync* Ctor
        (
            AgentRecipeNote*      owner,
            RecipeFavoriteModule* favoriteModule
        );

        /// <summary>
        ///     释放等待器与两个收藏列表。
        /// </summary>
        [MemberFunction
        (
            "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 33 FF 48 8B 49 ?? 48 85 C9 74 ?? 48 8B 43 ?? 48 85 C0 74 ?? 48 8B D1 48 8B C8 E8 ?? ?? ?? ?? 48 8B 4B ?? 48 85 C9 74 ?? 48 8B 01 BA ?? ?? ?? ?? FF 10 48 89 7B ?? 48 89 7B"
        )]
        public partial void Dtor();

        /// <summary>
        ///     重置同步状态并释放全部缓存。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8B D9 89 79 ?? 48 8B 49 ?? 48 85 C9 74 ?? 48 8B 43")]
        public partial void Reset();

        /// <summary>
        ///     开始一次收藏配方同步；已有收藏时直接完成，否则逐批校验。
        /// </summary>
        [MemberFunction("40 57 48 83 EC ?? 8B 41 ?? 48 8B F9 83 F8")]
        public partial void Start();

        /// <summary>
        ///     向 Recipe 表登记本批需要校验的配方行。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 56 57 41 56 48 83 EC ?? 48 8B D9 48 8B 49")]
        public partial void LoadBatch();

        /// <summary>
        ///     Recipe 表加载完成回调，逐条校验收藏配方的解锁条件。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 41 54 41 56 48 83 EC ?? 45 33 E4")]
        public partial void OnRowsLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     结束同步：删除失效收藏并通知界面刷新。
        /// </summary>
        [MemberFunction("40 57 48 83 EC ?? 83 79 ?? ?? 48 8B F9 75 ?? 48 89 5C 24 ?? 48 8B 59")]
        public partial void Finish();
    }

    /// <summary>
    ///     收藏配方条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct FavoriteRecipeEntry
    {
        /// <summary>
        ///     工艺职业索引。
        /// </summary>
        [FieldOffset(0)]
        public int CraftType;

        /// <summary>
        ///     配方行 id。
        /// </summary>
        [FieldOffset(4)]
        public ushort RecipeID;
    }
    
    /// <summary>
    ///     按物品名称检索配方时使用的 RecipeLookup 表等待器。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe partial struct RecipeLookupWaiter
    {
        /// <summary>
        ///     界面模块接口，用于取得查询表。
        /// </summary>
        [FieldOffset(8)]
        public UIModuleInterface* UIModuleInterface;

        /// <summary>
        ///     等待器状态：0 表示空闲，1 表示等待表加载，2 表示已完成。
        /// </summary>
        [FieldOffset(16)]
        public uint State;

        /// <summary>
        ///     正在查询的物品 id。
        /// </summary>
        [FieldOffset(20)]
        public uint ItemID;

        /// <summary>
        ///     接收配方行 id 的目标列表。
        /// </summary>
        [FieldOffset(24)]
        public StdVector<uint>* SearchResults;

        /// <summary>
        ///     等待器是否已完成。
        /// </summary>
        /// <returns>状态为已完成时返回 <see langword="true" />。</returns>
        [VirtualFunction(1)]
        public partial bool GetIsComplete();

        /// <summary>
        ///     表加载完成后把 RecipeLookup 行内的 8 个工艺职业配方写入目标列表。
        /// </summary>
        [VirtualFunction(2)]
        public partial void Iterate();
    }
}
