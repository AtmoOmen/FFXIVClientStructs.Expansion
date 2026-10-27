using FFXIVClientStructs.FFXIV.Component.GUI;
using NativeAtkComponentNumericInput = FFXIVClientStructs.FFXIV.Component.GUI.AtkComponentNumericInput;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

/// <summary>
///     数字输入组件的原生交互事件处理。
/// </summary>
[GenerateInterop]
[Inherits<NativeAtkComponentNumericInput>]
[StructLayout(LayoutKind.Explicit, Size = 824)]
public unsafe partial struct AtkComponentNumericInput
{
    /// <summary>
    ///     正在以文本方式编辑数字，提交或取消后清除。
    /// </summary>
    [FieldOffset(518)]
    public bool IsEditingDigits;

    /// <summary>
    ///     本次数字编辑以取消结束，用于在提交路径上避免重复触发焦点回调。
    /// </summary>
    [FieldOffset(519)]
    public bool IsEditingCancelled;

    /// <summary>
    ///     加减按钮的操作引导待重新定位，下一帧 <see cref="Update" /> 在输入管理器进入数字编辑状态后处理并清除。
    /// </summary>
    [FieldOffset(529)]
    public bool NeedsOperationGuideRefresh;

    /// <summary>
    ///     处理键盘输入、鼠标滚轮、加减按钮和编辑焦点事件。
    /// </summary>
    /// <param name="eventType">输入事件类型。</param>
    /// <param name="eventParam">输入事件参数。</param>
    /// <param name="atkEvent">输入事件。</param>
    /// <param name="eventData">输入事件数据。</param>
    [MemberFunction("4C 8B DC 55 53 57 41 57 49 8D AB ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B 9D")]
    public partial void ReceiveNumericEvent
    (
        AtkEventType  eventType,
        int           eventParam,
        AtkEvent*     atkEvent,
        AtkEventData* eventData
    );

    /// <summary>
    ///     更新允许输入的最大值，校正当前数量并更新显示位数。
    /// </summary>
    /// <param name="maximum">允许输入的最大值。</param>
    /// <param name="triggerCallback">校正数量时是否通知所属界面。</param>
    /// <param name="playSoundEffect">校正数量时是否播放音效。</param>
    [MemberFunction("48 85 C9 74 ?? 53 48 83 EC ?? 89 91")]
    public partial void SetMaximum
    (
        int  maximum,
        bool triggerCallback,
        bool playSoundEffect
    );
}
