namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 64)]
public partial struct BlacklistRemoveResponse
{
    [FieldOffset(0)]
    public ulong ID;

    [FieldOffset(8)]
    public ushort EntryKey;

    [BitField<bool>(nameof(IsAccount), 0)]
    [FieldOffset(10)]
    public byte IDType;

    [FieldOffset(11), FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;

    [FieldOffset(52)]
    public uint ErrorCode;
}
