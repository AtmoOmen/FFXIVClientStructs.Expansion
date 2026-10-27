using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的爆发文字窗口（KTGScreenTextFever）。<br />
///     窗口只有一段全屏文字，进入爆发状态时播放时间线 102，结束时播放时间线 101 并播放音效。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGScreenTextFever")]
[StructLayout(LayoutKind.Explicit, Size = 576)]
public unsafe partial struct AddonKTGScreenTextFever
{
    /// <summary>
    ///     爆发文字节点（节点 2）。
    /// </summary>
    [FieldOffset(568)]
    public AtkResNode* TextNode;

    /// <summary>
    ///     构造窗口并把爆发文字节点置空。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 81 8B ?? ?? ?? ?? ?? ?? ?? ?? 48 89 03 48 8B C3 48 C7 83 ?? ?? ?? ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 41 B9"
    )]
    public partial AddonKTGScreenTextFever* Ctor();
}
