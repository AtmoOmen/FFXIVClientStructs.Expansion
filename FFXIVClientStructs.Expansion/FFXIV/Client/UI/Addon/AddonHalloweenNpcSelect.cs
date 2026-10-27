using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     “选择变身角色”窗口（HalloweenNpcSelect）。<br />
///     它显示六个版本页签、对应版本下的变身角色列表以及选中角色的预览图。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("HalloweenNpcSelect")]
[StructLayout(LayoutKind.Explicit, Size = 1088)]
public unsafe partial struct AddonHalloweenNpcSelect
{
    /// <summary>
    ///     窗口标题栏的关闭按钮。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentButton* CloseButton;

    /// <summary>
    ///     六个版本页签的单选按钮，依次对应版本 0 至 5。
    /// </summary>
    [FieldOffset(576)]
    public StdVector<Pointer<AtkComponentButton>> OptionButtons;

    /// <summary>
    ///     变身角色列表。
    /// </summary>
    [FieldOffset(600)]
    public AtkComponentList* OptionList;

    /// <summary>
    ///     列表各项显示的角色名文本指针，共 50 项。
    /// </summary>
    [FieldOffset(608)]
    [FixedSizeArray]
    internal FixedSizeArray50<nint> OptionNameStorage;

    /// <summary>
    ///     列表各项在同版本内是否可用的标志，共 50 项。
    /// </summary>
    [FieldOffset(1008)]
    [FixedSizeArray]
    internal FixedSizeArray50<bool> OptionUnlockedStorage;

    /// <summary>
    ///     选中角色的预览图标。
    /// </summary>
    [FieldOffset(1064)]
    public AtkImageNode* PreviewIconNode;

    /// <summary>
    ///     当前选中的版本页签索引，取值 0 至 5。
    /// </summary>
    [FieldOffset(1072)]
    public uint SelectedButtonIndex;

    /// <summary>
    ///     列表中当前选中的行索引。
    /// </summary>
    [FieldOffset(1076)]
    public uint SelectedListItemIndex;

    /// <summary>
    ///     当前版本可用的选项数量。
    /// </summary>
    [FieldOffset(1080)]
    public uint AvailableOptionCount;

    /// <summary>
    ///     构造时随机选取的时间线编号，取值 101 至 106。
    /// </summary>
    [FieldOffset(1084)]
    public uint RandomTimelineID;

    /// <summary>
    ///     列表项渲染回调，写入角色名并把该行标记为可用或不可用。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 8B DA 49 8B 08 49 8B F0 E8 ?? ?? ?? ?? 4C 8B 8C DF ?? ?? ?? ?? 48 8D 15 ?? ?? ?? ?? 4D 85 C9 48 8B C8 49 0F 45 D1 E8 ?? ?? ?? ?? 0F B6 8C 3B"
    )]
    public partial void RenderOptionRow
    (
        uint  index,
        nint* renderArgs
    );

    /// <summary>
    ///     按代理下发的数据刷新窗口标题、页签与各扩展版本图标。
    /// </summary>
    [MemberFunction("40 55 53 56 41 54 41 55 48 8B EC")]
    public partial void RefreshHeader
    (
        nint      unused,
        AtkValue* values
    );

    /// <summary>
    ///     按代理下发的数据重建选项列表并恢复选中项。
    /// </summary>
    [MemberFunction("40 56 41 57 48 83 EC ?? 48 89 5C 24 ?? 48 8B F1 48 89 7C 24")]
    public partial void RefreshOptionList
    (
        nint      unused,
        AtkValue* values
    );
}
