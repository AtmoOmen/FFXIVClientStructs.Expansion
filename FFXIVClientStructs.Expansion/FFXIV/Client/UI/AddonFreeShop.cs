using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x1000)]
public unsafe partial struct AddonFreeShop
{
    [FieldOffset(0)]
    public AtkUnitBase AtkUnitBase;

    [FieldOffset(0x238)]
    public AtkComponentList* ItemList;

    [FieldOffset(0x240)]
    public AtkComponentCheckBox* ShowNewItemsOnlyCheckbox;

    [FieldOffset(0x248)]
    public AtkComponentDropDownList* ClassJobDropdown;

    [FieldOffset(0x250)]
    public AtkComponentButton* CurrentClassJobButton;

    [FieldOffset(0x258)]
    public Utf8String NoItemsText;

    [FieldOffset(0x2C0), FixedSizeArray]
    internal FixedSizeArray61<Item> ItemsStorage;

    [FieldOffset(0xC48), FixedSizeArray]
    internal FixedSizeArray61<Pointer<Item>> VisibleItemsStorage;

    [FieldOffset(0xE30)]
    public uint ItemCount;

    [FieldOffset(0xE34)]
    public uint NewItemCount;

    [FieldOffset(0xE38)]
    public bool ShowNewItemsOnly;

    [FieldOffset(0xE40), FixedSizeArray]
    internal FixedSizeArray35<Pointer<byte>> ClassJobNamesStorage;

    [FieldOffset(0xF58), FixedSizeArray]
    internal FixedSizeArray35<uint> ClassJobIDsStorage;

    [FieldOffset(0xFE4)]
    public uint SelectedClassJobID;

    [FieldOffset(0xFE8)]
    public uint ClassJobCount;

    [FieldOffset(0xFF0)]
    public ulong ClassJobMask;

    [FieldOffset(0xFF8)]
    public uint CurrentClassJobID;

    [StructLayout(LayoutKind.Explicit, Size = 0x28)]
    public struct Item
    {
        [FieldOffset(0)]
        public uint ItemID;

        [FieldOffset(4)]
        public uint IconID;

        [FieldOffset(0x08)]
        public byte* Name;

        [FieldOffset(0x10)]
        public byte* RequirementText;

        [FieldOffset(0x18)]
        public int Index;

        [FieldOffset(0x1C)]
        public uint ClassJobCategoryID;

        [FieldOffset(0x20)]
        public bool IsOwned;

        [FieldOffset(0x21)]
        public bool IsUnavailable;

        [FieldOffset(0x23)]
        public bool IsNew;
    }
}
