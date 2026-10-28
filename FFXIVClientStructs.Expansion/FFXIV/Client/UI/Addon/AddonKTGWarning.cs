namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的警告文字窗口（_KTGWarning）。<br />
///     窗口以 KTGScreenTextWarning 界面资源显示警告提示，尺寸按屏幕缩放。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGWarning")]
[StructLayout(LayoutKind.Explicit, Size = 680)]
public unsafe partial struct AddonKTGWarning;
