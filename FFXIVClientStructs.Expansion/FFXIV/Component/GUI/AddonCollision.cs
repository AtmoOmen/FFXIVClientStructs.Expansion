using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public unsafe struct AddonCollision
{
    [FieldOffset(0x00)]
    public AtkUnitBase* UnitBase;
    [FieldOffset(0x08)]
    public AtkCollisionNode* CollisionNode;
}
