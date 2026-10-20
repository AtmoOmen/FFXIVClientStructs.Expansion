using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeAgentFreeCompanyChest = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentFreeCompanyChest;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeAgentFreeCompanyChest>]
[Agent(AgentId.FreeCompanyChest)]
[StructLayout(LayoutKind.Explicit, Size = 0x1EF0)]
public unsafe partial struct AgentFreeCompanyChest
{
    /// <summary>
    ///     部队储物柜代理自身的基础接口子对象。
    /// </summary>
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    /// <summary>
    ///     物品上下文菜单事件子对象，用于接收物品右键菜单的点击回调。
    /// </summary>
    [FieldOffset(40)]
    public AgentInventoryContext.InventoryContextEvent InventoryContextEvent;

    /// <summary>
    ///     部队信息事件子对象，用于接收部队数据变更通知。
    /// </summary>
    [FieldOffset(48)]
    public InfoEventHandlerInterface InfoEventHandler;

    /// <summary>
    ///     构造函数中会被置零，但客户端的任何函数都未再读写该字节。
    /// </summary>
    [FieldOffset(56)]
    public byte Unk38;

    /// <summary>
    ///     打开该代理的事件回调接口，由事件场景 <c>EventSceneModuleUsualImpl.CompanyChest</c> 传入，用于在界面关闭时接收关闭事件；由其他途径打开时为
    ///     <see langword="null" />。
    /// </summary>
    [FieldOffset(64)]
    public AtkModuleInterface.AtkEventInterface* OwnerEventInterface;

    /// <summary>
    ///     部队信息代理，首次调用 <c>Show</c> 时通过 <see cref="InfoModule" /> 取得并缓存。
    /// </summary>
    [FieldOffset(72)]
    public InfoProxyFreeCompany* InfoProxyFreeCompany;

    /// <summary>
    ///     当前页签类别。<br />
    ///     0 表示部队储物柜物品页，具体页由 <see cref="TabIndex" /> 指定；<br />
    ///     1 表示部队水晶页；<br />
    ///     2 为界面打开时写入的初始值，此时既非物品页也非水晶页。
    /// </summary>
    [FieldOffset(80)]
    public byte TabType;

    /// <summary>
    ///     当前物品页的箱子序号，取值 0 至 4，对应部队储物柜第 1 至第 5 页。
    /// </summary>
    [FieldOffset(81)]
    public byte TabIndex;

    /// <summary>
    ///     物品图标缓存，共 50 项，与当前物品页的 50 个格子一一对应。
    /// </summary>
    [FieldOffset(88)]
    [FixedSizeArray]
    internal FixedSizeArray50<RaptureAtkModule.ItemCache> ItemCachesStorage;

    /// <summary>
    ///     上一次写入物品列表时统计到的非空格子数量。
    /// </summary>
    [FieldOffset(6888)]
    public int DisplayedItemCount;

    /// <summary>
    ///     上一次写入界面时记录的部队金币数量，用于避免重复刷新。
    /// </summary>
    [FieldOffset(6892)]
    public uint LastFreeCompanyGil;

    /// <summary>
    ///     金币存取对话框中当前输入的金币数量。
    /// </summary>
    [FieldOffset(6896)]
    public uint GilInputAmount;

    /// <summary>
    ///     金币存取方向，<see langword="true" /> 表示从部队取出到个人，<see langword="false" /> 表示由个人存入部队。
    /// </summary>
    [FieldOffset(6900)]
    public bool IsWithdrawingGil;

    /// <summary>
    ///     单按钮提示框（SelectOk2）的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(6904)]
    public uint SelectOkAddonID;

    /// <summary>
    ///     是/否确认框（SelectYesno）的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(6908)]
    public uint YesNoAddonID;

    /// <summary>
    ///     数量输入框（InputNumeric）的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(6912)]
    public uint InputNumericAddonID;

    /// <summary>
    ///     金币存取界面（Bank）的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(6916)]
    public uint BankAddonID;

    /// <summary>
    ///     部队储物柜日志界面（FreeCompanyChestLog）的界面 ID，未打开时为 0。
    /// </summary>
    [FieldOffset(6920)]
    public uint LogAddonID;

    /// <summary>
    ///     正在搬移的源物品。
    /// </summary>
    [FieldOffset(6924)]
    public InventoryItemRef SourceItem;

