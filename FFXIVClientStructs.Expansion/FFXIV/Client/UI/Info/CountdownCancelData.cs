namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     服务器发送的战斗倒计时取消数据。
/// </summary>
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 56)]
public partial struct CountdownCancelData
{
    /// <summary>
    ///     取消者的账号 ID，用于屏蔽名单名称处理。
    /// </summary>
    [FieldOffset(0)]
    public ulong AccountID;

    /// <summary>
    ///     取消者的角色内容 ID，用于屏蔽名单名称处理。
    /// </summary>
    [FieldOffset(8)]
    public ulong ContentID;

    /// <summary>
    ///     取消者的实体 ID。
    /// </summary>
    [FieldOffset(16)]
    public uint EntityID;

    /// <summary>
    ///     取消者的原始服务器 ID。
    /// </summary>
    [FieldOffset(20)]
    public ushort HomeWorldID;

    /// <summary>
    ///     无法从屏蔽名单、小队成员或场景角色取得名称时使用的取消者名称。
    /// </summary>
    [FieldOffset(24)]
    [FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;
}
