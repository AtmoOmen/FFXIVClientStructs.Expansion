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
    public float Turn;

    [FieldOffset(16)]
    public float Rotation;

    [FieldOffset(20)]
    public byte DirectionReference;

    [FieldOffset(21)]
    public bool HasMovementInput;
}
