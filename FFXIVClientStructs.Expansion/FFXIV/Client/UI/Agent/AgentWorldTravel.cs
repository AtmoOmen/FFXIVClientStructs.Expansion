using NativeAgentWorldTravel = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentWorldTravel;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;
using CStringPointer = InteropGenerator.Runtime.CStringPointer;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[Agent(AgentId.WorldTravel)]
[GenerateInterop]
[Inherits<NativeAgentWorldTravel>]
[StructLayout(LayoutKind.Explicit, Size = 368)]
public unsafe partial struct AgentWorldTravel
{
    [FieldOffset(48)]
    public uint YesNo2ID;

    [FieldOffset(52)]
    public uint WorldTravelFinderStatusAddonID;
    [FieldOffset(56)]
    public uint WorldTravelFinderReadyAddonID;

    [FieldOffset(60)]
    public int WorldCount;
    [FieldOffset(64)]
    public World* Worlds;

    [FieldOffset(72)]
    public float WorldListTimer;

    /// <summary>
    /// 排队与就绪期间为 1。
    /// </summary>
    [FieldOffset(288)]
    public byte IsWorldTraveling;

    /// <summary>
    /// 仅就绪期间为 1。
    /// </summary>
    [FieldOffset(289)]
    public byte IsReady;

    [FieldOffset(292)]
    public uint QueueStartTime;


    [FieldOffset(296)]
    public uint ElapsedQueueTimeSecond;
    [FieldOffset(300)]
    public uint QueuePosition;
    [FieldOffset(304)]
    public uint EstimatedWaitTimeMinute;
    [FieldOffset(308)]
    public uint LastElapsedQueueTimeSecond;

    [FieldOffset(312)]
    public byte BetweenAreas;

    [FieldOffset(320)]
    public World* AllWorlds;
    [FieldOffset(328)]
    public int AllWorldCount;

    [FieldOffset(332)]
    public ushort DataCenterID;
    [FieldOffset(336)]
    public uint SelectedDataCenterIndex;
    [FieldOffset(340)]
    public byte HomeWorldDataCenterID;
    [FieldOffset(341)]
    public byte CurrentWorldDataCenterID;
    [FieldOffset(344)]
    public StdVector<DataCenter> DataCenters;

    public Span<World> WorldsSpan =>
        Worlds != null ?
            new Span<World>(Worlds, WorldCount) :
            [];

    public Span<World> AllWorldsSpan =>
        AllWorlds != null ?
            new Span<World>(AllWorlds, AllWorldCount + 2) :
            [];
    
    [MemberFunction("E8 ?? ?? ?? ?? 40 0A F8 B9 ?? ?? ?? ??")]
    public partial bool UpdateAddons
    (
        NumberArrayData*  numberArray,
        StringArrayData*  stringArray,
        bool              a4
    );

    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 56 41 57 48 83 EC ?? 0F B7 42")]
    public partial void OnWorldListPacket
    (
        WorldListPacket* packet
    );

    /// <summary>
    /// 处理 UIModulePacketType 25 的世界旅行状态包。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B D9 48 8B FA 0F B6 4A ?? 83 E9")]
    public partial void OnWorldTravelStatusPacket
    (
        WorldTravelStatusPacket* packet
    );

    [MemberFunction("48 89 6C 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 33 ED")]
    public partial void StartWorldTravel();

    [MemberFunction("E8 ?? ?? ?? ?? 4C 8B C0 BA ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 4C 8B C0 C6 44 24 ?? ?? 45 33 C9 33 D2 48 8B CD")]
    public partial CStringPointer GetDestinationWorldName();

    [MemberFunction("E9 ?? ?? ?? ?? BA ?? ?? ?? ?? 48 8B CB E8 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B C8 48 83 C4")]
    public partial void UpdateFinderStatusAddon();

    [MemberFunction("E8 ?? ?? ?? ?? B9 ?? ?? ?? ?? 48 89 5C 24 ?? E8 ?? ?? ?? ?? 0F 57 C0")]
    public partial void BuildWorldList
    (
        World*           worlds,
        int              worldCount,
        WorldListPacket* packet
    );

