using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentContentsFinder = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentContentsFinder;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     任务搜索器代理的扩展声明。
/// </summary>
[Agent(AgentId.ContentsFinder)]
[GenerateInterop]
[Inherits<NativeAgentContentsFinder>]
[StructLayout(LayoutKind.Explicit, Size = 9304)]
public unsafe partial struct AgentContentsFinder
{
    /// <summary>
    ///     提交任务出发响应。接受时检查排队职业、角色状态和任务参加许可，
    ///     成功后发送接受响应并进入已确认状态，不显示键盘检测等附加确认提示。
    /// </summary>
    /// <param name="accept">是否接受本次任务出发。</param>
    /// <param name="immediateWithdraw">拒绝出发时，是否缩短退出等待时间。</param>
    /// <returns>是否成功处理出发响应。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 41 0F B6 F0 48 8D 0D ?? ?? ?? ?? 0F B6 DA")]
    public partial bool RespondToDuty
    (
        bool accept,
        bool immediateWithdraw = false
    );
}
