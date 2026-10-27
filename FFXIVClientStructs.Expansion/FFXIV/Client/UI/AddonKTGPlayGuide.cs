using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的操作指南窗口（KTGPlayGuide）。<br />
///     窗口以文本九宫格显示“按 Enter 发送、按退出键清空”的提示，并按当前键位配置动态生成按键名。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGPlayGuide")]
[StructLayout(LayoutKind.Explicit, Size = 592)]
public unsafe partial struct AddonKTGPlayGuide
{
    /// <summary>
    ///     提示文本所在的文本九宫格组件（节点 2）。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentTextNineGrid* TextNineGrid;

    /// <summary>
    ///     窗口是否已经完成初始化。
    /// </summary>
    [FieldOffset(576)]
    public bool IsInitialized;

    /// <summary>
    ///     窗口的横坐标，按提示文本宽度居中后得到。
    /// </summary>
    [FieldOffset(580)]
    public float PositionX;

    /// <summary>
    ///     窗口的纵坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(584)]
    public float PositionY;

    /// <summary>
    ///     构造窗口并初始化提示状态与坐标。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 80 8B ?? ?? ?? ?? ?? 48 8B CB 80 8B ?? ?? ?? ?? ?? 81 8B ?? ?? ?? ?? ?? ?? ?? ?? 48 89 03 33 C0 48 89 83 ?? ?? ?? ?? 88 83"
    )]
    public partial AddonKTGPlayGuide* Ctor();

    /// <summary>
    ///     按当前键位配置生成提示文本（含退出键名）写入文本九宫格，并按文本宽度把窗口水平居中。
    /// </summary>
    [MemberFunction("40 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 83 B9 ?? ?? ?? ?? ?? 48 8B F1 0F 84")]
    public partial void RefreshGuideText();
}
