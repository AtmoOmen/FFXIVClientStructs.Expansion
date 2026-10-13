using CStringPointer = InteropGenerator.Runtime.CStringPointer;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public partial struct BlacklistCharacter
{
    [FieldOffset(0)]
    public CStringPointer Name;

    [FieldOffset(8)]
    public ushort EntryKey;

    [FieldOffset(16)]
    public ulong ID;

    [BitField<bool>(nameof(IsServerNameEmpty), 0)]
    [BitField<bool>(nameof(IsAccount),         1)]
    [FieldOffset(24)]
    public byte Flags;
}
