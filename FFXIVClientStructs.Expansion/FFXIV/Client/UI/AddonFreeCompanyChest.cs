using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("FreeCompanyChest")]
[StructLayout(LayoutKind.Explicit, Size = 0x4F8)]
public unsafe partial struct AddonFreeCompanyChest
{
    /// <summary>
    ///     部队储物柜主内容节点，其高度用于计算界面的纵向偏移。
    /// </summary>
    [FieldOffset(568)]
    public AtkResNode* ContentNode;

    /// <summary>
    ///     页面切换动画节点。
    /// </summary>
    [FieldOffset(576)]
    public AtkResNode* PageSwitchNode;

    /// <summary>
    ///     页签选中指示动画节点。
    /// </summary>
    [FieldOffset(584)]
    public AtkResNode* TabIndicatorNode;

    /// <summary>
    ///     五个物品页页签的单选按钮，依次对应部队储物柜第 1 至第 5 页。
    /// </summary>
    [FieldOffset(592)]
    [FixedSizeArray]
    internal FixedSizeArray5<Pointer<AtkComponentRadioButton>> ItemTabButtonsStorage;

    /// <summary>
    ///     部队水晶页页签的单选按钮。
    /// </summary>
    [FieldOffset(632)]
    public AtkComponentRadioButton* CrystalTabButton;

    /// <summary>
    ///     部队金币页页签的单选按钮。
    /// </summary>
    [FieldOffset(640)]
    public AtkComponentRadioButton* GilTabButton;

    /// <summary>
    ///     水晶页的焦点节点。
    /// </summary>
    [FieldOffset(648)]
    public AtkResNode* CrystalFocusNode;

    /// <summary>
    ///     金币页的焦点节点。
    /// </summary>
    [FieldOffset(656)]
    public AtkResNode* GilFocusNode;

    /// <summary>
    ///     物品页的 50 个格子。
    /// </summary>
    [FieldOffset(664)]
    [FixedSizeArray]
    internal FixedSizeArray50<Pointer<AtkComponentDragDrop>> ItemSlotsStorage;

    /// <summary>
    ///     水晶页的 18 个格子。
    /// </summary>
    [FieldOffset(1064)]
    [FixedSizeArray]
    internal FixedSizeArray18<Pointer<AtkComponentDragDrop>> CrystalSlotsStorage;

    /// <summary>
    ///     显示当前物品页已占用格子数量的文本节点。
    /// </summary>
    [FieldOffset(1208)]
    public AtkTextNode* ItemCountTextNode;

    /// <summary>
    ///     仅在初始化时保存，界面的其他流程未再使用。
    /// </summary>
    [FieldOffset(1216)]
    public AtkResNode* UnusedNode;

    /// <summary>
    ///     显示部队金币数量并用于打开金币存取界面的按钮。
    /// </summary>
    [FieldOffset(1224)]
    public AtkComponentButton* GilButton;

    /// <summary>
    ///     用于打开部队储物柜日志界面的按钮。
    /// </summary>
    [FieldOffset(1232)]
    public AtkComponentButton* LogButton;

    /// <summary>
    ///     当前页签类别，由代理下发的数据写入。<br />
    ///     0 表示物品页，1 表示水晶页，2 表示既非物品页也非水晶页。
    /// </summary>
    [FieldOffset(1240)]
    public byte TabType;

    /// <summary>
    ///     当前物品页的箱子序号，取值 0 至 4。
    /// </summary>
    [FieldOffset(1241)]
    public byte TabIndex;

    /// <summary>
    ///     当前水晶页对应的箱子序号，取值 1 至 5。
    /// </summary>
    [FieldOffset(1242)]
    public byte CrystalPageIndex;

    /// <summary>
    ///     五个物品页的访问等级位图，每页占两位。
    /// </summary>
    [FieldOffset(1244)]
    public uint ChestAccessFlags;

    /// <summary>
    ///     部队水晶的访问等级，0 为无权限，1 为仅查看，2 为仅存入，3 为完全权限。
    /// </summary>
    [FieldOffset(1248)]
    public uint CrystalAccessLevel;

    /// <summary>
    ///     部队金币的访问等级，0 为无权限，1 为仅查看，2 为仅存入，3 为完全权限。
    /// </summary>
    [FieldOffset(1252)]
    public uint GilAccessLevel;

    /// <summary>
    ///     操作提示所依据的页面类别。<br />
    ///     0 表示不显示提示，1 表示物品页，2 表示水晶页。
    /// </summary>
    [FieldOffset(1256)]
    public uint HintPageType;

    /// <summary>
    ///     缓存自 <see cref="AtkStage" /> 的布尔状态，变化时会重新计算页签可用性；为真时所有页签都被禁用。
    /// </summary>
    [FieldOffset(1260)]
    public bool StageFlag;

    /// <summary>
    ///     是否隐藏各页签的权限信息。
    /// </summary>
    [FieldOffset(1261)]
    public bool HideAccess;

    /// <summary>
    ///     各页签的可访问性位图，来自代理的相同字段。<br />
    ///     位 0 至 4 分别表示第 1 至第 5 个物品页，位 5 表示水晶页，位 6 表示金币页；对应位为 1 时该页可访问。
    /// </summary>
    [BitField<bool>(nameof(IsPage1Accessible),   0)]
    [BitField<bool>(nameof(IsPage2Accessible),   1)]
    [BitField<bool>(nameof(IsPage3Accessible),   2)]
    [BitField<bool>(nameof(IsPage4Accessible),   3)]
    [BitField<bool>(nameof(IsPage5Accessible),   4)]
    [BitField<bool>(nameof(IsCrystalAccessible), 5)]
    [BitField<bool>(nameof(IsGilAccessible),     6)]
    [FieldOffset(1264)]
    public uint AccessMask;

    /// <summary>
    ///     部队储物柜数据是否尚未就绪，为真时按 <see cref="AccessMask" /> 决定页签可用性。
    /// </summary>
    [FieldOffset(1268)]
    public bool IsDataNotReady;
}
