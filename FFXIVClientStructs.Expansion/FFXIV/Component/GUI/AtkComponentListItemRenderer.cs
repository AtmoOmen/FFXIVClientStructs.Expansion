using NativeListItemRenderer = FFXIVClientStructs.FFXIV.Component.GUI.AtkComponentListItemRenderer;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

/// <summary>
///     普通列表项的原生数据填充入口。
/// </summary>
[GenerateInterop]
[Inherits<NativeListItemRenderer>]
[StructLayout(LayoutKind.Explicit, Size = 424)]
public unsafe partial struct AtkComponentListItemRenderer
{
    /// <summary>
    ///     绑定列表项索引、更新显示状态并调用界面提供的数据填充回调。
    /// </summary>
    /// <param name="itemData">本次填充使用的标签、图标及显示状态数据。</param>
    /// <param name="itemIndex">绑定的列表项索引。</param>
    /// <param name="skipPopulation">是否跳过界面提供的数据填充回调。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 45 32 F6 41 0F B7 C0")]
    public partial void Populate
    (
        void*  itemData,
        ushort itemIndex,
        bool   skipPopulation
    );
}
