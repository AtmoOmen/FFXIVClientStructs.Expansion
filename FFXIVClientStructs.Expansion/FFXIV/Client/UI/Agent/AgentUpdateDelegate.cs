using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

public unsafe delegate void AgentUpdateDelegate
(
    AgentInterface* agent,
    uint            frameCount
);
