namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Info;

/// <summary>
///     服务器发送的战斗倒计时数据。
/// </summary>
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 64)]
public partial struct CountdownStartData
{
    /// <summary>
    ///     发起者的账号 ID，用于屏蔽名单名称处理。
    /// </summary>
    [FieldOffset(0)]
    public ulong AccountID;

    /// <summary>
    ///     发起者的角色内容 ID，用于屏蔽名单名称处理。
    /// </summary>
    [FieldOffset(8)]
    public ulong ContentID;

    /// <summary>
    ///     发起者的实体 ID。
    /// </summary>
    [FieldOffset(16)]
    public uint EntityID;

    /// <summary>
    ///     发起者的原始服务器 ID，用于聊天消息中的角色名称。
    /// </summary>
    [FieldOffset(20)]
    public ushort HomeWorldID;

    /// <summary>
    ///     倒计时持续时间，单位为秒。
    /// </summary>
    [FieldOffset(22)]
    public ushort DurationSeconds;

    /// <summary>
    ///     是否因处于战斗状态而拒绝开始倒计时。
    /// </summary>
    [FieldOffset(24)]
    public bool IsCombatBlocked;

    /// <summary>
    ///     是否由文本指令发起，用于选择剩余时间的聊天消息类型。
    /// </summary>
    [FieldOffset(25)]
    public bool IsTextCommand;

    /// <summary>
    ///     是否由任务记录准备确认发起，并关闭仍在进行的普通准备确认界面。
    /// </summary>
    [FieldOffset(26)]
    public bool IsRecordReadyCheck;

    /// <summary>
    ///     无法从小队成员或屏蔽名单取得名称时使用的发起者名称。
    /// </summary>
    [FieldOffset(27)]
    [FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;
}
