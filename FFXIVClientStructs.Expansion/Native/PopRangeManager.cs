using System.Numerics;
using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine.Layer;
using FFXIVClientStructs.FFXIV.Client.Game.Control;

namespace FFXIVClientStructs.Expansion.Native;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 256)]
public unsafe partial struct PopRangeManager
{
    [FieldOffset(0)]
    public int State;

    [FieldOffset(4)]
    public float Time;
    [FieldOffset(16)]
    public ExitRangeLayoutInstance* ExitLayoutInstance;

    [FieldOffset(32)]
    public Vector3 Position;
    [FieldOffset(48)]
    public Vector3 RecoveredPosition;

    [StaticAddress("83 3D ?? ?? ?? ?? ?? 77 ??", 2)]
    public static partial PopRangeManager* Instance();

    public void PopRange(ILayoutInstance* exit, Vector3? recoveredPosition = null)
    {
        var pop = ((ExitRangeLayoutInstance*)exit)->ReturnInstance;

        if (recoveredPosition == null)
        {
            var player = Control.GetLocalPlayer();
            Position = player == null ? default : player->Position;
            RecoveredPosition = *pop->Base.GetTranslationImpl();
        }
        else
        {
            Position          = new(float.MaxValue);
            RecoveredPosition = recoveredPosition.Value;
        }
        
        ExitLayoutInstance = (ExitRangeLayoutInstance*)exit;
        State              = 2;
    }
}
