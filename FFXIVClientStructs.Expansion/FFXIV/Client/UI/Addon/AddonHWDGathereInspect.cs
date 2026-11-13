using CStringPointer = InteropGenerator.Runtime.CStringPointer;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     采集品鉴定主界面，显示鉴定点数量、剩余冷却与状态文本，
///     并提供鉴定范围下拉框以及提交、关闭两个按钮。
///     其 <c>ReceiveEvent</c>、<c>Hide2</c>、<c>LoadUldResourceHandle</c>、
///     <c>OnSetup</c>、<c>OnSetupComplete</c> 与 <c>OnRefresh</c> 均由
///     <see cref="AtkUnitBase" /> 继承而来的虚函数槽位覆盖。
/// </summary>
[Addon("HWDGathereInspect")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 824)]
public unsafe partial struct AddonHWDGathereInspect
{
    /// <summary>
    ///     界面第一行文本，由界面值下标 1 的字符串设置。
    /// </summary>
    [FieldOffset(568)]
    public AtkTextNode* FirstTextNode;

    /// <summary>
    ///     界面第二行文本，由界面值下标 2 的字符串设置。
    /// </summary>
    [FieldOffset(576)]
    public AtkTextNode* SecondTextNode;

    /// <summary>
    ///     界面第三行文本，由界面值下标 3 的字符串设置。
    /// </summary>
    [FieldOffset(584)]
    public AtkTextNode* ThirdTextNode;

    /// <summary>
    ///     鉴定范围下拉框，选项为「只听自己」与「队伍整体」。
    /// </summary>
    [FieldOffset(592)]
    public AtkComponentDropDownList* RangeDropDownList;

    /// <summary>
    ///     下拉框两个选项的文本缓冲，依次对应「只听自己」与「队伍整体」。
    /// </summary>
    [FieldOffset(600)]
    [FixedSizeArray]
    internal FixedSizeArray2<Utf8String> RangeLabelsStorage;

    /// <summary>
    ///     下拉框两个选项的标签指针，指向 <see cref="RangeLabelsStorage" /> 中各项的字符串正文。
    /// </summary>
    [FieldOffset(808)]
    [FixedSizeArray]
    internal FixedSizeArray2<CStringPointer> RangeLabelPointersStorage;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 06 48 8D BE ?? ?? ?? ?? 33 C0 48 89 86 ?? ?? ?? ?? 48 89 86 ?? ?? ?? ?? 48 89 86 ?? ?? ?? ?? 8D 58 ?? 48 89 86 ?? ?? ?? ?? 0F 1F 80"
    )]
    public partial AddonHWDGathereInspect* Ctor();

    /// <summary>
    ///     析构界面内嵌的两个文本缓冲并清理基类成员，不释放自身内存。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B F1 48 89 01 48 8D 99 ?? ?? ?? ?? BF ?? ?? ?? ?? 0F 1F 84 00 ?? ?? ?? ?? 48 83 EB ?? 48 8B CB E8 ?? ?? ?? ?? 48 83 EF ?? 75 ?? 48 8B CE 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F E9 ?? ?? ?? ?? CC CC CC CC CC CC CC 41 B9 ?? ?? ?? ?? 48 8D 15 ?? ?? ?? ?? 45 33 C0 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24"
    )]
    public partial void PrepareDestructor();
}
