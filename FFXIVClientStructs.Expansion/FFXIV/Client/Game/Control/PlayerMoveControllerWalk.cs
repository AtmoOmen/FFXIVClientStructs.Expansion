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

    [FieldOffset(40)]
    private int Unknown40;

    [FieldOffset(48)]
    private float Unknown48;

    [FieldOffset(52)]
    private float Unknown52;

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
    private void* Unknown80;

    [FieldOffset(88)]
    public float BaseMovementSpeed;

    [FieldOffset(96)]
    private nint Unknown96;

    [FieldOffset(104)]
    private nint Unknown104;

    [FieldOffset(112)]
    private nint Unknown112;

    [FieldOffset(120)]
    private nint Unknown120;

    [FieldOffset(128)]
    private nint Unknown128;

    [FieldOffset(132)]
    private bool UnknownFlag132;

    [FieldOffset(136)]
    public bool IsMovementInput;

    [FieldOffset(140)]
    private float Unknown140;

    [FieldOffset(144)]
    public float MovementDirRelToCharacterFacing;

    [FieldOffset(148)]
    public byte IsForced;

    [FieldOffset(160)]
    public Vector3 MovementDirectionWorld;

    [FieldOffset(176)]
    public float RotationDirection;

    [FieldOffset(180)]
    private bool UnknownFlag180;

    [FieldOffset(184)]
    private void* InternalVtable1;

    [FieldOffset(192)]
    private void* InternalVtable2;

    [FieldOffset(200)]
    public fixed float SentinelFloat[5];

    [FieldOffset(220)]
    private int Unknown220;

    [FieldOffset(224)]
    private nint Unknown224;

    [FieldOffset(232)]
    private nint Unknown232;

    [FieldOffset(240)]
    private nint Unknown240;

    [FieldOffset(244)]
    private ushort Unknown244;

    [FieldOffset(246)]
    private bool UnknownFlag246;

    [FieldOffset(247)]
    private ushort Unknown247;

    [FieldOffset(248)]
    private bool UnknownFlag248;

    [FieldOffset(260)]
    private byte Unknown260;

    [FieldOffset(264)]
    private void* InternalVtable3;

    [FieldOffset(272)]
    public uint MovementState;

    [FieldOffset(276)]
    public float MovementLeft;

    [FieldOffset(280)]
    public float MovementForward;

    [FieldOffset(288)]
    private bool UnknownFlag288;

    [FieldOffset(289)]
    private bool UnknownFlag289;

    [FieldOffset(290)]
    private bool UnknownFlag290;

    [FieldOffset(291)]
    private bool UnknownFlag291;

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
