using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[Agent(AgentId.Tryon)]
[GenerateInterop]
[Inherits<FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentTryon>]
[StructLayout(LayoutKind.Explicit, Size = 1760)]
public unsafe partial struct AgentTryon
{
    /// <summary>
    /// 试穿眼镜：寻找首个空试穿槽位，写入槽位类别 13、眼镜表行号与眼镜表图标，然后置位试穿项变更标记。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 33 ED 44 0F B7 F2")]
    public partial void TryOnGlasses
    (
        ushort glassesID
    );

    /// <summary>
    /// 眼镜试穿入口：试穿室隐藏时先打开试穿室，然后写入眼镜项。
    /// </summary>
    [MemberFunction("48 89 74 24 ?? 57 48 83 EC ?? 8B F9 0F B7 F2")]
    public partial void TryOnGlassesEntry
    (
        ushort glassesID
    );

    /// <summary>
    /// 刷新试穿室界面：构造界面参数并发送给试穿室 addon。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 80 B9 ?? ?? ?? ?? ?? 48 8B F9")]
    public partial void RefreshAddon();

    /// <summary>
    /// 清空全部试穿项：逐项重置槽位类别与字段，然后刷新角色视图与试穿室界面。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 C7 81 ?? ?? ?? ?? ?? ?? ?? ?? 33 C0")]
    public partial void ClearTryOnItems();
}
