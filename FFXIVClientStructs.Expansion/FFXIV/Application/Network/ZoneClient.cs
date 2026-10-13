using NativeZoneClient = FFXIVClientStructs.FFXIV.Application.Network.ZoneClient;

namespace FFXIVClientStructs.Expansion.FFXIV.Application.Network;

[GenerateInterop]
[Inherits<NativeZoneClient>]
[StructLayout(LayoutKind.Explicit, Size = 0xA0)]
public unsafe partial struct ZoneClient
{
    [FieldOffset(0x98)]
    public RaptureChannelManager* ChannelManager;
}
