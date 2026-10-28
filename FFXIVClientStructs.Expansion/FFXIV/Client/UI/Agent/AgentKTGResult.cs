using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using AtkEventInterface = FFXIVClientStructs.FFXIV.Component.GUI.AtkModuleInterface.AtkEventInterface;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     打字游戏的结果代理（AgentKTGResult）。<br />
///     代理在副本结束后收集四名玩家的得分、八项奖励道具与剩余时间，按 78 项界面值下发给 KTGResult 窗口，
///     并处理倒计时与关闭通知。
/// </summary>
[Agent(AgentId.KTGResult)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 2648)]
public unsafe partial struct AgentKTGResult
{
    /// <summary>
    ///     代理是否处于显示状态。
    /// </summary>
    [FieldOffset(40)]
    public bool IsShown;

    /// <summary>
    ///     结果状态，随界面值第 0 项下发。
    /// </summary>
    [FieldOffset(48)]
    public byte State;

    /// <summary>
    ///     得分条目数量，最大为 4，随界面值第 1 项下发。
    /// </summary>
    [FieldOffset(52)]
    public int PlayerCount;

    /// <summary>
    ///     四名玩家的得分数据。
    /// </summary>
    [FieldOffset(56)]
    [FixedSizeArray]
    internal FixedSizeArray4<PlayerEntry> PlayerEntriesStorage;

    /// <summary>
    ///     奖励条目数量，最大为 8，随界面值第 2 项下发。
    /// </summary>
    [FieldOffset(312)]
    public int ItemCount;

    /// <summary>
    ///     八个奖励道具的 id 与数量。
    /// </summary>
    [FieldOffset(316)]
    [FixedSizeArray]
    internal FixedSizeArray8<ItemEntry> ItemEntriesStorage;

    /// <summary>
    ///     构造时清零且未在已分析函数中使用的保留值。
    /// </summary>
    [FieldOffset(380)]
    public int Unk380;

    /// <summary>
    ///     结果说明文本，随界面值第 77 项下发。
    /// </summary>
    [FieldOffset(384)]
    public Utf8String Comment;

    /// <summary>
    ///     副本结果中的第三个数值，随界面值第 3 项下发，结果窗口读取后未使用。
    /// </summary>
    [FieldOffset(488)]
    public int ResultValue;

    /// <summary>
    ///     当前打开的 KTGResult 窗口 id。
    /// </summary>
    [FieldOffset(496)]
    public uint AddonID;

    /// <summary>
    ///     结果窗口是否已经打开。
    /// </summary>
    [FieldOffset(500)]
    public bool HasAddon;

    /// <summary>
    ///     是否已经结束并通知事件方。
    /// </summary>
    [FieldOffset(501)]
    public bool Finished;

    /// <summary>
    ///     四名玩家名的第一组文本。
    /// </summary>
    [FieldOffset(504)]
    [FixedSizeArray]
    internal FixedSizeArray4<Utf8String> PlayerNamesStorage;

    /// <summary>
    ///     四名玩家名的第二组文本，按是否请求更新在两组之间切换。
    /// </summary>
    [FieldOffset(920)]
    [FixedSizeArray]
    internal FixedSizeArray4<Utf8String> PlayerNamesAltStorage;

    /// <summary>
    ///     四名玩家的称号文本。
    /// </summary>
    [FieldOffset(1336)]
    [FixedSizeArray]
    internal FixedSizeArray4<Utf8String> PlayerTitlesStorage;

    /// <summary>
    ///     八个奖励道具的名称。
    /// </summary>
    [FieldOffset(1752)]
    [FixedSizeArray]
    internal FixedSizeArray8<Utf8String> ItemNamesStorage;

    /// <summary>
    ///     八个奖励道具的图标 id。
    /// </summary>
    [FieldOffset(2584)]
    [FixedSizeArray]
    internal FixedSizeArray8<int> ItemIconsStorage;

    /// <summary>
    ///     道具图标是否尚未解析完成。
    /// </summary>
    [FieldOffset(2616)]
    public bool ItemsPending;

    /// <summary>
    ///     窗口需要在本次更新中刷新。
    /// </summary>
    [FieldOffset(2617)]
    public bool UpdateRequested;

    /// <summary>
    ///     本地玩家的标识，用于在得分条目中标记自己。
    /// </summary>
    [FieldOffset(2624)]
    public ulong SelfPlayerKey;

    /// <summary>
    ///     剩余秒数，每满一秒递减。
    /// </summary>
    [FieldOffset(2632)]
    public int RemainingSeconds;

    /// <summary>
    ///     上一次刷新窗口时写入的剩余秒数，用于避免重复刷新。
    /// </summary>
    [FieldOffset(2636)]
    public int LastRefreshedSeconds;

