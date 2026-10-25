using System.Numerics;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Network;
using FFXIVClientStructs.FFXIV.Client.Graphics.Scene;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine.Layer;
using FFXIVClientStructs.FFXIV.Common.Component.BGCollision;
using FFXIVClientStructs.Interop;
using NativeWarpInfo = FFXIVClientStructs.FFXIV.Client.Game.UI.WarpInfo;
using WarpType = FFXIVClientStructs.FFXIV.Client.Game.UI.WarpType;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.UI;

[GenerateInterop]
[Inherits<NativeWarpInfo>]
[StructLayout(LayoutKind.Explicit, Size = 0xE8)]
public unsafe partial struct WarpInfo
{
    /// <summary>
    ///     当前所处的 warp 流程阶段。
    /// </summary>
    [FieldOffset(0)]
    public WarpState State;

    /// <summary>
    ///     自本次 warp 流程进入当前阶段后累计的秒数。
    /// </summary>
    /// <remarks>
    ///     每帧累加 <c>FrameDeltaTime * GameSpeedMultiplier</c>，阶段切换时由 <see cref="SetState" /> 或相关流程清零。
    /// </remarks>
    [FieldOffset(4)]
    public float ElapsedTime;

    /// <summary>
    ///     区域传送前保存的相机朝向弧度。
    /// </summary>
    /// <remarks>
    ///     由 <c>HandlePrepareZoningPacket</c> 在封包标志指示需要保持朝向时，从本地玩家控制器的相机角度复制而来，warp 结束后再写回。
    /// </remarks>
    [FieldOffset(8)]
    public float CameraRotation;

    /// <summary>
    ///     本次 warp 的类型。
    /// </summary>
    /// <remarks>
    ///     为 <see cref="WarpType.None" /> 时表示当前没有进行中的 warp，即 <see cref="IsWarping" /> 的判定依据。
    /// </remarks>
    [FieldOffset(12)]
    public WarpType WarpType;

