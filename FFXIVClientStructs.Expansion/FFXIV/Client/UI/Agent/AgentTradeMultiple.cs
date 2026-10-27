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
public unsafe partial struct AgentTradeMultiple : IEnumerable<AgentTradeMultiple.MateriaEntry>
{
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    [FieldOffset(40)]
    public TradeMultiple TradeHandler;

    public MateriaEntry this
    [
        int index
    ] =>
        SelectedMaterias[index];

    [UnscopedRef]
    public Span<MateriaEntry> SelectedMaterias =>
        TradeHandler.SelectedMaterias;

    public IEnumerator<MateriaEntry> GetEnumerator()
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

        TradeHandler.CurrentSelectMateria = new MateriaEntry
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
        foreach (ref readonly var materia in SelectedMaterias)
            count += materia.SelectCount;
        return count;
    }

    public bool IsMateriaSelected
    (
        InventoryItem* item
    )
    {
        foreach (ref readonly var materia in SelectedMaterias)
            if (materia.Type == item->Container && materia.Slot == item->Slot)
                return true;
        return false;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 136)]
    public unsafe partial struct TradeMultiple
    {
        [FieldOffset(8)]
        public nint UnkInstane;

        [FieldOffset(32)]
        public int MaxCount;

        [FieldOffset(40), FixedSizeArray]
        internal FixedSizeArray6<MateriaEntry> SelectedMateriasStorage;

        public MateriaEntry CurrentSelectMateria
        {
            get => SelectedMaterias[5];
            set => SelectedMaterias[5] = value;
        }

        public void AddMateria
        (
            InventoryType inventoryType,
            ushort        slot
        ) => AddMateriaNative(slot, inventoryType);

        [VirtualFunction(0)]
        public partial void AddMateriaNative
        (
            uint          slot,
            InventoryType inventoryType
        );
    }
    
    [StructLayout(LayoutKind.Explicit, Size = 0x10)]
    public struct MateriaEntry
    {
        [FieldOffset(0)]
        public InventoryType Type;

        [FieldOffset(4)]
        public ushort Slot;

        [FieldOffset(8)]
        public uint ItemID;

        [FieldOffset(12)]
        public uint SelectCount;
    }
}
