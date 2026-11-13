using CStringPointer = InteropGenerator.Runtime.CStringPointer;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     采集品鉴定目标列表界面，列出当前采集品类别下的五个候选目标，
///     由单选按钮切换选中项，并在顶部显示选中目标的说明文本。
///     其 <c>ReceiveEvent</c>、<c>SetDepthLayer</c>、<c>Finalizer</c>、
///     <c>LoadUldResourceHandle</c>、<c>OnSetup</c> 与 <c>OnRefresh</c> 均由
///     <see cref="AtkUnitBase" /> 继承而来的虚函数槽位覆盖。
/// </summary>
[Addon("HWDGathereInspectTargetList")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 1176)]
public unsafe partial struct AddonHWDGathereInspectTargetList
{
    /// <summary>
    ///     五个候选目标条目，每个条目对应一个单选按钮与一段显示文本。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray5<Entry> EntriesStorage;

    /// <summary>
    ///     当前选中的条目下标，取值 0 至 4。
    /// </summary>
    [FieldOffset(1168)]
    public int SelectedIndex;

    /// <summary>
    ///     一个候选目标条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 120)]
    public struct Entry
    {
        /// <summary>
        ///     条目显示文本，由刷新数据中的前缀与后缀拼接而成。
        /// </summary>
        [FieldOffset(0)]
        public Utf8String Text;

        /// <summary>
        ///     该条目对应的单选按钮组件。
        /// </summary>
        [FieldOffset(104)]
        public AtkComponentRadioButton* RadioButton;

        /// <summary>
        ///     条目文本前缀的指针缓存，用于刷新时只替换后缀而保留前缀。
        /// </summary>
        [FieldOffset(112)]
        public CStringPointer LabelPrefix;
    }

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? BF ?? ?? ?? ?? 48 89 06 48 8D 9E ?? ?? ?? ?? 33 ED 48 8B CB E8 ?? ?? ?? ?? 48 89 6B ?? 48 89 6B ?? 48 83 C3"
    )]
    public partial AddonHWDGathereInspectTargetList* Ctor();

    /// <summary>
    ///     析构界面内嵌的五个文本缓冲并清理基类成员，不释放自身内存。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F1 48 89 01 48 8D 99 ?? ?? ?? ?? BF ?? ?? ?? ?? 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B CE 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 0F B7 93 ?? ?? ?? ?? 41 B0 ?? 48 8D 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B CB"
    )]
    public partial void PrepareDestructor();

    /// <summary>
    ///     设置选中的条目下标，激活对应单选按钮并把该条目文本显示到说明节点。
    /// </summary>
    /// <param name="index">条目下标，大于 4 时不做任何处理。</param>
    [MemberFunction("83 FA ?? 77 ?? 57 48 83 EC ?? 48 63 C2 48 8B F9 89 91")]
    public partial void SetSelectedIndex
    (
        uint index
    );
}
