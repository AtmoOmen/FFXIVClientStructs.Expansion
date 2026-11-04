using NativeNetworkModule = FFXIVClientStructs.FFXIV.Application.Network.NetworkModule;

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

    /// <summary>
    ///     客户端收到最近一次区域服务器心跳回包后计算的往返耗时，单位为毫秒。
    /// </summary>
    [FieldOffset(3020)]
    public uint RoundTripTime;

    /// <summary>
    ///     客户端原生心跳包携带的 16 字节状态数据。
    /// </summary>
    [FieldOffset(3132)]
    public Guid HeartbeatData;
}
