using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.Excel;
using NativeShopEventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.ShopEventHandler;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

/// <summary>
///     普通金币商店的事件处理器扩展。
/// </summary>
[GenerateInterop]
[Inherits<NativeShopEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 12952)]
public unsafe partial struct ShopEventHandler
{
    /// <summary>
    ///     商店所属的节日 id，来自 GilShop 表的 FestivalId 列；为 0 时不校验节日。
    /// </summary>
    [FieldOffset(564)]
    public ushort FestivalID;

    /// <summary>
    ///     商店所属节日的阶段，来自 GilShop 表的 FestivalPhase 列；为 0 时只校验节日 id。
    /// </summary>
    [FieldOffset(566)]
    public ushort FestivalPhase;

    /// <summary>
    ///     解锁该商店所需的 Quest 表行 id，来自 GilShop 表的 Quest 列；为 0 时不校验解锁条件。
    /// </summary>
    [FieldOffset(568)]
    public uint QuestID;

    /// <summary>
    ///     商店参与解锁校验，来自 GilShop 表 Unknown2 列的最低位；为 0 时商店一律视为未解锁。
    /// </summary>
    [FieldOffset(572)]
    public bool UnlockCheckEnabled;

    /// <summary>
    ///     当前商店的商品，共 60 项。
    /// </summary>
    [FieldOffset(576)]
    [FixedSizeArray]
    internal FixedSizeArray60<ShopItem> ItemsStorage;

    /// <summary>
    ///     <see cref="Items" /> 中需要显示的商品索引，共 60 项。
    /// </summary>
    [FieldOffset(10660)]
    [FixedSizeArray]
    internal FixedSizeArray60<int> VisibleItemIndexStorage;

    /// <summary>
    ///     构造时清零，本类型的四十四项成员函数均未读取的保留字段。
    /// </summary>
    [FieldOffset(10912)]
    public int Unk2AA0;

    /// <summary>
    ///     回购列表，共 10 项。
    /// </summary>
    [FieldOffset(11048)]
    [FixedSizeArray]
    internal FixedSizeArray10<NativeShopEventHandler.BuybackItem> BuybackStorage;

    /// <summary>
    ///     上一次交易的服务器结果码，为 0 表示成功。
    /// </summary>
    [FieldOffset(12928)]
    public int TransactionResult;

    /// <summary>
    ///     创建 GilShop 表行的读取等待器并提交该商店对应行的读取请求。
    /// </summary>
    /// <returns>读取是否已提交。</returns>
    [VirtualFunction(2)]
    public partial bool StartReadGilShop();

    /// <summary>
    ///     商店当前是否可以进入。
    /// </summary>
    /// <returns>是否已解锁。</returns>
    [VirtualFunction(7)]
    public partial bool IsUnlocked();

    /// <summary>
    ///     处理商店场景的开始与结束，并按场景状态分派到购买、出售与回购的结果处理。
    /// </summary>
    /// <param name="chara">触发场景的角色。</param>
    /// <param name="scene">场景编号，0 为开始，10 为结果。</param>
    /// <param name="intParams">场景的整数参数。</param>
    /// <param name="intParamCount">整数参数数量。</param>
    [VirtualFunction(35)]
    public partial void ProcessShopScene
    (
        BattleChara* chara,
        short        scene,
        int*         intParams,
        byte         intParamCount
    );

    /// <summary>
    ///     处理 1687 至 1689 号战斗日志消息，把整数参数格式化为文本参数后输出。
    /// </summary>
    /// <param name="logMessageID">日志消息编号。</param>
    /// <param name="intParams">日志消息的整数参数。</param>
    /// <param name="intParamCount">整数参数数量。</param>
    [VirtualFunction(45)]
    public partial void ProcessLogMessage
    (
        uint logMessageID,
        int* intParams,
        byte intParamCount
    );

    /// <summary>
    ///     物品表行读取完成回调，填充商店商品或回购列表后按待处理的交易类型发起购买、出售或回购。
    /// </summary>
    /// <param name="rows">读取完成的物品表行数组。</param>
    /// <param name="rowCount">表行数量。</param>
    /// <param name="reserved">未使用的附加参数。</param>
    [VirtualFunction(47)]
    public partial void OnItemDataRead
    (
        ExcelRow** rows,
        ulong      rowCount,
        ulong      reserved
    );

    /// <summary>
    ///     把全零参数作为场景任务提交，并把处理器标记为等待回包。
    /// </summary>
    [VirtualFunction(86)]
    public partial void SendEmptyShopScene();

