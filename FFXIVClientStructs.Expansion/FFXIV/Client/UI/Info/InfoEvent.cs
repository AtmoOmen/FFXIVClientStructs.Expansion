using FFXIVClientStructs.FFXIV.Client.UI.Info;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     构造后沿事件处理器链表分发给各 <see cref="InfoEventHandlerInterface" />。
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x30)]
public unsafe struct InfoEvent
{
    /// <summary>
    ///     事件对象的虚表。
    /// </summary>
    [FieldOffset(0)]
    public void** VirtualTable;

    /// <summary>
    ///     事件类型，取值 1 至 19。
    /// </summary>
    [FieldOffset(8)]
    public byte Type;

    /// <summary>
    ///     触发该事件的部队信息代理。
    /// </summary>
    [FieldOffset(16)]
    public InfoProxyInterface* InfoProxy;

    /// <summary>
    ///     事件携带的第一个整数参数，其含义随事件类型而定。
    /// </summary>
    [FieldOffset(24)]
    public int IntParam;

    /// <summary>
    ///     事件携带的第二个整数参数，其含义随事件类型而定。
    /// </summary>
    [FieldOffset(32)]
    public long LongParam;

    /// <summary>
    ///     事件处理标记，处理器可在处理过程中置位以避免重复处理。
    /// </summary>
    [FieldOffset(40)]
    public int Flags;
}
