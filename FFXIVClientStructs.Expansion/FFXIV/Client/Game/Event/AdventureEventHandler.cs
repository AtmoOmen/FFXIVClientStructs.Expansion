using FFXIVClientStructs.FFXIV.Client.Game.Event;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.LayoutEngine;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using NativeCharacter = FFXIVClientStructs.FFXIV.Client.Game.Character.Character;
using NativeEventHandler = FFXIVClientStructs.FFXIV.Client.Game.Event.EventHandler;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game.Event;

/// <summary>
///     探索笔记的事件处理器，负责维护单条探索笔记的目的地事件范围、天气与时间条件以及记录状态。
///     事件标识的内容 ID 为 0x21，其条目 ID 即 Adventure 表的行号。
/// </summary>
[GenerateInterop(isInherited: true)]
[Inherits<NativeEventHandler>]
[StructLayout(LayoutKind.Explicit, Size = 480)]
public unsafe partial struct AdventureEventHandler
{
    /// <summary>
    ///     状态标志位。<see cref="IsRecordable" /> 表示该条探索笔记满足全部前置条件且尚未记录；
    ///     <see cref="IsUnlocked" /> 表示该条探索笔记所属阶段的解锁条件已满足；
    ///     <see cref="IsWithinTimeWindow" /> 表示当前艾欧泽亚时间处于 <see cref="MinTime" /> 与 <see cref="MaxTime" /> 构成的时间窗口内。
    /// </summary>
    [BitField<bool>(nameof(IsRecordable),       0)]
    [BitField<bool>(nameof(IsUnlocked),         1)]
    [BitField<bool>(nameof(IsWithinTimeWindow), 2)]
    [FieldOffset(440)]
    public byte Flags;

    /// <summary>
    ///     允许记录的艾欧泽亚时间窗口起始时刻，格式为 HHMM。
    /// </summary>
    [FieldOffset(442)]
    public ushort MinTime;

    /// <summary>
    ///     允许记录的艾欧泽亚时间窗口结束时刻，格式为 HHMM。
    /// </summary>
    [FieldOffset(444)]
    public ushort MaxTime;

    /// <summary>
    ///     记录该条探索笔记所需的最低等级，取自 Adventure 表的 MaxLevel 列。
    /// </summary>
    [FieldOffset(446)]
    public byte MaxLevel;

    /// <summary>
    ///     天气分类行号，取自 Adventure 表的 WeatherCategory 列；非零时以该行配置的天气集合判断天气条件。
    /// </summary>
    [FieldOffset(447)]
    public byte WeatherCategory;

    /// <summary>
    ///     天气行号，取自 Adventure 表的 Weather 列；<see cref="WeatherCategory" /> 为零时直接与当前天气比较。
    /// </summary>
    [FieldOffset(448)]
    public byte Weather;

    /// <summary>
    ///     该条探索笔记所属的资料片阶段行号，取自 AdventureExPhase 表，用于套用该阶段配置的前置任务。
    /// </summary>
    [FieldOffset(452)]
    public int AdventureExPhaseRowID;

    /// <summary>
    ///     记录该条探索笔记所需使用的情感动作行号，取自 Adventure 表的 Emote 列。
    /// </summary>
    [FieldOffset(456)]
    public ushort EmoteRowID;

    /// <summary>
    ///     该条探索笔记目的地的事件范围行号，取自 Adventure 表的 Level 列，对应 LayoutWorld 中 EventRange 类型的布局实例标识。
    /// </summary>
    [FieldOffset(460)]
    public uint LevelRowID;

    /// <summary>
    ///     Adventure 表的数据表对象。
    /// </summary>
    [FieldOffset(464)]
    public ExcelSheet* AdventureSheet;

    /// <summary>
    ///     等待 Adventure 表加载完成的等待器；加载完成后由回调填充各字段并自行释放。
    /// </summary>
    [FieldOffset(472)]
    public ExcelSheetWaiter* AdventureSheetWaiter;

    /// <summary>
    ///     加载 Adventure 表并注册数据表等待器，随后把事件处理器标记为已初始化。
    /// </summary>
    /// <returns>事件处理器的状态标志字节。</returns>
    [VirtualFunction(2)]
    public partial byte Setup();

    /// <summary>
    ///     检查玩家是否满足记录条件；传入非零情感动作行号时，同时校验动作是否匹配以及玩家是否已进入目的地事件范围。
    /// </summary>
    /// <param name="gameObject">执行情感动作的目标对象。</param>
    /// <param name="emoteID">待校验的情感动作行号；为零时不校验动作与目的地事件范围。</param>
    /// <returns>校验通过时返回 true；本方法不执行情感动作，也不写入记录。</returns>
    [VirtualFunction(9)]
    public partial bool TryPerformEmote
    (
        GameObject* gameObject,
        ushort      emoteID
    );

