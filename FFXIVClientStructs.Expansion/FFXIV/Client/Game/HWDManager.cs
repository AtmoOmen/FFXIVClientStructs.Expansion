using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using NativeHWDManager = FFXIVClientStructs.FFXIV.Client.Game.HWDManager;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game;

/// <summary>
///     天穹街重建管理器，管理重建阶段、场景切换、重建演出、筹备任务及采集品检验请求。
///     继承原有管理器的服务器请求回调接口、重建等级、区域编号和筹备任务事件处理器。
/// </summary>
[GenerateInterop]
[Inherits<NativeHWDManager>]
[StructLayout(LayoutKind.Explicit, Size = 104)]
public unsafe partial struct HWDManager
{
    /// <summary>是否已进入天穹街并持有重建场景资源。</summary>
    [FieldOffset(8)]
    public bool IsTerritoryInitialized;

    /// <summary>重建阶段发生变化后，等待准备场景切换。</summary>
    [FieldOffset(9)]
    public bool IsLevelChangePending;

    /// <summary>场景切换已准备，等待延迟结束后更新布局。</summary>
    [FieldOffset(10)]
    public bool IsLayoutChangePending;

    /// <summary>首次进入区域，跳过阶段变化演出。</summary>
    [FieldOffset(11)]
    public bool IsInitialTerritoryLoad;

    /// <summary>等待当前事件演出结束后再更新重建场景。</summary>
    [FieldOffset(12)]
    public bool IsWaitingForEventScene;

    /// <summary>等待延迟结束后播放阶段公告。</summary>
    [FieldOffset(13)]
    public bool IsAnnouncementPending;

    /// <summary>服务器随重建阶段下发的附加标记，具体含义尚未确认。</summary>
    [FieldOffset(14)]
    public byte LevelChangeArgument;

    /// <summary>场景布局已切换，等待刷新完成并恢复显示。</summary>
    [FieldOffset(15)]
    public bool IsWaitingForLayout;

    /// <summary>七个重建支线任务的完成标记，供天穹街重建场景与任务进度判断使用。</summary>
    /// <remarks>
    ///     <list type="table">
    ///         <listheader><term>位与原生任务编号</term><description>Quest 表行号与简体中文任务名</description></listheader>
    ///         <item><term>位 0：3760</term><description>69296：无名众人广场</description></item>
    ///         <item><term>位 1：3841</term><description>69377：新巢居住区</description></item>
    ///         <item><term>位 2：3725</term><description>69261：为大家带去温暖的家</description></item>
    ///         <item><term>位 3：3972</term><description>69508：落羽商业区</description></item>
    ///         <item><term>位 4：3955</term><description>69491：与森都的贸易交涉</description></item>
    ///         <item><term>位 5：4042</term><description>69578：升歌文化区</description></item>
    ///         <item><term>位 6：3999</term><description>69535：富足的生活</description></item>
    ///     </list>
    ///     原生任务编号为 ushort，Quest 表行号为 uint。
    ///     本管理器通过 <c>(uint)questID | 0x10000u</c> 还原表行号，反向换算为 <c>(ushort)(rowID - 65536u)</c>。
    /// </remarks>
    [FieldOffset(16)]
    [BitField<bool>(nameof(IsQuest3760Complete), 0)]
    [BitField<bool>(nameof(IsQuest3841Complete), 1)]
    [BitField<bool>(nameof(IsQuest3725Complete), 2)]
    [BitField<bool>(nameof(IsQuest3972Complete), 3)]
    [BitField<bool>(nameof(IsQuest3955Complete), 4)]
    [BitField<bool>(nameof(IsQuest4042Complete), 5)]
    [BitField<bool>(nameof(IsQuest3999Complete), 6)]
    public byte CompletedQuestFlags;

    /// <summary>场景布局切换前的剩余等待时间，单位为秒。</summary>
    [FieldOffset(20)]
    public float LayoutChangeDelay;

    /// <summary>播放阶段公告前的剩余等待时间，单位为秒。</summary>
    [FieldOffset(24)]
    public float AnnouncementDelay;

