using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Agent;

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
