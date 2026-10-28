using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     打字游戏的状态代理（AgentKTGState，内部编号 400）。<br />
///     代理只维护一个显示标志，显示时置位、隐藏时清零，不打开任何窗口；
///     其余虚函数为空实现或返回空事件结果。
/// </summary>
[Agent(AgentId.Unk400)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe partial struct AgentKTGState
{
    /// <summary>
    ///     代理是否处于显示状态。
    /// </summary>
    [FieldOffset(40)]
    public bool IsShown;

    /// <summary>
    ///     构造代理并将显示标志清零。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? C6 43 ?? ?? 48 89 03 48 8B C3 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 80 79"
    )]
    public partial AgentKTGState* Ctor();
}
