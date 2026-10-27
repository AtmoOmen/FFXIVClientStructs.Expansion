using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的 HUD 得分窗口（KTGHudScore）。<br />
///     窗口显示总分、奖励倍率与当前阶段进度条，并在进入爆发状态时把进度条填满。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGHudScore")]
[StructLayout(LayoutKind.Explicit, Size = 664)]
public unsafe partial struct AddonKTGHudScore
{
    /// <summary>
    ///     总分条组件（节点 2 的组件），分数增加时播放时间线 101。
    /// </summary>
    [FieldOffset(568)]
    public AtkComponentGaugeBar* ScoreGaugeBar;

    /// <summary>
    ///     进度条组件（节点 6 的组件），按进度值设置百分比。
    /// </summary>
    [FieldOffset(576)]
    public AtkComponentGaugeBar* ProgressGaugeBar;

    /// <summary>
    ///     进度条节点（进度条组件的节点 2）。
    /// </summary>
    [FieldOffset(584)]
    public AtkResNode* ProgressGaugeNode;

    /// <summary>
    ///     总分文本节点（节点 2 组件的节点 5）。
    /// </summary>
    [FieldOffset(592)]
    public AtkTextNode* ScoreTextNode;

    /// <summary>
    ///     总分文本节点（节点 2 组件的节点 3）。
    /// </summary>
    [FieldOffset(600)]
    public AtkTextNode* ScoreTextNodeSecondary;

    /// <summary>
    ///     奖励倍率文本节点（节点 6 组件的节点 0x0A）。
    /// </summary>
    [FieldOffset(608)]
    public AtkTextNode* BonusTextNode;

    /// <summary>
    ///     爆发状态文本节点之一（节点 6 组件的节点 7）。
    /// </summary>
    [FieldOffset(616)]
    public AtkTextNode* FeverTextNode;

    /// <summary>
    ///     爆发状态文本节点之二（节点 6 组件的节点 8）。
    /// </summary>
    [FieldOffset(624)]
    public AtkTextNode* FeverTextNodeSecondary;

    /// <summary>
    ///     当前总分。
    /// </summary>
    [FieldOffset(632)]
    public int Score;

    /// <summary>
    ///     副本控制器下发的阶段参数，保存后未被读取。
    /// </summary>
    [FieldOffset(636)]
    public int Stage;

    /// <summary>
    ///     当前阶段进度，作为进度的分子。
    /// </summary>
    [FieldOffset(640)]
    public int StageProgress;

    /// <summary>
    ///     阶段进度上限，作为进度的分母，代理固定下发 100。
    /// </summary>
    [FieldOffset(644)]
    public int StageProgressMax;

    /// <summary>
    ///     奖励倍率基数，代理固定下发 1000，显示为 “x倍率”。
    /// </summary>
    [FieldOffset(648)]
    public int BonusBase;

    /// <summary>
    ///     初始化时置为 1 的保留字节。
    /// </summary>
    [FieldOffset(652)]
    public byte Unk652;

    /// <summary>
    ///     是否处于爆发状态。
    /// </summary>
    [FieldOffset(653)]
    public bool IsFever;

    /// <summary>
    ///     窗口的横坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(656)]
    public float PositionX;

    /// <summary>
    ///     窗口的纵坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(660)]
    public float PositionY;

    /// <summary>
    ///     构造窗口并初始化全部显示字段。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 80 8B ?? ?? ?? ?? ?? 48 8B CB 80 8B ?? ?? ?? ?? ?? 81 8B ?? ?? ?? ?? ?? ?? ?? ?? 48 89 03 33 C0 48 89 83 ?? ?? ?? ?? 48 89 83 ?? ?? ?? ?? 48 89 83"
    )]
    public partial AddonKTGHudScore* Ctor();

    /// <summary>
    ///     复位进度条与爆发状态文本节点：进度条回到时间线 17，隐藏进度节点与两个爆发文本节点。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 89 ?? ?? ?? ?? BA ?? ?? ?? ?? E8")]
    public partial void ResetScoreDisplay();

    /// <summary>
    ///     更新总分、奖励倍率与进度条：分数增加时播放总分条动画，进入爆发状态时填满进度条并播放时间线 102。
    /// </summary>
    /// <param name="score">当前总分。</param>
    /// <param name="stage">副本控制器下发的阶段参数，保存后未参与显示。</param>
    /// <param name="stageProgress">当前阶段进度。</param>
    /// <param name="stageProgressMax">阶段进度上限。</param>
    /// <param name="bonusBase">奖励倍率基数。</param>
    /// <param name="bonusValue">爆发状态下替换奖励倍率基数的数值。</param>
    /// <param name="isFever">是否处于爆发状态。</param>
    /// <param name="playFeverEffect">是否播放爆发状态的动画与音效。</param>
    [MemberFunction("48 89 5C 24 ?? 44 89 4C 24 ?? 57")]
    public partial void UpdateScoreDisplay
    (
        int  score,
        int  stage,
        int  stageProgress,
        int  stageProgressMax,
        int  bonusBase,
        int  bonusValue,
        bool isFever,
        bool playFeverEffect
    );
}
