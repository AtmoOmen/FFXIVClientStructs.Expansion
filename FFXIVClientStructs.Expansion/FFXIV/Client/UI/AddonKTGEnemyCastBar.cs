using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏中的敌方法条窗口（KTGEnemyCastBar）。<br />
///     窗口持有二十个法条槽位，每个槽位由复制出来的组件节点组成，显示施法者、技能名、读条进度与剩余时间。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGEnemyCastBar")]
[StructLayout(LayoutKind.Explicit, Size = 2200)]
public unsafe partial struct AddonKTGEnemyCastBar
{
    /// <summary>
    ///     二十个法条槽位，每个槽位 80 字节。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray20<CastBarEntry> CastBarEntriesStorage;

    /// <summary>
    ///     第 0 个槽位节点的宽度，用于按缩放后宽度水平居中。
    /// </summary>
    [FieldOffset(2168)]
    public float EntryWidth;

    /// <summary>
    ///     第 0 个槽位节点的高度。
    /// </summary>
    [FieldOffset(2172)]
    public float EntryHeight;

    /// <summary>
    ///     法条布局的缩放系数，默认 1。
    /// </summary>
    [FieldOffset(2176)]
    public float LayoutScale;

    /// <summary>
    ///     法条缩放百分比对应的系数，默认 0.85，可由刷新参数改写。
    /// </summary>
    [FieldOffset(2180)]
    public float ScalePercentFactor;

    /// <summary>
    ///     法条的额外缩放系数，默认 1，可由刷新参数改写。
    /// </summary>
    [FieldOffset(2184)]
    public float ScaleMultiplier;

    /// <summary>
    ///     当前订阅的数值数组（索引 0xA1），由请求更新回调缓存。
    /// </summary>
    [FieldOffset(2192)]
    public NumberArrayData* NumberArray;

    /// <summary>
    ///     构造窗口并把全部槽位与缩放系数初始化为默认值。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 D2 48 8D 9F ?? ?? ?? ?? 48 89 07 48 8B CB 41 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 83 C3"
    )]
    public partial AddonKTGEnemyCastBar* Ctor();

    /// <summary>
    ///     播放指定槽位的打断动画（时间线标签 103），并把槽位标记为已应用。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    [MemberFunction("48 83 EC ?? 83 FA ?? 77 ?? 48 89 5C 24 ?? 48 63 C2 48 8D 1C 80")]
    public partial void PlayInterruptTimeline
    (
        uint index
    );

    /// <summary>
    ///     为指定槽位绑定敌人实体 id、施法者名与是否正在施法，并按施法状态播放或复位进度条动画。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="entityID">敌人实体 id。</param>
    /// <param name="casterName">施法者名文本。</param>
    /// <param name="isCasting">是否正在施法。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 57 48 83 EC ?? 48 89 5C 24")]
    public partial void AssignCastBarEntry
    (
        uint           index,
        int            entityID,
        CStringPointer casterName,
        bool           isCasting
    );

    /// <summary>
    ///     清空并隐藏全部槽位。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 33 F6 48 8B F9 8B DE 48 63 C3")]
    public partial void ResetCastBarEntries();

    /// <summary>
    ///     按坐标、缩放百分比与优先级摆放指定槽位。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="x">屏幕横坐标，法条按缩放后的宽度居中于该点。</param>
    /// <param name="y">屏幕纵坐标。</param>
    /// <param name="scalePercent">缩放百分比，取值下限为 60。</param>
    /// <param name="priority">优先级系数，取值 0 至 1。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 57 48 83 EC ?? 0F 29 7C 24")]
    public partial void LayoutCastBarEntry
    (
        uint  index,
        float x,
        float y,
        int   scalePercent,
        float priority
    );

    /// <summary>
    ///     设置指定槽位节点及其全部子节点的深度排序标志。
    /// </summary>
    /// <param name="index">槽位索引。</param>
    /// <param name="enable">是否启用按深度决定绘制优先级。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 55 48 83 EC ?? 48 63 C2 41 0F B6 E8 48 89 5C 24 ?? 48 8D 14 80")]
    public partial void SetEntryDepthBasedPriority
    (
        uint index,
        bool enable
    );

    /// <summary>
    ///     按数值数组 0xA1 中缓存的坐标与缩放重新摆放全部槽位。
    /// </summary>
    [MemberFunction
    (
        "41 56 48 83 EC ?? 48 8B 81 ?? ?? ?? ?? 4C 8B F1 48 85 C0 0F 84 ?? ?? ?? ?? 48 8B 40 ?? 48 63 10 85 D2 0F 8E ?? ?? ?? ?? 48 89 7C 24 ?? 33 FF 4C 89 7C 24 ?? 4C 8B FA 85 D2 0F 8E ?? ?? ?? ?? 48 89 5C 24 ?? 8D 5F ?? 48 89 6C 24 ?? 48 89 74 24 ?? 33 F6 33 ED 83 FF ?? 0F 8D ?? ?? ?? ?? 49 8B 86 ?? ?? ?? ?? 33 D2 48 8B 48 ?? 8B 44 19 ?? 44 8B 44 29 ?? 44 8B 4C 19 ?? 89 44 24 ?? 49 8D 86 ?? ?? ?? ?? 44 39 00 74 ?? FF C2 48 83 C0"
    )]
    public partial void UpdateCastBarLayout();

    /// <summary>
    ///     单个法条槽位。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 80)]
    public unsafe struct CastBarEntry
    {
        /// <summary>
        ///     复制出来的组件节点（节点 id 为 20000 + 槽位索引）。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     读条容器节点（节点 2），承载进度条与全部文本。
        /// </summary>
        [FieldOffset(8)]
        public AtkResNode* GaugeContainerNode;

        /// <summary>
        ///     施法者名文本节点（节点 4）。
        /// </summary>
        [FieldOffset(16)]
        public AtkTextNode* CasterTextNode;

        /// <summary>
        ///     技能名文本节点（节点 3）。
        /// </summary>
        [FieldOffset(24)]
        public AtkTextNode* SkillNameTextNode;

        /// <summary>
        ///     剩余时间文本节点（节点 5）。
        /// </summary>
        [FieldOffset(32)]
        public AtkTextNode* CastTimeTextNode;

        /// <summary>
        ///     读条进度填充图片节点（节点 6），按进度设置横向缩放。
        /// </summary>
        [FieldOffset(40)]
        public AtkImageNode* ProgressFillImageNode;

        /// <summary>
        ///     打断提示图片节点（节点 7）。
        /// </summary>
        [FieldOffset(48)]
        public AtkImageNode* InterruptImageNode;

        /// <summary>
        ///     读条底图图片节点（节点 8）。
        /// </summary>
        [FieldOffset(56)]
        public AtkImageNode* BackgroundImageNode;

        /// <summary>
        ///     法条对应的敌人实体 id，无效时为 -536870912。
        /// </summary>
        [FieldOffset(64)]
        public int EntityID;

        /// <summary>
        ///     读条进度，取值 0 至 1，由数值数组的百分比除以 100 得到。
        /// </summary>
        [FieldOffset(68)]
        public float Progress;

        /// <summary>
        ///     是否已经播放过打断动画。
        /// </summary>
        [FieldOffset(72)]
        public bool InterruptTimelineApplied;

        /// <summary>
        ///     是否正在施法，对应数值数组中的施法标志。
        /// </summary>
        [FieldOffset(73)]
        public bool IsCasting;

        /// <summary>
        ///     当前剩余读条时间，同时作为剩余时间文本的显示值。
        /// </summary>
        [FieldOffset(76)]
        public int CastTimeValue;
    }
}
