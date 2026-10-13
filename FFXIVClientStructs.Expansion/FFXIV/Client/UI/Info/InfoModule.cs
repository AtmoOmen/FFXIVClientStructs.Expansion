using NativeInfoModule = FFXIVClientStructs.FFXIV.Client.UI.Info.InfoModule;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[Inherits<NativeInfoModule>]
[StructLayout(LayoutKind.Explicit, Size = 7288)]
public unsafe partial struct InfoModule
{
    [MemberFunction("48 83 EC ?? 48 8B 89 ?? ?? ?? ?? F6 81")]
    public partial bool IsPlayerBlocked
    (
        ulong accountID,
        ulong contentID
    );
}