    /// <summary>
    ///     倒计时累计的小数秒。
    /// </summary>
    [FieldOffset(2640)]
    public float CountdownTimer;

    /// <summary>
    ///     构造代理并初始化四名玩家数据、八个奖励条目与全部文本字段。
    /// </summary>
    /// <returns>代理自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? C6 47")]
    public partial AgentKTGResult* Ctor();

    /// <summary>
    ///     把当前数据整理为 78 项界面值并刷新结果窗口。
    /// </summary>
    [MemberFunction("40 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 80 B9 ?? ?? ?? ?? ?? 48 8B F9 0F 84 ?? ?? ?? ?? 48 89 9C 24")]
    public partial void RefreshAddon();

    /// <summary>
    ///     把当前玩家、道具与说明整理为 78 项界面值。
    /// </summary>
    /// <param name="values">输出：78 项界面值。</param>
    /// <param name="valueCount">界面值容量，必须不小于 78。</param>
    [MemberFunction("48 85 D2 0F 84 ?? ?? ?? ?? 48 8B C4 48 89 50 ?? 55")]
    public partial void BuildAddonValues
    (
        AtkValue* values,
        ulong     valueCount
    );

    /// <summary>
    ///     关闭结果窗口并向打开者发送结束事件，参数为是否确认领取奖励。
    /// </summary>
    /// <param name="confirmed">是否确认领取奖励。</param>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 0F B6 FA")]
    public partial void Finish
    (
        bool confirmed
    );

    /// <summary>
    ///     打开结果窗口：先关闭旧窗口，再用 78 项界面值调用 OpenAddon，成功后立即刷新一次。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 33 F6 48 8B F9 40 38 B1")]
    public partial void OpenResultAddon();

    /// <summary>
    ///     关闭结果窗口并清除窗口 id 与已打开标志。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 80 B9 ?? ?? ?? ?? ?? 48 8B D9 74 ?? 48 8B 49 ?? 48 8B 01 FF 50 ?? 8B 93")]
    public partial void CloseResultAddon();

    /// <summary>
    ///     解析八个奖励道具的名称与图标，全部解析成功时刷新结果窗口。
    /// </summary>
    /// <returns>是否全部解析成功。</returns>
    [MemberFunction("4C 8B DC 53 57 41 54 41 55 48 81 EC")]
    public partial bool ResolveItemIcons();

    /// <summary>
    ///     从副本结果数据收集玩家、道具与说明文本，排序后打开结果窗口。
    /// </summary>
    /// <param name="data">副本结果数据。</param>
    /// <param name="opener">打开结果窗口的事件接口。</param>
    [MemberFunction
    (
        "48 89 5C 24 ?? 55 56 57 41 54 41 55 41 56 41 57 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 48 8B 01 49 8B D8 48 8B FA"
    )]
    public partial void CollectResultData
    (
        void*              data,
        AtkEventInterface* opener
    );

    /// <summary>
    ///     单个玩家的得分数据。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public unsafe struct PlayerEntry
    {
        /// <summary>
        ///     构造时清零且未在已分析函数中使用的保留值。
        /// </summary>
        [FieldOffset(0)]
        public uint Unk0;

        /// <summary>
        ///     玩家内容 id，低 32 位与高 32 位分别作为两项界面值下发。
        /// </summary>
        [FieldOffset(8)]
        public ulong ContentID;

        /// <summary>
        ///     玩家得分数值，低 32 位与高 32 位分别作为两项界面值下发。
        /// </summary>
        [FieldOffset(16)]
        public ulong Value;

        /// <summary>
        ///     构造时清零且未在已分析函数中使用的保留值。
        /// </summary>
        [FieldOffset(24)]
        public ulong Unk24;

        /// <summary>
        ///     构造时清零且未在已分析函数中使用的保留值。
        /// </summary>
        [FieldOffset(32)]
        public ulong Unk32;

        /// <summary>
        ///     构造时清零且未在已分析函数中使用的保留值。
        /// </summary>
        [FieldOffset(40)]
        public ulong Unk40;

        /// <summary>
        ///     构造时清零且未在已分析函数中使用的保留值。
        /// </summary>
        [FieldOffset(48)]
        public ulong Unk48;

        /// <summary>
        ///     比分，用于确定名次。
        /// </summary>
        [FieldOffset(56)]
        public uint Score;
    }

    /// <summary>
    ///     单个奖励道具条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe struct ItemEntry
    {
        /// <summary>
        ///     道具 id。
        /// </summary>
        [FieldOffset(0)]
        public uint ItemID;

        /// <summary>
        ///     道具数量。
        /// </summary>
        [FieldOffset(4)]
        public uint Quantity;
    }
}