    /// <summary>
    ///     正在搬移的目标物品。
    /// </summary>
    [FieldOffset(6940)]
    public InventoryItemRef DestinationItem;

    /// <summary>
    ///     当前右键点击的物品，供物品上下文菜单各菜单项使用。
    /// </summary>
    [FieldOffset(6956)]
    public InventoryItemRef ContextItem;

    /// <summary>
    ///     数量输入框的父界面 ID。
    /// </summary>
    [FieldOffset(6972)]
    public uint InputNumericParentAddonID;

    /// <summary>
    ///     代理的待处理状态。
    /// </summary>
    [FieldOffset(6976)]
    public ChestState State;

    /// <summary>
    ///     物品数量超过一格上限时，暂存的源物品。
    /// </summary>
    [FieldOffset(6980)]
    public InventoryItemRef PendingSourceItem;

    /// <summary>
    ///     物品数量超过一格上限时，暂存的目标物品。
    /// </summary>
    [FieldOffset(6996)]
    public InventoryItemRef PendingDestinationItem;

    /// <summary>
    ///     是/否确认框的父界面 ID。
    /// </summary>
    [FieldOffset(7012)]
    public uint YesNoParentAddonID;

    /// <summary>
    ///     部队储物柜数据是否已停止加载且未被锁定。
    /// </summary>
    [FieldOffset(7016)]
    public bool IsChestDataReady;

    /// <summary>
    ///     当前物品页是否无法访问。
    /// </summary>
    [FieldOffset(7017)]
    public bool IsCurrentPageAccessDenied;

    /// <summary>
    ///     可访问性位图，记录各页签的数据是否已可访问。
    /// </summary>
    [BitField<bool>(nameof(IsPage1Accessible),   0)]
    [BitField<bool>(nameof(IsPage2Accessible),   1)]
    [BitField<bool>(nameof(IsPage3Accessible),   2)]
    [BitField<bool>(nameof(IsPage4Accessible),   3)]
    [BitField<bool>(nameof(IsPage5Accessible),   4)]
    [BitField<bool>(nameof(IsCrystalAccessible), 5)]
    [BitField<bool>(nameof(IsGilAccessible),     6)]
    [FieldOffset(7020)]
    public uint AccessMask;

    /// <summary>
    ///     是否禁用界面各页签的交互，打开提示框与确认框期间为 <see langword="true" />。
    /// </summary>
    [FieldOffset(7024)]
    public bool AreTabsDisabled;

    /// <summary>
    ///     部队储物柜日志条目，共 100 项。
    /// </summary>
    [FieldOffset(7028)]
    [FixedSizeArray]
    internal FixedSizeArray100<ChestLogEntry> LogEntriesStorage;

    /// <summary>
    ///     当前已缓存的日志条目数量。
    /// </summary>
    [FieldOffset(7828)]
    public int LogEntryCount;

    /// <summary>
    ///     日志列表向界面写入时的当前索引。
    /// </summary>
    [FieldOffset(7832)]
    public int LogEntryIndex;

    /// <summary>
    ///     用于物品详情代理显示日志条目物品的临时物品对象。
    /// </summary>
    [FieldOffset(7840)]
    public InventoryItem LogDetailItem;

    /// <summary>
    ///     提示框关闭后是否需要打开背包界面。
    /// </summary>
    [FieldOffset(7912)]
    public bool ShouldOpenInventory;

    /// <summary>
    ///     处理页签点击事件。
    /// </summary>
    /// <param name="values">事件参数。</param>
    /// <param name="valueCount">事件参数数量。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 41 56 48 83 EC ?? 48 8B D9 45 8B F0")]
    public partial bool ProcessTabEvent
    (
        AtkValue* values,
        uint      valueCount
    );

    /// <summary>
    ///     单按钮提示框的回调处理。
    /// </summary>
    /// <param name="values">事件参数。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? FF C0 83 F8 ?? 77 ?? 48 8B 03 48 8B CB FF 50 ?? B0 ?? C7 83")]
    public partial bool OnOkPromptCallback
    (
        AtkValue* values
    );

    /// <summary>
    ///     确认并执行源物品与目标物品之间的搬移。
    /// </summary>
    /// <param name="values">事件参数，取第一个整数作为是否取消的标记。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? 48 8D BB")]
    public partial bool ConfirmItemMove
    (
        AtkValue* values
    );

