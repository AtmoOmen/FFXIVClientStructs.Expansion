using FFXIVClientStructs.FFXIV.Client.Game;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 136)]
public unsafe partial struct TradeMultiple
{
    [FieldOffset(8)]
    public nint UnkInstane;

    [FieldOffset(32)]
    public int MaxCount;

    [FieldOffset(40), FixedSizeArray]
    internal FixedSizeArray6<SelectedMateria> SelectedMateriaStorage;

    public SelectedMateria CurrentSelectMateria
    {
        get => SelectedMateria[5];
        set => SelectedMateria[5] = value;
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
