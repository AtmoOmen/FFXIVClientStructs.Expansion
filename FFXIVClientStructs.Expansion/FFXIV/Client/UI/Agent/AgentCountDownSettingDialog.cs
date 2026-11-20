using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using NativeAgentCountDownSettingDialog = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentCountDownSettingDialog;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     战斗倒计时设置代理，管理倒计时状态、界面及聊天通知。
/// </summary>
[Agent(AgentId.CountDownSettingDialog)]
[GenerateInterop]
[Inherits<NativeAgentCountDownSettingDialog>]
[StructLayout(LayoutKind.Explicit, Size = 72)]
public unsafe partial struct AgentCountDownSettingDialog
{
    /// <summary>
    ///     当前倒计时是否由文本指令发起，用于选择剩余时间的聊天消息类型。
    /// </summary>
    [FieldOffset(57)]
    public bool IsTextCommand;

    /// <summary>
    ///     是否已处理首次五秒间隔通知。首次仅置位，后续才发送剩余时间消息。
    /// </summary>
    [FieldOffset(58)]
    public bool HasProcessedFirstAnnouncement;

    /// <summary>
    ///     代理是否已收到登录事件。登出事件会清除此标志并终止倒计时。
    /// </summary>
    [FieldOffset(65)]
    public bool IsLoggedIn;

    /// <summary>
    ///     构造代理并清除倒计时状态、界面 ID 及登录标志。
    /// </summary>
    /// <param name="uiModule">代理所属的界面模块接口。</param>
    /// <returns>构造后的代理自身。</returns>
    [MemberFunction("80 61 ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 51 ?? 33 D2 48 89 01 48 8B C1 48 89 51 ?? 89 51 ?? 48 89 51 ?? 48 89 51 ?? 66 89 51")]
    public partial AgentCountDownSettingDialog* Ctor(UIModuleInterface* uiModule);

    /// <summary>
    ///     检查登录、准备确认及小队战斗状态是否允许请求倒计时，并显示对应错误消息。
    /// </summary>
    /// <returns>是否允许请求倒计时。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 41 56 48 83 EC ?? 32 DB")]
    public partial bool CanStartCountdown();

    /// <summary>
    ///     检查状态后请求开始倒计时；已有倒计时时打开取消确认界面。
    /// </summary>
    /// <param name="durationSeconds">倒计时持续时间，单位为秒。</param>
    /// <returns>是否通过状态检查并进入开始请求流程。</returns>
    [MemberFunction("48 89 5C 24 ?? 41 56 48 81 EC ?? ?? ?? ?? 8B DA")]
    public partial bool RequestStartCountdown(byte durationSeconds);

    /// <summary>
    ///     接受服务器的倒计时开始通知，检查操作权限后设置计时状态并发送开始消息。
    /// </summary>
    /// <param name="entityID">发起者的实体 ID。</param>
    /// <param name="durationSeconds">倒计时持续时间，单位为秒。</param>
    /// <param name="name">已解析的发起者名称。</param>
    /// <param name="isTextCommand">是否由文本指令发起。</param>
    /// <param name="isRecordReadyCheck">是否由任务记录准备确认发起。</param>
    /// <param name="homeWorldID">发起者的原始服务器 ID。</param>
    [MemberFunction("40 55 53 56 57 41 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 4D 8B F1")]
    public partial void StartCountdown
    (
        uint           entityID,
        ushort         durationSeconds,
        CStringPointer name,
        bool           isTextCommand,
        bool           isRecordReadyCheck,
        ushort         homeWorldID
    );

    /// <summary>
    ///     清除活动状态和发起者 ID，并关闭倒计时界面；有取消者时发送取消消息。
    /// </summary>
    /// <param name="entityID">取消者的实体 ID。系统清理使用无效实体 ID。</param>
    /// <param name="name">已解析的取消者名称。系统清理传入空指针。</param>
    /// <param name="homeWorldID">取消者的原始服务器 ID。</param>
    [MemberFunction("40 55 53 56 57 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 45 33 FF")]
    public partial void CancelCountdown(uint entityID, CStringPointer name, ushort homeWorldID);

    /// <summary>
    ///     推进剩余时间，处理回放暂停，并在显示秒数变化时调用倒计时更新函数。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 8B 41 ?? 48 8B D9 89 41")]
    public partial void UpdateRemainingTime();

    /// <summary>
    ///     处理显示秒数：最后五秒打开倒计时界面，每五秒处理聊天通知，到零时处理战斗开始。
    /// </summary>
    /// <param name="remainingSeconds">当前显示的剩余秒数，零表示计时结束。</param>
    [MemberFunction("40 55 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 8B EA")]
    public partial void UpdateCountdown(uint remainingSeconds);

    /// <summary>
    ///     女王古殿以外，当前玩家不是发起者且已不在小队或团队时，清理倒计时。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 84 C0 75 ?? 48 8B 0D")]
    public partial void CheckPartyMembership();

    /// <summary>
    ///     检查回放速度；加速回放时关闭倒计时界面，并根据剩余时间调整显示和活动状态。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 90")]
    public partial void CheckPlaybackSpeed();

    /// <summary>
    ///     取得发起者的团队分组编号，跨服团队或无法识别时为零。
    /// </summary>
    /// <param name="entityID">发起者的实体 ID。</param>
    /// <returns>从一开始的团队分组编号；未找到时为零，分组索引越界时为 256。</returns>
    [MemberFunction("40 53 48 83 EC ?? 8B DA E8 ?? ?? ?? ?? 84 C0 74 ?? 33 C0")]
    public partial uint GetInitiatorAllianceGroup(uint entityID);

    /// <summary>
    ///     关闭倒计时界面及取消确认界面，清除对应界面 ID 和显示标志。
    ///     此函数本身不清除活动状态和发起者 ID。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 83 79 ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 53 ?? 48 8B C8 4C 8B 00 41 FF 90 ?? ?? ?? ?? C7 43 ?? ?? ?? ?? ?? 83 7B ?? ?? C6 43"
    )]
    public partial void CloseCountdown();

    /// <summary>
    ///     发送战斗开始消息并播放提示音；活动状态由倒计时界面的后续回调清除。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 49 ?? 48 8B 01 FF 90 ?? ?? ?? ?? 48 8B 88 ?? ?? ?? ?? 48 85 C9 74 ?? E8 ?? ?? ?? ?? 83 F8")]
    public partial void CompleteCountdown();
}
