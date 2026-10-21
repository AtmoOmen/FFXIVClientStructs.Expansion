using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentMap = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentMap;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     Client::UI::Agent::AgentMap 的扩展声明。
/// </summary>
[Agent(AgentId.Map)]
[GenerateInterop]
[Inherits<NativeAgentMap>]
[StructLayout(LayoutKind.Explicit, Size = 0x144E8)]
public unsafe partial struct AgentMap
{
    /// <summary>
    ///     按住 Ctrl 时收集的以太之光地图标记索引，共 20 项，0xFFFF 表示列表结束。
    ///     这些标记会在普通地图标记之后追加到绘制列表，从而显示在其他地图标记之上。
    /// </summary>
    [FieldOffset(23248), FixedSizeArray]
    internal FixedSizeArray20<ushort> PriorityMarkerIndicesStorage;
}
