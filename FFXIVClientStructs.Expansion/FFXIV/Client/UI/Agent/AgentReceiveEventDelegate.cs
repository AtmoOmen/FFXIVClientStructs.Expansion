using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

public unsafe delegate AtkValue* AgentReceiveEventDelegate
(
    AgentInterface* agent,
    AtkValue*       returnValues,
    AtkValue*       values,
    uint            valueCount,
    ulong           eventKind
);
