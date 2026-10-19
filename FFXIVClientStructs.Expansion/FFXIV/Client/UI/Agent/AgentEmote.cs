using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;
using NativeAgentEmote = FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentEmote;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

/// <summary>
///     Emote 窗口对应的 Agent。
/// </summary>
[Agent(AgentId.Emote)]
[GenerateInterop]
[Inherits<NativeAgentEmote>]
[StructLayout(LayoutKind.Explicit, Size = 272)]
public unsafe partial struct AgentEmote
{
    /// <summary>
    ///     TextCommand 表，用于解析情感动作的文本指令名。
    /// </summary>
    [FieldOffset(40)]
    public ExcelSheet* TextCommandSheet;

    /// <summary>
    ///     置位后改为在下一个 addon 事件中刷新列表。
    /// </summary>
    [FieldOffset(48)]
    public byte NeedsRefresh;

    /// <summary>
    ///     当前页签。0 为历史，1 为通常，2 为特别，3 为表情，4 为搜索。
    /// </summary>
    [FieldOffset(49)]
    public byte SelectedTab;

    /// <summary>
    ///     <see cref="SelectedTab" /> 之下的子页签。
    /// </summary>
    [FieldOffset(50)]
    public byte SelectedSubTab;

    /// <summary>
    ///     <see cref="EmoteList" /> 中当前写入 addon 的情感动作数量。
    /// </summary>
    [FieldOffset(52)]
    public ushort DisplayedEmoteCount;

    /// <summary>
    ///     当前搜索结果中的情感动作数量。
    /// </summary>
    [FieldOffset(54)]
    public ushort SearchResultCount;

    /// <summary>
    ///     打开上下文菜单的列表项索引。
    /// </summary>
    [FieldOffset(56)]
    public uint ContextMenuSlotIndex;

    /// <summary>
    ///     打开上下文菜单的列表项情感动作 ID。
    /// </summary>
    [FieldOffset(60)]
    public uint ContextMenuEmoteID;

    /// <summary>
    ///     EmoteStatusHelp 窗口的 addon ID。
    /// </summary>
    [FieldOffset(64)]
    public uint EmoteStatusHelpAddonID;

    /// <summary>
    ///     当前选中或正在搜索的情感动作 ID。
    /// </summary>
    [FieldOffset(68)]
    public uint SelectedEmoteID;

    /// <summary>
    ///     上下文菜单执行情感动作时传给 <see cref="NativeAgentEmote.ExecuteEmote" /> 的参数。
    /// </summary>
    [FieldOffset(72)]
    public byte AddToHistory;

    /// <summary>
    ///     上下文菜单执行情感动作时传给 <see cref="NativeAgentEmote.ExecuteEmote" /> 的参数。
    /// </summary>
    [FieldOffset(73)]
    public byte LiveUpdateHistory;

    /// <summary>
    ///     可用情感动作的缓存列表，按名字排序。
    /// </summary>
    [FieldOffset(80)]
    public StdVector<EmoteEntry> EmoteList;

    /// <summary>
    ///     加载 Emote 表并填充 <see cref="EmoteList" /> 的等待器。
    /// </summary>
    [FieldOffset(104)]
    public EmoteSheetWaiter SheetWaiter;

    /// <summary>
    ///     列表排序模式。0 为按编号，1 为按名字。
    /// </summary>
    [FieldOffset(264)]
    public byte SortMode;

    /// <summary>
    ///     就地构造 AgentEmote，初始化基类、字段列表、文本指令表等待器与排序模式。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 33 ED 66 C7 46")]
    public partial AgentEmote* Ctor();

    /// <summary>
    ///     检查本地玩家是否已解锁指定情感动作。
    /// </summary>
    [MemberFunction("48 83 EC ?? 44 8B C2 8B CA")]
    public partial bool IsEmoteUnlocked
    (
        uint emoteID
    );

    /// <summary>
    ///     把当前的 EmoteTextType 系统配置值推送到 Emote 窗口。
    /// </summary>
    [MemberFunction("40 55 48 83 EC ?? 48 8B E9 48 8B 49 ?? 48 8B 01 FF 50 ?? 48 8B C8")]
    public partial void RefreshAddonTextType();