    /// <summary>重建阶段切换演出表的行加载等待器。</summary>
    [FieldOffset(32)]
    public ExcelSheetWaiter* LevelChangeSheetWaiter;

    /// <summary>重建阶段切换演出表。</summary>
    [FieldOffset(40)]
    public ExcelSheet* LevelChangeSheet;

    /// <summary>重建阶段公告表的行加载等待器。</summary>
    [FieldOffset(48)]
    public ExcelSheetWaiter* AnnouncementSheetWaiter;

    /// <summary>重建阶段公告表。</summary>
    [FieldOffset(56)]
    public ExcelSheet* AnnouncementSheet;

    /// <summary>重建场景布局控制器，客户端分配的对象大小为 40 字节。</summary>
    [FieldOffset(72)]
    public void* LayoutController;

    /// <summary>重建阶段演出控制器，客户端分配的对象大小为 40 字节。</summary>
    [FieldOffset(80)]
    public void* DevelopmentController;

    /// <summary>重建共享布局时间轴控制器，客户端分配的对象大小为 24 字节。</summary>
    [FieldOffset(88)]
    public void* TimelineController;

    /// <summary>等待交给共享布局控制器播放的时间轴编号。</summary>
    [FieldOffset(96)]
    public byte PendingTimeline;

    /// <summary>重建状态消息中的第二个字节，具体含义尚未确认。</summary>
    [FieldOffset(97)]
    public byte StatusArgument1;

    /// <summary>重建状态消息中的第三个字节，具体含义尚未确认。</summary>
    [FieldOffset(98)]
    public byte StatusArgument2;

    /// <summary>重建状态消息中的第四个字节，供状态查询函数返回。</summary>
    [FieldOffset(99)]
    public byte StatusArgument3;

    /// <summary>重建状态消息中的第五个字节，供状态查询函数返回。</summary>
    [FieldOffset(100)]
    public byte StatusArgument4;

    /// <summary>保留字节，在已确认的管理器流程中未发现访问。</summary>
    [FieldOffset(101)]
    public byte Reserved101;

    /// <summary>筹备物资提交请求正在等待服务器回包。</summary>
    [FieldOffset(102)]
    public bool IsSupplyRequestPending;

    /// <summary>采集品检验请求正在等待服务器回包，命令 19 的回包处理完成后清除。</summary>
    [FieldOffset(103)]
    public bool IsInspectionRequestPending;

    /// <summary>提交最多五个背包格中的采集品，由原生管理器发送命令 19 并设置请求处理中标记。</summary>
    /// <param name="request">按背包类型、格下标、物品编号、类别及表内下标分组的请求数据。</param>
    /// <returns>原生请求函数的返回值；是否开始等待回包应以 <see cref="IsInspectionRequestPending" /> 为准。</returns>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 0F 57 C0")]
    public partial bool InspectItems(InspectionRequest* request);

    /// <summary>提交一个背包格中的筹备物资，由原生管理器发送命令 18。</summary>
    /// <param name="inventoryType">物资所在背包类型。</param>
    /// <param name="inventorySlot">物资所在背包格下标。</param>
    /// <param name="supplyCategory">筹备物资类别，为代理当前列表下标加一。</param>
    /// <param name="supplyIndex">所选项目的提交标识，取自代理项目记录偏移 28 的字节。</param>
    /// <returns>原生请求函数的返回值；是否开始等待回包应以 <see cref="IsSupplyRequestPending" /> 为准。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 0F B7 C2")]
    public partial bool SupplyItem(ushort inventoryType, ushort inventorySlot, byte supplyCategory, byte supplyIndex);

    /// <summary>处理区域变化，进入天穹街时加载重建资源，离开时释放资源并清理阶段演出状态。</summary>
    /// <param name="territoryTypeID">切换后的区域编号。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 66 C7 41")]
    public partial void SetTerritory(ushort territoryTypeID);

