using NativeDutySlot = FFXIVClientStructs.FFXIV.Client.UI.DutySlot;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

[GenerateInterop]
[Inherits<NativeDutySlot>]
[StructLayout(LayoutKind.Explicit, Size = 360)]
public unsafe partial struct DutySlot
{
    [MemberFunction("66 83 FA ?? 75 ?? 53 48 83 EC ?? 49 8B D9 45 85 C0 75 ?? 48 8B 54 24")]
    public partial void ReceiveEvent
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* atkEventData
    );
}
