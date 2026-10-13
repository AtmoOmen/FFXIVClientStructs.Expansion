using NativeAgentFreeCompanyChest = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentFreeCompanyChest;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeAgentFreeCompanyChest>]
[Agent(AgentId.FreeCompanyChest)]
[StructLayout(LayoutKind.Explicit, Size = 0x1EF0)]
public unsafe partial struct AgentFreeCompanyChest
{
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    [FieldOffset(6956)]
    public InventoryType ContextInventoryType;

    [FieldOffset(6960)]
    public short ContextInventorySlot;

    public InventoryItem* GetContextInventoryItem()
    {
        var manager = InventoryManager.Instance();
        if (manager == null) return null;

        return ContextInventoryType == InventoryType.Invalid ?
                   null :
                   manager->GetInventorySlot(ContextInventoryType, ContextInventorySlot);
    }
}
