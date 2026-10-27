using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeListItemRenderer = FFXIVClientStructs.FFXIV.Component.GUI.AtkComponentListItemRenderer;
using NativeTreeList = FFXIVClientStructs.FFXIV.Component.GUI.AtkComponentTreeList;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

/// <summary>
///     树形列表项的原生数据填充入口。
/// </summary>
[GenerateInterop]
[Inherits<NativeTreeList>]
[StructLayout(LayoutKind.Explicit, Size = 560)]
public unsafe partial struct AtkComponentTreeList
{
    /// <summary>
    ///     将树形列表项绑定到渲染器，并执行对应模板的数据填充回调及焦点处理。
    /// </summary>
    /// <param name="itemIndex">树形列表项索引。</param>
    /// <param name="item">本次填充的树形列表项。</param>
    /// <param name="renderer">接收列表项数据的渲染器。</param>
    /// <param name="isHighlighted">是否显示为选中状态。</param>
    /// <param name="isDragging">是否显示为拖动状态。</param>
    /// <returns>原生焦点处理返回的状态值。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 0F B6 6C 24")]
    public partial byte PopulateItem
    (
        uint                      itemIndex,
        AtkComponentTreeListItem* item,
        NativeListItemRenderer*   renderer,
        bool                      isHighlighted,
        bool                      isDragging
    );
}