    /// <summary>
    ///     以对象参数取得图标 id，经事件框架的映射表转换后以网络字节序返回。
    /// </summary>
    /// <param name="gameObject">参与换算的对象。</param>
    /// <returns>映射后的 16 位值；映射表中不存在时低字节为 1。</returns>
    [VirtualFunction(212)]
    public partial ushort GetIconMappingForObject
    (
        GameObject* gameObject
    );

    /// <summary>
    ///     商店图标 id，即 GilShop 表的 Icon 列。
    /// </summary>
    /// <returns>商店图标 id。</returns>
    [VirtualFunction(227)]
    public partial uint GetIconID();

    /// <summary>
    ///     商店占用的状态 id。
    /// </summary>
    /// <returns>固定返回 31。</returns>
    [VirtualFunction(232)]
    public partial uint GetOccupiedConditionID();

    /// <summary>
    ///     以指定事件 id 构造事件处理器。
    /// </summary>
    /// <param name="eventID">商店事件 id，低 16 位为商店编号。</param>
    /// <returns>事件处理器自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 8B EA 48 8B F1 E8")]
    public partial ShopEventHandler* Ctor
    (
        uint eventID
    );

    /// <summary>
    ///     在堆上分配并构造一个商店事件处理器。
    /// </summary>
    /// <param name="eventID">商店事件 id。</param>
    /// <returns>新建的事件处理器。</returns>
    [MemberFunction("40 53 48 83 EC ?? 0F B7 D9 45 33 C0 B9 ?? ?? ?? ?? 33 D2 E8 ?? ?? ?? ?? 48 85 C0 74 ?? 8B D3 48 8B C8 0F BA EA")]
    public static partial ShopEventHandler* CreateInstance
    (
        ushort eventID
    );

    /// <summary>
    ///     按交易所处的模式把客户端操作结果转发给商店界面或回购界面。
    /// </summary>
    /// <param name="operation">操作类型。</param>
    /// <param name="arg1">操作参数一。</param>
    /// <param name="arg2">操作参数二。</param>
    /// <returns>是否处理了该操作。</returns>
    [MemberFunction("40 55 53 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 49 63 F8")]
    public partial bool ExecuteAgentOperation
    (
        int operation,
        int arg1,
        int arg2
    );

    /// <summary>
    ///     出售当前选中商品的整叠，不依赖具体的事件处理器实例。
    /// </summary>
    /// <param name="inventoryType">商品所在的背包容器。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 8B F2 8B E9")]
    public static partial void SellStackStatic
    (
        InventoryType inventoryType
    );

    /// <summary>
    ///     出售指定背包容器中当前选中商品的整叠。
    /// </summary>
    /// <param name="inventoryType">商品所在的背包容器。</param>
    /// <param name="slot">商品所在的背包格索引。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 41 8B F0")]
    public partial void SellStack
    (
        int           slot,
        InventoryType inventoryType
    );

    /// <summary>
    ///     解除待售背包格的占用，并按交易类型执行购买或出售。
    /// </summary>
    /// <param name="skipExecution">仅解除占用而不执行交易。</param>
    /// <returns>固定返回 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 44 0F B7 81 ?? ?? ?? ?? 8B FA")]
    public partial bool ExecuteTransaction
    (
        int skipExecution
    );

    /// <summary>
    ///     设置本次交易是否与雇员进行。
    /// </summary>
    /// <param name="isTradingWithRetainer">是否与雇员交易。</param>
    [MemberFunction("88 91 ?? ?? ?? ?? 84 D2 0F 85")]
    public partial void SetIsTradingWithRetainer
    (
        bool isTradingWithRetainer
    );

    /// <summary>
    ///     交易正在等待服务器回包，或已弹出确认对话框。
    /// </summary>
    /// <returns>是否处于等待状态。</returns>
    [MemberFunction("80 B9 ?? ?? ?? ?? ?? 75 ?? 83 3D")]
    public partial bool IsWaiting();

    /// <summary>
    ///     按回购列表中的索引查询回购价格。
    /// </summary>
    /// <param name="buybackIndex">回购列表索引。</param>
    /// <returns>回购价格。</returns>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B C8")]
    public static partial uint GetBuybackItemPrice
    (
        uint buybackIndex
    );

    /// <summary>
    ///     按回购列表中的索引填充物品数据，不依赖具体的事件处理器实例。
    /// </summary>
    /// <param name="buybackIndex">回购列表索引。</param>
    /// <param name="item">接收物品数据的背包物品。</param>
    /// <returns>是否填充成功。</returns>
    [MemberFunction("40 53 56 48 83 EC ?? 8B D9")]
    public static partial bool GetBuybackItemDataStatic
    (
        uint           buybackIndex,
        InventoryItem* item
    );

