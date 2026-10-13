using NativeAgentFreeCompanyProfile = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentFreeCompanyProfile;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeAgentFreeCompanyProfile>]
[Agent(AgentId.FreeCompanyProfile)]
[StructLayout(LayoutKind.Explicit, Size = 0x280)]
public unsafe partial struct AgentFreeCompanyProfile
{
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B 01 48 8B FA 48 8B D9 FF 50 ?? 84 C0 74 ?? 48 8B 03 48 8B CB FF 50 ?? 48 89 7B")]
    public partial void ShowProfile
    (
        ulong contentID
    );
}
