using System.Numerics;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 320)]
public unsafe partial struct PlayerMoveControllerWalk
{
    [FieldOffset(16)]
    public Vector3 MovementDirection;

    [FieldOffset(32)]
    public PlayerController* Parent;

    [FieldOffset(48)]
    public float MinSpeedFactor;

    [FieldOffset(52)]
    public float MaxSpeedFactor;

    [FieldOffset(56)]
    public float SpeedMultiplier;

    [FieldOffset(60)]
    public bool IsAutoRunForward;

    [FieldOffset(61)]
    public bool IsAutoRunStrafe;

    [FieldOffset(62)]
    public bool IsCameraRelativeMode;

    [FieldOffset(63)]
    public bool IsCameraOrientationActive;

    [FieldOffset(64)]
    public bool IsJumping;

    [FieldOffset(68)]
    public float CurrentSpeed;

    [FieldOffset(80)]
    public nint SpeedSource;

    [FieldOffset(88)]
    public float BaseMovementSpeed;

    [FieldOffset(136)]
    public bool IsMovementInput;

    [FieldOffset(144)]
    public float MovementDirRelToCharacterFacing;

    [FieldOffset(148)]
    public byte IsForced;

    [FieldOffset(160)]
    public Vector3 MovementDirectionWorld;

    [FieldOffset(176)]
    public float RotationDirection;

    [FieldOffset(180)]
    public bool IsBackwardOrStrafe;

    [FieldOffset(184)]
    private void* InternalVtable1;

    [FieldOffset(192)]
    private void* InternalVtable2;

    [FieldOffset(200)]
    public fixed float SentinelFloat[5];

    [FieldOffset(260)]
    public byte AutoTurnState;

    [FieldOffset(264)]
    private void* InternalVtable3;

    [FieldOffset(272)]
    public uint MovementState;

    [FieldOffset(276)]
    public float MovementLeft;

    [FieldOffset(280)]
    public float MovementForward;

    [FieldOffset(289)]
    public bool IsTurnLockedInCameraMode;

    [FieldOffset(290)]
    public bool ForceCameraOrientation;

    [FieldOffset(291)]
    public bool IsTurnLocked;

    [FieldOffset(292)]
    public bool IsMovementInputLocked;

    [FieldOffset(298)]
    public bool CameraTiltToggle;

    [MemberFunction("E8 ?? ?? ?? ?? 0F 28 F0 E9 ?? ?? ?? ?? 8B 81")]
    public partial float GetTargetSpeed();

    [MemberFunction("40 53 48 83 EC 20 48 8B D9 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 75 78")]
    public partial bool IsAbleToSetMisdirectionState();

    [StaticAddress("48 8D 0D ?? ?? ?? ?? 45 0F 28 D4", 3)]
    public static partial PlayerMoveControllerWalk* Instance();
}
