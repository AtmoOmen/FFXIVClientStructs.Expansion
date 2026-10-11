using System.Collections;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.Native;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 136)]
public unsafe partial struct TradeMultiple
{

    [FieldOffset(8)]
    public nint UnkInstane;

    [FieldOffset(32)]
    public int MaxCount;

    [FieldOffset(40)]
    public fixed byte SelectedMateriaData[96];

    public Span<SelectedMateria> SelectedMateria
    {
        get
        {
            fixed (void* ptr = SelectedMateriaData) return new Span<SelectedMateria>(ptr, 6);
        }
    }

    public SelectedMateria CurrentSelectMateria
    {
        get => SelectedMateria[5];
        set => SelectedMateria[5] = value;
    }

    public void AddMateria(InventoryType inventoryType, ushort slot) => AddMateriaNative(slot, inventoryType);

    [VirtualFunction(0)]
    public partial void AddMateriaNative(uint slot, InventoryType inventoryType);
}
