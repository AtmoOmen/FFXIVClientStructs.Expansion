using NativeBlacklist = FFXIVClientStructs.FFXIV.Client.UI.Info.InfoProxyBlacklist;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Misc;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[Inherits<NativeBlacklist>]
[InfoProxy(InfoProxyId.Blacklist)]
[StructLayout(LayoutKind.Explicit, Size = 6680)]
public unsafe partial struct InfoProxyBlacklist
{
    [FieldOffset(240), FixedSizeArray]
    internal FixedSizeArray200<BlacklistCharacter> CharactersStorage;

    [FieldOffset(6640)]
    public int CharacterCount;

    [BitField<bool>(nameof(IsLoaded),         0)]
    [BitField<bool>(nameof(IsRefreshing),     2)]
    [BitField<bool>(nameof(IsRequestPending), 3)]
    [FieldOffset(6646)]
    public byte OperationFlags;

    public bool IsBusy => IsRefreshing || IsRequestPending;

    public static BlacklistLocalEntry* GetLocalEntry
    (
        BlacklistCharacter* character
    )
    {
        var uiDataModule = UIDataModule.Instance();

        if (character->EntryKey != 0)
            return uiDataModule->GetBlacklistEntryByKey(character->EntryKey);

        return character->IsAccount ?
                   null :
                   uiDataModule->GetBlacklistEntryByContentID(character->ID);
    }

    public ushort GetHomeWorldID
    (
        uint index
    )
    {
        fixed (InfoProxyBlacklist* instance = &this)
        {
            var localEntry = GetLocalEntry(&instance->CharactersStorage[(int)index]);

            if (localEntry == null)
                return 0;

            return localEntry->HomeWorldID;
        }
    }

    [MemberFunction("40 53 41 54 41 55 41 56 41 57 48 83 EC ?? 4C 8B E1")]
    public partial nint UpdateEntryDisplay
    (
        uint                index,
        BlacklistCharacter* entry,
        ushort              homeWorldID
    );

    [MemberFunction("48 83 EC ?? F6 81 ?? ?? ?? ?? ?? 75 ?? 33 C0 83 F8")]
    public partial bool IsPlayerBlocked
    (
        ulong accountID,
        ulong contentID
    );

    [MemberFunction("48 83 EC ?? 48 85 D2 75 ?? B0 ?? 48 83 C4 ?? C3 F6 81 ?? ?? ?? ?? ?? 75 ?? 33 C0 83 F8 ?? 0F 95 C0 48 83 C4 ?? C3 4C 8B CA")]
    public partial bool IsContentBlocked
    (
        ulong contentID
    );

    [MemberFunction("40 53 55 56 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 0F B7 9C 24")]
    public partial bool RequestAddPlayer
    (
        ulong          accountID,
        ulong          contentID,
        CStringPointer name,
        ushort         homeWorldID
    );

    [MemberFunction("40 55 56 57 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? B8")]
    public partial bool RequestRemovePlayer
    (
        ulong          accountID,
        ulong          contentID,
        CStringPointer name
    );
}
