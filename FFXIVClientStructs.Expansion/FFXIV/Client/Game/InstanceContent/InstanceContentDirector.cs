using NativeInstanceContentDirector = FFXIVClientStructs.FFXIV.Client.Game.InstanceContent.InstanceContentDirector;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.InstanceContent;

/// <summary>
///     副本控制器的最优队员推荐数据与提交接口。
/// </summary>
[GenerateInterop]
[Inherits<NativeInstanceContentDirector>]
[StructLayout(LayoutKind.Explicit, Size = 8088)]
public unsafe partial struct InstanceContentDirector
{
    /// <summary>
    ///     服务器下发的可推荐角色 Content ID，按收到的顺序移除零值后存储，最多七名。
    ///     只有前 <see cref="CommendationCandidateCount" /> 项有效；提交推荐后，数量清零。
    /// </summary>
    [FieldOffset(5760)]
    [FixedSizeArray]
    internal FixedSizeArray7<ulong> CommendationCandidatesStorage;

    /// <summary>
    ///     当前尚可提交的推荐候选人数；零表示没有候选人或已经提交推荐。
    /// </summary>
    [FieldOffset(8067)]
    public byte CommendationCandidateCount;

    /// <summary>
    ///     接收服务器下发的七个候选角色 Content ID，压紧非零项并通知游戏推荐状态发生变化。
    /// </summary>
    /// <param name="contentIDs">恰好包含七个 Content ID 的原生数组，未使用项为零。</param>
    [MemberFunction("48 8B 02 45 33 C0 48 85 C0")]
    public partial void SetCommendationCandidates(ulong* contentIDs);

    /// <summary>
    ///     判断副本是否已允许推荐且仍有可提交的候选人，不依赖推荐界面是否打开。
    /// </summary>
    /// <returns>副本完成标记已置位且候选人数非零时返回真。</returns>
    [MemberFunction("F6 81 ?? ?? ?? ?? ?? 75 ?? 32 C0 C3 80 B9")]
    public partial bool IsCommendationAvailable();

    /// <summary>
    ///     将指定候选人的 Content ID 与当前副本事件 ID 提交至服务器，随后清空候选人数并更新游戏推荐状态。
    ///     Content ID 不在当前名单内时不发送请求；调用前应确认推荐已开放。
    /// </summary>
    /// <param name="contentID">当前候选名单中的角色 Content ID。</param>
    [MemberFunction("40 53 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 44 0F B6 81")]
    public partial void GiveCommendation(ulong contentID);
}