    /// <summary>
    ///     处理 EmoteStatusHelp 窗口的关闭值。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 48 8B D9 48 8B CA E8 ?? ?? ?? ?? 83 F8 ?? 75 ?? C7 43")]
    public partial bool OnEmoteStatusHelpClosed
    (
        AtkValue* value
    );

    /// <summary>
    ///     在跨服编辑器上下文中打开指定情感动作。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC ?? 8B DA 48 8B F9 8B CB")]
    public partial bool OpenCrossEditorEmote
    (
        uint emoteID
    );

    /// <summary>
    ///     设置 EmoteTextType 系统配置值。
    /// </summary>
    [MemberFunction
    (
        "40 53 48 83 EC ?? 48 8B 0D ?? ?? ?? ?? 0F B6 DA 48 83 C1 ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? 8B D3 41 B8 ?? ?? ?? ?? 48 8B C8 48 83 C4 ?? 5B E9 ?? ?? ?? ?? CC CC CC CC CC CC CC CC CC CC CC CC CC 80 FA"
    )]
    public partial bool SetEmoteTextType
    (
        byte value
    );

    /// <summary>
    ///     把 EmoteHistoryModule 的历史与常用情感动作写入 Emote 窗口。
    /// </summary>
    [MemberFunction("40 56 48 83 EC ?? 83 79 ?? ?? 48 8B F1 0F 84")]
    public partial void RefreshHistoryList();

    /// <summary>
    ///     搜索缓存的情感动作列表并把结果写入 Emote 窗口。
    /// </summary>
    [MemberFunction("40 55 41 54 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05")]
    public partial void SearchEmote
    (
        byte* searchString
    );

    /// <summary>
    ///     为指定列表项打开 Emote 窗口的上下文菜单。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 41 8B E8 8B F2 48 8B D9")]
    public partial void OpenContextMenu
    (
        uint slotIndex,
        uint emoteID
    );

    /// <summary>
    ///     设置常用槽位的情感动作并刷新 Emote 窗口。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 41 8B F8 48 8B 49 ?? 8B DA")]
    public partial void SetFavorite
    (
        int    slotIndex,
        ushort emoteID
    );

    /// <summary>
    ///     交换两个常用槽位并刷新 Emote 窗口。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 41 8B D8 48 8B 49 ?? 8B FA 48 8B 01 FF 90 ?? ?? ?? ?? 44 8B C3")]
    public partial void SwapFavorites
    (
        int slotIndex1,
        int slotIndex2
    );

    /// <summary>
    ///     设置 <see cref="SortMode" /> 并刷新 Emote 窗口。
    /// </summary>
    [MemberFunction("40 55 48 83 EC ?? 0F B6 81 ?? ?? ?? ?? 48 8B E9 88 91")]
    public partial void SetSortMode
    (
        byte sortMode
    );

    /// <summary>
    ///     在 EmoteHistoryModule 中把指定情感动作标记为已读。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? F6 41 ?? ?? 41 0F B6 F0")]
    public partial void MarkEmoteSeen
    (
        ushort emoteID,
        bool   refresh
    );

    /// <summary>
    ///     把指定情感动作加入常用列表并输出日志。
    /// </summary>
    [MemberFunction("40 55 53 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 48 8B F9 8B DA")]
    public partial void AddToFavorites
    (
        uint emoteID
    );

    /// <summary>
    ///     移除指定常用槽位的情感动作并输出日志。
    /// </summary>
    [MemberFunction("4C 8B DC 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 80 79")]
    public partial void RemoveFromFavorites
    (
        uint slotIndex
    );

    /// <summary>
    ///     从历史记录中移除指定情感动作并输出日志。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 57 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 8D 42")]
    public partial bool RemoveFromHistory
    (
        uint emoteID
    );

    /// <summary>
    ///     构建指定情感动作的可用性标志位。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 8B FA 33 C0")]
    public partial uint GetEmoteAvailabilityFlags
    (
        uint                          emoteID,
        EmoteController.EmoteDetails* details
    );

