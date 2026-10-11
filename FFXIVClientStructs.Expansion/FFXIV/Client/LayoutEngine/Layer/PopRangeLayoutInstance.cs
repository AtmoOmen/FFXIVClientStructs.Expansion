using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine.Layer;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.LayoutEngine.Layer;

[StructLayout(LayoutKind.Explicit, Size = 160)]
public unsafe struct PopRangeLayoutInstance
{
    [FieldOffset(0)]
    public TriggerBoxLayoutInstance Base;

    [FieldOffset(128)]
    public Vector3* AddPos;
}
