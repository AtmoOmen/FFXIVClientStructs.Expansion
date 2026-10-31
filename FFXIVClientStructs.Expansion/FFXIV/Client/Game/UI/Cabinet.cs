using NativeCabinet = FFXIVClientStructs.FFXIV.Client.Game.UI.Cabinet;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.UI;

/// <summary>
///     收藏柜的基础及扩展库存，两部分各自维护独立的服务器加载状态。
/// </summary>
[GenerateInterop]
[Inherits<NativeCabinet>]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public unsafe partial struct Cabinet
{
    /// <summary>
    ///     扩展收藏柜的加载状态：无效、请求中、已加载或服务器返回不可用。
    /// </summary>
    [FieldOffset(4)]
    public NativeCabinet.CabinetState AdditionalItemsState;

    /// <summary>
    ///     请求基础收藏柜库存，覆盖收藏柜表行号 0 至 1047，不会打开界面。
    /// </summary>
    /// <returns>是否已发送请求。</returns>
    [MemberFunction("48 83 EC ?? C7 01 ?? ?? ?? ?? 45 33 C9")]
    public partial bool RequestBaseItems();

    /// <summary>
    ///     请求扩展收藏柜库存，覆盖收藏柜表行号 1048 至 5047，不会打开界面。
    /// </summary>
    /// <returns>是否已发送请求。</returns>
    [MemberFunction
    (
        "48 83 EC ?? C7 41 ?? ?? ?? ?? ?? 45 33 C9 B9 ?? ?? ?? ?? C7 44 24 ?? ?? ?? ?? ?? 45 33 C0 33 D2 E8 ?? ?? ?? ?? 48 83 C4 ?? C3 CC CC CC CC CC CC 48 89 5C 24 ?? 48 89 6C 24"
    )]
    public partial bool RequestAdditionalItems();
}