    /// <summary>
    ///     物品上下文菜单的回调处理，根据数量决定直接搬移还是弹出数量输入框。
    /// </summary>
    /// <param name="values">事件参数，取第一个整数作为数量。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B CA")]
    public partial bool OnItemContextMenuCallback
    (
        AtkValue* values
    );

    /// <summary>
    ///     金币存取界面的回调处理。
    /// </summary>
    /// <param name="values">事件参数，取第一个整数作为操作类型。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B F2 48 8B CA")]
    public partial bool OnBankCallback
    (
        AtkValue* values
    );

    /// <summary>
    ///     物品上下文菜单中某个菜单项被点击时的处理。
    /// </summary>
    /// <param name="menuItem">被点击的菜单项编号，0 为取出，1 为装备属性对比，2 为试穿，3 为查看持有情况，4 为展示道具属性，5 为预览。</param>
    /// <returns>事件是否已被处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 41 83 F9 ?? 0F 87")]
    public partial bool OnItemContextMenuClick
    (
        int menuItem
    );

    /// <summary>
    ///     物品上下文菜单事件子对象的关闭回调。
    /// </summary>
    /// <returns>始终返回 <see langword="true" />。</returns>
    [MemberFunction("B0 ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 57 48 83 EC ?? 0F B6 41")]
    public partial bool OnInventoryContextClose();

    /// <summary>
    ///     显示部队储物柜界面。
    /// </summary>
    /// <param name="opener">打开该界面的事件回调接口。</param>
    [MemberFunction("40 55 53 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 83 79")]
    public partial void ShowInternal
    (
        AtkModuleInterface.AtkEventInterface* opener
    );

    /// <summary>
    ///     按索引取得日志条目对应的物品对象。
    /// </summary>
    /// <param name="index">日志条目索引。</param>
    /// <returns>日志条目对应的物品对象；索引越界时返回 <see langword="null" />。</returns>
    [MemberFunction("48 83 EC ?? 85 D2 78 ?? 3B 91 ?? ?? ?? ?? 7D ?? 48 89 5C 24")]
    public partial InventoryItem* GetLogEntryItem
    (
        int index
    );

    /// <summary>
    ///     将服务器下发的日志数据追加到本地日志列表并刷新界面。
    /// </summary>
    /// <param name="logData">日志数据起始地址。</param>
    [MemberFunction("40 53 55 56 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1 48 8B FA")]
    public partial void UpdateLog
    (
        void* logData
    );

    /// <summary>
    ///     把物品搬入部队储物柜。
    /// </summary>
    /// <param name="sourceItem">源物品。</param>
    /// <param name="destinationItem">目标物品。</param>
    /// <param name="sourceRow">源物品的物品表行。</param>
    /// <param name="destinationRow">目标物品的物品表行。</param>
    [MemberFunction("48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 49 8B F8 4C 8B F2 48 8B F1 4D 85 C0")]
    public partial void MoveItemToChest
    (
        InventoryItem* sourceItem,
        InventoryItem* destinationItem,
        void*          sourceRow,
        void*          destinationRow
    );

    /// <summary>
    ///     处理部队水晶与个人水晶之间的兑换。
    /// </summary>
    /// <param name="sourceItem">源物品。</param>
    /// <param name="destinationItem">目标物品。</param>
    /// <param name="sourceRow">源物品的物品表行。</param>
    /// <param name="destinationRow">目标物品的物品表行。</param>
    [MemberFunction("48 89 74 24 ?? 57 41 54 41 56 48 83 EC ?? 48 8B 02 48 8B F1")]
    public partial void ExchangeCrystals
    (
        InventoryItem* sourceItem,
        InventoryItem* destinationItem,
        void*          sourceRow,
        void*          destinationRow
    );

