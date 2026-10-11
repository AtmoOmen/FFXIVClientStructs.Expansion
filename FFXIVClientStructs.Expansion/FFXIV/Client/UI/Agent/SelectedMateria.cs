using FFXIVClientStructs.FFXIV.Client.Game;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct SelectedMateria
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
