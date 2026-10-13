using NativeAgentWorldTravel = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentWorldTravel;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.STD;
using CStringPointer = InteropGenerator.Runtime.CStringPointer;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[Agent(AgentId.WorldTravel)]
[GenerateInterop]
[Inherits<NativeAgentWorldTravel>]
[StructLayout(LayoutKind.Explicit, Size = 0x170)]
public unsafe partial struct AgentWorldTravel
{
    [FieldOffset(0x30)]
    public uint YesNo2Id;

    [FieldOffset(0x34)]
    public uint WorldTravelFinderStatusAddonId;
    [FieldOffset(0x38)]
    public uint WorldTravelFinderReadyAddonId;

    [FieldOffset(0x3C)]
    public int WorldCount;
    [FieldOffset(0x40)]
    public World* Worlds;

    [FieldOffset(0x48)]
    public float WorldListTimer;

    [FieldOffset(0x120)]
    public byte IsWorldTraveling;
    [FieldOffset(0x121)]
    public byte IsInWorldTravelQueue;

    [FieldOffset(0x124)]
    public uint QueueStartTime;


    [FieldOffset(0x128)]
    public uint ElapsedQueueTimeSecond;
    [FieldOffset(0x12C)]
    public uint QueuePosition;
    [FieldOffset(0x130)]
    public uint EstimatedWaitTimeMinute;
    [FieldOffset(0x134)]
    public uint LastElapsedQueueTimeSecond;

    [FieldOffset(0x138)]
    public byte BetweenAreas;

    [FieldOffset(0x140)]
    public World* AllWorlds;
    [FieldOffset(0x148)]
    public int AllWorldCount;

    [FieldOffset(0x14C)]
    public ushort DataCenterId;
    [FieldOffset(0x150)]
    public uint SelectedDataCenterIndex;
    [FieldOffset(0x154)]
    public byte HomeWorldDataCenterId;
    [FieldOffset(0x155)]
    public byte CurrentWorldDataCenterId;
    [FieldOffset(0x158)]
    public StdVector<DataCenter> DataCenters;

    public Span<World> WorldsSpan =>
        Worlds != null ?
            new Span<World>(Worlds, WorldCount) :
            [];

    public Span<World> AllWorldsSpan =>
        AllWorlds != null ?
            new Span<World>(AllWorlds, AllWorldCount + 2) :
            [];

    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 56 41 57 48 83 EC ?? 0F B7 42")]
    public partial void OnWorldListPacket
    (
        WorldListPacket* packet
    );

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
        ushort dataCenterId
    );

    [MemberFunction("40 53 48 83 EC ?? 48 8B 49 ?? 0F B7 DA 48 8B 01 FF 90 ?? ?? ?? ?? 66 85 DB")]
    public partial byte GetWorldDataCenter
    (
        ushort worldId
    );

    [MemberFunction("48 8B 81 ?? ?? ?? ?? 48 8B 91 ?? ?? ?? ?? 48 3B C2 74 ?? 48 2B D0")]
    public partial int GetDataCenterCount();

    [StructLayout(LayoutKind.Explicit, Size = 0x0C)]
    public struct World
    {
        [FieldOffset(0x00)]
        public ushort WorldID;
        
        /// <summary>
        /// 1 = LogMessageId is 0 <br/>
        /// 2 = home world <br/>
        /// 4 = current world <br/>
        /// </summary>
        [FieldOffset(0x02)]
        public ushort Flags;
        [FieldOffset(0x04)]
        public byte DataCenterId;
        [FieldOffset(0x08)]
        public uint LogMessageID;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x70)]
    public struct DataCenter
    {
        [FieldOffset(0x00)]
        public ushort DataCenterId;
        [FieldOffset(0x08)]
        public Utf8String Name;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x410)]
    public partial struct WorldListPacket
    {
        [FieldOffset(0x10)]
        [FixedSizeArray]
        internal FixedSizeArray128<WorldListEntry> EntriesStorage;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x08)]
    public struct WorldListEntry
    {
        [FieldOffset(0x00)]
        public uint LogMessageId;
        [FieldOffset(0x04)]
        public ushort WorldId;
        [FieldOffset(0x06)]
        private ushort unk06;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x1C)]
    public struct WorldTravelStatusPacket
    {
        [FieldOffset(0x10)]
        public byte Status;
        [FieldOffset(0x14)]
        public uint QueuePosition;
        [FieldOffset(0x18)]
        public uint EstimatedWaitTimeMinute;
        [FieldOffset(0x14)]
        public uint LogMessageId1;
        [FieldOffset(0x18)]
        public uint LogMessageId2;
    }
}
