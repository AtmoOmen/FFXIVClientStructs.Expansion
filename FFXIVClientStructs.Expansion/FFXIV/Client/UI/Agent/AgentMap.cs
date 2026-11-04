using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeAgentMap = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentMap;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     Client::UI::Agent::AgentMap 的扩展声明。
/// </summary>
[Agent(AgentId.Map)]
[GenerateInterop]
[Inherits<NativeAgentMap>]
[StructLayout(LayoutKind.Explicit, Size = 83176)]
public unsafe partial struct AgentMap
{
    /// <summary>大区候选列表的起点，由游戏管理其内存。</summary>
    [FieldOffset(40)]
    public RegionSelectionEntry* RegionSelectionBegin;

    /// <summary>大区候选列表的末尾。</summary>
    [FieldOffset(48)]
    public RegionSelectionEntry* RegionSelectionEnd;

    /// <summary>大区候选列表的容量末尾。</summary>
    [FieldOffset(56)]
    public RegionSelectionEntry* RegionSelectionCapacity;

    /// <summary>区域候选列表的起点，由游戏管理其内存。</summary>
    [FieldOffset(88)]
    public TerritorySelectionEntry* TerritorySelectionBegin;

    /// <summary>区域候选列表的末尾。</summary>
    [FieldOffset(96)]
    public TerritorySelectionEntry* TerritorySelectionEnd;

    /// <summary>区域候选列表的容量末尾。</summary>
    [FieldOffset(104)]
    public TerritorySelectionEntry* TerritorySelectionCapacity;

    /// <summary>原生地图切换列表的起点。</summary>
    [FieldOffset(112)]
    public LayerSelectionEntry* LayerSelectionBegin;

    /// <summary>原生地图切换列表的末尾。</summary>
    [FieldOffset(120)]
    public LayerSelectionEntry* LayerSelectionEnd;

    /// <summary>当前选中地图的类型。</summary>
    [FieldOffset(23128)]
    public uint SelectedMapType;

    /// <summary>构建大区候选和界面刷新参数。</summary>
    [MemberFunction("48 89 54 24 ?? 53 55 56 57 41 54 41 55 41 56 41 57 48 83 EC ?? 33 ED")]
    public partial void BuildRegionSelection(AtkValue* values);

    /// <summary>构建指定大区的区域候选，负值表示当前地图的大区。</summary>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B 79")]
    public partial void BuildTerritorySelection(AtkValue* values, int regionPlaceNameID);

    /// <summary>构建当前地图的原生地图切换列表，可不输出界面刷新参数。</summary>
    [MemberFunction("48 89 54 24 ?? 55 53 57")]
    public partial void BuildLayerSelection(AtkValue* values);

    /// <summary>检查地图条件对应的副本与任务解锁要求，返回非零表示满足。</summary>
    [MemberFunction("40 53 48 83 EC ?? 84 C9 74 ?? 0F B6 C9")]
    public static partial byte IsMapConditionMet(byte condition);

    /// <summary>取得游戏管理的地名文本，调用方不持有其所有权；未满足地图条件时返回隐藏地名。</summary>
    [MemberFunction("40 53 48 83 EC ?? 8B D9 E8 ?? ?? ?? ?? 48 85 C0 74 ?? 0F B6 48")]
    public static partial CStringPointer GetPlaceNameText(uint placeNameID);

