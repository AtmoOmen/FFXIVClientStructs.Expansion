using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的结束文字窗口（_KTGGameover）。<br />
///     窗口以 KTGScreenTextGameOver 界面资源显示游戏结束提示，并在主图标之外显示三个附加图标。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGGameover")]
[StructLayout(LayoutKind.Explicit, Size = 704)]
public unsafe partial struct AddonKTGGameover
{
    /// <summary>
    ///     附加图标节点 1，取节点 4。
    /// </summary>
    [FieldOffset(680)]
    public AtkImageNode* ExtraImageNode1;

    /// <summary>
    ///     附加图标节点 2，取节点 3。
    /// </summary>
    [FieldOffset(688)]
    public AtkImageNode* ExtraImageNode2;

    /// <summary>
    ///     附加图标节点 3，取节点 5。
    /// </summary>
    [FieldOffset(696)]
    public AtkImageNode* ExtraImageNode3;
}
