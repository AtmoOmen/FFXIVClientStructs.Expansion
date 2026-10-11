using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.Game.Object;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Control;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 1424)]
public unsafe partial struct PlayerController
{
    [FieldOffset(16)]
    public PlayerMoveControllerWalk MoveControllerWalk;

    [FieldOffset(336)]
    public PlayerMoveControllerFly MoveControllerFly;

    [FieldOffset(512)]
    public MovementModeType MovementMode;

    [FieldOffset(472)]
    public float CachedWalkSpeed;

    [FieldOffset(476)]
    public float CachedRunSpeed;

    [FieldOffset(484)]
    public float WalkSpeedBlendDuration;

    [FieldOffset(488)]
    public float RunSpeedBlendDuration;

    [FieldOffset(492)]
    public int SpeedTransitionState;

    [FieldOffset(500)]
    public int MovementModeFlags;

    [FieldOffset(510)]
    public byte IsRunning;

    [FieldOffset(511)]
    public byte IsAutoMove;

    [FieldOffset(514)]
    private byte UnknownMoveFlag514;

    [FieldOffset(536)]
    public float WalkBlendCurrent;

    [FieldOffset(540)]
    public float WalkBlendPrevious;

    [FieldOffset(568)]
    public float RunBlendCurrent;

    [FieldOffset(572)]
    public float RunBlendPrevious;

    [FieldOffset(592)]
    private fixed byte MoveSubController592[116];

    [FieldOffset(708)]
    private byte UnknownFlag708;

    [FieldOffset(709)]
    private byte UnknownFlag709;

    [FieldOffset(710)]
    public byte IsMountedOrCompanion;

    [FieldOffset(720)]
    private fixed byte MoveSubController720A[36];

    [FieldOffset(756)]
    public byte IsLocalPlayerCached;

    [FieldOffset(757)]
    public byte IsRunningServerSide;

    [FieldOffset(758)]
    private fixed byte MoveSubController720B[26];

    [FieldOffset(768)]
    public int MovementPathState;

    [FieldOffset(784)]
    public float MoveDirX;

    [FieldOffset(788)]
    public float MoveDirZ;

    [FieldOffset(792)]
    public float PushInitialSpeed;

    [FieldOffset(796)]
    public float AnimMoveSpeedMul;

    [FieldOffset(800)]
    public float BlendEndY;

    [FieldOffset(804)]
    public float VerticalVelocity;

    [FieldOffset(808)]
    public float BurstDirX;

    [FieldOffset(812)]
    public float BurstDirZ;

    [FieldOffset(816)]
    public float AccumulatedTime;

    [FieldOffset(820)]
    public float BurstRemaining;

    [FieldOffset(824)]
    public float MaxSpeed;

    [FieldOffset(828)]
    public float FrameTimeLimit;

    [FieldOffset(832)]
    public float BlendElapsed;

    [FieldOffset(832)]
    public byte MoveMode;

    [FieldOffset(833)]
    public byte AnimCurveIndex;

    [FieldOffset(837)]
    public byte BlendAngleFlag;

    [FieldOffset(838)]
    public byte BlendPosComplete;

    [FieldOffset(839)]
    public byte BlendImmediateSet;

    [FieldOffset(840)]
    public uint TargetEntityID;

    [FieldOffset(844)]
    public uint TargetEntityIDHigh;

    [FieldOffset(848)]
    public bool MovementInputPending;

    [FieldOffset(849)]
    public bool MovementActive;

    [FieldOffset(853)]
    public bool ControlFlagByte5;

    [FieldOffset(854)]
    public bool ForceMovementReset;

    [FieldOffset(855)]
    public byte MovementHopType;

    [FieldOffset(856)]
    public bool PositionUpdateComplete;

    [FieldOffset(859)]
    public bool PositionUpdateFlag2;

    [FieldOffset(861)]
    public byte TurnSpeedFlags;

    [FieldOffset(864)]
    public float GainStatusSpeedParam;

    [FieldOffset(868)]
    public float PendingMoveSpeed;

    [FieldOffset(872)]
    public ushort GainStatusID;

    [FieldOffset(876)]
    private uint Padding876;

    [FieldOffset(880)]
    public void* MovementPathHandle;

    [FieldOffset(888)]
    private fixed byte UnknownPadding888[8];

    [FieldOffset(896)]
    private fixed byte VerticalSubController[8];

    [FieldOffset(904)]
    public void* StatusStructPtr;

    [FieldOffset(912)]
    private fixed byte ActionMoveSubController[8];

    [FieldOffset(920)]
    public void* ActionTypePtr;

    [FieldOffset(936)]
    public long TargetPositionX;

    [FieldOffset(944)]
    public long TargetPositionY;

    [FieldOffset(952)]
    public int TargetPositionZ;

    [FieldOffset(956)]
    public byte HasTargetFlag;

    [FieldOffset(960)]
    public fixed byte TargetExtraData[32];

    [FieldOffset(992)]
    public GameObject* ControlledChara;

    [FieldOffset(1004)]
    public uint ActionExtraParam;

