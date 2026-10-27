using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的 HUD 待办窗口（KTGHudTodo）。<br />
///     窗口显示当前关卡目标与完成进度，目标类型由刷新命令决定（击倒数、全部击倒或指定目标）。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGHudTodo")]
[StructLayout(LayoutKind.Explicit, Size = 584)]
public unsafe partial struct AddonKTGHudTodo
{
    /// <summary>
    ///     窗口是否已经完成初始化。
    /// </summary>
    [FieldOffset(568)]
    public bool IsInitialized;

    /// <summary>
    ///     窗口的横坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(572)]
    public float PositionX;

    /// <summary>
    ///     窗口的纵坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(576)]
    public float PositionY;

    /// <summary>
    ///     构造窗口并初始化目标显示状态。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 80 8B ?? ?? ?? ?? ?? 48 8B CB 80 8B ?? ?? ?? ?? ?? 81 8B ?? ?? ?? ?? ?? ?? ?? ?? 48 89 03 33 C0 C6 83"
    )]
    public partial AddonKTGHudTodo* Ctor();
}
