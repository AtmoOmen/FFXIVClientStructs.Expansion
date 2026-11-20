using FFXIVClientStructs.FFXIV.Client.UI.Info;
using NativeInfoProxyPartyMember = FFXIVClientStructs.FFXIV.Client.UI.Info.InfoProxyPartyMember;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     小队成员信息代理，处理服务器发送的战斗倒计时数据。
/// </summary>
[GenerateInterop]
[Inherits<NativeInfoProxyPartyMember>]
[InfoProxy(InfoProxyId.PartyMember)]
[StructLayout(LayoutKind.Explicit, Size = 832)]
public unsafe partial struct InfoProxyPartyMember
{
    /// <summary>
    ///     处理服务器发送的倒计时数据，解析发起者名称并通知倒计时设置代理。
    ///     战斗状态检查失败时仅显示错误消息，否则继续处理倒计时。
    /// </summary>
    /// <param name="data">服务器发送的倒计时数据，仅在本次调用期间有效。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 48 8B DA 48 8B F9 48 8B 49 ?? 80 7B")]
    public partial void InitCountdown(CountdownStartData* data);

    /// <summary>
    ///     向服务器请求开始战斗倒计时。
    /// </summary>
    /// <param name="durationSeconds">倒计时持续时间，单位为秒。</param>
    /// <param name="isTextCommand">是否由文本指令发起。</param>
    /// <param name="isRecordReadyCheck">是否由任务记录准备确认发起。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? 41 0F B6 D9")]
    public partial void RequestStartCountdown(byte durationSeconds, bool isTextCommand, bool isRecordReadyCheck);

    /// <summary>
    ///     向服务器请求取消战斗倒计时。
    /// </summary>
    [MemberFunction("48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 8B 0D")]
    public partial void RequestCancelCountdown();

    /// <summary>
    ///     处理倒计时取消通知，解析取消者名称并关闭倒计时界面。
    /// </summary>
    /// <param name="entityID">取消者的实体 ID。系统清理使用无效实体 ID。</param>
    /// <param name="name">取消者名称。系统清理传入空指针。</param>
    /// <param name="homeWorldID">取消者的原始服务器 ID。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B 49 ?? 41 0F B7 E9")]
    public partial void CancelCountdown(uint entityID, CStringPointer name, ushort homeWorldID);
}
