using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("WeeklyBingoContentList")]
[StructLayout(LayoutKind.Explicit, Size = 584)]
public unsafe partial struct AddonWeeklyBingoContentList
{
    /// <summary>
    /// 标题节点。
    /// </summary>
    [FieldOffset(568)]
    public AtkTextNode* TitleNode;

    /// <summary>
    /// 副本列表组件。
    /// </summary>
    [FieldOffset(576)]
    public AtkComponentList* ContentList;
}
