using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeAgentMJICraftSchedule = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentMJICraftSchedule;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     工房生产计划的服务器排班回包处理入口。
/// </summary>
[Agent(AgentId.MJICraftSchedule)]
[GenerateInterop]
[Inherits<NativeAgentMJICraftSchedule>]
[StructLayout(LayoutKind.Explicit, Size = 56)]
public unsafe partial struct AgentMJICraftSchedule
{
    /// <summary>
    ///     接收当前显示日期的工房排班、材料分配、休息日与干劲数据。
    /// </summary>
    /// <param name="cycleInProgress">当前进行中的生产日。</param>
    /// <param name="durations">各工房六个排班位置的生产时长。</param>
    /// <param name="startingHours">各排班位置的游戏起始小时。</param>
    /// <param name="craftObjectIDs">各排班位置的产品编号。</param>
    /// <param name="currentRestDays">本周期的休息日编号。</param>
    /// <param name="nextRestDays">下周期的休息日编号。</param>
    /// <param name="efficiencies">各工房二十四小时的效率标记。</param>
    /// <param name="groove">当前干劲。</param>
    [MemberFunction("48 89 4C 24 ?? 53 56 57 48 83 EC ?? 48 8B 41")]
    public partial void UpdateScheduleData
    (
        byte  cycleInProgress,
        byte* durations,
        byte* startingHours,
        byte* craftObjectIDs,
        byte* currentRestDays,
        byte* nextRestDays,
        byte* efficiencies,
        uint  groove
    );

    /// <summary>
    ///     处理休息日修改确认框的返回值。
    /// </summary>
    /// <param name="values">首项为零表示确认修改。</param>
    /// <returns>是否已完成事件处理。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 41 ?? 48 8B D9 48 8B CA C7 40 ?? ?? ?? ?? ?? E8 ?? ?? ?? ?? 85 C0 0F 85 ?? ?? ?? ?? 48 8B 4B")]
    public partial bool HandleEventSetRestConfirm(AtkValue* values);

    /// <summary>
    ///     创建休息日变更的原生确认框。
    /// </summary>
    /// <param name="restCycles">十四个生产日的休息日位掩码。</param>
    /// <returns>确认框的界面编号。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 41 ?? 48 8B F9")]
    public partial uint AskSetRestConfirm(uint restCycles);

    /// <summary>
    ///     根据当前排班数据刷新工房生产计划界面。
    /// </summary>
    /// <returns>界面是否刷新成功。</returns>
    [MemberFunction("40 55 57 41 55 41 56 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B 41")]
    public partial bool UpdateMainAddon();
}