    /// <summary>
    ///     物品在背包间移动时把待售背包格跟随移动，使交易仍然指向同一格物品。
    /// </summary>
    /// <param name="sourceContainer">移动前所在的背包容器。</param>
    /// <param name="sourceSlot">移动前的背包格索引。</param>
    /// <param name="targetContainer">移动后所在的背包容器。</param>
    /// <param name="targetSlot">移动后的背包格索引。</param>
    /// <returns>待售背包格是否随之更新。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 41 8B F9 41 8B F0 8B EA 8B D9")]
    public static partial bool HandleItemMoved
    (
        InventoryType sourceContainer,
        ushort        sourceSlot,
        InventoryType targetContainer,
        ushort        targetSlot
    );

    /// <summary>
    ///     重新统计商品与回购列表中各物品的持有数量，并按需刷新界面。
    /// </summary>
    /// <returns>是否发生了数量变化。</returns>
    [MemberFunction("48 89 5C 24 ?? 41 56 48 83 EC ?? E8")]
    public static partial bool UpdateOwnedItemCounts();

    /// <summary>
    ///     GilShop 表行读取完成回调，写入商店名称、图标、节日与解锁条件并开始读取商品。
    /// </summary>
    /// <param name="row">读取完成的表行。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 02 48 8B D9 48 8B 38 48 85 FF 0F 84 ?? ?? ?? ?? 8B 47")]
    public partial void OnGilShopRead
    (
        ExcelRow** row
    );

    /// <summary>
    ///     GilShopItem 表行读取完成回调，写入商品的解锁条件、排序与显示相关列。
    /// </summary>
    /// <param name="rows">读取完成的表行数组。</param>
    /// <param name="rowCount">表行数量。</param>
    [MemberFunction("40 53 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4D 8B F8")]
    public partial void OnGilShopItemRead
    (
        ExcelRow** rows,
        ulong      rowCount
    );

    /// <summary>
    ///     根据持有金币和商品状态生成商品数据，并打开或刷新商店。
    /// </summary>
    /// <param name="open">是否新建商店界面。</param>
    [MemberFunction("40 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B F9")]
    public partial void OpenOrRefreshAddon
    (
        bool open
    );

    /// <summary>
    ///     生成回购列表数据，并打开或刷新商店的回购页。
    /// </summary>
    [MemberFunction("40 55 41 57 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B F9")]
    public partial void OpenOrRefreshAddonBuyback();

