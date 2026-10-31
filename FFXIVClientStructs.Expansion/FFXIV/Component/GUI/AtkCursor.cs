using NativeAtkCursor = FFXIVClientStructs.FFXIV.Component.GUI.AtkCursor;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<NativeAtkCursor>]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe partial struct AtkCursor
{
    [FieldOffset(27)]
    public bool IsTypeLocked;
}