    /// <summary>
    ///     玩家进入该条探索笔记的目的地范围时触发，提示已到达目的地并通知天气与时间条件是否满足。
    /// </summary>
    /// <param name="layoutInstance">进入的 EventRange 布局实例。</param>
    /// <returns>始终返回 false。</returns>
    [VirtualFunction(15)]
    public partial bool OnEnterEventRange
    (
        ILayoutInstance* layoutInstance
    );

    /// <summary>
    ///     玩家离开该条探索笔记的目的地范围时触发，提示已离开目的地。
    /// </summary>
    /// <param name="layoutInstance">离开的 EventRange 布局实例。</param>
    /// <returns>始终返回 false。</returns>
    [VirtualFunction(17)]
    public partial bool OnExitEventRange
    (
        ILayoutInstance* layoutInstance
    );

    /// <summary>
    ///     处理场景任务的阶段回调；当发起任务的事件处理器标识为 591557 时弹出探索笔记提示。
    /// </summary>
    /// <param name="taskHandler">发起该场景任务的事件处理器。</param>
    /// <param name="sceneGameObject">该事件处理器绑定的场景对象。</param>
    /// <param name="scene">该事件处理器的场景号。</param>
    /// <param name="sceneData">当前场景数据。</param>
    /// <param name="sceneDataCount">当前场景数据中的有效值数量。</param>
    [VirtualFunction(36)]
    public partial void ProcessSceneTask
    (
        NativeEventHandler* taskHandler,
        GameObject*         sceneGameObject,
        short               scene,
        SceneData*          sceneData,
        byte                sceneDataCount
    );

    /// <summary>
    ///     艾欧泽亚时间进入该条探索笔记的时间窗口时触发，启用对应的事件范围并刷新状态标志位。
    /// </summary>
    /// <param name="windowID">时间窗口标识。</param>
    /// <param name="userData">注册时间窗口时附加的用户数据。</param>
    /// <returns>始终返回 true，保持该时间窗口继续注册。</returns>
    [VirtualFunction(53)]
    public partial bool OnTimeWindowEnter
    (
        uint  windowID,
        void* userData
    );

    /// <summary>
    ///     艾欧泽亚时间离开该条探索笔记的时间窗口时触发，禁用对应的事件范围并刷新状态标志位。
    /// </summary>
    /// <param name="windowID">时间窗口标识。</param>
    /// <param name="userData">注册时间窗口时附加的用户数据。</param>
    /// <returns>始终返回 true，保持该时间窗口继续注册。</returns>
    [VirtualFunction(54)]
    public partial bool OnTimeWindowExit
    (
        uint  windowID,
        void* userData
    );

    /// <summary>
    ///     服务端下发进入事件范围通知时，判断是否接受该事件范围。
    /// </summary>
    /// <param name="gameObject">触发事件范围的对象。</param>
    /// <param name="instanceKey">事件范围布局实例的实例键。</param>
    /// <param name="noticeFlags">通知包附带的标志字节，值为 7 时拒绝接受该事件范围。</param>
    /// <returns>接受该事件范围时返回 true。</returns>
    [VirtualFunction(67)]
    public partial bool IsEventRangeAccepted
    (
        GameObject* gameObject,
        uint        instanceKey,
        byte        noticeFlags
    );

    /// <summary>
    ///     重新计算状态标志位，并依据结果启用或禁用目的地事件范围的对应状态。
    /// </summary>
    [VirtualFunction(223)]
    public partial void Update();

    /// <summary>
    ///     获取事件处理器所占用的条件 ID。
    /// </summary>
    /// <param name="localPlayer">本地玩家。</param>
    /// <param name="target">交互目标。</param>
    /// <param name="eventType">事件类型。</param>
    /// <param name="eventParam">事件参数。</param>
    /// <returns>事件类型为进入或离开事件范围时返回 0，其余情况交由基类处理。</returns>
    [VirtualFunction(232)]
    public partial uint GetOccupiedConditionID
    (
        NativeCharacter* localPlayer,
        NativeCharacter* target,
        byte             eventType,
        uint             eventParam
    );

    /// <summary>
    ///     注册时间窗口，并重新计算状态标志位与目的地事件范围的对应状态。
    /// </summary>
    [VirtualFunction(274)]
    public partial void Initialize();
}
