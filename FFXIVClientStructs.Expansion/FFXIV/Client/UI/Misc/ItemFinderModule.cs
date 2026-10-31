using NativeItemFinderModule = FFXIVClientStructs.FFXIV.Client.UI.Misc.ItemFinderModule;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Misc;

/// <summary>
///     保存当前角色投影台及扩展收藏柜数据的原生物品检索缓存。
/// </summary>
[GenerateInterop]
[Inherits<NativeItemFinderModule>]
[StructLayout(LayoutKind.Explicit, Size = 6672)]
public unsafe partial struct ItemFinderModule
{
    /// <summary>
    ///     扩展收藏柜是否已在本次会话同步；该状态不会保存至缓存文件。
    /// </summary>
    [FieldOffset(173)]
    public bool IsCabinetCached;

    /// <summary>
    ///     是否需要根据当前角色的功能解锁状态清理无效缓存。
    /// </summary>
    [FieldOffset(174)]
    public bool IsCacheResetPending;

    /// <summary>
    ///     将已加载的实时投影台物品及套装缺失位图同步至原生缓存，在数据改变时标记文件待保存。
    /// </summary>
    [MemberFunction("48 89 4C 24 ?? 53 41 57 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 4C 8B F8 48 85 C0 0F 84")]
    public partial void SyncGlamourDresserCache();

    /// <summary>
    ///     将已加载的扩展收藏柜位图同步至原生缓存，在数据改变时标记文件待保存。
    /// </summary>
    [MemberFunction("40 55 48 83 EC ?? 48 8B E9 48 8D 0D")]
    public partial void SyncCabinetCache();
}
