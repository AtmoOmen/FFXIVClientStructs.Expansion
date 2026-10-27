using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的 HUD 日志窗口（KTGHudLog）。<br />
///     窗口持有六个日志槽位，按刷新命令下发的事件文本逐条显示，并在固定存活时间后回收。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGHudLog")]
[StructLayout(LayoutKind.Explicit, Size = 792)]
public unsafe partial struct AddonKTGHudLog
{
    /// <summary>
    ///     六个日志槽位的组件节点（节点 2 至 7）。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray6<Pointer<AtkComponentNode>> ComponentNodesStorage;

    /// <summary>
    ///     六个日志槽位。
    /// </summary>
    [FieldOffset(616)]
    [FixedSizeArray]
    internal FixedSizeArray6<LogEntry> EntriesStorage;

    /// <summary>
    ///     每条日志的存活秒数，超时后播放消失动画并回收槽位。
    /// </summary>
    [FieldOffset(760)]
    public float EntryLifetime;

    /// <summary>
    ///     构造时写入的保留值 1966080。
    /// </summary>
    [FieldOffset(764)]
    public int Unk764;

    /// <summary>
    ///     构造时写入的保留值 10。
    /// </summary>
    [FieldOffset(768)]
    public int Unk768;

    /// <summary>
    ///     下一个新日志使用的槽位编号，随分配递增。
    /// </summary>
    [FieldOffset(770)]
    public byte NextSlotIndex;

    /// <summary>
    ///     当前占用的槽位数量。
    /// </summary>
    [FieldOffset(771)]
    public byte ActiveEntryCount;

    /// <summary>
    ///     窗口是否已经完成初始化。
    /// </summary>
    [FieldOffset(776)]
    public bool IsInitialized;

    /// <summary>
    ///     窗口的横坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(780)]
    public float PositionX;

    /// <summary>
    ///     窗口的纵坐标，由刷新命令设置。
    /// </summary>
    [FieldOffset(784)]
    public float PositionY;

    /// <summary>
    ///     构造窗口并初始化六个日志槽位。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F9 E8 ?? ?? ?? ?? 0F 57 C0")]
    public partial AddonKTGHudLog* Ctor();

    /// <summary>
    ///     取得一个可用的日志槽位并播放出现动画；没有空闲槽位时先回收存活时间最长的一项。
    /// </summary>
    /// <returns>槽位索引，失败时为 -1。</returns>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 57 41 57 48 83 EC ?? 33 FF")]
    public partial int FindFreeEntry();

    /// <summary>
    ///     单个日志槽位。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe struct LogEntry
    {
        /// <summary>
        ///     槽位对应的组件。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentBase* Component;

        /// <summary>
        ///     已经显示的秒数，超过 <see cref="EntryLifetime"/> 后回收。
        /// </summary>
        [FieldOffset(8)]
        public float ElapsedTime;

        /// <summary>
        ///     槽位编号，-1 表示空闲。
        /// </summary>
        [FieldOffset(12)]
        public sbyte SlotIndex;

        /// <summary>
        ///     文本所使用的 Addon 表行号。
        /// </summary>
        [FieldOffset(16)]
        public int TextID;

        /// <summary>
        ///     文本格式化参数。
        /// </summary>
        [FieldOffset(20)]
        public int TextParameter;
    }
}
