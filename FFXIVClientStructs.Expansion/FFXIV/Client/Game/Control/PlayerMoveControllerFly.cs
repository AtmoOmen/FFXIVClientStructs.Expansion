using System.Numerics;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[StructLayout(LayoutKind.Explicit, Size = 176)]
public struct PlayerMoveControllerFly
{
    [FieldOffset(0)]
    public float CurrentFlyRotation;

    [FieldOffset(4)]
    public float CurrentFlySpeed;

    [FieldOffset(16)]
    public Vector3 MountPosition;

    [FieldOffset(38)]
    public byte FlyStateFlag1;

    [FieldOffset(44)]
    public byte FlyStateFlag2;

    [FieldOffset(64)]
    private float Unknown40;

    [FieldOffset(68)]
    private float Unknown44;

    [FieldOffset(72)]
    private uint Unknown48;

    [FieldOffset(76)]
    private uint Unknown4C;

    [FieldOffset(80)]
    private uint Unknown50;

    [FieldOffset(88)]
    private float Unknown58;

    [FieldOffset(92)]
    private float Unknown5C;

    [FieldOffset(100)]
    public byte IsTakingOff;

    [FieldOffset(102)]
    public byte IsFlying;

    [FieldOffset(112)]
    public long FlyTransitionStartPosX;

    [FieldOffset(120)]
    public long FlyTransitionStartPosY;

    [FieldOffset(128)]
    public long FlyTransitionStartPosZ;

    [FieldOffset(136)]
    public float FlyBlendDuration0;

    [FieldOffset(140)]
    public float FlyBlendDuration1;

    [FieldOffset(144)]
    public float FlyBlendDuration2;

    [FieldOffset(148)]
    public float FlyTakeoffBlendDuration;

    [FieldOffset(152)]
    public float FlyLandBlendDuration;

    [FieldOffset(156)]
    public int FlyTakeoffPhase;

    [FieldOffset(160)]
    private long FlyUnkQword160;

    [FieldOffset(168)]
    public int FlyLandPhase;

    [FieldOffset(172)]
    public byte FlyFlagA;
}
