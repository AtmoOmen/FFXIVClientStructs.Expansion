using System.Text;
using FFXIVClientStructs.FFXIV.Client.UI.Info;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[InfoProxy(InfoProxyId.FreeCompany)]
[StructLayout(LayoutKind.Explicit, Size = 0x6E8)]
public unsafe partial struct InfoProxyFreeCompany
{
    [FieldOffset(122)]
    public ushort TotalMembers;

    [FieldOffset(320), FixedSizeArray]
    internal FixedSizeArray16<RankData> RanksStorage;

    public RankData* GetRankData
    (
        byte rankIndex
    )
    {
        if (rankIndex >= RANK_COUNT) return null;

        fixed (InfoProxyFreeCompany* instance = &this)
            return &instance->RanksStorage[rankIndex];
    }

    public bool IsRankAssignable
    (
        byte rankIndex
    )
    {
        if (rankIndex >= RANK_COUNT - 1) return false;

        var rank = GetRankData(rankIndex);
        return rank != null && rank->Name[0] != 0;
    }

    public string GetRankNameText
    (
        byte rankIndex
    )
    {
        var rank = GetRankData(rankIndex);
        if (rank == null) return string.Empty;

        var name = GetRankName(rank);
        return name == null ?
                   string.Empty :
                   Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(name));
    }

    public string GetMemberRankNameText
    (
        uint memberExtraFlags
    )
        => GetRankNameText((byte)(memberExtraFlags >> 12));

    public List<(byte RankIndex, string Name)> GetAssignableRanks()
    {
        List<(byte, string)> ranks = [];

        for (byte i = 0; i < RANK_COUNT; i++)
        {
            if (!IsRankAssignable(i)) continue;

            var name = GetRankNameText(i);
            if (string.IsNullOrWhiteSpace(name)) continue;

            ranks.Add((i, name));
        }

        return ranks;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 88)]
    public partial struct RankData
    {
        [FieldOffset(32)]
        public ushort MemberCount;

        [FieldOffset(34)]
        public byte RankNumber;

        [FieldOffset(35), FixedSizeArray(isString: true)]
        internal FixedSizeArray16<byte> NameStorage;
    }

    #region 常量

    private const int RANK_COUNT = 16;

    #endregion

    [MemberFunction("48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 85 D2 75")]
    private partial byte* GetRankName
    (
        RankData* rank
    );
}
