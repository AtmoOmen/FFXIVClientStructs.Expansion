using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using AtkEventInterface = FFXIVClientStructs.FFXIV.Component.GUI.AtkModuleInterface.AtkEventInterface;
using EventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.EventHandler;
using UIModuleInterface = FFXIVClientStructs.FFXIV.Client.UI.UIModuleInterface;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     部队战绩商店代理，保存当前商品的成本和部队战绩余额。
/// </summary>
[Agent(AgentId.FreeCompanyCreditShop)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 272)]
public unsafe partial struct AgentFreeCompanyCreditShop
{
    /// <summary>
    ///     注册到部队信息代理上的事件回调接口子对象，部队信息变更时由此接收通知。
    /// </summary>
    [FieldOffset(40)]
    public FreeCompanyInfoEventHandler InfoEventHandler;

    /// <summary>
    ///     处理物品目录右键菜单的事件子对象。
    /// </summary>
    [FieldOffset(56)]
    public ItemCatalogContextEvent ItemCatalogContextEvent;

    /// <summary>
    ///     接收界面未处理事件并转发购买确认结果的事件回调接口，指向全局静态实例，本代理不持有其所有权。
    /// </summary>
    [FieldOffset(112)]
    public AtkEventInterface* EventInterface;

    /// <summary>
    ///     加载商品物品表数据的事件处理器，打开商店时在 UI 内存空间中分配，关闭时释放。
    /// </summary>
    [FieldOffset(120)]
    public EventHandler* ItemSheetEventHandler;

    /// <summary>
    ///     当前页面的商品成本和数量输入标记，最多 10 项。
    /// </summary>
    [FieldOffset(128)]
    [FixedSizeArray]
    internal FixedSizeArray10<ShopItem> ItemsStorage;

    /// <summary>
    ///     当前页面的有效商品数量。
    /// </summary>
    [FieldOffset(248)]
    public uint ItemCount;

    /// <summary>
    ///     购买确认对话框的界面编号，为 0 表示对话框未打开。
    /// </summary>
    [FieldOffset(252)]
    public uint DialogAddonID;

    /// <summary>
    ///     当前可使用的部队战绩余额。
    /// </summary>
    [FieldOffset(256)]
    public uint Credits;

    /// <summary>
    ///     当前选中的商品索引。
    /// </summary>
    [FieldOffset(260)]
    public uint SelectedItemIndex;

    /// <summary>
    ///     本次购买的数量。
    /// </summary>
    [FieldOffset(264)]
    public uint Quantity;

    /// <summary>
    ///     玩家在部队中的军衔，由部队信息封包写入，并作为界面值回传。
    /// </summary>
    [FieldOffset(268)]
    public byte Rank;

    /// <summary>
    ///     购买确认对话框正在等待回答。
    /// </summary>
    [FieldOffset(269)]
    public bool IsConfirmingPurchase;

    /// <summary>
    ///     部队信息有效，由部队信息封包置位，部队信息变更事件触发时清除。
    /// </summary>
    [FieldOffset(270)]
    public bool IsFreeCompanyInfoValid;

    /// <summary>
    ///     商品数据已从物品表加载完成。
    /// </summary>
    [FieldOffset(271)]
    public bool AreItemsLoaded;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <param name="uiModuleInterface">UI 模块接口。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 80 61 ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 01")]
    public partial AgentFreeCompanyCreditShop* Ctor
    (
        UIModuleInterface* uiModuleInterface
    );

    /// <summary>
    ///     重置虚表并调用基类终结函数。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 41 ?? 48 8D 3D")]
    public partial void PrepareDestructor();

    /// <summary>
    ///     清空商品、余额、数量与各状态标记，并释放商品物品表数据的事件处理器。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 33 FF 48 8B 49 ?? 48 85 C9 74 ?? 48 8B 01 8D 57 ?? FF 10 48 89 7B ?? 89 BB")]
    public partial void Reset();

    /// <summary>
    ///     关闭购买确认对话框并清空对话框界面编号。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 8B BB ?? ?? ?? ?? 85 FF 74 ?? C7 83")]
    public partial void CloseDialogAddon();