    /// <summary>
    ///     当前触发 warp 的 <c>ExitRange</c> 布局实例。
    /// </summary>
    [FieldOffset(16)]
    public ExitRangeLayoutInstance* ExitRange;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(24)]
    public int Unk18;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(28)]
    public int Unk1C;

    /// <summary>
    ///     最近一次穿越 <c>ExitRange</c> 边界时本地玩家的坐标。
    /// </summary>
    /// <remarks>
    ///     用于在发送区域切换请求时上报穿线位置。
    /// </remarks>
    [FieldOffset(32)]
    public Vector3 ZoneLineLastHit;

    /// <summary>
    ///     本次 warp 的目标坐标。
    /// </summary>
    /// <remarks>
    ///     进入 <c>PopRange</c> 时写入目标点，传送类 warp 时写入本地玩家当前位置。
    /// </remarks>
    [FieldOffset(48)]
    public Vector3 WarpPos;

    /// <summary>
    ///     是否存在待处理的 warp 中断请求。
    /// </summary>
    /// <remarks>
    ///     由 <c>HandleWarpInterrupted</c> 在流程无法立即取消时置位，随后的 <see cref="Update" /> 依据该标志执行取消，
    ///     并在取消时改用 <see cref="PendingWarpType" /> 作为取消原因。
    /// </remarks>
    [FieldOffset(64)]
    public byte CancelPending;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(65)]
    public byte Unk41;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(66)]
    public byte Unk42;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(67)]
    public byte Unk43;

    /// <summary>
    ///     待处理的中断原因（<see cref="WarpType" /> 的取值）。
    /// </summary>
    /// <remarks>
    ///     与 <see cref="CancelPending" /> 成对写入，取消流程在 <c>WarpType.Translate</c> 时会无条件走取消分支。
    /// </remarks>
    [FieldOffset(68)]
    public WarpType PendingWarpType;

    /// <summary>
    ///     本次 warp 是否为登录进入区域。
    /// </summary>
    /// <remarks>
    ///     来源于 <c>PrepareZoningPacket.Flags</c> 的第 3 位，登录时会在完成 warp 后触发新手指南、回归者对话与区域活动提示等一次性流程。
    /// </remarks>
    [FieldOffset(72)]
    public byte IsLogin;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(73)]
    public byte Unk49;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(74)]
    public ushort Unk4A;

    /// <summary>
    ///     本次 warp 目标 <c>PopRange</c> 在 <c>PlanMap</c> 中的实例 ID。
    /// </summary>
    /// <remarks>
    ///     来源于 <c>ZoneInitPacket.PopRangeID</c>。
    /// </remarks>
    [FieldOffset(76)]
    public uint PopRangeID;

    /// <summary>
    ///     是否允许跳过最短等待时间提前完成 warp。
    /// </summary>
    /// <remarks>
    ///     取消流程与区域初始化完成后置位；置位后不再要求累计时间达到 3 秒即可进入完成判定。
    /// </remarks>
    [FieldOffset(80)]
    public byte AllowEarlyCompletion;

    /// <summary>
    ///     是否需要在 warp 结束后恢复 <see cref="CameraRotation" />。
    /// </summary>
    /// <remarks>
    ///     来源于 <c>PrepareZoningPacket.Flags</c> 的第 2 位，置位时在流程结束前把保存的角度写回控制器。
    /// </remarks>
    [FieldOffset(81)]
    public byte RestoreCameraRotation;

    /// <summary>
    ///     加载画面是否已被隐藏。
    /// </summary>
    /// <remarks>
    ///     用于避免重复下发隐藏指令，隐藏后置零。
    /// </remarks>
    [FieldOffset(82)]
    public byte LoadingScreenHidden;

    /// <summary>
    ///     加载画面是否已经由 warp 流程接管。
    /// </summary>
    /// <remarks>
    ///     置位期间外部 UI 代码不会用默认加载画面覆盖当前画面，流程结束时清零。
    /// </remarks>
    [FieldOffset(83)]
    public byte LoadingScreenActive;

    /// <summary>
    ///     是否已经执行过拉拉菲尔族的区域限制检查。
    /// </summary>
    /// <remarks>
    ///     保证在单次 warp 流程内只做一次安全点与限制判定，流程重置时清零。
    /// </remarks>
    [FieldOffset(84)]
    public byte LalafellCheckDone;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(85)]
    public byte Unk55;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(86)]
    public byte Unk56;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(87)]
    public byte Unk57;

    /// <summary>
    ///     当前 <c>ExitRange</c> 事件的标识。
    /// </summary>
    /// <remarks>
    ///     由 <c>ExitRange</c> 事件结果写入，并在请求穿越边界时随位置一同上报；流程初始化时被置为 <c>-1</c>。
    /// </remarks>
    [FieldOffset(88)]
    public uint ExitRangeEventID;

    /// <summary>
    ///     区域初始化包携带的回放模式标记。
    /// </summary>
    /// <remarks>
    ///     <c>0</c> 为正常游戏，非零表示正在回放已记录的内容，此时不会显示登录后的引导类界面。
    /// </remarks>
    [FieldOffset(92)]
    public byte ReplayMode;

    /// <summary>
    ///     是否把本次完成推迟一帧。
    /// </summary>
    /// <remarks>
    ///     置位时完成流程不清空加载画面，而是把 <see cref="CompletionDelay" /> 设为 1 秒，待计时结束后再完成。
    /// </remarks>
    [FieldOffset(93)]
    public byte DelayCompletion;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(94)]
    public byte Unk5E;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(95)]
    public byte Unk5F;

    /// <summary>
    ///     完成 warp 前的剩余延迟秒数。
    /// </summary>
    /// <remarks>
    ///     大于零时每帧递减，减至零后执行真正的完成逻辑。
    /// </remarks>
    [FieldOffset(96)]
    public float CompletionDelay;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(100)]
    public int Unk64;

    /// <summary>
    ///     当前预取使用的碰撞体。
    /// </summary>
    [FieldOffset(104)]
    public Collider* PrefetchCollider;

    /// <summary>
    ///     当前预取命中的 <c>PrefetchRange</c> 布局实例。
    /// </summary>
    [FieldOffset(112)]
    public PrefetchRangeLayoutInstance* PrefetchRange;

    /// <summary>
    ///     <see cref="PrefetchRange" /> 绑定的实例 ID。
    /// </summary>
    [FieldOffset(120)]
    public uint PrefetchRangeBoundInstanceID;

    /// <summary>
    ///     <see cref="PrefetchRange" /> 的优先级，数值越大越优先。
    /// </summary>
    [FieldOffset(124)]
    public ushort PrefetchRangePriority;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(126)]
    public ushort Unk7E;

    /// <summary>
    ///     由 <see cref="PrefetchRange" /> 推导出的 <c>ExitRange</c> 实例 ID。
    /// </summary>
    [FieldOffset(128)]
    public uint PrefetchExitRangeID;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(132)]
    public int Unk84;

    /// <summary>
    ///     由 <see cref="PrefetchRange" /> 推导出的 <c>ExitRange</c> 布局实例。
    /// </summary>
    [FieldOffset(136)]
    public ExitRangeLayoutInstance* PrefetchExitRange;

    /// <summary>
    ///     <see cref="PrefetchExitRange" /> 的世界坐标。
    /// </summary>
    [FieldOffset(144)]
    public Vector3 PrefetchExitRangePos;

    /// <summary>
    ///     <see cref="PrefetchExitRange" /> 指向的目标区域。
    /// </summary>
    [FieldOffset(160)]
    public ushort PrefetchExitRangeTerritoryType;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(162)]
    public ushort UnkA2;

    /// <summary>
    ///     <see cref="PrefetchExitRange" /> 指向的目标副本实例 ID。
    /// </summary>
    [FieldOffset(164)]
    public uint PrefetchExitRangeDestInstanceID;

    /// <summary>
    ///     预取布局的优先级，数值越大越优先。
    /// </summary>
    /// <remarks>
    ///     <c>0</c> 表示当前没有预取；写入时只有不低于现有值才会生效。
    /// </remarks>
    [FieldOffset(168)]
    public int PrefetchPriority;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(172)]
    public int UnkAC;

    /// <summary>
    ///     预取区域的背景字符串指针。
    /// </summary>
    /// <remarks>
    ///     取自 <c>TerritoryType</c> 行的背景路径列，用于提前加载目标区域布局。
    /// </remarks>
    [FieldOffset(176)]
    public byte* PrefetchTerritoryBg;

    /// <summary>
    ///     预取区域所使用的实例类型。
    /// </summary>
    [FieldOffset(184)]
    public InstanceType PrefetchInstanceType;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(185)]
    public byte UnkB9;

    /// <summary>
    ///     保留字段，未被任何客户端代码访问。
    /// </summary>
    [FieldOffset(186)]
    public ushort UnkBA;

    /// <summary>
    ///     预取区域的目标副本实例 ID。
    /// </summary>
    [FieldOffset(188)]
    public uint PrefetchDestInstanceID;

    /// <summary>
    ///     预取区域的目标区域 ID。
    /// </summary>
    [FieldOffset(192)]
    public uint PrefetchTerritoryTypeID;

    /// <summary>
    ///     是否已经提示过只有拉拉菲尔族可以进入当前区域。
    /// </summary>
    /// <remarks>
    ///     避免同一段停留期间重复弹出限制提示。
    /// </remarks>
    [FieldOffset(196)]
    public byte LalafellNoticeShown;

    /// <summary>
    ///     warp 是否已经完成。
    /// </summary>
    /// <remarks>
    ///     完成流程末段置位，复活类流程开始时清零。
    /// </remarks>
    [FieldOffset(197)]
    public byte WarpCompleted;

    /// <summary>
    ///     是否抑制加载画面上的文字。
    /// </summary>
    /// <remarks>
    ///     来源于 <c>PrepareZoningPacket.Flags</c> 的第 5 位，显示加载画面时原样传入。
    /// </remarks>
    [FieldOffset(198)]
    public byte SuppressLoadingScreenText;

    /// <summary>
    ///     区域行动限制提示的阶段。
    /// </summary>
    /// <remarks>
    ///     <c>1</c> 表示区域初始化已登记待提示，加载画面结束后推进为 <c>2</c>，由 <see cref="Update" /> 消费并清零。
    /// </remarks>
    [FieldOffset(199)]
    public byte RegionRestrictionNoticeState;

    /// <summary>
    ///     warp 期间被从场景中摘下的本地玩家绘制对象。
    /// </summary>
    [FieldOffset(200)]
    public DrawObject* LocalPlayerDrawObject;

    /// <summary>
    ///     warp 期间用于维持角色外观的三把武器绘制对象。
    /// </summary>
    [FieldOffset(208), FixedSizeArray]
    internal FixedSizeArray3<Pointer<Weapon>> WeaponsStorage;

    /// <summary>
    ///     获取全局唯一的 <see cref="WarpInfo" /> 实例。
    /// </summary>
    [StaticAddress("48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 75 ?? 48 8B D7", 3)]
    public static partial WarpInfo* Instance();

    /// <summary>
    ///     初始化结构并分配三把武器绘制对象。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 48 8D 9E")]
    public partial void Initialize();

    /// <summary>
    ///     释放 <see cref="LocalPlayerDrawObject" /> 与 <see cref="WeaponsStorage" />。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 33 F6 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 01 FF 50 ?? 48 8B 8B")]
    public partial void FinalizeWarpInfo();

    /// <summary>
    ///     清理全部流程字段，并把状态恢复到空闲。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 33 FF 48 8B D9 48 89 39 89 79 ?? 48 89 79")]
    public partial void Reset();

    /// <summary>
    ///     切换 warp 流程阶段。
    /// </summary>
    /// <param name="state">目标阶段。</param>
    /// <remarks>
    ///     进入 <see cref="WarpState.WaitingForReady" /> 或 <see cref="WarpState.Idle" /> 时会清零 <see cref="ElapsedTime" />，
    ///     进入 <see cref="WarpState.WaitingForReady" /> 时还会清零 <see cref="LalafellCheckDone" />。
    /// </remarks>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 44 8B C2 8B DA 48 8B F9")]
    public partial void SetState
    (
        WarpState state
    );

    /// <summary>
    ///     每帧驱动 warp 流程。
    /// </summary>
    /// <remarks>
    ///     仅在区域切换状态为已完成时运行，按 <see cref="State" /> 分派各阶段的推进与超时处理。
    /// </remarks>
    [MemberFunction
        ("40 55 57 41 54 41 55 41 56 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 4C 8B 35")]
    public partial void Update();

    /// <summary>
    ///     结束当前 warp 流程并恢复界面。
    /// </summary>
    /// <param name="eventParam">由事件处理器调用时传入的事件参数，否则为 0。</param>
    /// <param name="eventID">由事件框架调用时传入的事件 ID，否则为 0。</param>
    /// <remarks>
    ///     会隐藏加载画面，并在登录进入且非回放时触发新手指南、回归者对话与成就提示，随后发送区域变更完成命令。
    /// </remarks>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 41 8B E8 48 8B F9")]
    public partial void CompleteWarp
    (
        int eventParam,
        int eventID
    );

    /// <summary>
    ///     判断当前是否存在进行中的 warp。
    /// </summary>
    /// <returns>存在时为 <c>true</c>。</returns>
    [MemberFunction("83 79 ?? ?? 0F 95 C0 C3 CC CC CC CC CC CC CC CC F3 0F 10 41")]
    public partial bool IsWarping();

    /// <summary>
    ///     获取当前阶段的累计时长。
    /// </summary>
    /// <returns>累计秒数。</returns>
    [MemberFunction("F3 0F 10 41 ?? C3 CC CC CC CC CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 74 24")]
    public partial float GetElapsedTime();

    /// <summary>
    ///     处理区域切换预告包。
    /// </summary>
    /// <param name="packet">区域切换预告包。</param>
    /// <param name="replayFlag">是否为内容回放。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? F6 42 ?? ?? 45 0F B6 C8")]
    public partial void HandlePrepareZoningPacket
    (
        PrepareZoningPacket* packet,
        byte                 replayFlag
    );

    /// <summary>
    ///     应用区域切换预告包的参数，配置加载画面与流程初始状态。
    /// </summary>
    /// <param name="warpType">本次 warp 的类型。</param>
    /// <param name="flags">封包携带的标志位。</param>
    /// <param name="territoryTypeID">目标区域 ID。</param>
    /// <param name="fadeOutDelay">淡出延迟，<c>0xFF</c> 表示不淡出。</param>
    /// <param name="loadingScreenVfxID">加载画面使用的特效 ID，为 0 时使用默认画面。</param>
    /// <param name="logMessageID">需要写入聊天栏的日志 ID，为 0 时不写入。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 44 89 4C 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B F9 41 8B E9")]
    public partial void PrepareZoning
    (
        WarpType warpType,
        uint     flags,
        uint     territoryTypeID,
        byte     fadeOutDelay,
        ushort   loadingScreenVfxID,
        uint     logMessageID
    );

    /// <summary>
    ///     处理区域初始化包。
    /// </summary>
    /// <param name="entityID">本地玩家的实体 ID。</param>
    /// <param name="packet">区域初始化包。</param>
    /// <param name="replayFlag">是否为内容回放。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 89 54 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 4C 8B 3D")]
    public partial void HandleZoneInitPacket
    (
        uint            entityID,
        ZoneInitPacket* packet,
        byte            replayFlag
    );

    /// <summary>
    ///     处理区域边界事件的状态回执。
    /// </summary>
    /// <param name="result">事件结果，<c>0</c> 表示等待穿越边界，<c>1</c> 表示事件结束，其余值表示直接结束流程。</param>
    /// <param name="eventID">事件 ID，等待穿越边界时记录到 <see cref="ExitRangeEventID" />。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 83 39 ?? 41 8B F8")]
    public partial void HandleWarpStateChanged
    (
        int result,
        int eventID
    );

    /// <summary>
    ///     处理被外部打断的 warp 请求。
    /// </summary>
    /// <param name="warpType">中断来源对应的 warp 类型。</param>
    /// <param name="isForced">是否强制立即处理。</param>
    /// <param name="type">中断类型。</param>
    /// <param name="id">中断附带的标识。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B 35 ?? ?? ?? ?? 41 83 C1")]
    public partial void HandleWarpInterrupted
    (
        uint warpType,
        byte isForced,
        int  type,
        uint id
    );

    /// <summary>
    ///     更新玩家与区域边界的交互，决定是否开始一次 warp。
    /// </summary>
    /// <param name="localPlayer">本地玩家。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B D9 48 8B FA")]
    public partial void UpdateExitRangeInteraction
    (
        BattleChara* localPlayer
    );

    /// <summary>
    ///     查找当前可用的预取区域并推导其目标出口。
    /// </summary>
    /// <param name="exitRange">输出：命中的出口布局实例。</param>
    /// <param name="territoryTypeID">输出：目标区域 ID。</param>
    /// <param name="destInstanceID">输出：目标副本实例 ID。</param>
    /// <param name="isInFront">输出：玩家是否位于该出口前方。</param>
    /// <returns>找到可用出口时为 <c>true</c>。</returns>
    [MemberFunction("4C 8B DC 4D 89 4B ?? 4D 89 43 ?? 49 89 53 ?? 53")]
    public partial bool TryGetExitRangeDestination
    (
        ILayoutInstance** exitRange,
        ushort*           territoryTypeID,
        int*              destInstanceID,
        bool*             isInFront
    );

    /// <summary>
    ///     获取本地玩家当前所处的出口布局实例。
    /// </summary>
    /// <returns>找到时为出口布局实例，否则为 <c>null</c>。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 83 3D ?? ?? ?? ?? ?? 74")]
    public partial ExitRangeLayoutInstance* GetExitRange();

    /// <summary>
    ///     每帧刷新预取区域的检测结果。
    /// </summary>
    [MemberFunction("4C 8B DC 53 48 83 EC ?? 80 3D")]
    public partial void UpdatePrefetch();

    /// <summary>
    ///     设置预取区域。
    /// </summary>
    /// <param name="priority">该预取的优先级，低于当前值时不生效。</param>
    /// <param name="territoryBg">目标区域的背景字符串指针。</param>
    /// <param name="instanceType">目标实例类型。</param>
    /// <param name="destInstanceID">目标副本实例 ID。</param>
    /// <param name="territoryTypeID">目标区域 ID。</param>
    [MemberFunction("48 89 5C 24 ?? 57 41 56 41 57 48 83 EC ?? 83 39")]
    public partial void SetupPrefetch
    (
        int   priority,
        byte* territoryBg,
        byte  instanceType,
        int   destInstanceID,
        int   territoryTypeID
    );

    /// <summary>
    ///     按优先级清除预取。
    /// </summary>
    /// <param name="priority">当前请求清除的优先级，低于已记录的预取优先级时不生效。</param>
    [MemberFunction("40 53 48 83 EC ?? 8B 81 ?? ?? ?? ?? 48 8B D9 3B D0 7C")]
    public partial void ClearPrefetch
    (
        int priority
    );

    /// <summary>
    ///     发起一次区域切换，等待建立区域连接。
    /// </summary>
    /// <returns>成功进入等待状态时为 <c>true</c>。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 3D")]
    public partial bool StartWarp();

    /// <summary>
    ///     以本地玩家当前位置为目标开始一次传送类 warp。
    /// </summary>
    /// <returns>成功记录目标点并进入等待状态时为 <c>true</c>。</returns>
    [MemberFunction("40 53 48 83 EC ?? 80 3D ?? ?? ?? ?? ?? 48 8B D9 48 8B 0D")]
    public partial bool StartWarpToCurrentPosition();

    /// <summary>
    ///     设置加载画面，并记录本次完成所需的延迟。
    /// </summary>
    /// <param name="delay">完成判定前的额外延迟秒数。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 80 79 ?? ?? 8B FA 48 8B D9 74 ?? 48 8B 0D")]
    public partial void SetLoadingScreen
    (
        uint delay
    );

    /// <summary>
    ///     以指定特效显示加载画面。
    /// </summary>
    /// <param name="vfxID">加载画面特效 ID。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 33 F6 0F 29 74 24 ?? 0F 28 F1")]
    public partial void SetLoadingScreenVfx
    (
        ushort vfxID
    );

    /// <summary>
    ///     切换加载画面的隐藏标记，必要时一并隐藏画面。
    /// </summary>
    /// <param name="hidden">目标隐藏状态。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 0F B6 41 ?? 0F B6 DA")]
    public partial void SetLoadingScreenHidden
    (
        byte hidden
    );

    /// <summary>
    ///     隐藏加载画面并清除相关标记。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 33 F6")]
    public partial void HideLoadingScreen();

    /// <summary>
    ///     卸载预取布局并清空预取相关字段。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 33 C0 89 83 ?? ?? ?? ?? 48 89 83")]
    public partial void ResetPrefetch();

    /// <summary>
    ///     清空预取区域与出口相关字段。
    /// </summary>
    [MemberFunction("33 C0 48 89 41 ?? 48 89 41 ?? 89 41 ?? 66 89 41 ?? 89 81")]
    public partial void ResetPrefetchExitRange();

    /// <summary>
    ///     处理复活流程中的角色状态恢复。
    /// </summary>
    /// <param name="character">被复活的角色。</param>
    /// <param name="type">复活类型，<c>1</c> 表示由他人复活。</param>
    /// <param name="isForced">是否强制解除无法战斗状态。</param>
    /// <param name="unknown1">未知参数，非零时写入角色状态。</param>
    /// <param name="unknown2">未知参数，非零时播放对应特效。</param>
    [MemberFunction("48 85 D2 0F 84 ?? ?? ?? ?? 48 89 5C 24 ?? 55 56 41 56 48 83 EC")]
    public partial void HandleRevive
    (
        BattleChara* character,
        int          type,
        byte         isForced,
        ushort       unknown1,
        ushort       unknown2
    );

    /// <summary>
    ///     处理角色的位置修正数据。
    /// </summary>
    /// <param name="entityID">目标角色实体 ID。</param>
    /// <param name="data">位置与朝向数据。</param>
    [MemberFunction("48 8B C4 48 89 58 ?? 48 89 70 ?? 57 41 56")]
    public partial void HandlePositionUpdate
    (
        uint  entityID,
        byte* data
    );

    /// <summary>
    ///     在区域名与当前区域不同的时候显示区域标题。
    /// </summary>
    /// <param name="territoryTypeID">目标区域 ID。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 8B F2 44 8B C2")]
    public partial void ShowLocationTitle
    (
        uint territoryTypeID
    );

    /// <summary>
    ///     检查本地玩家是否处于需要被送回安全点的区域，必要时提示并送回。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? C6 41 ?? ?? 48 8B D9 48 8B 3D")]
    public partial void CheckLalafellSafePoint();

    /// <summary>
    ///     初始化流程字段，随后调用 <see cref="Reset" /> 完成剩余清理。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 33 C0 48 8B D9 48 89 41 ?? 48 89 41 ?? 48 89 41 ?? 48 89 41 ?? 88 41")]
    public partial void InitializeFields();

    /// <summary>
    ///     重置流程并重置声音系统的过渡状态。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? E8 ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 41 B8")]
    public partial void ResetWithSound();

    /// <summary>
    ///     在当前区域行动受限时显示对应提示。
    /// </summary>
    [MemberFunction("48 83 EC ?? 80 3D ?? ?? ?? ?? ?? 75 ?? 48 8B 0D ?? ?? ?? ?? E8")]
    public partial void ShowRegionRestrictionNotice();

    /// <summary>
    ///     在区域切换前准备本地玩家的绘制状态。
    /// </summary>
    /// <param name="localPlayer">本地玩家。</param>
    /// <param name="hideCharacter">隐藏角色的模式。</param>
    /// <param name="vfxID">需要在角色身上播放的特效 ID。</param>
    /// <param name="isLogin">是否为登录进入区域。</param>
    /// <param name="unknown">未知参数，原样传递给移动控制。</param>
    /// <param name="replayFlag">是否为内容回放。</param>
    [MemberFunction("48 85 D2 0F 84 ?? ?? ?? ?? 48 89 5C 24 ?? 56 48 83 EC ?? 48 89 6C 24")]
    public partial void PrepareCharacter
    (
        BattleChara* localPlayer,
        int          hideCharacter,
        ushort       vfxID,
        byte         isLogin,
        byte         unknown,
        byte         replayFlag
    );

    /// <summary>
    ///     判断指定玩家当前是否可以自由行动。
    /// </summary>
    /// <param name="character">待检查的角色。</param>
    /// <returns>可以自由行动时为 <c>true</c>。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8D 8A ?? ?? ?? ?? 48 8B DA E8 ?? ?? ?? ?? 84 C0 75 ?? 48 8D 8B ?? ?? ?? ?? E8 ?? ?? ?? ?? 84 C0 75 ?? 0F B6 83")]
    public partial bool CanMoveFreely
    (
        BattleChara* character
    );

    /// <summary>
    ///     判断加载画面当前是否可见。
    /// </summary>
    /// <returns>加载画面由 warp 流程接管且尚未隐藏时为 <c>true</c>。</returns>
    public bool IsLoadingScreenVisible() => LoadingScreenActive != 0 && LoadingScreenHidden == 0;

    /// <summary>
    ///     判断本次 warp 是否为登录进入区域且非内容回放。
    /// </summary>
    /// <returns>满足登录进入且非回放的条件时为 <c>true</c>。</returns>
    public bool IsLoginWarp() => IsWarping() && IsLogin != 0 && ReplayMode == 0;

    /// <summary>
    ///     判断区域切换流程当前是否尚未结束。
    /// </summary>
    /// <returns>存在 warp 类型或流程阶段不处于空闲时为 <c>true</c>。</returns>
    public bool IsInTransition() => IsWarping() || State != WarpState.Idle;
}

/// <summary>
///     <see cref="WarpInfo.State" /> 的取值。
/// </summary>
public enum WarpState : uint
{
    /// <summary>
    ///     空闲，处理玩家与 <c>ExitRange</c> 的交互。
    /// </summary>
    Idle = 0,

    /// <summary>
    ///     正在等待 <c>ExitRange</c> 事件结束。
    /// </summary>
    WaitingForExitRangeEvent = 1,

    /// <summary>
    ///     正在等待玩家穿过区域边界线。
    /// </summary>
    WaitingForZoneLine = 2,

    /// <summary>
    ///     已发起区域切换请求，等待建立区域连接。
    /// </summary>
    Connecting = 3,

    /// <summary>
    ///     已发出区域切换指令，等待区域初始化数据。
    /// </summary>
    WaitingForZoneInit = 4,

    /// <summary>
    ///     区域数据已就绪，等待各子系统就绪后完成 warp。
    /// </summary>
    WaitingForReady = 5
}
