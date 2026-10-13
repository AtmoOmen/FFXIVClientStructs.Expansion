using NativePacketDispatcher = FFXIVClientStructs.FFXIV.Client.Network.PacketDispatcher;
using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Network;

[GenerateInterop]
[Inherits<NativePacketDispatcher>]
[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe partial struct PacketDispatcher
{
    [MemberFunction("40 57 41 56 48 83 EC ?? 48 8B 0D")]
    public static partial void HandleBlacklistAddResponse
    (
        nint                  handler,
        BlacklistAddResponse* response
    );

    [MemberFunction("48 89 5C 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 44 0F B6 7A")]
    public static partial nint HandleBlacklistRemoveResponse
    (
        nint                     handler,
        BlacklistRemoveResponse* response
    );
}
