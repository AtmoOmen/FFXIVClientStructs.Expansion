using FFXIVClientStructs.FFXIV.Client.Enums;
using NativeContentsFinderQueueInfo = FFXIVClientStructs.FFXIV.Client.Game.UI.ContentsFinderQueueInfo;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.UI;

/// <summary>
///     任务搜索器排队信息的扩展声明。
/// </summary>
[GenerateInterop]
[Inherits<NativeContentsFinderQueueInfo>]
[StructLayout(LayoutKind.Explicit, Size = 144)]
public unsafe partial struct ContentsFinderQueueInfo
{
    /// <summary>
    ///     设置排队状态；状态发生变化时，通知任务搜索器代理更新出发确认流程。
    /// </summary>
    /// <param name="newState">新的排队状态。</param>
    [MemberFunction("40 53 48 83 EC ?? 0F B6 59 ?? 3B D3")]
    public partial void SetQueueState
    (
        ContentsFinderQueueState newState
    );
}
