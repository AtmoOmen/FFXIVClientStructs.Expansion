using NativeWarpEventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.WarpEventHandler;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

[GenerateInterop]
[Inherits<NativeWarpEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 1256)]
public unsafe partial struct WarpEventHandler
{
    /// <summary>
    ///     根据当前玩家状态检查费用、最高职业等级、解锁标记及任务条件，并保留任务重玩状态的原生判断。
    /// </summary>
    /// <remarks>
    ///     只读取继承的条件字段，允许使用从 WarpCondition 表填充的局部实例检查尚未加载的区域线路。
    ///     前三个任务按 CompleteParam 组合判断，第四个任务用于排除正在进行的任务。
    /// </remarks>
    /// <returns>条件失败标志的组合；全部满足时为零。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B D9 E8")]
    public partial WarpConditionFailure GetConditionFailureFlags();

    [Flags]
    public enum WarpConditionFailure : uint
    {
        None                = 0,
        InsufficientLevel   = 1,
        MissingUnlock       = 2,
        IncompleteQuests    = 4,
        ExcludedQuestActive = 8,
        InsufficientGil     = 16
    }
}