    /// <summary>
    ///     把一个情感动作写入 AtkValue 数组的指定槽位区间。
    /// </summary>
    /// <param name="categoryMode">5 表示普通列表项，6 表示搜索结果项。</param>
    [MemberFunction("40 55 53 56 57 41 54 41 55 41 56 41 57 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 44 8B A5")]
    public partial void FillEmoteListEntry
    (
        uint      emoteID,
        int       valueIndex,
        AtkValue* values,
        int       categoryMode
    );

    /// <summary>
    ///     构建发送给 Emote 窗口的完整 AtkValue 数组。
    /// </summary>
    [MemberFunction("40 55 53 56 57 41 54 41 55 41 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 ?? 4C 8B E9")]
    public partial bool BuildEmoteListValues
    (
        AtkValue* values
    );

    /// <summary>
    ///     解析指定情感动作的详细信息。
    /// </summary>
    [MemberFunction("40 53 48 83 EC ?? 49 8B D8 44 8B CA")]
    public partial bool TryGetEmoteDetails
    (
        uint                          emoteID,
        EmoteController.EmoteDetails* details
    );

    /// <summary>
    ///     把字符串去重后追加到字符串指针数组。
    ///     待添加的字符串为空、以字符 0 开头（表数据中的空占位）、或已在数组中存在时，本次添加跳过。
    ///     追加成功时把计数指针指向的值加一。
    /// </summary>
    /// <param name="strings">目标字符串指针数组。</param>
    /// <param name="unused">保留参数，用于维持原生调用约定。</param>
    /// <param name="count">数组当前元素数量，成功追加后自增。</param>
    /// <param name="value">待追加的字符串。</param>
    [MemberFunction("48 89 5C 24 ?? 48 89 7C 24 ?? 48 8B 5C 24")]
    public partial void AddUniqueString
    (
        byte** strings,
        nint   unused,
        byte*  count,
        byte*  value
    );

    /// <summary>
    ///     Emote 表每加载一行时由 <see cref="EmoteSheetWaiter" /> 调用。
    /// </summary>
    [MemberFunction("4C 8B DC 55 53 56 57 41 56 41 57")]
    public partial void OnEmoteRowLoaded
    (
        byte** row,
        uint   emoteID,
        bool   openAddon
    );

    /// <summary>
    ///     检查指定情感动作条目是否匹配搜索字符串。
    /// </summary>
    [MemberFunction("40 53 56 57 41 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 4C 8B F2")]
    public partial bool MatchesSearch
    (
        Utf8String*                   searchString,
        EmoteEntry*                   entry,
        EmoteController.EmoteDetails* details
    );

    /// <summary>
    ///     AgentEmote 为 Emote 窗口缓存的情感动作列表条目。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 112)]
    public unsafe struct EmoteEntry
    {
        /// <summary>
        ///     情感动作行号。
        /// </summary>
        [FieldOffset(0)]
        public uint EmoteID;

        /// <summary>
        ///     情感动作名字，用于排序与搜索。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String Name;
    }

    /// <summary>
    ///     内嵌在 <see cref="AgentEmote" /> 中的 Emote 表等待器。
    ///     它派生自 <see cref="ExcelSheetWaiter" />，把 Emote 表行送入该 Agent。
    /// </summary>
    [GenerateInterop]
    [Inherits<ExcelSheetWaiter>]
    [VirtualTable("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 48 83 EC ?? 48 8B F1 E8 ?? ?? ?? ?? 33 ED 66 C7 46", 106, 2)]
    [StructLayout(LayoutKind.Explicit, Size = 160)]
    public unsafe partial struct EmoteSheetWaiter
    {
        /// <summary>
        ///     行号队列的哨兵节点。
        /// </summary>
        [FieldOffset(48)]
        public nint RowQueueNode;

        /// <summary>
        ///     行号队列的桶数组。
        /// </summary>
        [FieldOffset(56)]
        public nint RowQueueBuckets;

        /// <summary>
        ///     行号队列的桶数量。
        /// </summary>
        [FieldOffset(64)]
        public nint RowQueueCapacity;

        /// <summary>
        ///     行号队列的队头索引。
        /// </summary>
        [FieldOffset(72)]
        public nint RowQueueHead;

