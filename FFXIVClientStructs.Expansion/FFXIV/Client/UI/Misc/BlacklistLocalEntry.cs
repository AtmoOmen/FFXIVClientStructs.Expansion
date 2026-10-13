using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Misc;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 232)]
public partial struct BlacklistLocalEntry
{
    [FieldOffset(0)]
    public ushort EntryKey;

    [FieldOffset(8)]
    public ulong ContentID;

    [BitField<bool>(nameof(IsAccount), 0)]
    [FieldOffset(16)]
    public byte IDType;

    [FieldOffset(18)]
    public ushort HomeWorldID;

    [FieldOffset(24)]
    public Utf8String Name;

    [FieldOffset(128)]
    public Utf8String Remark;
}
