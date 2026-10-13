using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Application.Network;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x118)]
public unsafe partial struct RaptureChannelManager
{
    [FieldOffset(0x10)]
    public ServiceConsumerConnectionManager* ConnectionManager;
    [FieldOffset(0x18)]
    public StdVector<uint> PendingPacketIds;
    [FieldOffset(0x30)]
    public StdMap<uint, nint> PrimaryEntityMap;
    [FieldOffset(0x50)]
    public nint PrimaryEntity;
    [FieldOffset(0x60)]
    public nint PrimaryEntityFactory;
    [FieldOffset(0x68)]
    public bool OwnsPrimaryEntityFactory;
    [FieldOffset(0x70)]
    public nint TargetEntityFactory;
    [FieldOffset(0x78)]
    public bool OwnsTargetEntityFactory;
    [FieldOffset(0x98)]
    public NetBufferFactory UpBufferFactory;
    [FieldOffset(0xC8)]
    public NetBufferFactory DownBufferFactory;
    [FieldOffset(0xF8)]
    public NetBufferFactory* UpBufferFactoryOverride;
    [FieldOffset(0x100)]
    public NetBufferFactory* DownBufferFactoryOverride;
    [FieldOffset(0x110)]
    public int LifecycleState;

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x30)]
    public partial struct NetBufferFactory
    {
        [FieldOffset(0x10)]
        public StdMap<uint, nint> FreeBuffers;
        [FieldOffset(0x20)]
        public StdMap<uint, nint> InUseBuffers;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x270)]
    public partial struct ServiceConsumerConnectionManager;
}
