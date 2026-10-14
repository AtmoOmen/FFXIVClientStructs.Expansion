using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Misc;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 14552)]
[Inherits<UiDataModule>]
public unsafe partial struct UIDataModule
{
    [FieldOffset(4504)]
    public StdVector<BlacklistLocalEntry> BlacklistEntries;

    [MemberFunction("48 8B 81 ?? ?? ?? ?? 4C 8B 81 ?? ?? ?? ?? 49 3B C0 74 ?? 0F B7 08")]
    public partial BlacklistLocalEntry* GetBlacklistEntryByKey
    (
        ushort entryKey
    );

    [MemberFunction("48 8B 81 ?? ?? ?? ?? 4C 8B 81 ?? ?? ?? ?? 49 3B C0 74 ?? 66 83 38")]
    public partial BlacklistLocalEntry* GetBlacklistEntryByContentID
    (
        ulong contentID
    );

    public static unsafe UIDataModule* Instance()
    {
        var uiModulePtr = UIModule.Instance();
        return (nint)uiModulePtr != nint.Zero ?
                   (UIDataModule*)uiModulePtr->GetUiDataModule() :
                   null;
    }
}
