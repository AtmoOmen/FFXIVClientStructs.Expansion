using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     打字游戏的玩家名窗口（KTGPlayerName）。<br />
///     窗口持有四个玩家名槽位，把队员名显示在各自对应的目标位置上方，并在需要时显示目标标记。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[Addon("KTGPlayerName")]
[StructLayout(LayoutKind.Explicit, Size = 728)]
public unsafe partial struct AddonKTGPlayerName
{
    /// <summary>
    ///     四个玩家名槽位，每个槽位 40 字节。
    /// </summary>
    [FieldOffset(568)]
    [FixedSizeArray]
    internal FixedSizeArray4<PlayerNameEntry> EntriesStorage;

    /// <summary>
    ///     构造窗口并把四个玩家名槽位清零。
    /// </summary>
    /// <returns>窗口自身。</returns>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 33 D2 48 8D 8B ?? ?? ?? ?? 48 89 03 41 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 80 8B ?? ?? ?? ?? ?? 41 B0 ?? 80 8B ?? ?? ?? ?? ?? BA"
    )]
    public partial AddonKTGPlayerName* Ctor();

    /// <summary>
    ///     设置指定槽位的玩家名与目标标记：文本变化时写入文本节点，标记变化时播放或隐藏标记动画。
    /// </summary>
    /// <param name="index">槽位索引，取值 0 至 3。</param>
    /// <param name="name">玩家名文本。</param>
    /// <param name="showIndicator">是否显示目标标记。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 56 48 83 EC ?? 48 63 EA 41 0F B6 F1")]
    public partial void SetPlayerName
    (
        uint           index,
        CStringPointer name,
        bool           showIndicator
    );

    /// <summary>
    ///     隐藏指定槽位；所有槽位都隐藏后一并隐藏窗口。
    /// </summary>
    /// <param name="index">槽位索引，取值 0 至 3。</param>
    [MemberFunction("83 FA ?? 77 ?? 57 48 83 EC ?? 48 63 C2 48 8B F9 48 89 5C 24")]
    public partial void HidePlayerName
    (
        uint index
    );

    /// <summary>
    ///     按槽位记录的坐标与缩放摆放玩家名，并按名字宽度水平居中。
    /// </summary>
    /// <param name="index">槽位索引，取值 0 至 3。</param>
    [MemberFunction("83 FA ?? 0F 87 ?? ?? ?? ?? 53 48 83 EC ?? 48 63 C2 48 8B D9 48 89 7C 24 ?? 48 8D 3C 80")]
    public partial void LayoutPlayerName
    (
        uint index
    );

    /// <summary>
    ///     单个玩家名槽位。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 40)]
    public unsafe struct PlayerNameEntry
    {
        /// <summary>
        ///     复制出来的组件节点（节点 id 为 20000 + 槽位索引）。
        /// </summary>
        [FieldOffset(0)]
        public AtkComponentNode* ComponentNode;

        /// <summary>
        ///     玩家名文本节点（组件节点 3）。
        /// </summary>
        [FieldOffset(8)]
        public AtkTextNode* NameTextNode;

        /// <summary>
        ///     目标标记节点（组件节点 2）。
        /// </summary>
        [FieldOffset(16)]
        public AtkResNode* IndicatorNode;

        /// <summary>
        ///     槽位是否已经显示过。
        /// </summary>
        [FieldOffset(24)]
        public bool IsShown;

        /// <summary>
        ///     目标标记当前是否可见。
        /// </summary>
        [FieldOffset(25)]
        public bool IndicatorVisible;

        /// <summary>
        ///     目标位置的横坐标。
        /// </summary>
        [FieldOffset(28)]
        public float X;

        /// <summary>
        ///     目标位置的纵坐标。
        /// </summary>
        [FieldOffset(32)]
        public float Y;

        /// <summary>
        ///     玩家名缩放系数，默认 1。
        /// </summary>
        [FieldOffset(36)]
        public float Scale;
    }
}
