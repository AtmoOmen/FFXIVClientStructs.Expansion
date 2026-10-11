using FFXIVClientStructs.FFXIV.Client.Game.Object;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct FollowRequest
{
    [FieldOffset(0)]
    public GameObjectId TargetGameObjectID;

    [FieldOffset(8)]
    public FollowMode Mode;

    [FieldOffset(9)]
    public byte SilentFlag;

    public enum FollowMode
    {
        Cancel       = 1,
        GroundFollow = 2,
        FlyFollow    = 3,
        Follow       = 4,
        SilentFollow = 5
    }
}
