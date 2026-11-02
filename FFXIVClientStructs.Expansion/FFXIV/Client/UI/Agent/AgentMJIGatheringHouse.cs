using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentMJIGatheringHouse = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentMJIGatheringHouse;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     屯货仓库探索目的地请求的回包处理。
/// </summary>
[Agent(AgentId.MJIGatheringHouse)]
[GenerateInterop]
[Inherits<NativeAgentMJIGatheringHouse>]
[StructLayout(LayoutKind.Explicit, Size = 520)]
public unsafe partial struct AgentMJIGatheringHouse
{
    /// <summary>
    ///     探索目的地请求完成后刷新当前仓库的探索选择。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 48 8D 91")]
    public partial void RefreshExpeditions();
}
