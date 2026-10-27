using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏中的血条窗口（KTGIGaugeHp）。<br />
///     窗口持有二十个血条槽位，每个槽位由复制出来的组件节点与其内部的 <see cref="AtkComponentGaugeBar"/> 组成，
///     按数值数组下发的敌人实体 id、坐标、缩放与血量百分比显示敌人血条。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGIGaugeHp")]
[StructLayout(LayoutKind.Explicit, Size = 1080)]
public unsafe partial struct AddonKTGIGaugeHp
{
    /// <summary>
    ///     二十个血条槽位，每个槽位 24 字节。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray20<GaugeEntry> GaugeEntriesStorage;

    /// <summary>
    ///     第 0 个槽位节点的宽度，用于按缩放后宽度水平居中。
    /// </summary>
    [FieldOffset(1048)]
    public float EntryWidth;

    /// <summary>
    ///     第 0 个槽位节点的高度。
    /// </summary>
    [FieldOffset(1052)]
    public float EntryHeight;

    /// <summary>
    ///     血条布局的缩放系数，默认 1。
    /// </summary>
    [FieldOffset(1056)]
    public float LayoutScale;

    /// <summary>
    ///     血条缩放百分比对应的系数，默认 0.85，可由刷新参数改写。
    /// </summary>
    [FieldOffset(1060)]
    public float GaugeScale;

    /// <summary>
    ///     血条的额外缩放系数，默认 1，可由刷新参数改写。
    /// </summary>
    [FieldOffset(1064)]
    public float ScaleMultiplier;

    /// <summary>
    ///     当前订阅的数值数组（索引 0xA1），由请求更新回调缓存。
    /// </summary>
    [FieldOffset(1072)]
    public NumberArrayData* NumberArray;

    /// <summary>
    ///     构造窗口并初始化血条槽位与缩放系数。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 D2 48 8D 8B ?? ?? ?? ?? 48 89 03 41 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 33 C0 80 8B ?? ?? ?? ?? ?? 41 B0"
    )]
    public partial AddonKTGIGaugeHp* Ctor();

    /// <summary>
    ///     按数值数组 0xA1 更新全部血条槽位：为新出现的敌人分配槽位、刷新已有槽位的血量与位置、隐藏已经消失的槽位。
    /// </summary>
    [MemberFunction("4C 8B DC 56 48 81 EC ?? ?? ?? ?? 48 8B 81")]
    public partial void UpdateGaugeEntries();

    /// <summary>
    ///     按坐标、缩放百分比与优先级摆放指定槽位。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="x">屏幕横坐标，血条按缩放后的宽度居中于该点。</param>
    /// <param name="y">屏幕纵坐标。</param>
    /// <param name="scalePercent">缩放百分比，取值下限为 60。</param>
    /// <param name="priority">优先级系数，取值 0 至 1。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 53 48 83 EC ?? 0F 29 7C 24 ?? 48 8B D9 44 0F 29 44 24 ?? 0F 28 FA 48 63 C2 44 0F 28 C3 48 89 7C 24 ?? 48 8D 3C 40")]
    public partial void LayoutGaugeEntry
    (
        uint  index,
        float x,
        float y,
        int   scalePercent,
        float priority
    );

    /// <summary>
    ///     在数值变化时更新指定槽位的血量百分比。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="value">血量百分比。</param>
    [MemberFunction("83 FA ?? 77 ?? 53 48 83 EC ?? 48 63 C2 48 8B D9 48 89 7C 24 ?? 48 8D 3C 40 48 83 BC F9")]
    public partial void SetGaugeValue
    (
        uint  index,
        float value
    );

    /// <summary>
    ///     为指定槽位绑定敌人实体 id 与血量百分比，并把节点标记为待显示。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="value">血量百分比。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 57 48 83 EC ?? 0F 29 74 24")]
    public partial void AssignGaugeEntry
    (
        uint  index,
        int   entityID,
        float value
    );

    /// <summary>
    ///     隐藏指定槽位的节点并移出屏幕。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("83 FA ?? 77 ?? 53 48 83 EC ?? 48 63 C2 48 8B D9 48 89 7C 24 ?? 48 8D 3C 40 48 8B 8C F9")]
    public partial void HideGaugeEntry
    (
        uint index
    );

    /// <summary>
    ///     清空并隐藏全部槽位。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 0F 29 74 24 ?? 48 8D B9")]
    public partial void ResetGaugeEntries();

    /// <summary>
    ///     设置指定槽位节点及其全部子节点的深度排序标志。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="enable">是否启用按深度决定绘制优先级。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 55 48 83 EC ?? 48 63 C2 41 0F B6 E8 48 89 5C 24 ?? 48 8D 14 40")]
    public partial void SetEntryDepthBasedPriority
    (
        uint index,
        bool enable
    );

    /// <summary>
    ///     单个血条槽位。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe struct GaugeEntry
    {
        /// <summary>
        ///     复制出来的组件节点（节点 id 为 20000 + 槽位索引）。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     组件节点内部的血条组件。
        /// </summary>
        [FieldOffset(8)]
        public AtkComponentGaugeBar* GaugeBar;

        /// <summary>
        ///     血条对应的敌人实体 id，无效时为 -536870912。
        /// </summary>
        [FieldOffset(16)]
        public int EntityID;

        /// <summary>
        ///     最近一次写入血条组件的血量百分比。
        /// </summary>
        [FieldOffset(20)]
        public int GaugeValue;
    }
}
