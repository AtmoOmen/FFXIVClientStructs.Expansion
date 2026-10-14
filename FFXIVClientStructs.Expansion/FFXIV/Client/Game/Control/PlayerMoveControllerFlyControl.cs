using System.Numerics;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[StructLayout(LayoutKind.Explicit, Size = 320)]
public unsafe struct PlayerMoveControllerFlyControl
{
    [FieldOffset(16)]
    public Vector3 Position;

    [FieldOffset(32)]
    public Vector3 MovementDirection;

    [FieldOffset(48)]
    public PlayerController* Parent;

    [FieldOffset(64)]
    public float Rotation;

    [FieldOffset(68)]
    public float MovedDistance;

    [FieldOffset(72)]
    public float CurrentSpeed;

    [FieldOffset(76)]
    public float TargetSpeed;

    [FieldOffset(80)]
    public float MaxSpeed;

    [FieldOffset(102)]
    public byte ActivationState;

    [FieldOffset(103)]
    public byte SpeedMode;

    [FieldOffset(104)]
    public bool IsCameraOrientationActive;

    [FieldOffset(105)]
    public bool IsWalking;

    [FieldOffset(107)]
    public bool IsDirectionAdjusted;

    [FieldOffset(108)]
    public bool HasCollision;

    [FieldOffset(136)]
    public float RunSpeed;

    [FieldOffset(140)]
    public float WalkSpeed;

    [FieldOffset(144)]
    public float BaseRunSpeed;

    [FieldOffset(148)]
    public float BaseWalkSpeed;

    [FieldOffset(152)]
    public float PendingRotation;

    [FieldOffset(156)]
    public float TargetRotation;

    [FieldOffset(160)]
    public byte RequestedSpeedMode;

    [FieldOffset(161)]
    public byte DirectionReference;

    [FieldOffset(162)]
    public bool IsCameraTiltActive;

    [FieldOffset(163)]
    public bool IsActionStateActive;

    [FieldOffset(164)]
    public bool IsRotationPending;
}
