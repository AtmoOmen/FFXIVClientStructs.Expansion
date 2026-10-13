namespace FFXIVClientStructs.Expansion.FFXIV.Client.System;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public struct BlacklistChange
{
    [FieldOffset(0)]
    public uint Type;

    [FieldOffset(8)]
    public bool IsAccount;

    [FieldOffset(16)]
    public ulong ID;
}
