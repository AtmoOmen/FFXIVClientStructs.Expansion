namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[Flags]
public enum AtkUnitManagerFlags : byte
{
    None = 0,

    UnitListsChanged = 0x01,

    Unk02    = 0x02,
    UiHidden = 0x04,
    Unk08    = 0x08,
    Unk10    = 0x10,

    InGame = 0x20,
    Unk40  = 0x40,
    Unk80  = 0x80
}
