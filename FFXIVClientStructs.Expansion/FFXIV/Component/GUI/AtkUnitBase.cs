using NativeAtkUnitBase = FFXIVClientStructs.FFXIV.Component.GUI.AtkUnitBase;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop(isInherited: true)]
[Inherits<NativeAtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x238)]
public unsafe partial struct AtkUnitBase
{
    [FieldOffset(0x8), FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;
    [FieldOffset(0xE0), FixedSizeArray]
    internal FixedSizeArray2<Pointer<AtkResNode>> AdditionalMoveableNodesStorage;
    [FieldOffset(0x108), FixedSizeArray]
    internal FixedSizeArray2<Pointer<AtkResNode>> AdditionalFocusableNodesStorage;

    [BitField<bool>(nameof(OnlyHideWhenFireCallback), 0)]
    [FieldOffset(0x1A0)]
    public byte Flags1A0;
    [BitField<bool>(nameof(HideAfterReady), 1)]
    [FieldOffset(0x1A1)]
    public byte Flags1A1;
    [BitField<bool>(nameof(HasChildAddons), 7)]
    [FieldOffset(0x1A2)]
    public byte Flags1A2;

    [FieldOffset(0x1B8)]
    private byte addonParamUnknown1;
    [FieldOffset(0x1BB)]
    private byte unk1BB;
    [FieldOffset(0x1EF)]
    private bool unk1EF;

    [FieldOffset(0x1FC), FixedSizeArray]
    internal FixedSizeArray5<OperationGuide> OperationGuidesStorage;

    [MemberFunction("66 83 B9 ?? ?? ?? ?? ?? 75 ?? 66 83 B9 ?? ?? ?? ?? ?? 75 ?? F6 81")]
    public partial bool IsStandaloneAddon();
}