        /// <summary>
        ///     队列中的行号数量。
        /// </summary>
        [FieldOffset(80)]
        public nint RowQueueCount;

        /// <summary>
        ///     行号队列快照的哨兵节点。
        /// </summary>
        [FieldOffset(88)]
        public nint RowQueueSnapshotNode;

        /// <summary>
        ///     队列快照与队头之间的保留字段。
        /// </summary>
        [FieldOffset(96)]
        public nint Reserved60;

        /// <summary>
        ///     行号队列快照的队头索引。
        /// </summary>
        [FieldOffset(104)]
        public nint RowQueueSnapshotHead;

        /// <summary>
        ///     每加载一行 Emote 表时调用的回调。
        /// </summary>
        [FieldOffset(112)]
        public nint RowLoadedCallback;

        /// <summary>
        ///     表缺失时调用的次要回调。
        /// </summary>
        [FieldOffset(120)]
        public nint SecondaryCallback;

        /// <summary>
        ///     传给 <see cref="RowLoadedCallback" /> 的参数。
        /// </summary>
        [FieldOffset(128)]
        public nint CallbackArgument;

        /// <summary>
        ///     该等待器加载的 Excel 表索引。
        /// </summary>
        [FieldOffset(136)]
        public uint SheetIndex;

        /// <summary>
        ///     当前正在加载的行索引。
        /// </summary>
        [FieldOffset(140)]
        public uint CurrentRowIndex;

        /// <summary>
        ///     每次重置与准备时递增，用于丢弃过期的行回调。
        /// </summary>
        [FieldOffset(144)]
        public uint RequestGeneration;

        /// <summary>
        ///     表数据就绪后置为 1。
        /// </summary>
        [FieldOffset(148)]
        public uint LoadState;

        /// <summary>
        ///     处理行回调期间置位。
        /// </summary>
        [FieldOffset(152)]
        public byte IsProcessing;

        /// <summary>
        ///     置位时 <see cref="Prepare" /> 跳过完整重载，只设置队列中的第一行。
        /// </summary>
        [FieldOffset(153)]
        public byte SkipReload;

        /// <summary>
        ///     Excel 表请求的行加载完成时调用。
        /// </summary>
        [VirtualFunction(1)]
        public partial void Finish
        (
            int  rowCount,
            nint rows,
            uint requestGeneration
        );

        /// <summary>
        ///     清空队列并释放当前的表等待状态。
        /// </summary>
        [MemberFunction
        (
            "48 89 5C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 55 48 8B EC 48 83 EC ?? FF 81 ?? ?? ?? ?? 33 F6 48 8B F9 48 89 B1 ?? ?? ?? ?? 48 89 71 ?? 33 D2 48 89 71 ?? 41 B8 ?? ?? ?? ?? 89 B1 ?? ?? ?? ?? 89 B1 ?? ?? ?? ?? 8D 4E ?? 48 89 75 ?? 48 89 75 ?? 48 89 75 ?? 48 89 75 ?? E8 ?? ?? ?? ?? 48 8D 4D ?? 4C 8D 47 ?? 48 89 70 ?? 48 89 45 ?? 48 89 08 48 8D 45 ?? 49 3B C0 74 ?? 49 8B 08 48 8B 45 ?? 48 89 4D ?? 49 89 00 48 85 C9 74 ?? 48 8D 45 ?? 48 89 01 49 8B 00 48 85 C0 74 ?? 4C 89 00 48 8B 45 ?? 4D 8B 48 ?? 49 8B 58 ?? 49 8B 48 ?? 49 8B 50 ?? 49 89 40 ?? 48 8B 45 ?? 49 89 40 ?? 48 8B 45 ?? 49 89 40 ?? 48 8B 45 ?? 49 89 40 ?? 4C 89 4D ?? 48 89 5D ?? 48 89 4D ?? 48 89 55 ?? EB ?? 48 8B 55 ?? 48 8B 4D ?? 48 8B 5D ?? 4C 8B 4D ?? 48 85 D2 74 ?? 66 66 66 0F 1F 84 00 ?? ?? ?? ?? 48 83 EA ?? 48 0F 44 CE 48 85 D2 75 ?? 48 89 4D ?? 48 89 55 ?? 48 85 DB 74 ?? 66 0F 1F 44 00 ?? 49 8B 4C D9 ?? 48 FF CB 48 85 C9 74 ?? BA ?? ?? ?? ?? 41 B8 ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8B 4D ?? 48 85 DB 75 ?? 48 8B 5D ?? 4D 85 C9 74 ?? 48 8D 14 DD ?? ?? ?? ?? 41 B8 ?? ?? ?? ?? 49 8B C9 E8 ?? ?? ?? ?? 48 8B 4D ?? BA ?? ?? ?? ?? 41 B8 ?? ?? ?? ?? 48 89 75 ?? 48 89 75 ?? 48 89 75 ?? E8 ?? ?? ?? ?? 40 38 B7 ?? ?? ?? ?? 75 ?? 48 8B 05 ?? ?? ?? ?? 8B 97 ?? ?? ?? ?? 48 8B 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 ?? 48 8B D7 48 8B C8 E8 ?? ?? ?? ?? 48 8B CF E8 ?? ?? ?? ?? 48 8B 5C 24 ?? 89 B7 ?? ?? ?? ?? 48 8B 74 24 ?? 48 8B 7C 24 ?? 48 83 C4 ?? 5D C3"
        )]
        public partial void Reset();

