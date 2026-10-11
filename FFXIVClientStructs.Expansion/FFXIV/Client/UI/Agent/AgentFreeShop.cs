using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using AtkEventInterface = FFXIVClientStructs.FFXIV.Component.GUI.AtkModuleInterface.AtkEventInterface;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Agent(AgentId.FreeShop)]
[StructLayout(LayoutKind.Explicit, Size = 0x630)]
public unsafe partial struct AgentFreeShop
{
    [FieldOffset(0)]
    public AgentInterface AgentInterface;

    [FieldOffset(0x28), FixedSizeArray]
    internal FixedSizeArray61<Item> ItemsStorage;

    [FieldOffset(0x5E0)]
    public AtkEventInterface ItemCatalogContextEvent;

    [FieldOffset(0x618)]
    public uint ItemCount;

    [FieldOffset(0x620)]
    public ulong ClassJobMask;

    [FieldOffset(0x628)]
    public bool IsInteractionBlocked;

    [FieldOffset(0x629)]
    public bool IsLoadingItems;

    [StructLayout(LayoutKind.Explicit, Size = 0x18)]
    public struct Item
    {
        [FieldOffset(0)]
        public uint ItemID;

        [FieldOffset(4)]
        public uint Quantity;

        [FieldOffset(0x0C)]
        public ushort Patch;

        [FieldOffset(0x14)]
        public bool IsOwned;

        [FieldOffset(0x15)]
        public bool IsUnavailable;

        [FieldOffset(0x16)]
        public bool IsCached;
    }
}
