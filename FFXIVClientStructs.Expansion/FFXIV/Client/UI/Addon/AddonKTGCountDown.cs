using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     打字游戏的倒计时窗口（_KTGCountDown）。<br />
///     窗口以 KTGScreenTextStart 界面资源显示开战倒计时，按屏幕缩放并播放倒计时音效；
///     时间线标签 102 与 103 播放普通音效，标签 104 播放结束音效。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfoChild>]
[Addon("_KTGCountDown")]
[StructLayout(LayoutKind.Explicit, Size = 648)]
public unsafe partial struct AddonKTGCountDown
{
    /// <summary>
    ///     构造时清零并置最低位为 1 的标志，已分析的方法只读取最低位。
    /// </summary>
    [FieldOffset(584)]
    public byte Flags248;

    /// <summary>
    ///     构造时清零、已分析的方法均未读写的保留指针。
    /// </summary>
    [FieldOffset(592)]
    public void* Unk250;

    /// <summary>
    ///     构造时清零、已分析的方法均未读写的保留值。
    /// </summary>
    [FieldOffset(600)]
    public int Unk258;

    /// <summary>
    ///     构造时置为 -1 的保留值。
    /// </summary>
    [FieldOffset(604)]
    public int Unk25C;

    /// <summary>
    ///     等待纹理加载完成的图标列表。
    /// </summary>
    [FieldOffset(616)]
    public StdList<AddonScreenInfo.TextureLoadCallback> PendingTextureLoads;

    /// <summary>
    ///     窗口缩放系数，取自窗口当前缩放。
    /// </summary>
    [FieldOffset(632)]
    public float Scale;

    /// <summary>
    ///     窗口缩放系数的副本，取自窗口当前缩放。
    /// </summary>
    [FieldOffset(636)]
    public float ScaleCopy;

    /// <summary>
    ///     按缩放计算的窗口宽度。
    /// </summary>
    [FieldOffset(640)]
    public ushort ScaledWidth;

    /// <summary>
    ///     按缩放计算的窗口高度。
    /// </summary>
    [FieldOffset(642)]
    public ushort ScaledHeight;

    /// <summary>
    ///     构造时清零、已分析的方法均未读写的保留值。
    /// </summary>
    [FieldOffset(644)]
    public ushort Unk284;

    /// <summary>
    ///     是否跳过倒计时；界面值第 9 项为真时置为 1，窗口的完成检查据此返回真。
    /// </summary>
    [FieldOffset(645)]
    public bool SkipCountdown;
}
