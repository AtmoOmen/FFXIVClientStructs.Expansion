using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     采集品鉴定物品数量界面，以七列表格列出各采集品鉴定条目的名称、需求数量与持有数量，
///     并在鼠标经过或点击某一行时显示物品提示与触发回调。
///     其 <c>ReceiveEvent</c>、<c>Hide2</c>、<c>LoadUldResourceHandle</c>、
///     <c>OnSetup</c> 与 <c>OnRefresh</c> 均由 <see cref="AtkUnitBase" /> 继承而来的虚函数槽位覆盖。
/// </summary>
[Addon("HWDGathererInspectionItemCount")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 592)]
public unsafe partial struct AddonHWDGathererInspectionItemCount
{
    /// <summary>
    ///     顶部标题文本节点。
    /// </summary>
    [FieldOffset(568)]
    public AtkTextNode* TitleTextNode;

    /// <summary>
    ///     列表状态节点，通过播放不同动画标签在「有数据」与「无数据」两种表现间切换。
    /// </summary>
    [FieldOffset(576)]
    public AtkResNode* StateAnimationNode;

    /// <summary>
    ///     物品列表组件，共七列。
    /// </summary>
    [FieldOffset(584)]
    public AtkComponentList* ItemList;

    /// <summary>
    ///     构造界面。
    /// </summary>
    /// <returns>界面自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 03 33 C0 80 8B ?? ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? B8 ?? ?? ?? ?? 66 89 83 ?? ?? ?? ?? 48 8B C3 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 74 24"
    )]
    public partial AddonHWDGathererInspectionItemCount* Ctor();

    /// <summary>
    ///     析构界面并清理基类成员，不释放自身内存。
    /// </summary>
    [MemberFunction("48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 89 74 24 ?? 57 48 83 EC ?? 48 8B B1")]
    public partial void PrepareDestructor();

    /// <summary>
    ///     列表行渲染回调，按界面值为该行的图标、文本与数字赋值。
    /// </summary>
    /// <param name="listItemIndex">列表行下标。</param>
    /// <param name="nodeList">该行的七个节点，依次为图标、第一段文本、动画载体、第一个数字、第二个数字、第二段文本、第三个数字。</param>
    /// <param name="renderer">列表项渲染器。</param>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 48 8B B1 ?? ?? ?? ?? 49 8B F8")]
    public partial void RenderListItem
    (
        int                           listItemIndex,
        AtkResNode**                  nodeList,
        AtkComponentListItemRenderer* renderer
    );

    /// <summary>
    ///     按界面值设置标题、列表行数与各列内容，是初始化与刷新的共用实现。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">界面值数组。</param>
    [MemberFunction("4D 85 C0 74 ?? 48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9")]
    public partial void SetAddonValues
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     选中列表某一行并显示该行物品的提示框。
    /// </summary>
    /// <param name="itemIndex">列表行下标。</param>
    /// <returns>选中成功时为 <see langword="true" />。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 41 56 48 81 EC ?? ?? ?? ?? 48 8B F1")]
    public partial bool SelectItem
    (
        int itemIndex
    );

    /// <summary>
    ///     隐藏物品提示框并发送取消选中回调。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 0F B7 97 ?? ?? ?? ?? 45 33 C0 48 8D 88")]
    public partial void DeselectItem();
}
