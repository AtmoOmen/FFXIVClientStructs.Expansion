using NativeAtkUnitBase = FFXIVClientStructs.FFXIV.Component.GUI.AtkUnitBase;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop(isInherited: true)]
[Inherits<NativeAtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x238)]
public unsafe partial struct AtkUnitBase
{
    [FieldOffset(0x8), FixedSizeArray(isString: true)]
    internal FixedSizeArray32<byte> NameStorage;
    
    [FieldOffset(0xE0), FixedSizeArray]
    internal FixedSizeArray2<Pointer<AtkResNode>> AdditionalMoveableNodesStorage;
    
    /// <summary>
    ///     补充窗口范围预筛选的节点，仅检查各节点自身的矩形，不遍历子节点。
    /// </summary>
    [FieldOffset(0x108), FixedSizeArray]
    internal FixedSizeArray2<Pointer<AtkResNode>> AdditionalFocusableNodesStorage;

    [BitField<bool>(nameof(OnlyHideWhenFireCallback), 0)]
    [BitField<bool>(nameof(IsMoving),                 3)]
    [FieldOffset(416)]
    public byte Flags1A0;
    [BitField<bool>(nameof(HideAfterReady), 1)]
    [FieldOffset(0x1A1)]
    public byte Flags1A1;
    [BitField<bool>(nameof(HasChildAddons), 7)]
    [FieldOffset(0x1A2)]
    public byte Flags1A2;
    [BitField<bool>(nameof(DisableWindowCollisionNodeInteraction), 6)]
    [FieldOffset(419)]
    public byte Flags1A3;

    /// <summary>
    ///     获取界面是否正处于原生移动流程。
    /// </summary>
    /// <remarks>
    ///     移动期间跳过窗口及附加聚焦节点的范围预筛选；结束移动或隐藏界面时由客户端清除。
    /// </remarks>
    public partial bool IsMoving { get; }

    /// <summary>
    ///     禁止窗口碰撞节点接收鼠标输入，仅在命中其以外的交互节点时选中界面。
    /// </summary>
    public partial bool DisableWindowCollisionNodeInteraction { get; set; }

    [FieldOffset(0x1B8)]
    private byte addonParamUnknown1;
    [FieldOffset(0x1BB)]
    private byte unk1BB;
    [FieldOffset(0x1EF)]
    private bool unk1EF;

    [FieldOffset(0x1FC), FixedSizeArray]
    internal FixedSizeArray5<OperationGuide> OperationGuidesStorage;

    [MemberFunction("66 83 B9 ?? ?? ?? ?? ?? 75 ?? 66 83 B9 ?? ?? ?? ?? ?? 75 ?? F6 81")]
    public partial bool IsStandaloneAddon();

    /// <summary>
    ///     在界面完成节点设置、默认定位及坐标回写后执行初始化完成回调。
    /// </summary>
    /// <param name="valueCount">初始化参数的数量。</param>
    /// <param name="values">初始化参数数组。</param>
    [VirtualFunction(50)]
    public partial void OnSetupComplete
    (
        uint      valueCount,
        AtkValue* values
    );

    /// <summary>
    ///     直接更新界面及根节点的缩放，不写入界面配置。
    /// </summary>
    /// <param name="scale">相对于全局界面缩放的倍率。</param>
    /// <returns>成功更新缩放时返回 true；禁止缩放、启用全局缩放模式或根节点不存在时返回 false。</returns>
    [MemberFunction("48 83 EC ?? 8B 91 ?? ?? ?? ?? 8B C2")]
    public partial bool SetScaleWithoutConfiguration
    (
        float scale
    );
}
