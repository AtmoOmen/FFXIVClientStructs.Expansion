namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的开始文字窗口（_KTGStart）。<br />
///     窗口沿用 LevelUp 界面资源显示任务开始提示，尺寸按屏幕缩放。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGStart")]
[StructLayout(LayoutKind.Explicit, Size = 680)]
public unsafe partial struct AddonKTGStart;
