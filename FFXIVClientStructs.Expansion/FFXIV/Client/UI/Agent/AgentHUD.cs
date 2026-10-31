using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentHUD = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentHUD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Agent(AgentId.Hud)]
[Inherits<NativeAgentHUD>]
[StructLayout(LayoutKind.Explicit, Size = 20168)]
public unsafe partial struct AgentHUD
{
    [MemberFunction("40 55 41 54 41 55 41 56 41 57 48 8D 6C 24 90 48 81 EC 70 01 00 00")]
    public partial CStringPointer GetStatusTooltipText
    (
        Utf8String* output,
        uint        statusID,
        uint        param
    );
}
