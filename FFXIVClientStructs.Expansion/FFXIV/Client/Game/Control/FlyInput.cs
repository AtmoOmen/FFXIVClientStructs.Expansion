namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[StructLayout(LayoutKind.Explicit, Size = 24)]
public struct FlyInput
{
    [FieldOffset(0)]
    public float Forward;

    [FieldOffset(4)]
    public float Left;

    [FieldOffset(8)]
    public float Up;

    [FieldOffset(12)]
    public float Unknown12;

    [FieldOffset(16)]
    public uint Unknown16;

    [FieldOffset(20)]
    public byte Unknown20;

    [FieldOffset(21)]
    public bool UnknownFlag21;
}
