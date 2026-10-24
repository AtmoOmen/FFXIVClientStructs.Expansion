using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     采集品交易代理，负责采集品交易界面（CollectablesShop）的数据加载、类别与物品选择、详情刷新以及提交。
/// </summary>
[GenerateInterop]
[Agent(AgentId.CollectablesShop)]
[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe partial struct AgentCollectablesShop
{
    /// <summary>
    ///     代理自身的基础接口子对象。
    /// </summary>
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    /// <summary>
    ///     向打开该代理的事件回调接口回传当前选择的事件接口子对象。
    /// </summary>
    [FieldOffset(40)]
    public CollectablesShopEventInterface EventInterface;

    /// <summary>
    ///     由采集品数据计算精炼结果并转发界面打开请求的事件接口子对象。
    /// </summary>
    [FieldOffset(48)]
    public CollectablesShopEventInterface DetailEventInterface;

    /// <summary>
    ///     采集品交易数据，首次初始化时在 UI 内存空间中分配，关闭界面时释放。
    /// </summary>
    [FieldOffset(56)]
    public CollectablesShopData* Data;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 C7 43 ?? ?? ?? ?? ?? 48 89 03 48 8D 05 ?? ?? ?? ?? 48 89 43 ?? 48 8D 05 ?? ?? ?? ?? 48 89 43 ?? 48 8B C3 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 32 C0"
    )]
    public partial AgentCollectablesShop* Ctor();

    /// <summary>
    ///     重置虚表并调用基类终结函数。
    /// </summary>
    [MemberFunction
    (
        "48 8D 05 ?? ?? ?? ?? 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 32 C0"
    )]
    public partial void Finalizer();

    /// <summary>
    ///     处理界面发来的事件。
    /// </summary>
    /// <param name="returnValue">返回值容器。</param>
    /// <param name="values">事件参数。</param>
    /// <param name="valueCount">事件参数数量。</param>
    /// <param name="eventKind">事件种类。</param>
    /// <returns>返回值容器。</returns>
    [VirtualFunction(0)]
    public partial AtkValue* ReceiveEvent
    (
        AtkValue* returnValue,
        AtkValue* values,
        uint      valueCount,
        ulong     eventKind
    );

    /// <summary>
    ///     析构代理。
    /// </summary>
    /// <param name="freeFlags">释放标志。</param>
    /// <returns>代理自身。</returns>
    [VirtualFunction(2)]
    public partial AgentCollectablesShop* Dtor
    (
        byte freeFlags
    );

    /// <summary>
    ///     打开采集品交易界面；该代理的显示由 <see cref="Update" /> 延迟完成，此方法不执行任何操作。
    /// </summary>
    [VirtualFunction(3)]
    public partial void Show();

    /// <summary>
    ///     关闭采集品交易界面并释放采集品交易数据。
    /// </summary>
    [VirtualFunction(5)]
    public partial void Hide();

    /// <summary>
    ///     每帧推进界面数据刷新状态机，并在界面尚未打开时打开界面。
    /// </summary>
    /// <param name="frameCount">当前帧计数。</param>
    [VirtualFunction(7)]
    public partial void Update
    (
        uint frameCount
    );

    /// <summary>
    ///     提交当前选中的采集品。
    /// </summary>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1 48 8B 49")]
    public partial void HandIn();

    /// <summary>
    ///     初始化采集品交易数据，并请求 CollectablesShop 表。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 41 8B E8 48 8B FA 48 8B D9 0F 85 ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8B C0 45 33 C9 33 D2 B9 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 0F 84 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 48 89 43 ?? 48 85 C0 0F 84 ?? ?? ?? ?? 48 89 7B ?? 33 FF 89 A8 ?? ?? ?? ?? 48 8D 05"
    )]
    public partial void InitializeData();

    /// <summary>
    ///     结束采集品交易，通知打开者并关闭界面。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 01 FF 50 ?? 84 C0 74 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B 53 ?? 45 33 C0 48 8B C8 4C 8B 08 41 FF 91 ?? ?? ?? ?? 48 8B 43 ?? 48 8B CB"
    )]
    public partial void EndData();

    /// <summary>
    ///     把当前选中的类别、物品与提交数量回传给打开该代理的事件回调接口。
    /// </summary>
    [MemberFunction("4C 8B DC 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B 49")]
    public partial void NotifyOpener();

    /// <summary>
    ///     打开采集品交易界面，并传入类别图标、布局类型与禁用掩码。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 41 ?? 45 33 E4"
    )]
    public partial void ShowAddon();

    /// <summary>
    ///     界面尚未打开时打开界面，否则按刷新状态位刷新界面数据。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 79 ?? ?? 48 8B D9 75 ?? 48 83 C4 ?? 5B E9 ?? ?? ?? ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 53 ?? 48 8B 08 4C 8B 81 ?? ?? ?? ?? 48 8B C8 41 FF D0 84 C0 74 ?? 48 8B 43"
    )]
    public partial void TryOpenOrRefresh();

    /// <summary>
    ///     按刷新状态位刷新界面数据。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 41 ?? 48 8B D9 0F B6 90")]
    public partial void RefreshState();

    /// <summary>
    ///     构造并发送物品列表数据。
    /// </summary>
    /// <returns>是否成功发送。</returns>
    [MemberFunction("40 55 53 56 57 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 89 A4 24")]
    public partial bool RefreshList();

    /// <summary>
    ///     构造并发送选中物品的详情数据。
    /// </summary>
    /// <param name="secondItem">是否使用第二个物品索引。</param>
    /// <returns>是否成功发送。</returns>
    [MemberFunction("40 53 55 57 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 89 B4 24")]
    public partial bool RefreshDetail
    (
        bool secondItem
    );

    /// <summary>
    ///     重建当前物品可提交的背包格列表，并设置列表刷新状态位。
    /// </summary>
    [MemberFunction("40 56 48 83 EC ?? 48 8B 41 ?? 48 8B F1 80 88")]
    public partial void RebuildSubmitItems();

    /// <summary>
    ///     重建当前物品可提交的背包格列表。
    /// </summary>
    [MemberFunction("40 56 48 83 EC ?? 48 8B 51 ?? 48 8B F1 48 8B 82")]
    public partial void RebuildSubmitItemsQuiet();

    /// <summary>
    ///     在界面可见时关闭界面。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 01 FF 50 ?? 84 C0 74 ?? 48 8B 4B ?? 48 8B 01 FF 50 ?? 8B 53 ?? 45 33 C0 48 8B C8 4C 8B 08 48 83 C4 ?? 5B 49 FF A1 ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 4C 8B DC"
    )]
    public partial void CloseAddon();

    /// <summary>
    ///     取得奖励物品行在奖励物品缓存中的索引，不存在时追加。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <returns>奖励物品缓存索引。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B 49 ?? 48 B8 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B FA 4D 8B 59 ?? 49 8B 49 ?? 49 2B CB 48 F7 E9 4C 8B C2"
    )]
    public partial ushort GetRewardItemIndex
    (
        CollectablesShopData.Item* item
    );

    /// <summary>
    ///     取得奖励货币行在奖励货币缓存中的索引，不存在时追加。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <returns>奖励货币缓存索引。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B 49 ?? 48 B8 ?? ?? ?? ?? ?? ?? ?? ?? 48 8B FA 4D 8B 59 ?? 49 8B 49 ?? 49 2B CB 48 F7 E9 33 C0"
    )]
    public partial ushort GetRewardScripIndex
    (
        CollectablesShopData.Item* item
    );

    /// <summary>
    ///     取得精炼行在精炼缓存中的索引，不存在时追加。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <returns>精炼缓存索引。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B 49")]
    public partial ushort GetRefineIndex
    (
        CollectablesShopData.Item* item
    );

    /// <summary>
    ///     取得物品组在物品组缓存中的索引，不存在时追加。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <returns>物品组缓存索引。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B 41 ?? 48 B8")]
    public partial ushort GetItemGroupIndex
    (
        CollectablesShopData.Item* item
    );

    /// <summary>
    ///     按普通货币模式填充选中物品的详情数据。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否全部成功填充。</returns>
    [MemberFunction("4C 89 44 24 ?? 48 89 54 24 ?? 48 89 4C 24 ?? 53 57 41 54 41 56")]
    public partial bool FillDetailNormal
    (
        CollectablesShopData.Item* item,
        AtkValue*                  values
    );

    /// <summary>
    ///     按特殊货币模式填充选中物品的详情数据。
    /// </summary>
    /// <param name="item">物品条目。</param>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否全部成功填充。</returns>
    [MemberFunction("4C 89 44 24 ?? 48 89 4C 24 ?? 53 55 56 57 41 57")]
    public partial bool FillDetailScrip
    (
        CollectablesShopData.Item* item,
        AtkValue*                  values
    );

    /// <summary>
    ///     生成采集品评价区间文本。
    /// </summary>
    /// <param name="classJobID">当前职业。</param>
    /// <param name="lowerBound">区间下界。</param>
    /// <param name="upperBound">区间上界。</param>
    /// <param name="addonTextID">文本格式编号。</param>
    /// <returns>区间文本。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 80 EA")]
    public partial CStringPointer BuildThresholdText
    (
        byte   classJobID,
        ushort lowerBound,
        ushort upperBound,
        uint   addonTextID
    );

    /// <summary>
    ///     在指定背包中查找可提交的格子并追加到可提交列表。
    /// </summary>
    /// <param name="itemID">物品 id。</param>
    /// <param name="requiredCount">所需数量。</param>
    /// <param name="containerGroup">背包分组，取值 0 至 5。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 4C 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 45 33 FF")]
    public partial void AddRewardItemEntry
    (
        uint   itemID,
        ushort requiredCount,
        uint   containerGroup
    );

    /// <summary>
    ///     为物品条目建立奖励货币、精炼与物品组缓存的索引。
    /// </summary>
    /// <param name="item">物品条目。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B 49 ?? 48 8B DA")]
    public partial void ProcessItem
    (
        CollectablesShopData.Item* item
    );

    /// <summary>
    ///     处理界面回调事件。
    /// </summary>
    /// <param name="values">事件参数。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B FA 48 8B CA E8 ?? ?? ?? ?? 83 C0 ?? 83 F8 ?? 0F 87 ?? ?? ?? ?? 48 8D 0D ?? ?? ?? ?? 48 98 44 8B 84 81 ?? ?? ?? ?? 4C 03 C1 41 FF E0 48 8B 43 ?? 33 C9"
    )]
    public partial bool HandleEvent
    (
        AtkValue* values
    );

    /// <summary>
    ///     代理内嵌的事件接口子对象，虚表槽位与 <see cref="AtkModuleInterface.AtkEventInterface" /> 兼容。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe partial struct CollectablesShopEventInterface
    {
        /// <summary>
        ///     析构所属代理。
        /// </summary>
        /// <param name="freeFlags">释放标志。</param>
        /// <returns>所属代理。</returns>
        [VirtualFunction(0)]
        public partial AgentCollectablesShop* Dtor
        (
            byte freeFlags
        );

        /// <summary>
        ///     接收界面回调。
        /// </summary>
        /// <param name="returnValue">返回值容器。</param>
        /// <param name="values">事件参数。</param>
        /// <param name="valueCount">事件参数数量。</param>
        /// <param name="eventKind">事件种类。</param>
        /// <returns>返回值容器。</returns>
        [VirtualFunction(1)]
        public partial AtkValue* ReceiveEvent
        (
            AtkValue* returnValue,
            AtkValue* values,
            uint      valueCount,
            ulong     eventKind
        );
    }

    /// <summary>
    ///     采集品交易数据，由代理在 UI 内存空间中分配并持有。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 864)]
    public unsafe partial struct CollectablesShopData
    {
        /// <summary>
        ///     各职业类别对应的 CollectablesShopItem 表等待器，共 11 项。
        /// </summary>
        [FieldOffset(0)]
        [FixedSizeArray]
        internal FixedSizeArray11<Pointer<ItemSheetWaiter>> ItemSheetWaitersStorage;

        /// <summary>
        ///     奖励货币行缓存。
        /// </summary>
        [FieldOffset(88)]
        public StdVector<RewardScripEntry> RewardScrips;

        /// <summary>
        ///     奖励物品行缓存。
        /// </summary>
        [FieldOffset(112)]
        public StdVector<RewardItemEntry> RewardItems;

        /// <summary>
        ///     精炼行缓存。
        /// </summary>
        [FieldOffset(136)]
        public StdVector<RefineEntry> Refines;

        /// <summary>
        ///     物品组行缓存。
        /// </summary>
        [FieldOffset(160)]
        public StdVector<ItemGroupEntry> ItemGroups;

        /// <summary>
        ///     职业类别，共 11 项。
        /// </summary>
        [FieldOffset(184)]
        [FixedSizeArray]
        internal FixedSizeArray11<Category> CategoriesStorage;

        /// <summary>
        ///     打开该数据的事件 id，由采集品交易事件处理器传入。
        /// </summary>
        [FieldOffset(536)]
        public uint EventID;

        /// <summary>
        ///     奖励类型：0 表示普通货币，1 表示特殊货币。
        /// </summary>
        [FieldOffset(540)]
        public byte RewardType;

        /// <summary>
        ///     采集品评价区间文本。
        /// </summary>
        [FieldOffset(544)]
        public Utf8String ThresholdText;

        /// <summary>
        ///     驱动各阶段表加载的等待器。
        /// </summary>
        [FieldOffset(648)]
        public SheetWaiter Waiter;

        /// <summary>
        ///     采集品交易界面的界面 id，未打开时为 0。
        /// </summary>
        [FieldOffset(824)]
        public uint AddonID;

        /// <summary>
        ///     当前物品可提交的背包格列表。
        /// </summary>
        [FieldOffset(832)]
        public StdVector<SubmitItem> SubmitItems;

        /// <summary>
        ///     当前选中的物品索引。
        /// </summary>
        [FieldOffset(856)]
        public ushort SelectedItemIndex;

        /// <summary>
        ///     当前悬停的物品索引。
        /// </summary>
        [FieldOffset(858)]
        public ushort HoveredItemIndex;

        /// <summary>
        ///     当前选中的类别索引。
        /// </summary>
        [FieldOffset(860)]
        public byte SelectedCategoryIndex;

        /// <summary>
        ///     已加载完成的类别数量。
        /// </summary>
        [FieldOffset(861)]
        public byte LoadedCategoryCount;

        /// <summary>
        ///     全部类别均无可用物品时为 <see langword="true" />。
        /// </summary>
        [FieldOffset(862)]
        public bool AllItemsUnavailable;

        /// <summary>
        ///     界面数据刷新状态位。
        /// </summary>
        [FieldOffset(863)]
        public RefreshFlags RefreshFlags;

        /// <summary>
        ///     初始化采集品交易数据。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 0F 57 C0 33 C0 0F 11 01 33 ED")]
        public partial void Ctor();

        /// <summary>
        ///     设置事件 id 并请求 CollectablesShop 表。
        /// </summary>
        /// <param name="eventID">事件 id。</param>
        [MemberFunction
        (
            "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 41 ?? 33 ED 8B F2 48 89 6C 24 ?? 48 8B F9 89 6C 24 ?? 89 6C 24 ?? 89 90 ?? ?? ?? ?? 48 8D 05"
        )]
        public partial void LoadMainSheet
        (
            uint eventID
        );

        /// <summary>
        ///     CollectablesShop 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("40 57 48 83 EC ?? 4C 8B 41 ?? 45 0F B6 D9")]
        public partial void OnMainSheetLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     CollectablesShopItem 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="category">所属类别。</param>
        [MemberFunction("40 55 56 41 56 41 57 48 83 EC ?? 48 83 79")]
        public partial void OnItemSheetLoaded
        (
            void**    rows,
            ushort    rowCount,
            Category* category
        );

        /// <summary>
        ///     全部类别的物品加载完成后，建立缓存索引并继续请求后续表。
        /// </summary>
        [MemberFunction
        (
            "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 33 D2"
        )]
        public partial void PostProcessItems();

        /// <summary>
        ///     请求 Item 表，并把物品 id 与物品条目的对应关系写入等待器。
        /// </summary>
        [MemberFunction("4C 8B DC 49 89 5B ?? 49 89 6B ?? 49 89 73 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B 59")]
        public partial void LoadItemSheet();

        /// <summary>
        ///     Item 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        /// <param name="context">等待器上下文。</param>
        [MemberFunction("48 89 6C 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 41 0F B6 F9")]
        public partial void OnItemRowLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload,
            void*  context
        );

        /// <summary>
        ///     按奖励类型请求 CollectablesShopRewardItem 或 CollectablesShopRewardScrip 表。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 55 56 57 48 83 EC ?? 48 8B 59 ?? 48 8B E9")]
        public partial void LoadRewardSheet();

        /// <summary>
        ///     CollectablesShopRewardScrip 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("4C 8B 51 ?? 4D 85 D2 74 ?? 48 8B 02 0F 10 00 41 8B C0 48 8D 14 C0")]
        public partial void OnRewardScripLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     CollectablesShopRewardItem 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("4C 8B 51 ?? 4D 85 D2 74 ?? 48 8B 02 0F 10 00 41 8B C0 48 8D 14 80")]
        public partial void OnRewardItemLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     请求 CollectablesShopRefine 表。
        /// </summary>
        [MemberFunction("48 89 5C 24 ?? 55 48 83 EC ?? 48 89 7C 24 ?? 48 8B F9")]
        public partial void LoadRefineSheet();

        /// <summary>
        ///     CollectablesShopRefine 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("40 57 48 83 EC ?? 4C 8B D2 48 8B F9 48 8B 51")]
        public partial void OnRefineLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     CollectablesShopItemGroup 表加载完成回调。
        /// </summary>
        /// <param name="rows">表行指针数组。</param>
        /// <param name="rowCount">表行数量。</param>
        /// <param name="isReload">是否为重新加载。</param>
        [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 41 0F B6 F1 48 8B 49 ?? 48 85 C9")]
        public partial void OnItemGroupLoaded
        (
            void** rows,
            uint   rowCount,
            bool   isReload
        );

        /// <summary>
        ///     职业类别。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public struct Category
        {
            /// <summary>
            ///     CollectablesShop 表行 id。
            /// </summary>
            [FieldOffset(0)]
            public ushort RowID;

            /// <summary>
            ///     可用物品数量。
            /// </summary>
            [FieldOffset(2)]
            public ushort ItemCount;

            /// <summary>
            ///     物品条目数组。
            /// </summary>
            [FieldOffset(8)]
            public Item* Items;

            /// <summary>
            ///     该类别涉及的货币物品 id，最多两个。
            /// </summary>
            [FieldOffset(16)]
            [FixedSizeArray]
            internal FixedSizeArray2<uint> CurrencyItemIDsStorage;

            /// <summary>
            ///     该类别涉及的货币物品数量，最多为 2。
            /// </summary>
            [FieldOffset(24)]
            public byte CurrencyItemCount;
        }

        /// <summary>
        ///     物品条目。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 40)]
        public partial struct Item
        {
            /// <summary>
            ///     按当前职业与评价计算得到的精炼结果物品 id，为 0 时表示尚未计算。
            /// </summary>
            [FieldOffset(0)]
            public uint RefinedItemID;

            /// <summary>
            ///     该物品在 CollectablesShopItem 表中的行内索引。
            /// </summary>
            [FieldOffset(4)]
            public ushort RowIndex;

            /// <summary>
            ///     物品的 SubcategorySort 排序键。
            /// </summary>
            [FieldOffset(6)]
            public ushort SubcategorySort;

            /// <summary>
            ///     物品的 LevelItem 排序键。
            /// </summary>
            [FieldOffset(8)]
            public ushort ItemLevelSort;

            /// <summary>
            ///     物品的 ItemUICategory 排序键，低字节为 OrderMinor，高字节为 OrderMajor。
            /// </summary>
            [FieldOffset(10)]
            public ushort ItemUICategorySort;

            /// <summary>
            ///     奖励货币行在奖励货币缓存中的索引。
            /// </summary>
            [FieldOffset(12)]
            public ushort RewardScripIndex;

            /// <summary>
            ///     精炼行在精炼缓存中的索引。
            /// </summary>
            [FieldOffset(14)]
            public ushort RefineIndex;

            /// <summary>
            ///     物品组在物品组缓存中的索引。
            /// </summary>
            [FieldOffset(16)]
            public byte ItemGroupIndex;

            /// <summary>
            ///     物品条目的状态位。
            /// </summary>
            [BitField<bool>(nameof(IsSelected), 0)]
            [FieldOffset(17)]
            public byte Flags;

            /// <summary>
            ///     CollectablesShopItem 表的 Item 字段，HQ 物品会加上 500000。
            /// </summary>
            [FieldOffset(20)]
            public uint ItemID;

            /// <summary>
            ///     CollectablesShopItem 表的 RequiredQuest 字段。
            /// </summary>
            [FieldOffset(24)]
            public uint RequiredQuest;

            /// <summary>
            ///     CollectablesShopItem 表的 LevelMin 字段。
            /// </summary>
            [FieldOffset(28)]
            public ushort LevelMin;

            /// <summary>
            ///     CollectablesShopItem 表的 LevelMax 字段。
            /// </summary>
            [FieldOffset(30)]
            public ushort LevelMax;

            /// <summary>
            ///     CollectablesShopItem 表的 CollectablesShopRefine 字段。
            /// </summary>
            [FieldOffset(32)]
            public ushort RefineRowID;

            /// <summary>
            ///     CollectablesShopItem 表的 CollectablesShopRewardScrip 字段。
            /// </summary>
            [FieldOffset(34)]
            public ushort RewardScripRowID;

            /// <summary>
            ///     CollectablesShopItem 表的 CollectablesShopItemGroup 字段。
            /// </summary>
            [FieldOffset(36)]
            public byte ItemGroupRowID;

            /// <summary>
            ///     CollectablesShopItem 表的 Stars 字段。
            /// </summary>
            [FieldOffset(37)]
            public byte Stars;

            /// <summary>
            ///     CollectablesShopItem 表的 Key 字段，表示评价档位数量。
            /// </summary>
            [FieldOffset(38)]
            public byte Key;
        }

        /// <summary>
        ///     奖励货币行缓存项。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 18)]
        public struct RewardScripEntry
        {
            /// <summary>
            ///     CollectablesShopRewardScrip 表行 id。
            /// </summary>
            [FieldOffset(0)]
            public ushort RowID;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 Currency 字段。
            /// </summary>
            [FieldOffset(2)]
            public byte Currency;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 LowReward 字段。
            /// </summary>
            [FieldOffset(4)]
            public ushort LowReward;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 MidReward 字段。
            /// </summary>
            [FieldOffset(6)]
            public ushort MidReward;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 HighReward 字段。
            /// </summary>
            [FieldOffset(8)]
            public ushort HighReward;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 ExpRatioLow 字段。
            /// </summary>
            [FieldOffset(10)]
            public ushort ExpRatioLow;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 ExpRatioMid 字段。
            /// </summary>
            [FieldOffset(12)]
            public ushort ExpRatioMid;

            /// <summary>
            ///     CollectablesShopRewardScrip 表的 ExpRatioHigh 字段。
            /// </summary>
            [FieldOffset(14)]
            public ushort ExpRatioHigh;
        }

        /// <summary>
        ///     奖励物品行缓存项。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 20)]
        public struct RewardItemEntry
        {
            /// <summary>
            ///     CollectablesShopRewardItem 表行 id。
            /// </summary>
            [FieldOffset(0)]
            public ushort RowID;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 Item 字段。
            /// </summary>
            [FieldOffset(4)]
            public uint ItemID;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 Unknown0 字段。
            /// </summary>
            [FieldOffset(8)]
            public uint Unknown0;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 RewardLow 字段。
            /// </summary>
            [FieldOffset(12)]
            public byte RewardLow;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 RewardMid 字段。
            /// </summary>
            [FieldOffset(13)]
            public byte RewardMid;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 RewardHigh 字段。
            /// </summary>
            [FieldOffset(14)]
            public byte RewardHigh;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 Unknown1 字段。
            /// </summary>
            [FieldOffset(15)]
            public byte Unknown1;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 Unknown2 字段。
            /// </summary>
            [FieldOffset(16)]
            public byte Unknown2;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 Unknown3 字段。
            /// </summary>
            [FieldOffset(17)]
            public byte Unknown3;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 PackedBool0 字段。
            /// </summary>
            [FieldOffset(18)]
            public byte Unknown4;

            /// <summary>
            ///     CollectablesShopRewardItem 表的 PackedBool1 字段。
            /// </summary>
            [FieldOffset(18)]
            public byte Unknown5;
        }

        /// <summary>
        ///     精炼行缓存项。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 10)]
        public struct RefineEntry
        {
            /// <summary>
            ///     CollectablesShopRefine 表行 id。
            /// </summary>
            [FieldOffset(0)]
            public ushort RowID;

            /// <summary>
            ///     CollectablesShopRefine 表的 LowCollectability 字段。
            /// </summary>
            [FieldOffset(2)]
            public ushort LowCollectability;

            /// <summary>
            ///     CollectablesShopRefine 表的 MidCollectability 字段。
            /// </summary>
            [FieldOffset(4)]
            public ushort MidCollectability;

            /// <summary>
            ///     CollectablesShopRefine 表的 HighCollectability 字段。
            /// </summary>
            [FieldOffset(6)]
            public ushort HighCollectability;
        }

        /// <summary>
        ///     物品组行缓存项。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 112)]
        public struct ItemGroupEntry
        {
            /// <summary>
            ///     CollectablesShopItemGroup 表行 id。
            /// </summary>
            [FieldOffset(0)]
            public byte RowID;

            /// <summary>
            ///     CollectablesShopItemGroup 表的 Name 字段。
            /// </summary>
            [FieldOffset(8)]
            public Utf8String Name;
        }

        /// <summary>
        ///     可提交的背包格。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 8)]
        public struct SubmitItem
        {
            /// <summary>
            ///     物品 id。
            /// </summary>
            [FieldOffset(0)]
            public uint ItemID;

            /// <summary>
            ///     背包格索引。
            /// </summary>
            [FieldOffset(4)]
            public ushort Slot;

            /// <summary>
            ///     该格中的物品数量。
            /// </summary>
            [FieldOffset(6)]
            public ushort Count;
        }

        /// <summary>
        ///     驱动表加载的等待器，继承自 <see cref="Common.Component.Excel.ExcelSheetWaiter" /> 的派生类型。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 176)]
        public unsafe partial struct SheetWaiter
        {
            /// <summary>
            ///     等待器虚表。
            /// </summary>
            [FieldOffset(0)]
            public void** VTable;

            /// <summary>
            ///     当前已准备的行数量。
            /// </summary>
            [FieldOffset(8)]
            public uint RowCount;

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
            ///     加载代次，每次重置或重新加载时递增。
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
            ///     行 id 到数据指针的映射。
            /// </summary>
            [FieldOffset(48)]
            public SheetRowMap RowMap;

            /// <summary>
            ///     当前遍历到的行节点。
            /// </summary>
            [FieldOffset(88)]
            public void* CurrentRow;

            /// <summary>
            ///     客户端未读写的保留字段。
            /// </summary>
            [FieldOffset(96)]
            public ulong Unk60;

            /// <summary>
            ///     当前遍历到的行索引。
            /// </summary>
            [FieldOffset(104)]
            public ulong CurrentRowIndex;

            /// <summary>
            ///     表加载完成后的回调。
            /// </summary>
            [FieldOffset(112)]
            public delegate* unmanaged<AgentCollectablesShop*, void**, uint, bool, void> OnLoaded;

            /// <summary>
            ///     表加载完成回调的上下文。
            /// </summary>
            [FieldOffset(120)]
            public void* OnLoadedContext;

            /// <summary>
            ///     备用回调，当前版本未使用。
            /// </summary>
            [FieldOffset(128)]
            public void* SecondaryCallback;

            /// <summary>
            ///     备用回调的上下文，当前版本未使用。
            /// </summary>
            [FieldOffset(136)]
            public void* SecondaryCallbackContext;

            /// <summary>
            ///     等待器的持有者代理。
            /// </summary>
            [FieldOffset(144)]
            public AgentCollectablesShop* Owner;

            /// <summary>
            ///     当前请求的表索引。
            /// </summary>
            [FieldOffset(152)]
            public uint SheetIndex;

            /// <summary>
            ///     已加载的行数量。
            /// </summary>
            [FieldOffset(156)]
            public uint LoadedRowCount;

            /// <summary>
            ///     加载尝试次数。
            /// </summary>
            [FieldOffset(160)]
            public uint LoadAttempt;

            /// <summary>
            ///     加载状态，1 表示已完成行准备。
            /// </summary>
            [FieldOffset(164)]
            public uint State;

            /// <summary>
            ///     为 <see langword="true" /> 时重置等待器不会从表移除自身。
            /// </summary>
            [FieldOffset(168)]
            public bool KeepRegistered;

            /// <summary>
            ///     行描述符已准备完成时为 <see langword="true" />。
            /// </summary>
            [FieldOffset(169)]
            public bool RowsPrepared;

            /// <summary>
            ///     行 id 到数据指针的映射。
            /// </summary>
            [StructLayout(LayoutKind.Explicit, Size = 40)]
            public struct SheetRowMap
            {
                /// <summary>
                ///     映射节点链表头。
                /// </summary>
                [FieldOffset(0)]
                public void* Head;

                /// <summary>
                ///     桶数组。
                /// </summary>
                [FieldOffset(8)]
                public void** Buckets;

                /// <summary>
                ///     桶数量。
                /// </summary>
                [FieldOffset(16)]
                public ulong BucketCount;

                /// <summary>
                ///     当前插入位置。
                /// </summary>
                [FieldOffset(24)]
                public ulong InsertIndex;

                /// <summary>
                ///     已存储的元素数量。
                /// </summary>
                [FieldOffset(32)]
                public ulong Count;
            }
        }

        /// <summary>
        ///     CollectablesShopItem 表的等待器，继承自 <see cref="Common.Component.Excel.ExcelSheetWaiter" /> 的派生类型。
        /// </summary>
        [GenerateInterop]
        [StructLayout(LayoutKind.Explicit, Size = 96)]
        public unsafe partial struct ItemSheetWaiter
        {
            /// <summary>
            ///     等待器虚表。
            /// </summary>
            [FieldOffset(0)]
            public void** VTable;

            /// <summary>
            ///     当前已准备的行数量。
            /// </summary>
            [FieldOffset(8)]
            public uint RowCount;

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
            ///     客户端写入的固定值 -1。
            /// </summary>
            [FieldOffset(48)]
            public long Unk30;

            /// <summary>
            ///     等待器的持有者代理。
            /// </summary>
            [FieldOffset(56)]
            public AgentCollectablesShop* Owner;

            /// <summary>
            ///     表加载完成后的回调。
            /// </summary>
            [FieldOffset(64)]
            public delegate* unmanaged<AgentCollectablesShop*, void**, ushort, Category*, void> OnLoaded;

            /// <summary>
            ///     表加载完成回调的上下文。
            /// </summary>
            [FieldOffset(72)]
            public void* OnLoadedContext;

            /// <summary>
            ///     客户端未读写的保留字段。
            /// </summary>
            [FieldOffset(80)]
            public ulong Unk50;

            /// <summary>
            ///     客户端未读写的保留字段。
            /// </summary>
            [FieldOffset(88)]
            public ulong Unk58;
        }
    }

    /// <summary>
    ///     界面数据刷新状态位。
    /// </summary>
    [Flags]
    public enum RefreshFlags : byte
    {
        /// <summary>
        ///     无待处理刷新。
        /// </summary>
        None = 0,

        /// <summary>
        ///     需要刷新物品列表数据。
        /// </summary>
        List = 1 << 0,

        /// <summary>
        ///     需要刷新选中物品的详情数据。
        /// </summary>
        Detail = 1 << 1,

        /// <summary>
        ///     需要以第二物品索引刷新详情数据。
        /// </summary>
        SecondaryDetail = 1 << 2
    }
}
