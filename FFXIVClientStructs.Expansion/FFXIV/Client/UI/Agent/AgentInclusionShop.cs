using FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentInclusionShop = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentInclusionShop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     综合商店的商品数据生成扩展。
/// </summary>
[Agent(AgentId.InclusionShop)]
[GenerateInterop]
[Inherits<NativeAgentInclusionShop>]
[StructLayout(LayoutKind.Explicit, Size = 64)]
public unsafe partial struct AgentInclusionShop
{
    /// <summary>
    ///     交接物品结果的事件回调接口子对象。
    /// </summary>
    [FieldOffset(40)]
    public InfoEventHandlerInterface HandInInfoEventHandler;

    /// <summary>
    ///     兑换请求结果的事件回调接口子对象。
    /// </summary>
    [FieldOffset(48)]
    public InfoEventHandlerInterface TradeInfoEventHandler;

    /// <summary>
    ///     构造代理。
    /// </summary>
    /// <param name="uiModuleInterface">UI 模块接口。</param>
    /// <returns>代理自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 C7 43 ?? ?? ?? ?? ?? 48 89 03 48 8D 05 ?? ?? ?? ?? 48 89 43 ?? 48 8D 05 ?? ?? ?? ?? 48 89 43 ?? 48 8B C3 48 83 C4 ?? 5B C3 CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC CC 48 8D 05 ?? ?? ?? ?? 48 89 01 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? 48 8D 05 ?? ?? ?? ?? 48 89 41 ?? E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC 40 53 48 83 EC ?? 48 8B DA"
    )]
    public partial AgentInclusionShop* Ctor
    (
        UIModuleInterface* uiModuleInterface
    );

    /// <summary>
    ///     根据商品产出、收纳容量及各项材料余额刷新商店界面值。
    /// </summary>
    [MemberFunction
    (
        "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 2B E0 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 41 BD"
    )]
    public partial void RefreshShopValues();
}
