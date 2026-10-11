namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

[StructLayout(LayoutKind.Explicit, Size = 304)]
public struct GCShopItemSlot
{
    [FieldOffset(4)]
    public uint ItemID;

    [FieldOffset(12)]
    public uint CostGCSeals;

    [FieldOffset(16)]
    public uint IsValid;

    [FieldOffset(20)]
    public uint DisplayIndex;
}