    /// <summary>
    ///     打开部队战绩商店界面，创建商品物品表数据的事件处理器并请求 Item 表。
    /// </summary>
    /// <param name="eventInterface">接收界面事件的事件回调接口。</param>
    /// <param name="shopData">FccShop 表行数据，提供商品编号、成本与所需军衔。</param>
    /// <returns>打开的界面编号。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B D9 45 33 E4"
    )]
    public partial uint Open
    (
        AtkEventInterface* eventInterface,
        FccShopData*       shopData
    );

    /// <summary>
    ///     请求部队信息代理刷新数据，并把部队信息标记为无效。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B C8 E8 ?? ?? ?? ?? C6 83 ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24"
    )]
    public partial void RefreshFreeCompanyInfo();

    /// <summary>
    ///     更新部队战绩余额与军衔，并把可购买次数、持有数量与商品状态回传到界面。
    /// </summary>
    /// <param name="credits">当前可使用的部队战绩余额。</param>
    /// <param name="rank">玩家在部队中的军衔。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 89 91 ?? ?? ?? ?? 48 8D 44 24")]
    public partial void UpdateCredits
    (
        uint credits,
        byte rank
    );

    /// <summary>
    ///     根据物品表行补全商品名称、图标与数量输入标记，并把完整界面值回传到界面。
    /// </summary>
    /// <param name="itemRows">物品表行数组。</param>
    /// <param name="rowCount">物品表行数量。</param>
    [MemberFunction
    (
        "4C 8B DC 53 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 49 89 6B ?? 48 8D 44 24 ?? 4D 89 63 ?? BE ?? ?? ?? ?? 4D 89 6B ?? 33 ED"
    )]
    public partial void RefreshItemData
    (
        ExcelRow** itemRows,
        uint       rowCount
    );

    /// <summary>
    ///     从部队信息封包更新部队战绩余额与军衔，并把部队信息标记为有效。
    /// </summary>
    /// <param name="packet">部队信息封包，余额位于偏移 24、军衔位于偏移 49。</param>
    [MemberFunction("66 C7 81 ?? ?? ?? ?? ?? ?? 44 0F B6 42 ?? 8B 52 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC 66 C7 81")]
    public partial void UpdateFromDialogPacket
    (
        void* packet
    );

    /// <summary>
    ///     从部队信息封包更新部队战绩余额与军衔，并把部队信息标记为有效。
    /// </summary>
    /// <param name="packet">部队信息封包，余额位于偏移 16、军衔位于偏移 56。</param>
    [MemberFunction("66 C7 81 ?? ?? ?? ?? ?? ?? 44 0F B6 42 ?? 8B 52 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC 41 56")]
    public partial void UpdateFromInfoPacket
    (
        void* packet
    );

    /// <summary>
    ///     生成商品可购买次数与商品状态界面值。
    /// </summary>
    /// <param name="values">接收结果的界面值数组，可购买次数从索引 110 开始。</param>
    [MemberFunction("41 56 41 57 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 4C 8B FA")]
    public partial void RefreshPurchaseLimits
    (
        AtkValue* values
    );

    /// <summary>
    ///     部队信息变更事件回调，部队退出时刷新部队信息，部队战绩余额变更时更新余额。
    /// </summary>
    /// <param name="infoEvent">本次事件的数据。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 80 7A ?? ?? 48 8B FA 48 8B D9 75")]
    public partial void OnInfoEvent
    (
        InfoEvent* infoEvent
    );

    /// <summary>
    ///     按客户端的单价及可指定数量标记计算商品上限。
    /// </summary>
    /// <param name="itemIndex">当前页面的商品索引。</param>
    /// <param name="maximum">允许的单次购买次数，不能超过 255。</param>
    /// <returns>可购买次数；不允许指定数量的商品返回 -1。</returns>
    public int GetPurchaseLimit
    (
        int itemIndex,
        int maximum
    )
    {
        ref var item = ref Items[itemIndex];
        if (!item.CanSpecifyQuantity)
            return -1;

        return (int)Math.Max(1U, Math.Min(Credits / item.Cost, (uint)Math.Min(maximum, 255)));
    }

    /// <summary>
    ///     内嵌于代理的信息事件回调接口子对象。
    /// </summary>
    [GenerateInterop]
    [Inherits<InfoEventHandlerInterface>]
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public unsafe partial struct FreeCompanyInfoEventHandler
    {
        /// <summary>
        ///     已从部队信息代理的事件处理器链表中移除，分发期间只置位、待分发结束后才真正摘除。
        /// </summary>
        [FieldOffset(8)]
        public bool IsRemoved;
    }

    /// <summary>
    ///     FccShop 表行在内存中的布局。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 124)]
    public unsafe struct FccShopData
    {
        /// <summary>
        ///     Name 列在行内的原始值，需经字符串间接解析后才能作为文本使用。
        /// </summary>
        [FieldOffset(0)]
        public uint Name;

        /// <summary>
        ///     商品数据，共 10 项。
        /// </summary>
        [FieldOffset(4)]
        [FixedSizeArray]
        internal FixedSizeArray10<FccShopItem> ItemDataStorage;
    }

    /// <summary>
    ///     FccShop 表的一项商品。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public unsafe struct FccShopItem
    {
        /// <summary>
        ///     商品对应的 Item 表行 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     购买一件商品消耗的部队战绩。
        /// </summary>
        [FieldOffset(4)]
        public uint Cost;

        /// <summary>
        ///     购买该商品所需的 FCRank 表行 id，为 0 表示不限制军衔。
        /// </summary>
        [FieldOffset(8)]
        public uint FCRankRequired;
    }

    /// <summary>
    ///     当前页面中一项商品的成本、物品及数量输入状态。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct ShopItem
    {
        /// <summary>
        ///     购买一件商品消耗的部队战绩。
        /// </summary>
        [FieldOffset(0)]
        public uint Cost;

        /// <summary>
        ///     商品的物品编号。
        /// </summary>
        [FieldOffset(4)]
        public uint ItemID;

        /// <summary>
        ///     客户端是否为该商品启用数量输入。
        /// </summary>
        [FieldOffset(8)]
        public bool CanSpecifyQuantity;
    }
}
