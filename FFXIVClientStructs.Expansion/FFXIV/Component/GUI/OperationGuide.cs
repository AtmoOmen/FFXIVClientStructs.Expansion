namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0xC)]
public partial struct OperationGuide
{
    [FieldOffset(0x00)]
    public byte Index;
    [BitField<OperationGuidePoint>(nameof(RelativePoint), 0, 4)]
    [BitField<OperationGuidePoint>(nameof(Point),         4, 4)]
    [FieldOffset(0x01)]
    public byte PositionFlags;
    [FieldOffset(0x02)]
    public short OffsetX;
    [FieldOffset(0x04)]
    public short OffsetY;

    [FieldOffset(0x08)]
    public uint AddonTransientID;

    public partial OperationGuidePoint RelativePoint { readonly get; set; }

    public partial OperationGuidePoint Point { readonly get; set; }
}
