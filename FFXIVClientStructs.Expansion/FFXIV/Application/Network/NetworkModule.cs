using NativeNetworkModule = FFXIVClientStructs.FFXIV.Application.Network.NetworkModule;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Common;

namespace FFXIVClientStructs.Expansion.FFXIV.Application.Network;

[GenerateInterop]
[Inherits<NativeNetworkModule>]
[StructLayout(LayoutKind.Explicit, Size = 0xC50)]
public unsafe partial struct NetworkModule
{
    [FieldOffset(0xA70)]
    public ZoneClient* ZoneClient;
    
    [FieldOffset(0xA78)]
    public ChatClient* ChatClient;
}
