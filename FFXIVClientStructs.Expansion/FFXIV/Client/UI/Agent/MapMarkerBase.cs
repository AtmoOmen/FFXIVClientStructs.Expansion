using NativeMapMarkerBase = FFXIVClientStructs.FFXIV.Client.UI.Agent.MapMarkerBase;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Inherits<NativeMapMarkerBase>]
[StructLayout(LayoutKind.Explicit, Size = 56)]
public partial struct MapMarkerBase
{
    /// <summary>
    ///     标记事件上下文，高 8 位为标记类型，低 24 位为标记索引。
    /// </summary>
    [FieldOffset(24)]
    public uint TooltipContext;
}
