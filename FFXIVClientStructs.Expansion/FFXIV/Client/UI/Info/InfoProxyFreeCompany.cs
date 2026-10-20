using System.Text;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using NativeInfoProxyFreeCompany = FFXIVClientStructs.FFXIV.Client.UI.Info.InfoProxyFreeCompany;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

[GenerateInterop]
[Inherits<NativeInfoProxyFreeCompany>]
[InfoProxy(InfoProxyId.FreeCompany)]
[StructLayout(LayoutKind.Explicit, Size = 0x6E8)]
public unsafe partial struct InfoProxyFreeCompany
{
    /// <summary>
    ///     当前军衔所持有的权限位图，共 128 位，按权限索引逐位存放。
    /// </summary>
    [FieldOffset(72)]
    [FixedSizeArray]
    internal FixedSizeArray16<byte> PermissionBitsStorage;

    /// <summary>
    ///     部队状态，0 表示未加入任何部队，1 至 3 表示已加入。
    /// </summary>
    [FieldOffset(104)]
    public byte FreeCompanyState;

    /// <summary>
    ///     <see cref="PermissionBits" /> 是否已从服务器取得，为 <see langword="false" /> 时权限查询会退化为按权限索引的默认判定。
    /// </summary>
    [FieldOffset(106)]
    public bool ArePermissionsLoaded;

    /// <summary>
    ///     已加入部队并完成初始化，由部队信息变更事件置位。
    /// </summary>
    [FieldOffset(179)]
    public bool IsJoined;

    /// <summary>
    ///     部队信息事件处理器链表。
    /// </summary>
    [FieldOffset(1736)]
    public InfoEventHandlerList EventHandlers;

    [FieldOffset(320)]
    [FixedSizeArray]
    internal FixedSizeArray16<RankData> RanksStorage;

    /// <summary>
    ///     查询当前军衔是否被授予指定权限。
    /// </summary>
    /// <param name="permissionIndex">权限索引，取值 0 至 127。</param>
    /// <returns>当前军衔是否被授予该权限。</returns>
    [MemberFunction("4C 8B C1 0F B6 CA")]
    public partial bool IsPermissionGranted
    (
        byte permissionIndex
    );

    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 48 89 7C 24 ?? 41 56 48 83 EC ?? 48 8B BC 24 ?? ?? ?? ?? 45 0F B7 F1")]
    public partial bool RequestInvitePlayer
    (
        ulong          accountID,
        ulong          contentID,
        ushort         worldID,
        CStringPointer name
    );

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
        return name.Value == null ?
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

        [FieldOffset(35)]
        [FixedSizeArray(true)]
        internal FixedSizeArray16<byte> NameStorage;
    }

    #region 常量

    private const int RANK_COUNT = 16;

    #endregion

    [MemberFunction("48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 85 D2 75")]
    private partial CStringPointer GetRankName
    (
        RankData* rank
    );
}
