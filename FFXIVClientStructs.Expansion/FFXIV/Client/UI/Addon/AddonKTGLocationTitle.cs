namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的地点标题窗口（_KTGLocationTitle）。<br />
///     窗口以 KTGImgaeLocationTitle 界面资源显示地点名称与图标，尺寸按屏幕缩放；
///     收到时间线标签变化事件后播放入场动画。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfo>]
[Addon("_KTGLocationTitle")]
[StructLayout(LayoutKind.Explicit, Size = 688)]
public unsafe partial struct AddonKTGLocationTitle
{
    /// <summary>
    ///     是否需要在本次更新中播放入场时间线 105；收到时间线标签 104 的事件时置位，播放后清零。
    /// </summary>
    [FieldOffset(680)]
    public bool PlayAppearTimeline;
}
