using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

[Agent(AgentId.WeeklyBingo)]
[GenerateInterop]
[Inherits<FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentWeeklyBingo>]
[StructLayout(LayoutKind.Explicit, Size = 704)]
public unsafe partial struct AgentWeeklyBingo
{
    /// <summary>
    /// 当前打开的 WeeklyBingoContentList 窗口 ID，为 0 时表示窗口未打开。
    /// </summary>
    [FieldOffset(116)]
    public uint ContentListAddonID;

    /// <summary>
    /// 当前操作的填空槽位索引。
    /// </summary>
    [FieldOffset(140)]
    public uint SelectedDutySlotIndex;

    /// <summary>
    /// WeeklyBingoContentList 顶部显示的标题。
    /// </summary>
    [FieldOffset(144)]
    public Utf8String ContentListTitle;

    /// <summary>
    /// 当前槽位可进入的副本列表。
    /// </summary>
    [FieldOffset(344)]
    public StdVector<WeeklyBingoContentListEntry> ContentList;

    /// <summary>
    /// 按 <see cref="SelectedDutySlotIndex"/> 指向的槽位打开副本；槽位对应多个副本时改为打开 WeeklyBingoContentList。
    /// </summary>
    [MemberFunction("40 55 41 55 48 8D AC 24 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 85 ?? ?? ?? ?? 8B 91")]
    public partial void OpenDutyOrContentList();

    /// <summary>
    /// 打开 WeeklyBingoContentList，展示 <see cref="ContentList"/> 中的副本。
    /// </summary>
    [MemberFunction("41 57 48 83 EC ?? 83 79 ?? ?? 4C 8B F9 0F 85")]
    public partial void OpenContentList();
    
    [StructLayout(LayoutKind.Explicit, Size = 216)]
    public unsafe struct WeeklyBingoContentListEntry
    {
        /// <summary>
        /// 副本对应的 ContentFinderCondition 行 ID。
        /// </summary>
        [FieldOffset(0)]
        public uint ContentFinderConditionID;

        /// <summary>
        /// 副本所需等级。
        /// </summary>
        [FieldOffset(4)]
        public ushort RequiredLevel;

        /// <summary>
        /// 副本在任务搜索器中的排序键，与所需等级一起用于列表排序。
        /// </summary>
        [FieldOffset(6)]
        public ushort SortKey;

        /// <summary>
        /// 列表左侧显示的等级文本。
        /// </summary>
        [FieldOffset(8)]
        public Utf8String LevelText;

        /// <summary>
        /// 列表右侧显示的副本名。
        /// </summary>
        [FieldOffset(112)]
        public Utf8String NameText;
    }
}
