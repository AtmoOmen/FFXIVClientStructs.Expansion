using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的 HUD 血条窗口（KTGHudGaugeHp）。<br />
///     窗口以一条血条显示当前目标的血量百分比，并按刷新命令设置显示状态与屏幕位置。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGHudGaugeHp")]
[StructLayout(LayoutKind.Explicit, Size = 600)]
public unsafe partial struct AddonKTGHudGaugeHp
{
    /// <summary>
    ///     血条组件（节点 2 的组件）。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentGaugeBar* GaugeBar;

    /// <summary>
    ///     当前血量。
    /// </summary>
    [FieldOffset(576)]
    public int CurrentHealth;

    /// <summary>
    ///     最大血量，为 0 时不更新血条。
    /// </summary>
    [FieldOffset(580)]
    public int MaxHealth;

    /// <summary>
    ///     窗口是否已经完成初始化。
    /// </summary>
    [FieldOffset(584)]
    public bool IsInitialized;

    /// <summary>
    ///     窗口的横坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(588)]
    public float PositionX;

    /// <summary>
    ///     窗口的纵坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(592)]
    public float PositionY;

    /// <summary>
    ///     构造窗口并初始化血条状态与坐标。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 80 8B ?? ?? ?? ?? ?? 48 8B CB 80 8B ?? ?? ?? ?? ?? 81 8B ?? ?? ?? ?? ?? ?? ?? ?? 48 89 03 33 C0 48 89 83 ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? 88 83"
    )]
    public partial AddonKTGHudGaugeHp* Ctor();
}