    /// <summary>使用游戏分配器扩容并追加一项大区候选。</summary>
    [MemberFunction
    (
        "40 53 56 57 41 55 48 83 EC ?? 4C 8B 11 4C 8B CA 48 8B F2 49 BB ?? ?? ?? ?? ?? ?? ?? ?? 4D 2B CA 48 8B F9 48 8B 49 ?? 49 8B C3 49 F7 E9 49 2B CA 4D 8B E8 48 8B DA 41 B8 ?? ?? ?? ?? 48 C1 FB ?? 48 8B C3 48 C1 E8 ?? 48 03 D8 49 8B C3 48 F7 E9 48 C1 FA ?? 48 8B C2 48 C1 E8 ?? 48 03 D0 49 3B D0 0F 84 ?? ?? ?? ?? 48 8B 4F ?? 49 8B C3 49 2B CA 48 89 6C 24 ?? 4C 89 64 24 ?? 4C 89 74 24 ?? 4C 89 7C 24 ?? 4C 8D 7A ?? 48 F7 E9 48 C1 FA ?? 48 8B C2 48 C1 E8 ?? 48 03 D0 41 8B C0 48 8B CA 48 D1 E9 48 2B C1 48 3B D0 77 ?? 4C 8D 04 11 4D 3B C7 4D 0F 42 C7 4B 8D 04 40 33 D2 4C 8D 24 C5 ?? ?? ?? ?? 41 B8 ?? ?? ?? ?? 49 8B CC E8 ?? ?? ?? ?? 4C 8B F0 4C 8D 0D ?? ?? ?? ?? 48 8D 04 5B 49 8D 2C C6 4C 89 4D ?? 41 8B 45 ?? 89 45 ?? 41 8B 45 ?? 89 45 ?? 41 8B 45"
    )]
    public static partial void GrowRegionSelection(void* collection, RegionSelectionEntry* end, RegionSelectionEntry* entry);

    /// <summary>使用游戏分配器扩容并追加一项区域候选。</summary>
    [MemberFunction("48 89 5C 24 ?? 55 56 57 41 55 41 56 48 83 EC ?? 48 8B 01 48 8B DA 48 8B 71")]
    public static partial void GrowTerritorySelection(void* collection, TerritorySelectionEntry* end, TerritorySelectionEntry* entry);

    /// <summary>
    ///     按住 Ctrl 时收集的以太之光地图标记索引，共 20 项，0xFFFF 表示列表结束。
    ///     这些标记会在普通地图标记之后追加到绘制列表，从而显示在其他地图标记之上。
    /// </summary>
    [FieldOffset(23248), FixedSizeArray]
    internal FixedSizeArray20<ushort> PriorityMarkerIndicesStorage;

    [MemberFunction("44 88 4C 24 ?? 44 88 44 24 ?? 53 41 55 41 56")]
    public partial uint BuildMapMarkers
    (
        ExtendArrayData* array,
        bool             omitAetherytes,
        bool             onlyAetherytes
    );

    /// <summary>游戏拥有的大区候选项。</summary>
    [GenerateInterop]
    [VirtualTable("48 8D 1D ?? ?? ?? ?? 0F B7 45 ?? 0F B6 4D", 3)]
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public partial struct RegionSelectionEntry
    {
        /// <summary>用于打开该大区的地图行号。</summary>
        [FieldOffset(8)]
        public uint MapID;

        /// <summary>大区地名行号。</summary>
        [FieldOffset(12)]
        public uint PlaceNameRegionID;

        /// <summary>大区的界面排序优先级。</summary>
        [FieldOffset(16)]
        public int Priority;

        /// <summary>代表地图的类型。</summary>
        [FieldOffset(20)]
        public int MapType;
    }

    /// <summary>游戏拥有的区域候选项。</summary>
    [GenerateInterop]
    [VirtualTable("48 8D 35 ?? ?? ?? ?? 48 8B D8 48 85 C0 0F 84", 3)]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct TerritorySelectionEntry
    {
        /// <summary>区域地名行号。</summary>
        [FieldOffset(8)]
        public uint PlaceNameID;

        /// <summary>用于打开该区域的地图行号。</summary>
        [FieldOffset(12)]
        public uint MapID;

        /// <summary>所属大区的地名行号。</summary>
        [FieldOffset(16)]
        public uint PlaceNameRegionID;

        /// <summary>区域的界面排序优先级。</summary>
        [FieldOffset(20)]
        public int Priority;

        /// <summary>该项是否为上级地图入口。</summary>
        [FieldOffset(24)]
        public bool HasParentMapOverride;
    }

    /// <summary>原生地图切换候选项。</summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct LayerSelectionEntry
    {
        /// <summary>子区域或世界地图的地名行号。</summary>
        [FieldOffset(8)]
        public uint PlaceNameID;

        /// <summary>替换解析后的地图行号。</summary>
        [FieldOffset(12)]
        public uint MapID;
    }
}