    [MemberFunction("E8 ?? ?? ?? ?? 83 F8 ?? 0F 82 ?? ?? ?? ?? 8B C0")]
    public partial int AddWorldEntry
    (
        World*          worlds,
        int*            index,
        ushort          homeWorld,
        ushort          currentWorld,
        WorldListEntry* entry
    );

    [MemberFunction("4C 8B DC 55 53 41 57 48 8D 6C 24")]
    public partial void UpdateWorldTravelAddon();

    [MemberFunction("E8 ?? ?? ?? ?? E9 ?? ?? ?? ?? 48 8B 4E ?? 48 8B 01 FF 50 ?? 48 8B C8 BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 8B 4E ?? 48 8B 01")]
    public partial void OpenFinderStatusAddon();

    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 53 ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 43 ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC 4C 8B DC 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 83 79"
    )]
    public partial void CloseFinderStatusAddon();

    [MemberFunction("4C 8B DC 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 83 79 ?? ?? 48 8B F1 0F 85 ?? ?? ?? ?? F3 0F 10 05")]
    public partial void OpenFinderReadyAddon();

    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 53 ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 43 ?? ?? ?? ?? ?? 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 85 D2"
    )]
    public partial void CloseFinderReadyAddon();

    [MemberFunction("40 53 48 83 EC ?? 85 D2 44 89 81")]
    public partial void SetQueueInfo
    (
        int position,
        int estimatedWaitTimeMinute
    );

    [MemberFunction("40 57 44 8B 81")]
    public partial void FilterWorldList
    (
        ushort dataCenterID
    );

    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 0F B7 DA 48 8B 01 FF 90 ?? ?? ?? ?? 66 85 DB")]
    public partial byte GetWorldDataCenter
    (
        ushort worldID
    );

    [MemberFunction("48 8B 81 ?? ?? ?? ?? 48 8B 91 ?? ?? ?? ?? 48 3B C2 74 ?? 48 2B D0")]
    public partial int GetDataCenterCount();

    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct World
    {
        [FieldOffset(0)]
        public ushort WorldID;

        [FieldOffset(2)]
        public ushort Flags;
        
        [FieldOffset(4)]
        public byte DataCenterID;
        
        [FieldOffset(8)]
        public uint LogMessageID;
    }

    [StructLayout(LayoutKind.Explicit, Size = 112)]
    public struct DataCenter
    {
        [FieldOffset(0)]
        public ushort DataCenterID;
        [FieldOffset(8)]
        public Utf8String Name;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 1040)]
    public partial struct WorldListPacket
    {
        [FieldOffset(16)]
        [FixedSizeArray]
        internal FixedSizeArray128<WorldListEntry> EntriesStorage;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct WorldListEntry
    {
        [FieldOffset(0)]
        public uint LogMessageID;
        
        [FieldOffset(4)]
        public ushort WorldID;
        
        [FieldOffset(6)]
        private ushort unk06;
    }

    /// <summary>
    /// UIModulePacketType 25 携带的世界旅行状态包。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct WorldTravelStatusPacket
    {
        [FieldOffset(16)]
        public WorldTravelStatus Status;

        /// <summary>
        /// Status 为 InQueue 时的队列位置，为 0 时按 1 处理。
        /// </summary>
        [FieldOffset(20)]
        public uint QueuePosition;

        /// <summary>
        /// Status 为 InQueue 时的预计等待分钟数。
        /// </summary>
        [FieldOffset(24)]
        public uint EstimatedWaitTimeMinute;

        /// <summary>
        /// Status 为 Finished 时输出的第一条日志消息。
        /// </summary>
        [FieldOffset(20)]
        public uint LogMessageID1;

        /// <summary>
        /// Status 为 Finished 时输出的第二条日志消息。
        /// </summary>
        [FieldOffset(24)]
        public uint LogMessageID2;
    }

    public enum WorldTravelStatus : byte
    {
        /// <summary>
        /// 排队中，随包更新队列位置与预计等待时间。
        /// </summary>
        InQueue = 1,

        /// <summary>
        /// 排队完成，关闭状态窗口并打开 WorldTravelFinderReady 窗口。
        /// </summary>
        Ready = 2,

        /// <summary>
        /// 流程结束，清空排队状态、关闭两个窗口并按包内的日志消息输出提示。
        /// </summary>
        Finished = 3
    }
}
