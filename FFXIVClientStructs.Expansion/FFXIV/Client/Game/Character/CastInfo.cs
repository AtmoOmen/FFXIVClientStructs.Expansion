using NativeCastInfo = FFXIVClientStructs.FFXIV.Client.Game.Character.CastInfo;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Character;

[GenerateInterop]
[Inherits<NativeCastInfo>]
[StructLayout(LayoutKind.Explicit, Size = 0x170)]
public unsafe partial struct CastInfo
{
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B F9 0F 29 74 24 ?? 0F B6 49")]
    public static partial uint UpdateTotal
    (
        CastInfo* data,
        uint      spellActionID,
        float     process,
        float     processTotal
    );
}
