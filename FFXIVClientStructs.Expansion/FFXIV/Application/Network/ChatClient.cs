namespace FFXIVClientStructs.Expansion.FFXIV.Application.Network;

[GenerateInterop]
[Inherits<ClientBase>]
[StructLayout(LayoutKind.Explicit, Size = 0xA8)]
public unsafe partial struct ChatClient
{
    [FieldOffset(0xA0)]
    public RaptureChannelManager* ChannelManager;
}
