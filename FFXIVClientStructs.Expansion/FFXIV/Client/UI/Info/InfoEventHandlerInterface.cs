namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     事件处理器接口，注册到事件链表上，用于接收数据变更通知。
/// </summary>
[GenerateInterop(true)]
[StructLayout(LayoutKind.Explicit, Size = 0x8)]
public unsafe partial struct InfoEventHandlerInterface
{
    /// <summary>
    ///     事件回调。
    /// </summary>
    /// <param name="infoEvent">本次事件的数据。</param>
    [VirtualFunction(0)]
    public partial void OnInfoEvent
    (
        InfoEvent* infoEvent
    );
}
