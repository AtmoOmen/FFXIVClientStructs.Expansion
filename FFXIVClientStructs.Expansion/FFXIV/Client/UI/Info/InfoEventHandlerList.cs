namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     事件处理器链表，分发时依次调用各处理器的 <see cref="InfoEventHandlerInterface.OnInfoEvent" />。
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public unsafe struct InfoEventHandlerList
{
    /// <summary>
    ///     链表对象的虚表。
    /// </summary>
    [FieldOffset(0)]
    public void** VirtualTable;

    /// <summary>
    ///     环形链表的哨兵节点，节点的 <c>+0x10</c> 处存放对应的处理器指针。
    /// </summary>
    [FieldOffset(8)]
    public void* Sentinel;

    /// <summary>
    ///     链表中当前的处理器数量。
    /// </summary>
    [FieldOffset(16)]
    public long Count;

    /// <summary>
    ///     是否正在分发事件，分发期间不再改动链表结构。
    /// </summary>
    [FieldOffset(24)]
    public bool IsDispatching;
}
