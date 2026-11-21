using NativePacketDispatcher = FFXIVClientStructs.FFXIV.Client.Network.PacketDispatcher;
using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Network;

/// <summary>扩展客户端的数据包处理器，提供黑名单响应处理和事件场景交互发送接口。</summary>
[GenerateInterop]
[Inherits<NativePacketDispatcher>]
[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe partial struct PacketDispatcher
{
    /// <summary>接收服务器返回的黑名单添加结果并更新本地黑名单状态。</summary>
    /// <param name="handler">处理响应的原生对象地址。</param>
    /// <param name="response">服务器返回的添加结果。</param>
    [MemberFunction("40 57 41 56 48 83 EC ?? 48 8B 0D")]
    public static partial void HandleBlacklistAddResponse
    (
        nint                  handler,
        BlacklistAddResponse* response
    );

    /// <summary>接收服务器返回的黑名单移除结果并更新本地黑名单状态。</summary>
    /// <param name="handler">处理响应的原生对象地址。</param>
    /// <param name="response">服务器返回的移除结果。</param>
    /// <returns>原生响应处理函数的返回值。</returns>
    [MemberFunction("48 89 5C 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 44 0F B6 7A")]
    public static partial nint HandleBlacklistRemoveResponse
    (
        nint                     handler,
        BlacklistRemoveResponse* response
    );

    /// <summary>向服务器发送事件场景的交互结果，并按参数数量选择对应长度的数据包。</summary>
    /// <param name="eventID">事件处理器标识。</param>
    /// <param name="scene">场景编号。</param>
    /// <param name="yieldID">当前交互步骤的编号。</param>
    /// <param name="parameters">交互参数；缓冲区至少容纳两个整数，并按实际包体长度补零。</param>
    /// <param name="parameterCount">有效整数参数数量。</param>
    [MemberFunction
    (
        "4C 8B DC 49 89 73 ?? 55 41 56 41 57 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? F6 05 ?? ?? ?? ?? ?? 41 0F B6 F0"
    )]
    public static partial void SendEventActionPacket(uint eventID, ushort scene, byte yieldID, uint* parameters, byte parameterCount);
}
