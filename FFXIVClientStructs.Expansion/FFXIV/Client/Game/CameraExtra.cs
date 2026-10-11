namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game;

[StructLayout(LayoutKind.Explicit, Size = 0x2C0)]
public struct CameraExtra
{
    [FieldOffset(320)]
    public float DirH;

    [FieldOffset(324)]
    public float DirV;

    [FieldOffset(328)]
    public float InputDeltaHAdjusted;

    [FieldOffset(316 + 0x10)]
    public float InputDeltaVAdjusted;

    [FieldOffset(336)]
    public float InputDeltaH;

    [FieldOffset(340)]
    public float InputDeltaV;

    [FieldOffset(344)]
    public float DirVMin;

    [FieldOffset(348)]
    public float DirVMax;
}