    /// <summary>更新服务器下发的重建阶段，并准备阶段切换演出表、公告表和场景刷新。</summary>
    /// <param name="level">新的重建阶段。</param>
    /// <param name="argument">服务器随阶段变化下发的附加标记。</param>
    [MemberFunction("40 53 56 41 56 48 83 EC ?? 0F B6 41")]
    public partial void SetDevelopmentLevel(byte level, byte argument);

    /// <summary>按当前阶段推进重建场景、公告、筹备任务和共享布局时间轴的原生更新流程。</summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B D9 48 8B 49 ?? 48 85 C9")]
    public partial void Update();

    /// <summary>更新管理器记录的七个特定重建任务之一的完成标记，其他任务编号不产生修改。</summary>
    /// <param name="questID">
    ///     原生 ushort 任务编号，取值为 Quest 表行号减 65536。
    ///     例如「无名众人广场」的表行号为 69296，此处传入 3760。
    ///     支持的七个任务及对应标记见 <see cref="CompletedQuestFlags" />。
    /// </param>
    /// <param name="completed">任务是否完成。</param>
    [MemberFunction("0F B7 C2 4C 8B C9 0F BA E8")]
    public partial void SetQuestCompleted(ushort questID, bool completed);

    /// <summary>查询管理器记录的七个特定重建任务之一是否完成。</summary>
    /// <param name="questID">
    ///     原生 ushort 任务编号，取值为 Quest 表行号减 65536。
    ///     例如「无名众人广场」的表行号为 69296，此处传入 3760。
    ///     支持的七个任务及对应标记见 <see cref="CompletedQuestFlags" />。
    /// </param>
    /// <returns>任务编号受到管理器支持且对应完成标记已设置时返回真。</returns>
    [MemberFunction("0F B7 C2 0F BA E8")]
    public partial bool IsQuestCompleted(ushort questID);

    /// <summary>查询当前区域的重建场景是否已经准备完成；不在天穹街或尚未初始化时直接返回真。</summary>
    /// <returns>所需表、重建阶段及场景控制器均就绪，或当前不需要等待重建场景时返回真。</returns>
    [MemberFunction("40 53 48 83 EC ?? B8 ?? ?? ?? ?? 48 8B D9 66 39 41 ?? 75 ?? 80 79")]
    public partial bool IsReady();

    /// <summary>设置共享布局待播放的时间轴，首次调用时创建对应的原生控制器。</summary>
    /// <param name="timeline">时间轴编号，原生更新流程接受 0 至 9。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 83 79 ?? ?? 0F B6 FA 48 8B D9 75 ?? 33 D2")]
    public partial void SetTimeline(byte timeline);

    /// <summary>
    ///     采集品检验请求，五个位置相同的数组元素共同描述一个背包格。
    ///     未使用项必须全部为零，客户端按五组、每组五个整数的顺序发送服务器请求。
    /// </summary>
    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    public partial struct InspectionRequest
    {
        /// <summary>五个待检验背包格各自的背包类型。</summary>
        [FieldOffset(0)]
        [FixedSizeArray]
        internal FixedSizeArray5<ushort> InventoryTypesStorage;

        /// <summary>五个待检验背包格各自的格下标。</summary>
        [FieldOffset(10)]
        [FixedSizeArray]
        internal FixedSizeArray5<ushort> InventorySlotsStorage;

        /// <summary>五个待检验背包格各自的物品编号。</summary>
        [FieldOffset(20)]
        [FixedSizeArray]
        internal FixedSizeArray5<uint> ItemIDsStorage;

        /// <summary>五个待检验背包格各自的采集品类别，矿物为 1，植物为 2，水产品为 3。</summary>
        [FieldOffset(40)]
        [FixedSizeArray]
        internal FixedSizeArray5<byte> CategoriesStorage;

        /// <summary>五个待检验背包格各自在所属类别的 79 项采集品表中的下标。</summary>
        [FieldOffset(45)]
        [FixedSizeArray]
        internal FixedSizeArray5<byte> ItemIndicesStorage;
    }
}
