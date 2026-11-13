using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;
using AtkEventInterface = FFXIVClientStructs.FFXIV.Component.GUI.AtkModuleInterface.AtkEventInterface;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     采集品鉴定代理，负责重建计划采集品鉴定界面的数据加载、类别与筛选切换、
///     待提交物品选择、鉴定提交以及目标列表的刷新。
///     其 <c>ReceiveEvent</c>、<c>Dtor</c>、<c>Hide</c> 与 <c>Update</c> 均由
///     <see cref="AgentInterface" /> 与 <see cref="AtkEventInterface" /> 继承而来的虚函数槽位覆盖。
/// </summary>
[GenerateInterop]
[Agent(AgentId.HwdGathererInspection)]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 232)]
public unsafe partial struct AgentHwdGathererInspection
{
    /// <summary>
    ///     供表加载完成事件使用的空事件接口，其两个虚函数均为空实现。
    /// </summary>
    [FieldOffset(40)]
    public AtkEventInterface TableLoadEventInterface;

    /// <summary>
    ///     运行时数据，首次初始化时在界面内存空间中分配，界面关闭时释放。
    /// </summary>
    [FieldOffset(48)]
    public InspectionData* Data;

    /// <summary>
    ///     多行表等待器，负责依次载入主表、奖励表、采集品表、钓鱼参数表与期数表。
    /// </summary>
    [FieldOffset(56)]
    public InspectionSheetWaiter SheetWaiter;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <param name="uiModuleInterface">界面模块接口。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 ED 48 89 06")]
    public partial AgentHwdGathererInspection* Ctor
    (
        UIModuleInterface* uiModuleInterface
    );