        /// <summary>
        ///     把表的行加入队列并向 Excel 模块注册该等待器。
        /// </summary>
        [MemberFunction
        (
            "40 53 57 41 56 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 8B D9 8B 91 ?? ?? ?? ?? 48 8B 88 ?? ?? ?? ?? E8 ?? ?? ?? ?? 4C 8B F0 48 85 C0 75 ?? 48 8B 8B ?? ?? ?? ?? 8B BB ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 43 ?? 48 85 C0 74 ?? FF D0 39 BB ?? ?? ?? ?? 0F 85 ?? ?? ?? ?? 48 8B CB 48 83 C4 ?? 41 5E 5F 5B E9 ?? ?? ?? ?? FF 83 ?? ?? ?? ?? 48 8B CB 8B 93 ?? ?? ?? ?? E8 ?? ?? ?? ?? 80 BB ?? ?? ?? ?? ?? 0F 85 ?? ?? ?? ?? 48 89 6C 24 ?? 49 8B CE 48 89 74 24 ?? 4C 89 64 24 ?? 4C 89 7C 24 ?? E8 ?? ?? ?? ?? 48 8B 4B ?? 8B E8 8B 83 ?? ?? ?? ?? 48 2B C8 48 3B CD 48 0F 42 E9 48 8B CB E8 ?? ?? ?? ?? 44 8B C5 33 D2 48 8B CB E8 ?? ?? ?? ?? 4C 8B 7B ?? 33 FF 4C 8B 63 ?? 8B F7 48 85 ED 74 ?? 90 48 8B 43 ?? 48 85 C0 74 ?? 48 8B 00 EB ?? 48 8B C7 48 8B 48 ?? 8B D6 48 8B 40 ?? 48 FF C9 48 23 4B ?? 48 8B 0C C8 44 8B 01 48 8B CB E8 ?? ?? ?? ?? 48 FF 43 ?? FF C6 8B C6 48 3B C5 72 ?? 48 8B 74 24 ?? 48 8B 6C 24 ?? 4C 89 7B ?? 4C 8B 7C 24 ?? 4C 89 63 ?? 4C 8B 64 24 ?? EB ?? 33 FF 89 7B ?? 48 8B 43 ?? 48 85 C0 74 ?? 48 8B 38 48 8B 4F ?? 33 D2 48 8B 47 ?? 48 FF C9 48 23 4B ?? 48 8B 0C C8 44 8B 01 48 8B CB E8 ?? ?? ?? ?? 48 8B D3 49 8B CE 48 83 C4 ?? 41 5E 5F 5B E9 ?? ?? ?? ?? 48 83 C4 ?? 41 5E 5F 5B C3"
        )]
        public partial void Prepare();
    }
}
