using NativeAtkUnitManager = FFXIVClientStructs.FFXIV.Component.GUI.AtkUnitManager;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop(isInherited: true)]
[Inherits<NativeAtkUnitManager>]
[StructLayout(LayoutKind.Explicit, Size = 0x9CA0)]
public unsafe partial struct AtkUnitManager
{
    [MemberFunction("E8 ?? ?? ?? ?? 44 0F B6 8C 24 ?? ?? ?? ?? 48 8D 8F")]
    public partial void SetAddonParentID
    (
        ushort addonID,
        ushort parentID
    );

    public void SetAddonBlocking
    (
        ushort addonID,
        bool   blocking
    ) =>
        SetAddonBlocking
        (
            addonID,
            blocking ?
                (byte)1 :
                (byte)0
        );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 07 44 8B C5")]
    public partial void SetAddonBlocking
    (
        ushort addonID,
        byte   isBlocking
    );

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 9C 24 ?? ?? ?? ?? 4D 8D 6D 20")]
    public partial void SetAddonHideOnCallback
    (
        ushort addonID,
        bool   hideAfterAddonReady,
        bool   addToHudInitList
    );

    [MemberFunction("E8 ?? ?? ?? ?? 4D 85 F6 0F 84 ?? ?? ?? ?? 0F B7 D6")]
    public partial void AddAddonToHudInitAddonIds
    (
        ushort addonID
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 33 F6")]
    public partial uint GetParentAddonsByID
    (
        AtkUnitBase** outAddons,
        ushort        parentID,
        int           maxCount
    );

    [MemberFunction("40 55 41 56 48 83 EC ?? 48 8B 01 4C 8B F1")]
    public partial void HideBlockedAddons();

    [MemberFunction("40 56 41 55 41 57 48 83 EC ?? 48 8B 01")]
    public partial void RestoreBlockedAddons
    (
        AtkUnitBase* excludedAddon
    );

    [MemberFunction("E8 ?? ?? ?? ?? BF ?? ?? ?? ?? 49 FF C7")]
    public partial void CloseAddonByID
    (
        ushort addonID
    );

    [MemberFunction("40 53 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 32 DB")]
    public partial void CloseOrHideAddon
    (
        AtkUnitBase* addon
    );
}
