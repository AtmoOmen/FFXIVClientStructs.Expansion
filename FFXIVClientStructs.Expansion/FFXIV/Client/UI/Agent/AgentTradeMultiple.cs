using NativeAgentTradeMultiple = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentTradeMultiple;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeAgentTradeMultiple>]
[Agent(AgentId.TradeMultiple)]
[StructLayout(LayoutKind.Explicit, Size = 176)]
public unsafe partial struct AgentTradeMultiple : IEnumerable<SelectedMateria>
{
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    [FieldOffset(40)]
    public TradeMultiple TradeMultiple;

    public SelectedMateria this
    [
        int index
    ] =>
        SelectedMateria[index];

    [UnscopedRef]
    public Span<SelectedMateria> SelectedMateria =>
        TradeMultiple.SelectedMateria;

    public IEnumerator<SelectedMateria> GetEnumerator()
    {
        for (var i = 0; i < 5; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void StartTransmutation()
    {
        var outValue = new AtkValue();
        var inValue  = new AtkValue();
        inValue.SetInt(0);
        AgentInterface.ReceiveEvent(&outValue, &inValue, 1, 2);
    }

    public void AddMateria
    (
        InventoryType inventoryType,
        ushort        slot,
        uint          count
    )
    {
        var item = InventoryManager.Instance()->GetInventorySlot(inventoryType, slot);

        TradeMultiple.CurrentSelectMateria = new SelectedMateria
        {
            Type        = inventoryType,
            Slot        = slot,
            ItemID      = item->ItemId,
            SelectCount = count
        };

        var returnValue = new AtkValue();
        var value       = new AtkValue();
        value.SetInt((int)count);
        AgentInterface.ReceiveEvent(&returnValue, &value, 1, 1);
    }

    public bool IsAllMateriaSelected() => GetCurrentSelectedMateriaCount() == 5;

    public uint GetCurrentSelectedMateriaCount()
    {
        uint count = 0;
        foreach (ref readonly var materia in SelectedMateria)
            count += materia.SelectCount;
        return count;
    }

    public bool IsMateriaSelected
    (
        InventoryItem* item
    )
    {
        foreach (ref readonly var materia in SelectedMateria)
            if (materia.Type == item->Container && materia.Slot == item->Slot)
                return true;
        return false;
    }
}
