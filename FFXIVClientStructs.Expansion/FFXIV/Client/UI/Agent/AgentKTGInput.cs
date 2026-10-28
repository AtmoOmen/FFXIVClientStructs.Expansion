using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     打字游戏的输入代理（AgentKTGInput，内部编号 398）。<br />
///     代理在副本内接管本地玩家的打字输入：每帧把聊天输入框的文本、副本控制器的阶段图像编号与爆发状态剩余时间
///     下发给 KTGInput 窗口，接收窗口提交的打字结果并转发给副本控制器，同时负责退出确认与长时间未输入的处理。
/// </summary>
[Agent(AgentId.Unk398)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 96)]
public unsafe partial struct AgentKTGInput
{
    /// <summary>
    ///     KTG 的 HUD 代理，显示时取得，隐藏时清空。<br />
    ///     代理通过它读取 KTGInput 窗口 id 与爆发状态剩余时间。
    /// </summary>
    [FieldOffset(40)]
    public AgentKTGHud* HudAgent;

    /// <summary>
    ///     聊天窗口代理，代理从中读取本地玩家的输入法编辑文本。
    /// </summary>
    [FieldOffset(48)]
    public AgentChatLog* ChatLogAgent;

    /// <summary>
    ///     当前打开的退出确认窗口 id，0 表示没有确认窗口。
    /// </summary>
    [FieldOffset(56)]
    public uint ExitConfirmAddonID;

    /// <summary>
    ///     显示前保存的 RaptureAtkModule 输入模式，隐藏时恢复；6 表示无需恢复。
    /// </summary>
    [FieldOffset(60)]
    public int SavedInputMode;

    /// <summary>
    ///     显示时记录的客户端界面语言，仅日语时切换输入模式。
    /// </summary>
    [FieldOffset(64)]
    public ExcelModuleInterface.ExcelLanguage Language;

    /// <summary>
    ///     从聊天窗口输入文本复制的前三个字节，作为字符串参数下发给 KTGInput 窗口。
    /// </summary>
    [FieldOffset(68)]
    [FixedSizeArray]
    internal FixedSizeArray3<byte> InputTextStorage;

    /// <summary>
    ///     上一次下发时是否存在新的打字结果。
    /// </summary>
    [FieldOffset(72)]
    public bool HasNewInput;

    /// <summary>
    ///     本次更新是否收到了新的打字结果，下发后清空。
    /// </summary>
    [FieldOffset(73)]
    public bool InputSubmitted;

    /// <summary>
    ///     上一次下发给 KTGInput 窗口的阶段图像编号，副本控制器的阶段图像编号变化时更新。
    /// </summary>
    [FieldOffset(76)]
    public int ScreenImageIndex;

    /// <summary>
    ///     无有效按键输入的累计秒数，达到 60 秒时退出副本。
    /// </summary>
    [FieldOffset(80)]
    public float IdleSeconds;

    /// <summary>
    ///     同一按键按下后累计的秒数，按住同一按键十帧后用于判定长按。
    /// </summary>
    [FieldOffset(84)]
    public float KeyHoldSeconds;

    /// <summary>
    ///     当前按住的按键码，0 表示没有按键。
    /// </summary>
    [FieldOffset(88)]
    public ushort HeldKey;

    /// <summary>
    ///     同一按键连续按下的帧数，上限为 10。
    /// </summary>
    [FieldOffset(90)]
    public ushort HeldKeyFrames;

    /// <summary>
    ///     构造代理并初始化输入模式、阶段图像编号与按键状态。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? C7 43 ?? ?? ?? ?? ?? 48 89 03 33 C0 48 89 43 ?? 48 89 43 ?? 89 43 ?? 66 89 43")]
    public partial AgentKTGInput* Ctor();

    /// <summary>
    ///     提交魔光键影词条，按当前客户端语言规范化文字，并匹配副本内敌人或屏障的名字与称号。<br />
    ///     匹配成功时交由客户端发动攻击或强化屏障，不依赖当前选中的目标。
    /// </summary>
    /// <param name="inputValue">类型为字符串的界面值，文字必须在调用期间保持有效。</param>
    /// <returns>是否匹配到当前副本中的有效词条。</returns>
    [MemberFunction("40 55 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 8B 02")]
    public partial bool SubmitWord(AtkValue* inputValue);

    /// <summary>
    ///     打开退出确认窗口，确认后退出当前副本。<br />
    ///     窗口已经打开时先关闭它，副本控制器的阶段状态为 4、5 或 6 时不打开窗口。
    /// </summary>
    [MemberFunction("40 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 8B F9 48 8B 49 ?? 83 7F")]
    public partial void ShowExitConfirmation();

    /// <summary>
    ///     按副本控制器的阶段图像编号显示对应的阶段图像（界面图像 1620 至 1622），编号没有变化时不做处理。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8B C8 E8 ?? ?? ?? ?? 48 85 C0")]
    public partial void UpdateScreenImage();
}