    /// <summary>
    ///     刷新物品页。
    /// </summary>
    /// <param name="numberArray">数字数组数据。</param>
    /// <param name="stringArray">字符串数组数据。</param>
    [MemberFunction("48 8B C4 4C 89 40 ?? 48 89 50 ?? 48 89 48 ?? 41 54 41 56")]
    public partial void UpdateItemPage
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray
    );

    /// <summary>
    ///     刷新水晶页。
    /// </summary>
    /// <param name="numberArray">数字数组数据。</param>
    /// <param name="stringArray">字符串数组数据。</param>
    [MemberFunction("4C 89 44 24 ?? 48 89 4C 24 ?? 53 56 57 41 56 41 57 48 83 EC ?? 48 8B F1 40 32 FF")]
    public partial void UpdateCrystalPage
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray
    );

    /// <summary>
    ///     刷新部队金币数量的显示。
    /// </summary>
    /// <param name="numberArray">数字数组数据。</param>
    /// <param name="stringArray">字符串数组数据。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 33 FF 48 8B 49 ?? 49 8B F0")]
    public partial void UpdateGilDisplay
    (
        NumberArrayData* numberArray,
        StringArrayData* stringArray
    );

    /// <summary>
    ///     切换当前页签。
    /// </summary>
    /// <param name="tabType">页签类别。</param>
    /// <param name="tabIndex">物品页的箱子序号。</param>
    [MemberFunction("41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F1 80 FA")]
    public partial void SetTab
    (
        byte tabType,
        byte tabIndex
    );

    /// <summary>
    ///     向界面发送权限数据。
    /// </summary>
    /// <param name="hideAccess">是否隐藏权限信息。</param>
    [MemberFunction("48 89 5C 24 ?? 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 01 0F B6 DA")]
    public partial void SendPermissionData
    (
        bool hideAccess
    );

    /// <summary>
    ///     向界面发送当前军衔可用的储物柜格数。
    /// </summary>
    [MemberFunction
    (
        "40 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 01 48 8B F9 FF 50 ?? 84 C0 0F 84 ?? ?? ?? ?? 48 8B 4F ?? 48 89 9C 24"
    )]
    public partial void SendChestSlotCount();

    /// <summary>
    ///     向界面发送储物柜状态。
    /// </summary>
    [MemberFunction
    (
        "41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 01 4C 8B F1 FF 50 ?? 84 C0 0F 84 ?? ?? ?? ?? 48 89 9C 24 ?? ?? ?? ?? 48 8D 44 24 ?? 48 89 B4 24 ?? ?? ?? ?? 33 F6"
    )]
    public partial void SendChestState();

    /// <summary>
    ///     取得五个储物柜页签的访问等级位图，每页占两位。
    /// </summary>
    /// <returns>位 0 至 1 为第 1 页，位 2 至 3 为第 2 页，依次类推直至位 8 至 9 为第 5 页。</returns>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 41 56 48 83 EC ?? 45 33 F6 48 8B F1 41 8B EE")]
    public partial uint GetChestAccessFlags();

    /// <summary>
    ///     取得部队水晶的访问等级。
    /// </summary>
    /// <returns>0 为无权限，1 为仅查看，2 为仅存入，3 为完全权限。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 33 DB 48 8B 49 ?? 48 85 C9 74 ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? 8D 43 ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? B8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? B8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 8B C3 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial uint GetCrystalAccessLevel();

    /// <summary>
    ///     取得部队金币的访问等级。
    /// </summary>
    /// <returns>0 为无权限，1 为仅查看，2 为仅存入，3 为完全权限。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 33 DB 48 8B 49 ?? 48 85 C9 74 ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? 8D 43 ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? B8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 84 C0 74 ?? B8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 48 8B 4F ?? B2 ?? E8 ?? ?? ?? ?? 8B C3 48 8B 5C 24 ?? 48 83 C4 ?? 5F C3 CC CC CC CC CC CC CC CC CC 80 FA"
    )]
    public partial uint GetGilAccessLevel();

    /// <summary>
    ///     处理部队水晶兑换的结果。
    /// </summary>
    /// <param name="item">兑换后的物品。</param>
    [MemberFunction("40 55 56 57 48 83 EC ?? 33 ED")]
    public partial void HandleCrystalExchangeResult
    (
        InventoryItem* item
    );

    /// <summary>
    ///     打开背包界面。
    /// </summary>
    /// <param name="unknown">传给背包界面的附加参数。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 0F B6 F2 48 8B 49")]
    public partial void OpenInventory
    (
        bool unknown
    );

    /// <summary>
    ///     关闭背包界面。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B 48 ?? 48 85 C9 74 ?? BA")]
    public partial void CloseInventory();

    /// <summary>
    ///     关闭提示框并打开背包界面。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8B D9 39 B9 ?? ?? ?? ?? 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? 89 BB ?? ?? ?? ?? 39 BB"
    )]
    public partial void ClosePromptAndOpenInventory();

    /// <summary>
    ///     在没有其他弹窗时打开背包界面。
    /// </summary>
    [MemberFunction("83 B9 ?? ?? ?? ?? ?? 75 ?? 83 B9 ?? ?? ?? ?? ?? 75 ?? 80 B9")]
    public partial void TryOpenInventory();

    /// <summary>
    ///     显示单按钮提示框。
    /// </summary>
    /// <param name="textID">提示文本的 Addon 表行号。</param>
    [MemberFunction("48 89 6C 24 ?? 56 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 8B EA")]
    public partial void ShowOkPrompt
    (
        uint textID
    );

    /// <summary>
    ///     关闭单按钮提示框。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 76 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 74 24"
    )]
    public partial void CloseOkPrompt();

    /// <summary>
    ///     显示搬移确认框。
    /// </summary>
    /// <param name="sourceItem">源物品。</param>
    /// <param name="destinationItem">目标物品。</param>
    /// <param name="quantity">搬移数量。</param>
    /// <param name="parentAddonID">父插件 id。</param>
    [MemberFunction("48 89 74 24 ?? 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 01 45 8B F9")]
    public partial void ShowMoveConfirm
    (
        InventoryItem* sourceItem,
        InventoryItem* destinationItem,
        uint           quantity,
        uint           parentAddonID
    );

    /// <summary>
    ///     关闭是/否确认框。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 56"
    )]
    public partial void CloseYesNo();

    /// <summary>
    ///     显示数量输入框。
    /// </summary>
    /// <param name="sourceItem">源物品。</param>
    /// <param name="destinationItem">目标物品。</param>
    /// <param name="quantity">可搬移的最大数量。</param>
    /// <param name="parentAddonID">父插件 id。</param>
    [MemberFunction("40 56 57 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 01")]
    public partial void ShowQuantityInput
    (
        InventoryItem* sourceItem,
        InventoryItem* destinationItem,
        uint           quantity,
        uint           parentAddonID
    );

    /// <summary>
    ///     关闭数量输入框。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 55 53"
    )]
    public partial void CloseQuantityInput();

    /// <summary>
    ///     刷新金币存取界面。
    /// </summary>
    [MemberFunction
    (
        "40 55 53 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B D9 48 8B 49 ?? 48 85 C9 0F 84 ?? ?? ?? ?? B2"
    )]
    public partial void UpdateBankDialog();

    /// <summary>
    ///     关闭金币存取界面。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 B9 ?? ?? ?? ?? ?? 48 8B D9 76 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93 ?? ?? ?? ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 44 8B C2"
    )]
    public partial void CloseBank();

    /// <summary>
    ///     打开物品上下文菜单。
    /// </summary>
    /// <param name="index">当前物品页中物品的格子序号。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 44 8B C2")]
    public partial void OpenItemContextMenu
    (
        int index
    );

    /// <summary>
    ///     检查当前玩家对部队金币的访问权限，并记录到可访问性位图。
    /// </summary>
    /// <returns>玩家是否可以访问部队金币。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 85 C9 74 ?? B2")]
    public partial bool CheckGilAccess();

    /// <summary>
    ///     检查当前页签是否可以访问。
    /// </summary>
    /// <param name="checkGil"><see langword="true" /> 表示检查部队金币，<see langword="false" /> 表示检查当前页签。</param>
    /// <returns>当前页签是否可以访问。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 84 D2 74 ?? BA")]
    public partial bool CanAccessCurrentTab
    (
        bool checkGil
    );

    /// <summary>
    ///     处理部队储物柜当前不可用的情形。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 82 ?? ?? ?? ?? 8B 53 ?? 41 FF D0 84 C0 74 ?? B2 ?? 48 8B CB E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC 40 53"
    )]
    public partial void HandleFreeCompanyUnavailable();

    /// <summary>
    ///     处理玩家没有使用部队储物柜权限的情形。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 82 ?? ?? ?? ?? 8B 53 ?? 41 FF D0 84 C0 74 ?? B2 ?? 48 8B CB E8 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 5B E9 ?? ?? ?? ?? C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC 48 89 5C 24"
    )]
    public partial void HandlePermissionDenied();

    /// <summary>
    ///     检查玩家对指定容器的访问权限。
    /// </summary>
    /// <param name="inventoryType">要检查的容器类型。</param>
    /// <param name="checkDeposit">是否同时接受仅存入权限。</param>
    /// <returns>玩家是否拥有该容器的访问权限。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 41 0F B6 F0 48 8B 49 ?? 48 85 C9")]
    public partial bool HasItemAccess
    (
        InventoryType inventoryType,
        bool          checkDeposit
    );

    /// <summary>
    ///     检查玩家对指定容器是否拥有任意一种访问权限。
    /// </summary>
    /// <param name="inventoryType">要检查的容器类型。</param>
    /// <returns>玩家是否拥有该容器的任意一种访问权限。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 85 C9 0F 84")]
    public partial bool HasAnyItemAccess
    (
        InventoryType inventoryType
    );

    /// <summary>
    ///     取得当前页签中指定格子的物品。
    /// </summary>
    /// <param name="index">格子序号。</param>
    /// <returns>该格子的物品；当前页签既非物品页也非水晶页时返回 <see langword="null" />。</returns>
    [MemberFunction("44 0F B6 41 ?? 44 8B CA")]
    public partial InventoryItem* GetCurrentTabItem
    (
        int index
    );

    /// <summary>
    ///     取得指定容器的显示名称。
    /// </summary>
    /// <param name="inventoryType">容器类型。</param>
    /// <returns>该容器的显示名称。</returns>
    [MemberFunction("40 53 48 83 EC ?? 8B DA 81 FA ?? ?? ?? ?? 75 ?? 48 8B 49")]
    public partial CStringPointer GetTabName
    (
        InventoryType inventoryType
    );

    /// <summary>
    ///     打开部队储物柜日志界面。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8 48 8B 10 4C 8B 42 ?? BA ?? ?? ?? ?? 41 FF D0 48 8B F8 48 85 C0 74 ?? 48 8D 0D"
    )]
    public partial void OpenLog();

    /// <summary>
    ///     取得当前右键点击的物品。
    /// </summary>
    /// <returns>当前右键点击的物品；未右键点击任何物品时返回 <see langword="null" />。</returns>
    public InventoryItem* GetContextInventoryItem()
    {
        var manager = InventoryManager.Instance();
        if (manager == null) return null;

        return ContextItem.Container == InventoryType.Invalid ?
                   null :
                   manager->GetInventorySlot(ContextItem.Container, ContextItem.Slot);
    }

    /// <summary>
    ///     取得当前页对应的 InventoryType
    /// </summary>
    /// <param name="page">Inventory Type</param>
    /// <returns>是否获取成功</returns>
    public bool TryGetCurrentInventoryType
    (
        out InventoryType page
    )
    {
        page = InventoryType.Invalid;

        switch (TabType)
        {
            case 1:
                page = InventoryType.FreeCompanyCrystals;
                return true;
            case 0:
                page = (InventoryType)(20000 + TabIndex);
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    ///     部队储物柜日志中的单个条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct ChestLogEntry
    {
        /// <summary>
        ///     该日志条目所记录物品的物品 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     该日志条目所记录物品的数量。
        /// </summary>
        [FieldOffset(4)]
        public uint Quantity;
    }

    /// <summary>
    ///     部队储物柜代理的待处理状态，存放在 <see cref="AgentFreeCompanyChest.State" /> 中。
    /// </summary>
    public enum ChestState : uint
    {
        /// <summary>
        ///     无待处理操作。
        /// </summary>
        Idle = 0,

        /// <summary>
        ///     已请求打开部队储物柜界面，正在等待界面就绪。
        /// </summary>
        WaitingForAddon = 1,

        /// <summary>
        ///     部队储物柜当前不可用，需要向玩家显示提示。
        /// </summary>
        FreeCompanyUnavailable = 2,

        /// <summary>
        ///     当前玩家没有使用部队储物柜的权限，需要向玩家显示提示。
        /// </summary>
        PermissionDenied = 3,

        /// <summary>
        ///     需要执行一次跨容器物品搬移。
        /// </summary>
        MoveItem = 4,

        /// <summary>
        ///     已弹出搬移确认框，正在等待玩家确认。
        /// </summary>
        ConfirmingMove = 5
    }
}
