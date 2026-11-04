namespace FFXIVClientStructs.Expansion.FFXIV.Client.Network.Socket;

/// <summary>
///     游戏 TCP 套接字的原生实现。
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 376)]
public struct SocketImpl
{
    /// <summary>
    ///     Winsock 套接字句柄。
    /// </summary>
    [FieldOffset(8)]
    public nuint Handle;
}
