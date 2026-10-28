namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的波次文字窗口（_KTGWave）。<br />
///     窗口以 KTGScreenTextWave 界面资源显示当前波次，尺寸按屏幕缩放。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGWave")]
[StructLayout(LayoutKind.Explicit, Size = 680)]
public unsafe partial struct AddonKTGWave;
