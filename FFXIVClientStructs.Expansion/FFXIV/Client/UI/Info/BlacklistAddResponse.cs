namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 72)]
public partial struct BlacklistAddResponse
{
    [FieldOffset(0)]
    public ulong AccountID;

    [FieldOffset(8)]
    public ulong ContentID;

    [FieldOffset(16)]
    public ushort EntryKey;

    [FieldOffset(18), FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;

    [FieldOffset(60)]
    public uint ErrorCode;
}
