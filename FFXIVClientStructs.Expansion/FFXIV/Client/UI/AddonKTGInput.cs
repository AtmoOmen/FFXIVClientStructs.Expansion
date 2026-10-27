using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的输入窗口（KTGInput）。<br />
///     窗口内嵌一个文本输入组件，接收玩家键入的单词并回传给游戏，同时负责窗口的显示、隐藏与定位。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGInput")]
[StructLayout(LayoutKind.Explicit, Size = 592)]
public unsafe partial struct AddonKTGInput
{
    /// <summary>
    ///     输入框节点（节点 2）当前播放的时间线标签，取值 17、20、101 或 102。
    /// </summary>
    [FieldOffset(568)]
    public int InputTimelineState;

    /// <summary>
    ///     窗口是否处于显示状态。
    /// </summary>
    [FieldOffset(572)]
    public bool IsShown;

    /// <summary>
    ///     是否正在把焦点切换到输入组件，设置焦点期间忽略输入。
    /// </summary>
    [FieldOffset(573)]
    public bool IsSettingFocus;

    /// <summary>
    ///     窗口最终的横坐标，按目标点与缩放后的窗口宽度计算。
    /// </summary>
    [FieldOffset(576)]
    public float PositionX;

    /// <summary>
    ///     窗口最终的纵坐标，按目标点与缩放后的窗口高度计算。
    /// </summary>
    [FieldOffset(580)]
    public float PositionY;

    /// <summary>
    ///     内嵌的文本输入组件，取自节点 7。
    /// </summary>
    [FieldOffset(584)]
    public AtkComponentTextInput* TextInput;

    /// <summary>
    ///     构造窗口并初始化输入框状态与坐标。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 A3 ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 81 8B")]
    public partial AddonKTGInput* Ctor();
}