    /// <summary>
    ///     弹出购买确认对话框。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B F1 48 8D 4D"
    )]
    public partial void AskConfirmBuy();

    /// <summary>
    ///     弹出出售确认对话框。
    /// </summary>
    [MemberFunction("40 55 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 83 B9")]
    public partial void AskConfirmSell();

    /// <summary>
    ///     以指定文本弹出是否确认的对话框。
    /// </summary>
    /// <param name="message">对话框正文。</param>
    /// <returns>对话框界面编号。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B D9 48 89 0D")]
    public partial uint OpenConfirmation
    (
        Utf8String* message
    );

    /// <summary>
    ///     关闭商店界面与购买确认对话框。
    /// </summary>
    /// <param name="clearHandler">是否同时清空静态代理持有的处理器引用。</param>
    /// <returns>是否关闭了任意界面。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 0F B6 FA 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 0F 84")]
    public partial bool CloseAddons
    (
        bool clearHandler
    );

    /// <summary>
    ///     按回购列表中的索引填充物品数据。
    /// </summary>
    /// <param name="buybackIndex">回购列表索引。</param>
    /// <param name="item">接收物品数据的背包物品。</param>
    /// <returns>是否填充成功。</returns>
    [MemberFunction("40 55 56 48 83 EC ?? 0F B6 C2")]
    public partial bool GetBuybackItemData
    (
        byte           buybackIndex,
        InventoryItem* item
    );

    /// <summary>
    ///     把回购列表中的指定项复制到目标结构。
    /// </summary>
    /// <param name="target">接收数据的回购项。</param>
    /// <param name="buybackIndex">回购列表索引。</param>
    /// <returns>接收数据的回购项。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 41 8B C0 48 8D 99")]
    public partial NativeShopEventHandler.BuybackItem* GetBuybackItem
    (
        NativeShopEventHandler.BuybackItem* target,
        uint                                buybackIndex
    );

    /// <summary>
    ///     出售待售背包格中的物品；与雇员交易时改为通知雇员界面。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 74")]
    public partial void ExecuteSell();

    /// <summary>
    ///     执行回购。
    /// </summary>
    /// <returns>是否发出回购请求。</returns>
    [MemberFunction("48 83 EC ?? 8B 81 ?? ?? ?? ?? 48 8D 54 24")]
    public partial bool ExecuteBuyback();

    /// <summary>
    ///     按服务器回包设置回购列表中的指定项。
    /// </summary>
    /// <param name="intParams">回包中的整数参数。</param>
    /// <param name="intParamCount">整数参数数量。</param>
    /// <param name="mode">回购模式。</param>
    /// <returns>是否设置成功。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 49 8B D9 48 8D B1")]
    public partial bool SetBuyback
    (
        int* intParams,
        byte intParamCount,
        int* mode
    );

    /// <summary>
    ///     请求服务器刷新回购列表。
    /// </summary>
    /// <returns>是否发出了请求。</returns>
    [MemberFunction("48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 45 33 C0 0F 57 C0")]
    public partial bool RequestBuybackUpdate();

    /// <summary>
    ///     开始读取当前商店的 GilShopItem 表行。
    /// </summary>
    /// <returns>是否开始读取。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 33 D2 48 8B F9 45 33 C0 8D 4A ?? E8 ?? ?? ?? ?? 33 DB 48 8B F0 48 85 C0 74")]
    public partial bool StartReadGilShopItems();

    /// <summary>
    ///     商店中的一件商品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 168)]
    public unsafe struct ShopItem
    {
        /// <summary>
        ///     GilShopItem 表的 Item 列，即商品的 Item 表行 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     从商人购买该商品的价格。
        /// </summary>
        [FieldOffset(4)]
        public int PriceBuy;

        /// <summary>
        ///     卖给商人的价格。
        /// </summary>
        [FieldOffset(8)]
        public int PriceSell;

        /// <summary>
        ///     玩家持有的该商品数量。
        /// </summary>
        [FieldOffset(12)]
        public int NumOwned;

        /// <summary>
        ///     该商品的堆叠上限。
        /// </summary>
        [FieldOffset(16)]
        public int StackSize;

        /// <summary>
        ///     该商品为唯一物品。
        /// </summary>
        [FieldOffset(20)]
        public bool IsUnique;

        /// <summary>
        ///     商品在界面中显示的图标 id。
        /// </summary>
        [FieldOffset(24)]
        public uint CategoryIcon;

        /// <summary>
        ///     GilShopItem 表的 Unknown_70_1 列。
        /// </summary>
        [FieldOffset(28)]
        public ushort Unknown70_1;

        /// <summary>
        ///     商品名称。
        /// </summary>
        [FieldOffset(32)]
        public Utf8String ItemName;

        /// <summary>
        ///     该商品以高质量状态出售。
        /// </summary>
        [FieldOffset(136)]
        public bool IsHQ;

        /// <summary>
        ///     GilShopItem 表的 QuestRequired 第一项。
        /// </summary>
        [FieldOffset(140)]
        public uint QuestRequired1;

        /// <summary>
        ///     GilShopItem 表的 QuestRequired 第二项。
        /// </summary>
        [FieldOffset(144)]
        public uint QuestRequired2;

        /// <summary>
        ///     GilShopItem 表的 Unknown_70_2 列，决定解锁条件未满足时该商品是否显示。
        /// </summary>
        [FieldOffset(148)]
        public byte Unknown70_2;

        /// <summary>
        ///     解锁条件未满足时该商品仍然显示。
        /// </summary>
        [FieldOffset(149)]
        public byte ShowWhenLocked;

        /// <summary>
        ///     该商品在 GilShopItem 表中的子行 id。
        /// </summary>
        [FieldOffset(152)]
        public uint SubRowID;

        /// <summary>
        ///     分类排序的主序号。
        /// </summary>
        [FieldOffset(156)]
        public byte CategoryOrderMajor;

        /// <summary>
        ///     分类排序的次序号。
        /// </summary>
        [FieldOffset(157)]
        public byte CategoryOrderMinor;

        /// <summary>
        ///     子分类内的排序值。
        /// </summary>
        [FieldOffset(160)]
        public int SubcategorySort;

        /// <summary>
        ///     GilShopItem 表的 StateRequired 列。
        /// </summary>
        [FieldOffset(164)]
        public ushort StateRequired;

        /// <summary>
        ///     GilShopItem 表的 Patch 列。
        /// </summary>
        [FieldOffset(166)]
        public ushort PatchAdded;
    }
}
