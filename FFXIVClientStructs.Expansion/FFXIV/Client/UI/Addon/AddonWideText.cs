using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     普通浮动提示窗口，依次播放等待队列中的文本。
/// </summary>
[GenerateInterop]
[Inherits<AddonScreenInfoChild>]
[Addon("_WideText")]
[StructLayout(LayoutKind.Explicit, Size = 664)]
public unsafe partial struct AddonWideText
{
    /// <summary>
    ///     承载提示入场、停留和退场动画的时间线节点。
    /// </summary>
    [FieldOffset(616)]
    public AtkResNode* TimelineNode;

    /// <summary>
    ///     等待播放的提示数量，不包含正在播放的提示。
    /// </summary>
    [FieldOffset(648)]
    public ulong PendingMessageCount;

    /// <summary>
    ///     是否已有提示正在播放；后续提示在此期间加入等待队列。
    /// </summary>
    [FieldOffset(660)]
    public bool IsPlaying;

    /// <summary>
    ///     接收提示数据，有提示正在播放时将新提示加入等待队列。
    /// </summary>
    /// <param name="valueCount">界面值数量。</param>
    /// <param name="values">提示数据，文本、播放速度及消息编号分别位于第 4、5、10 项。</param>
    [MemberFunction("83 FA ?? 0F 8E ?? ?? ?? ?? 4C 8B DC 53 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F1")]
    public partial void ShowMessage(uint valueCount, AtkValue* values);

    /// <summary>
    ///     设置当前提示文本并启动播放动画，首次显示和取出队列中的提示时均会调用。
    /// </summary>
    /// <param name="text">提示文本。</param>
    /// <param name="isSlow">为 1 时使用普通播放时长，为 0 时使用快速播放时长。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 55 57 41 54 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 45 8B E0")]
    public partial void StartMessage(CStringPointer text, int isSlow);
}
