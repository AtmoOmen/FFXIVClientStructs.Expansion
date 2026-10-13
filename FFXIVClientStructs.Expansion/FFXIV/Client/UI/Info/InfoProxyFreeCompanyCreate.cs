using FFXIVClientStructs.FFXIV.Client.UI.Info;
using NativeFreeCompanyCreate = FFXIVClientStructs.FFXIV.Client.UI.Info.InfoProxyFreeCompanyCreate;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[Inherits<NativeFreeCompanyCreate>]
[InfoProxy(InfoProxyId.FreeCompanyCreate)]
[StructLayout(LayoutKind.Explicit, Size = 504)]
public unsafe partial struct InfoProxyFreeCompanyCreate
{
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 49 8B D9 49 8B F0 48 8B EA 48 8B F9 4D 85 C9 0F 84")]
    public partial bool RequestInvitePlayer
    (
        ulong          accountID,
        ulong          contentID,
        CStringPointer name
    );
}
