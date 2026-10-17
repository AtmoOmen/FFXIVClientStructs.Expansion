using FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

[GenerateInterop]
[Inherits<ContentDirector>]
[StructLayout(LayoutKind.Explicit, Size = 13248)]
public unsafe partial struct FrontlineDirector
{
    [FieldOffset(13240)]
    public int KillCount;
}
