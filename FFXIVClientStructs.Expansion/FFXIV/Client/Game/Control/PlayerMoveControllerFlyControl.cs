namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[StructLayout(LayoutKind.Explicit, Size = 320)]
public unsafe struct PlayerMoveControllerFlyControl
{
    [FieldOffset(32)]
    public PlayerController* Parent;

    [FieldOffset(48)]
    public PlayerController* Controller;

    [FieldOffset(52)]
    private float Unknown52;

    [FieldOffset(56)]
    public float SpeedMultiplier;

    [FieldOffset(63)]
    private bool UnknownFlag63;

    [FieldOffset(80)]
    private void* Unknown80;

    [FieldOffset(88)]
    public float BaseMovementSpeed;

    [FieldOffset(104)]
    private bool UnknownFlag104;

    [FieldOffset(156)]
    private float Unknown156;

    [FieldOffset(162)]
    private bool UnknownFlag162;

    [FieldOffset(163)]
    private bool UnknownFlag163;

    [FieldOffset(164)]
    private bool UnknownFlag164;

    [FieldOffset(292)]
    private bool UnknownFlag292;
}