    [FieldOffset(1008)]
    public byte ActionState;

    [FieldOffset(1009)]
    public byte CallbackIndex;

    [FieldOffset(1020)]
    public float DesiredSpeed;

    [FieldOffset(1024)]
    public float CurrentAbsoluteSpeed;

    [FieldOffset(1028)]
    public int MountTransitionState;

    [FieldOffset(1032)]
    public float CommandTimer;

    [FieldOffset(1036)]
    public int GroundClampCooldown;

    [FieldOffset(1040)]
    public byte SubMoveState;

    [FieldOffset(1041)]
    public ushort PositionSyncFlag;

    [FieldOffset(1072)]
    public GameObjectId FollowTargetIDStart;

    [FieldOffset(1080)]
    public byte BehaviorFlags;

    [FieldOffset(1081)]
    public byte SkipPhysicsFlags;

    [FieldOffset(1084)]
    public float CountdownTimer;

    [FieldOffset(1088)]
    public Vector3 SavedGroundPosition;

    [FieldOffset(1104)]
    public FollowRequest FollowRequestData;

    [FieldOffset(1120)]
    private void* FollowBehaviorVtable0;

    [FieldOffset(1128)]
    public byte SavedWalkingState;

    [FieldOffset(1129)]
    public ushort FollowMovementActive;

    [FieldOffset(1136)]
    private void* FollowBehaviorVtable1;

    [FieldOffset(1152)]
    public Vector3 FollowDestination;

    [FieldOffset(1168)]
    public Vector3 FollowLastKnownPosition;

    [FieldOffset(1200)]
    public GameObjectId FollowPrimaryTargetID;

    [FieldOffset(1208)]
    public GameObjectId FollowCurrentTargetID;

    [FieldOffset(1216)]
    public uint FollowWaypointIndex;

    [FieldOffset(1220)]
    public ushort FollowDistanceThreshold;

    [FieldOffset(1221)]
    public bool IsFollowBusy;

    [FieldOffset(1232)]
    private void* FollowBehaviorVtable2;

    [FieldOffset(1240)]
    public GameObjectId FollowSecondaryTargetID;

    [FieldOffset(1248)]
    private void* FollowBehaviorVtable3;

    [FieldOffset(1256)]
    public byte FollowStuckFlag;

    [FieldOffset(1264)]
    private fixed long AreaVfxRingBuffer[6];

    [FieldOffset(1312)]
    public long PathRequestHandle0;

    [FieldOffset(1320)]
    public long PathRequestHandle1;

    [FieldOffset(1328)]
    public uint ClientPathLayoutInstanceID;

    [FieldOffset(1332)]
    private uint Padding1332;

    [FieldOffset(1336)]
    public long MovementPathInstanceHandle;

    [FieldOffset(1344)]
    public long PathRequestHandle4;

    [FieldOffset(1352)]
    public long PathRequestHandle5;

    [FieldOffset(1360)]
    public long PathRequestHandle6;

    [FieldOffset(1368)]
    public long PathRequestHandle7;

    [FieldOffset(1376)]
    public long TimedMoveDispatchHandle;

    [FieldOffset(1392)]
    public long LastCollisionCheckTimestamp;

    [FieldOffset(1400)]
    public float CollisionPathGateFlag;

    [FieldOffset(1400)]
    public float FollowAreaVfxTimer;

    [FieldOffset(1404)]
    public uint FollowPermissionFlag;

    [FieldOffset(1408)]
    public uint ControlStateBitmask;

    public byte MoveState
    {
        get => (byte)(ControlStateBitmask >> 8);
        set => ControlStateBitmask = (ControlStateBitmask & 0xFFFF00FF) | ((uint)value << 8);
    }

    [FieldOffset(1413)]
    public byte ControlMode;

    [FieldOffset(1414)]
    private byte Field7636;

    [FieldOffset(1415)]
    public byte IsWalking;

    [FieldOffset(1416)]
    private fixed byte Field7638[8];

    [StaticAddress("48 8D 0D ?? ?? ?? ?? 0F B6 9A", 3)]
    public static partial PlayerController* Instance();

    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B FA 0F B6 89")]
    private partial void InterruptFollowNative
    (
        FollowRequest* request
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 44 0F B6 7A 08")]
    private partial void StartFollowNative
    (
        FollowRequest* request
    );

    public void InterruptFollow()
    {
        fixed (FollowRequest* request = &FollowRequestData)
            InterruptFollowNative(request);
    }

    public void StartFollow()
    {
        fixed (FollowRequest* request = &FollowRequestData)
            StartFollowNative(request);
    }

    public void DisableTurnSpeedLimit() =>
        TurnSpeedFlags &= 0xF7;

    public enum MovementModeType
    {
        Ground  = 0,
        Fly     = 2,
        Dive    = 3,
        Special = 5
    }

    public enum ActionStateType
    {
        Idle          = 0,
        Casting       = 3,
        HotBarSwitch7 = 7,
        HotBarSwitch8 = 8,
        HotBarSwitch9 = 9
    }
}
