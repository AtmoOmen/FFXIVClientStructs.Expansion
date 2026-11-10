using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeAgentHUD = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentHUD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[GenerateInterop]
[Agent(AgentId.Hud)]
[Inherits<NativeAgentHUD>]
[StructLayout(LayoutKind.Explicit, Size = 20168)]
public unsafe partial struct AgentHUD
{
    /// <summary>
    ///     根据当前选择的时钟组合生成“基本情报”界面的时间文本，并通知界面更新。
    /// </summary>
    /// <param name="stringArray">“基本情报”界面订阅的字符串数组。</param>
    /// <param name="clockDisplayFlags">时钟显示标志，位 1、2、3 分别对应艾欧泽亚时间、本地时间和服务器时间。</param>
    [MemberFunction("40 53 56 41 54 41 56 41 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 45 8B F8")]
    public partial void UpdateDTRTimeText
    (
        StringArrayData* stringArray,
        int              clockDisplayFlags
    );

    [MemberFunction("40 55 41 54 41 55 41 56 41 57 48 8D 6C 24 90 48 81 EC 70 01 00 00")]
    public partial CStringPointer GetStatusTooltipText
    (
        Utf8String* output,
        uint        statusID,
        uint        param
    );
}
