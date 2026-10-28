using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的清空文字窗口（_KTGClear）。<br />
///     窗口以 KTGScreenTextClear 界面资源显示清空提示，并在主图标之外显示三个附加图标。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGClear")]
[StructLayout(LayoutKind.Explicit, Size = 704)]
public unsafe partial struct AddonKTGClear
{
    /// <summary>
    ///     附加图标节点 1，取节点 3。
    /// </summary>
    [FieldOffset(680)]
    public AtkImageNode* ExtraImageNode1;

    /// <summary>
    ///     附加图标节点 2，取节点 5。
    /// </summary>
    [FieldOffset(688)]
    public AtkImageNode* ExtraImageNode2;

    /// <summary>
    ///     附加图标节点 3，取节点 4。
    /// </summary>
    [FieldOffset(696)]
    public AtkImageNode* ExtraImageNode3;
}
