using NativeClientBase = FFXIVClientStructs.FFXIV.Application.Network.ClientBase;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Application.Network;

[GenerateInterop(true)]
[Inherits<NativeClientBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x98)]
public partial struct ClientBase
{
    [FieldOffset(0x70)]
    public ushort ReconnectDelayMS;
    
    [FieldOffset(0x72)]
    public ushort IdleSendIntervalMS;

    [FieldOffset(0x78)]
    public StdVector<byte> Ticket;
    
    [FieldOffset(0x90)]
    public int KeepAlive;
    
    [FieldOffset(0x94)]
    public int KeepAliveInterval;
}