    /// <summary>
    ///     释放内嵌的表等待器与运行时数据引用，但不释放自身内存。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F9 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 83 C1 ?? E8 ?? ?? ?? ?? 48 8D 4F"
    )]
    public partial AgentHwdGathererInspection* Destroy();

    /// <summary>
    ///     主界面打开完成后的处理：关闭先前界面、按记录的类别重建槽位，
    ///     并把打开参数中的计数与标识写入运行时数据后刷新主界面。
    /// </summary>
    /// <param name="openInfo">打开界面时传入的附加标识，前 16 字节与末尾 8 字节被复制到运行时数据。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 48 8B FA 48 8B D9 74 ?? 48 8B 01")]
    public partial void HandleAddonOpened
    (
        nint* openInfo
    );

    /// <summary>
    ///     判断给定采集品在当前类别的槽位中是否存在满足数量要求的槽位。
    /// </summary>
    /// <param name="itemID">采集品物品 id。</param>
    /// <param name="amount">持有数量。</param>
    /// <returns>存在可提交槽位时为 <see langword="true" />。</returns>
    [MemberFunction("48 83 EC ?? 4C 8B 49 ?? 45 8B D8 44 8B D2")]
    public partial bool IsItemSubmittable
    (
        uint itemID,
        uint amount
    );

    /// <summary>
    ///     判断给定背包物品在当前类别的槽位中是否可提交。
    /// </summary>
    /// <param name="item">背包物品。</param>
    /// <returns>可提交时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 02 48 8B F9 48 8B CA 48 8B DA")]
    public partial bool IsInventoryItemSubmittable
    (
        InventoryItem* item
    );

    /// <summary>
    ///     把一个背包物品加入第一个空的待提交槽位，并刷新主界面数据。
    /// </summary>
    /// <param name="item">背包物品。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 48 8B F2 48 8B F9 0F 84")]
    public partial void AddInventoryItem
    (
        InventoryItem* item
    );

    /// <summary>
    ///     初始化运行时数据并启动全部相关表的加载。
    /// </summary>
    /// <param name="context">界面模块上下文。</param>
    /// <param name="argument">打开界面时传入的整数参数。</param>
    /// <param name="openInfo">打开界面时传入的附加标识，前 16 字节与末尾 8 字节被复制到运行时数据。</param>
    [MemberFunction("48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 83 79 ?? ?? 49 8B F9")]
    public partial void Initialize
    (
        ulong context,
        int   argument,
        void* openInfo
    );

    /// <summary>
    ///     存在可提交物品时打开主界面，否则输出提示日志。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 85 C0 75 ?? 48 8B 4B")]
    public partial void OpenMainAddon();

    /// <summary>
    ///     关闭主界面与目标列表界面。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 45 33 C9 C6 44 24")]
    public partial void CloseAddons();

    /// <summary>
    ///     打开目标列表界面，并把当前类别的每个槽位名称送入界面。
    /// </summary>
    [MemberFunction("41 57 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F9 48 8B 49 ?? F6 81")]
    public partial void OpenTargetList();

    /// <summary>
    ///     打开奖励详情界面，并把当前鉴定品的奖励与说明送入界面。
    /// </summary>
    [MemberFunction("40 57 48 81 EC ?? ?? ?? ?? 48 8B F9 B9")]
    public partial void ShowRewardDetail();

    /// <summary>
    ///     打开背包物品选择界面，并把全部可提交物品送入界面。
    /// </summary>
    /// <param name="slotIndex">待填入的槽位下标。</param>
    [MemberFunction("48 8B C4 53 41 57 48 83 EC ?? 0F B6 DA")]
    public partial void OpenInventorySelect
    (
        byte slotIndex
    );

    /// <summary>
    ///     关闭主界面之外的三个子界面。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 41 ?? 48 8B F9 8B 98")]
    public partial void CloseSubAddons();

    /// <summary>
    ///     关闭背包物品选择界面。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 48 8B 41 ?? 48 8B F9 83 B8")]
    public partial void CloseInventorySelect();

    /// <summary>
    ///     解除全部背包格的占用标记。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 41 ?? 32 DB")]
    public partial void UnblockSourceSlots();

    /// <summary>
    ///     把当前选中的全部待提交物品提交给服务器。
    /// </summary>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8B F0")]
    public partial void SubmitSelectedItems();

    /// <summary>
    ///     校验全部待提交槽位，对已经失效的槽位输出提示日志。
    /// </summary>
    /// <returns>全部槽位均有效时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B 71")]
    public partial bool ValidateSlots();

    /// <summary>
    ///     把界面传入的编号换算为表行偏移。
    /// </summary>
    /// <param name="value">界面传入的编号。</param>
    /// <returns>换算后的偏移。</returns>
    [MemberFunction("8D 42 ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 33 F6")]
    public static partial uint GetRowOffset
    (
        int value
    );

    /// <summary>
    ///     启动主表加载，按三个类别逐项请求采集品行。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 33 F6 48 8D 59 ?? 83 BB")]
    public partial void LoadMainSheet();

    /// <summary>
    ///     主表每一行加载完成时的回调，把该行的 79 个槽位写入对应类别。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 41 0F B6 E9")]
    public partial void OnMainSheetRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast
    );

    /// <summary>
    ///     启动奖励表加载，并按表行数预分配奖励缓存。
    /// </summary>
    [MemberFunction("40 53 41 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? BA")]
    public partial void LoadRewardSheet();

    /// <summary>
    ///     奖励表每一行加载完成时的回调，把该行的货币与点数追加到奖励缓存。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 41 0F B6 F9 48 8B 49 ?? 48 85 C9 74 ?? 48 8B 12")]
    public partial void OnRewardSheetRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast
    );

    /// <summary>
    ///     启动采集品表加载，请求全部带采集品来源标记的槽位对应行。
    /// </summary>
    [MemberFunction
    (
        "4C 8B DC 49 89 5B ?? 49 89 6B ?? 49 89 73 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 45 33 ED 48 8D 05 ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 48 8B E9 4D 89 6B ?? 45 89 6B ?? 49 89 43 ?? 45 89 6B ?? 74 ?? 48 83 C1 ?? E8 ?? ?? ?? ?? 0F 10 44 24 ?? C7 85 ?? ?? ?? ?? ?? ?? ?? ?? 0F 10 4C 24 ?? 48 89 AD ?? ?? ?? ?? 0F 11 85 ?? ?? ?? ?? 0F 11 8D ?? ?? ?? ?? 44 88 AD ?? ?? ?? ?? 45 32 E4 49 8B F5 40 32 FF 0F 1F 40 ?? 0F 1F 84 00 ?? ?? ?? ?? 48 8B 45 ?? F6 44 06 ?? ?? 0F 84 ?? ?? ?? ?? 83 BD ?? ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 8B 04 06 48 8B 55 ?? 40 0F B6 CF C1 E1 ?? 89 44 24 ?? 41 0F B6 C4 0B C8 4C 89 6C 24 ?? 89 4C 24 ?? 48 8B 8D ?? ?? ?? ?? 48 8D 41 ?? 48 3B D0 77 ?? BA ?? ?? ?? ?? 48 8D 4D ?? E8 ?? ?? ?? ?? 48 8B 55 ?? 48 8B 8D ?? ?? ?? ?? 48 8D 42 ?? 48 21 85 ?? ?? ?? ?? 48 8D 42 ?? 48 03 8D ?? ?? ?? ?? 48 23 C8 48 8B 45 ?? 4C 39 2C C8 4C 8D 34 CD ?? ?? ?? ?? 75 ?? 33 D2 41 B8 ?? ?? ?? ?? 8D 4A ?? E8 ?? ?? ?? ?? 48 8B 4D ?? 49 89 04 0E 48 8B 45 ?? 49 8B 04 06 0F 10 44 24 ?? 0F 11 00 48 FF 85 ?? ?? ?? ?? 40 FE C7 48 83 C6 ?? 40 80 FF ?? 0F 82 ?? ?? ?? ?? 41 FE C4 41 80 FC ?? 0F 82 ?? ?? ?? ?? 83 BD ?? ?? ?? ?? ?? 74 ?? C7 85 ?? ?? ?? ?? ?? ?? ?? ?? 44 89 AD ?? ?? ?? ?? 48 8B 95 ?? ?? ?? ?? 48 8B 45 ?? 48 89 85 ?? ?? ?? ?? 48 89 95 ?? ?? ?? ?? 44 38 AD ?? ?? ?? ?? 74 ?? 33 D2 48 8D 4D ?? 44 8D 42 ?? E8 ?? ?? ?? ?? 48 8D 4D ?? 4C 39 AD ?? ?? ?? ?? 75 ?? E8 ?? ?? ?? ?? EB ?? E8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 8B 6C 24 ?? 48 8B B4 24 ?? ?? ?? ?? 48 83 C4 ?? 41 5F 41 5E 41 5D 41 5C 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9"
    )]
    public partial void LoadGatheringItemSheet();

    /// <summary>
    ///     采集品表每一行加载完成时的回调，把采集品物品 id 写回槽位。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    /// <param name="key">该行对应的类别与槽位编码。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 4C 8B DA")]
    public partial void OnGatheringItemRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast,
        nint  key
    );

    /// <summary>
    ///     启动钓鱼参数表加载，请求全部带钓鱼来源标记的槽位对应行。
    /// </summary>
    [MemberFunction
    (
        "4C 8B DC 49 89 5B ?? 49 89 6B ?? 49 89 73 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 45 33 ED 48 8D 05 ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 48 8B E9 4D 89 6B ?? 45 89 6B ?? 49 89 43 ?? 45 89 6B ?? 74 ?? 48 83 C1 ?? E8 ?? ?? ?? ?? 0F 10 44 24 ?? C7 85 ?? ?? ?? ?? ?? ?? ?? ?? 0F 10 4C 24 ?? 48 89 AD ?? ?? ?? ?? 0F 11 85 ?? ?? ?? ?? 0F 11 8D ?? ?? ?? ?? 44 88 AD ?? ?? ?? ?? 45 32 E4 49 8B F5 40 32 FF 0F 1F 40 ?? 0F 1F 84 00 ?? ?? ?? ?? 48 8B 45 ?? F6 44 06 ?? ?? 0F 84 ?? ?? ?? ?? 83 BD ?? ?? ?? ?? ?? 0F 84 ?? ?? ?? ?? 8B 04 06 48 8B 55 ?? 40 0F B6 CF C1 E1 ?? 89 44 24 ?? 41 0F B6 C4 0B C8 4C 89 6C 24 ?? 89 4C 24 ?? 48 8B 8D ?? ?? ?? ?? 48 8D 41 ?? 48 3B D0 77 ?? BA ?? ?? ?? ?? 48 8D 4D ?? E8 ?? ?? ?? ?? 48 8B 55 ?? 48 8B 8D ?? ?? ?? ?? 48 8D 42 ?? 48 21 85 ?? ?? ?? ?? 48 8D 42 ?? 48 03 8D ?? ?? ?? ?? 48 23 C8 48 8B 45 ?? 4C 39 2C C8 4C 8D 34 CD ?? ?? ?? ?? 75 ?? 33 D2 41 B8 ?? ?? ?? ?? 8D 4A ?? E8 ?? ?? ?? ?? 48 8B 4D ?? 49 89 04 0E 48 8B 45 ?? 49 8B 04 06 0F 10 44 24 ?? 0F 11 00 48 FF 85 ?? ?? ?? ?? 40 FE C7 48 83 C6 ?? 40 80 FF ?? 0F 82 ?? ?? ?? ?? 41 FE C4 41 80 FC ?? 0F 82 ?? ?? ?? ?? 83 BD ?? ?? ?? ?? ?? 74 ?? C7 85 ?? ?? ?? ?? ?? ?? ?? ?? 44 89 AD ?? ?? ?? ?? 48 8B 95 ?? ?? ?? ?? 48 8B 45 ?? 48 89 85 ?? ?? ?? ?? 48 89 95 ?? ?? ?? ?? 44 38 AD ?? ?? ?? ?? 74 ?? 33 D2 48 8D 4D ?? 44 8D 42 ?? E8 ?? ?? ?? ?? 48 8D 4D ?? 4C 39 AD ?? ?? ?? ?? 75 ?? E8 ?? ?? ?? ?? EB ?? E8 ?? ?? ?? ?? 48 8B 5C 24 ?? 48 8B 6C 24 ?? 48 8B B4 24 ?? ?? ?? ?? 48 83 C4 ?? 41 5F 41 5E 41 5D 41 5C 5F C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 4C 8B 41"
    )]
    public partial void LoadFishParameterSheet();

    /// <summary>
    ///     钓鱼参数表每一行加载完成时的回调，把钓鱼物品 id 写回槽位。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    /// <param name="key">该行对应的类别与槽位编码。</param>
    [MemberFunction("40 53 48 83 EC ?? 4C 8B 41 ?? 48 8B D9 4D 85 C0 74 ?? 48 8B 44 24")]
    public partial void OnFishParameterRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast,
        nint  key
    );

    /// <summary>
    ///     启动采集品表补充加载，请求全部已取得物品 id 的槽位对应行。
    /// </summary>
    [MemberFunction
    (
        "4C 8B DC 49 89 5B ?? 49 89 6B ?? 49 89 73 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 45 33 ED 48 8D 05 ?? ?? ?? ?? 83 B9 ?? ?? ?? ?? ?? 48 8B E9 4D 89 6B ?? 45 89 6B ?? 49 89 43 ?? 45 89 6B ?? 74 ?? 48 83 C1 ?? E8 ?? ?? ?? ?? 0F 10 44 24 ?? C7 85 ?? ?? ?? ?? ?? ?? ?? ?? 0F 10 4C 24 ?? 48 89 AD ?? ?? ?? ?? 0F 11 85 ?? ?? ?? ?? 0F 11 8D ?? ?? ?? ?? 44 88 AD ?? ?? ?? ?? 45 32 E4 49 8B F5 40 32 FF 48 8B 45"
    )]
    public partial void LoadGatheringItemSheetDetail();

    /// <summary>
    ///     采集品表补充加载的每一行完成回调，把附加数值写回槽位并启动期数表加载。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    /// <param name="key">该行对应的类别与槽位编码。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 4C 8B 51")]
    public partial void OnGatheringItemDetailRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast,
        nint  key
    );

    /// <summary>
    ///     启动期数表加载，请求全部出现过的期数行。
    /// </summary>
    [MemberFunction("48 89 6C 24 ?? 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 33 ED")]
    public partial void LoadTermSheet();

    /// <summary>
    ///     期数表每一行加载完成时的回调，把期数名称追加到阶段缓存。
    /// </summary>
    /// <param name="row">已载入的行。</param>
    /// <param name="rowIndex">行索引。</param>
    /// <param name="isLast">是否为本次加载的最后一行。</param>
    /// <param name="key">该行的期数。</param>
    [MemberFunction("40 56 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 83 79")]
    public partial void OnTermRowLoaded
    (
        nint* row,
        uint  rowIndex,
        bool  isLast,
        nint  key
    );

    /// <summary>
    ///     处理主界面与目标列表界面发来的全部操作码。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <param name="valueCount">界面值数量。</param>
    /// <returns>是否已处理。</returns>
    [MemberFunction("40 53 55 56 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F1 48 8B DA")]
    public partial bool ProcessAddonEvent
    (
        AtkValue* values,
        uint      valueCount
    );

    /// <summary>
    ///     处理界面选中的待提交物品组。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否已处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 48 83 EC ?? 48 8B F1 48 8B DA")]
    public partial bool OnItemGroupSelected
    (
        AtkValue* values
    );

    /// <summary>
    ///     处理界面的类别与期数筛选变更。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否已处理。</returns>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 48 8B F2 48 8B CA E8 ?? ?? ?? ?? 83 E8 ?? 74")]
    public partial bool OnFilterChanged
    (
        AtkValue* values
    );

    /// <summary>
    ///     处理界面提交确认。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否已处理。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? 85 C0 75 ?? 48 8B CB E8 ?? ?? ?? ?? B0 ?? 48 83 C4")]
    public partial bool OnSubmitConfirmed
    (
        AtkValue* values
    );

    /// <summary>
    ///     处理背包物品选择界面选中的物品。
    /// </summary>
    /// <param name="values">界面值数组。</param>
    /// <returns>是否已处理。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 48 8B F2 48 8B CA E8 ?? ?? ?? ?? 48 8B CF")]
    public partial bool OnInventoryItemSelected
    (
        AtkValue* values
    );

    /// <summary>
    ///     把主界面标记为待刷新。
    /// </summary>
    [MemberFunction("48 8B 41 ?? 83 88 ?? ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 41 56")]
    public partial void MarkMainAddonDirty();

    /// <summary>
    ///     打开主界面并送入当前的倒计时、期数与累计计数。
    /// </summary>
    /// <returns>界面创建成功时为 <see langword="true" />。</returns>
    [MemberFunction("41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F1 B9")]
    public partial bool ShowMainAddon();

    /// <summary>
    ///     立即刷新主界面数据，失败时把主界面标记为待刷新。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 84 C0 75 ?? 48 8B 43 ?? 83 88")]
    public partial void RefreshMainAddonOrMark();

    /// <summary>
    ///     重建主界面的全部界面值并送入界面。
    /// </summary>
    /// <returns>界面已就绪且送出成功时为 <see langword="true" />。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B 69"
    )]
    public partial bool RefreshMainAddon();

    /// <summary>
    ///     重建目标列表界面的全部界面值并送入界面。
    /// </summary>
    /// <returns>界面已就绪且送出成功时为 <see langword="true" />。</returns>
    [MemberFunction("40 53 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B 51")]
    public partial bool RefreshTargetList();

    /// <summary>
    ///     与 <c>Update</c> 等价的刷新流程，供内部调用。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 41 ?? 48 8B D9 F6 80 ?? ?? ?? ?? ?? 74 ?? E8")]
    public partial void UpdateAddons();

    /// <summary>
    ///     统计背包中当前类别可提交的物品数量。
    /// </summary>
    /// <returns>可提交物品数量。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 4C 8B E1 4C 8D 3D")]
    public partial uint CountSubmittableItems();

    /// <summary>
    ///     把背包中当前类别可提交的物品依次自动填入空的待提交槽位。
    /// </summary>
    [MemberFunction("40 55 41 55 48 83 EC ?? 48 8B 05")]
    public partial void AutoFillSlots();

    /// <summary>
    ///     在指定类别的槽位中查找匹配物品 id 与数量要求的槽位下标。
    /// </summary>
    /// <param name="category">采集品类别。</param>
    /// <param name="itemID">物品 id。</param>
    /// <param name="amount">持有数量。</param>
    /// <param name="outIndex">找到时写入槽位下标。</param>
    /// <returns>找到匹配槽位时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 4C 8B 59 ?? 45 32 D2")]
    public partial bool FindItemIndex
    (
        byte  category,
        uint  itemID,
        uint  amount,
        byte* outIndex
    );

    /// <summary>
    ///     切换采集品类别并立即刷新主界面。
    /// </summary>
    /// <param name="category">采集品类别。</param>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 41 ?? 48 8B D9 88 90")]
    public partial void SelectCategory
    (
        byte category
    );

    /// <summary>
    ///     设置当前采集品类别，清空并解除全部待提交槽位，随后刷新主界面。
    /// </summary>
    /// <param name="category">采集品类别。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 45 33 FF 44 0F B6 F2")]
    public partial void SetSelectedCategory
    (
        byte category
    );

    /// <summary>
    ///     清空单个待提交槽位并解除其背包格占用。
    /// </summary>
    /// <param name="slotIndex">槽位下标。</param>
    [MemberFunction("40 53 48 83 EC ?? 0F B6 DA 48 C1 E3")]
    public partial void ClearSlot
    (
        byte slotIndex
    );

    /// <summary>
    ///     尝试把一个背包物品分配到指定下标的待提交槽位。
    /// </summary>
    /// <param name="category">采集品类别，当前实现未使用该参数。</param>
    /// <param name="slotIndex">槽位下标，必须小于 5。</param>
    /// <param name="item">背包物品。</param>
    /// <returns>分配成功时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 49 8B 01 48 8B D9")]
    public partial bool TryAssignSlot
    (
        byte           category,
        byte           slotIndex,
        InventoryItem* item
    );

    /// <summary>
    ///     采集品鉴定代理在界面内存空间中分配的运行时数据。
    ///     它在代理初始化时一次性分配，在代理隐藏时随界面一起释放。
    ///     内部按采集品类别分段保存从 <c>HWDGathererInspection</c> 表读入的槽位，
    ///     并额外保存当前选中的待提交物品、提交进度与界面刷新状态位。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 4056)]
    public unsafe partial struct InspectionData
    {
        /// <summary>
        ///     三个采集品类别的全部槽位，按类别顺序连续排列，每个类别固定 79 项。
        /// </summary>
        [FieldOffset(0)]
        [FixedSizeArray]
        internal FixedSizeArray237<Item> ItemsStorage;

        /// <summary>
        ///     鉴定奖励表行缓存，由 <c>HWDGathererInspectionReward</c> 表按行号载入。
        /// </summary>
        [FieldOffset(3792)]
        public StdVector<Reward> Rewards;

        /// <summary>
        ///     阶段名称缓存，由 <c>HWDGathereInspectTerm</c> 表载入。
        /// </summary>
        [FieldOffset(3816)]
        public StdVector<PhaseEntry> Phases;

        /// <summary>
        ///     可提交物品的背包来源格缓存，用于在界面关闭时逐格解除背包格的占用标记。
        /// </summary>
        [FieldOffset(3840)]
        public StdVector<SourceSlot> SourceSlots;

        /// <summary>
        ///     当前选中的采集品类别，取值 0 至 2，分别对应矿物、植物与生物、水产品。
        /// </summary>
        [FieldOffset(3864)]
        public byte SelectedCategory;

        /// <summary>
        ///     待提交的五组采集品，每组独立占用一个背包格。
        /// </summary>
        [FieldOffset(3868)]
        [FixedSizeArray]
        internal FixedSizeArray5<Slot> SlotsStorage;

        /// <summary>
        ///     当前已选中的待提交采集品组数。
        /// </summary>
        [FieldOffset(3948)]
        public byte SlotCount;

        /// <summary>
        ///     每组待提交采集品的进度，与 <see cref="SlotsStorage" /> 同下标对应。
        /// </summary>
        [FieldOffset(3952)]
        [FixedSizeArray]
        internal FixedSizeArray5<SubmitProgress> ProgressStorage;

        /// <summary>
        ///     全部已选采集品的累计提交量。
        /// </summary>
        [FieldOffset(3992)]
        public uint TotalAmount;

        /// <summary>
        ///     界面顶部显示的四个累计计数，按下标由界面值的索引取值。
        /// </summary>
        [FieldOffset(3996)]
        [FixedSizeArray]
        internal FixedSizeArray4<uint> CountersStorage;

        /// <summary>
        ///     打开界面时由调用方传入的附加标识。
        /// </summary>
        [FieldOffset(4012)]
        public ulong OpenIdentifier;

        /// <summary>
        ///     界面刷新状态位：第 0 位表示主界面数据待刷新，第 1 位表示主界面待重建，第 2 位表示目标列表待重建。
        /// </summary>
        [FieldOffset(4020)]
        public uint RefreshFlags;

        /// <summary>
        ///     下一次可再次鉴定的服务器时间戳，为 0 表示随时可鉴定。
        /// </summary>
        [FieldOffset(4024)]
        public uint ResetTimestamp;

        /// <summary>
        ///     目标列表界面的运行实例 id，打开时写入，界面关闭后不再使用。
        /// </summary>
        [FieldOffset(4028)]
        public uint TargetListAddonID;

        /// <summary>
        ///     打开目标列表界面时传给界面的附加整数。
        /// </summary>
        [FieldOffset(4040)]
        public uint TargetListArgument;

        /// <summary>
        ///     上一次由界面选中的采集品类别，与 <see cref="ActiveCategory" /> 成对刷新。
        /// </summary>
        [FieldOffset(4044)]
        public byte SelectedCategoryIndex;

        /// <summary>
        ///     当前生效的采集品类别，目标列表按此类别筛选槽位。
        /// </summary>
        [FieldOffset(4045)]
        public byte ActiveCategory;

        /// <summary>
        ///     上一次由界面选中的待提交物品组下标。
        /// </summary>
        [FieldOffset(4046)]
        public byte SelectedSlotIndex;

        /// <summary>
        ///     当前生效的阶段，目标列表只显示与该阶段匹配的槽位。
        /// </summary>
        [FieldOffset(4047)]
        public byte ActivePhase;

        /// <summary>
        ///     界面上一次显示的总需求数量。
        /// </summary>
        [FieldOffset(4048)]
        public ushort DisplayedAmount;

        /// <summary>
        ///     界面状态位：第 0 位表示奖励阶段已结束，第 1 位表示鉴定尚未就绪，第 2 位表示可提交列表待重建。
        /// </summary>
        [FieldOffset(4050)]
        public byte StateFlags;

        /// <summary>
        ///     <c>HWDGathererInspection</c> 表中的一个槽位。
        ///     每个类别固定 79 个槽位，按类别顺序连续排列。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct Item
        {
            /// <summary>
            ///     待鉴定物品 id。矿物与植物类别取自采集品的物品 id，水产品类别取自钓鱼参数对应的物品 id。
            /// </summary>
            [FieldOffset(0)]
            public uint ItemID;

            /// <summary>
            ///     鉴定产物物品 id。
            /// </summary>
            [FieldOffset(4)]
            public uint ResultItemID;

            /// <summary>
            ///     奖励行号，取自当前阶段对应的奖励列，指向 <c>HWDGathererInspectionReward</c> 表。
            ///     该表首行不载入缓存，因此对应缓存下标需减一。
            /// </summary>
            [FieldOffset(8)]
            public ushort RewardIndex;

            /// <summary>
            ///     采集品等级。先填入采集品表或钓鱼参数表的等级行号，
            ///     随后由等级转换表解析为实际等级值。
            /// </summary>
            [FieldOffset(10)]
            public ushort GatheringItemLevel;

            /// <summary>
            ///     单次鉴定需要提交的数量。
            /// </summary>
            [FieldOffset(12)]
            public byte AmountRequired;

            /// <summary>
            ///     该槽位所属阶段，对应 <c>HWDGathereInspectTerm</c> 表行号。
            /// </summary>
            [FieldOffset(13)]
            public byte Phase;

            /// <summary>
            ///     槽位状态位：第 0 位表示该槽位属于当前阶段，第 1 位表示槽位使用采集品来源，第 2 位表示槽位使用钓鱼来源。
            /// </summary>
            [FieldOffset(14)]
            public byte Flags;
        }

        /// <summary>
        ///     <c>HWDGathererInspectionReward</c> 表中的一行，保存该组奖励对应的货币与点数。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 4)]
        public struct Reward
        {
            /// <summary>
            ///     奖励货币数量。
            /// </summary>
            [FieldOffset(0)]
            public ushort Scrips;

            /// <summary>
            ///     奖励点数数量。
            /// </summary>
            [FieldOffset(2)]
            public ushort Points;
        }

        /// <summary>
        ///     阶段名称缓存中的一项，由 <c>HWDGathereInspectTerm</c> 表的行填入。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 112)]
        public struct PhaseEntry
        {
            /// <summary>
            ///     阶段 id，与 <see cref="Item.Phase" /> 比较。
            /// </summary>
            [FieldOffset(0)]
            public byte Phase;

            /// <summary>
            ///     阶段名称。
            /// </summary>
            [FieldOffset(8)]
            public Utf8String Name;
        }

        /// <summary>
        ///     一个被占用背包格的来源记录，用于在取消选择或关闭界面时解除占用。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct SourceSlot
        {
            /// <summary>
            ///     该记录的附加数值。解除占用时只读取背包类型与背包格下标，此字段不参与读写。
            /// </summary>
            [FieldOffset(0)]
            public ulong Unknown00;

            /// <summary>
            ///     背包类型。
            /// </summary>
            [FieldOffset(8)]
            public uint InventoryType;

            /// <summary>
            ///     背包格下标。
            /// </summary>
            [FieldOffset(12)]
            public ushort InventorySlot;
        }

        /// <summary>
        ///     一组待提交的采集品，对应目标列表中被选中的一行。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public struct Slot
        {
            /// <summary>
            ///     物品 id，为 0 表示该组未被占用。
            /// </summary>
            [FieldOffset(0)]
            public uint ItemID;

            /// <summary>
            ///     该背包格中物品的堆叠数量。
            /// </summary>
            [FieldOffset(4)]
            public uint Amount;

            /// <summary>
            ///     物品所在背包类型。
            /// </summary>
            [FieldOffset(8)]
            public uint InventoryType;

            /// <summary>
            ///     物品所在背包格下标。
            /// </summary>
            [FieldOffset(12)]
            public ushort InventorySlot;

            /// <summary>
            ///     该物品在当前类别槽位数组中的下标。
            /// </summary>
            [FieldOffset(14)]
            public byte ItemIndex;
        }

        /// <summary>
        ///     一组待提交采集品的提交进度。
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 8)]
        public struct SubmitProgress
        {
            /// <summary>
            ///     该背包格中物品的堆叠数量。
            /// </summary>
            [FieldOffset(0)]
            public uint Amount;

            /// <summary>
            ///     已满足的单次鉴定所需数量。
            /// </summary>
            [FieldOffset(4)]
            public uint Satisfied;
        }
    }

    /// <summary>
    ///     采集品鉴定代理内嵌的多行表等待器。
    ///     它派生自 <see cref="ExcelSheetWaiter" />，除基类的单表等待能力外，
    ///     还维护一个待加载行号队列，并在每一行载入完成后调用代理注册的回调。
    /// </summary>
    [GenerateInterop]
    [Inherits<ExcelSheetWaiter>]
    [StructLayout(LayoutKind.Explicit, Size = 176)]
    public unsafe partial struct InspectionSheetWaiter
    {
        /// <summary>
        ///     行号队列的哨兵节点。
        /// </summary>
        [FieldOffset(48)]
        public nint RowQueueNode;

        /// <summary>
        ///     行号队列的桶数组。
        /// </summary>
        [FieldOffset(56)]
        public nint RowQueueBuckets;

        /// <summary>
        ///     行号队列的桶数量。
        /// </summary>
        [FieldOffset(64)]
        public nint RowQueueCapacity;

        /// <summary>
        ///     行号队列的队头索引。
        /// </summary>
        [FieldOffset(72)]
        public nint RowQueueHead;

        /// <summary>
        ///     队列中的行号数量。
        /// </summary>
        [FieldOffset(80)]
        public nint RowQueueCount;

        /// <summary>
        ///     行号队列快照的哨兵节点。
        /// </summary>
        [FieldOffset(88)]
        public nint RowQueueSnapshotNode;

        /// <summary>
        ///     布局保留字段，在全部已知代码路径中未被读写。
        /// </summary>
        [FieldOffset(96)]
        public nint Reserved60;

        /// <summary>
        ///     行号队列快照的队头索引。
        /// </summary>
        [FieldOffset(104)]
        public nint RowQueueSnapshotHead;

        /// <summary>
        ///     每加载一行时调用的回调。
        /// </summary>
        [FieldOffset(112)]
        public nint RowLoadedCallback;

        /// <summary>
        ///     传给 <see cref="RowLoadedCallback" /> 的参数相对回调上下文的字节偏移。
        /// </summary>
        [FieldOffset(120)]
        public int RowLoadedCallbackArgumentOffset;

        /// <summary>
        ///     表缺失时调用的次要回调。
        /// </summary>
        [FieldOffset(128)]
        public nint SecondaryCallback;

        /// <summary>
        ///     布局保留字段，在全部已知代码路径中未被读写。
        /// </summary>
        [FieldOffset(136)]
        public nint Reserved88;

        /// <summary>
        ///     传给回调的上下文对象，指向所属代理。
        /// </summary>
        [FieldOffset(144)]
        public nint CallbackArgument;

        /// <summary>
        ///     该等待器加载的 Excel 表索引。
        /// </summary>
        [FieldOffset(152)]
        public uint SheetIndex;

        /// <summary>
        ///     当前正在加载的行索引。
        /// </summary>
        [FieldOffset(156)]
        public uint CurrentRowIndex;

        /// <summary>
        ///     本次加载的行总数，回调以该值判断是否为最后一行。
        /// </summary>
        [FieldOffset(160)]
        public uint RowCount;

        /// <summary>
        ///     表数据就绪后置为 1。
        /// </summary>
        [FieldOffset(164)]
        public uint LoadState;

        /// <summary>
        ///     处理行回调期间置位。
        /// </summary>
        [FieldOffset(168)]
        public byte IsProcessing;

        /// <summary>
        ///     置位时 <see cref="ExcelSheetWaiter.Prepare" /> 跳过完整重载，只设置队列中的第一行。
        /// </summary>
        [FieldOffset(169)]
        public byte SkipReload;
    }
}
