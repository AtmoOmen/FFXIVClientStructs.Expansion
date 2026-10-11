using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Lua;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.Native;

public unsafe delegate AtkValue* AgentReceiveEventDelegate
(
    AgentInterface* agent,
    AtkValue*       returnValues,
    AtkValue*       values,
    uint            valueCount,
    ulong           eventKind
);

