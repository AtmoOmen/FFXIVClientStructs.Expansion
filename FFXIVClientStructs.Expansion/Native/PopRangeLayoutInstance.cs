using System.Numerics;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine.Layer;
using FFXIVClientStructs.FFXIV.Client.Game.Control;

namespace FFXIVClientStructs.Expansion.Native;

[StructLayout(LayoutKind.Explicit, Size = 160)]
public unsafe partial struct PopRangeLayoutInstance
{
    [FieldOffset(0)]
    public TriggerBoxLayoutInstance Base;

    [FieldOffset(128)]
    public Vector3* AddPos;
}
