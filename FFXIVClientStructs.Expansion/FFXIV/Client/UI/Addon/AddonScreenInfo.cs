using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI.Addon;

/// <summary>
///     屏幕信息窗口的公共基类（AddonScreenInfo）。<br />
///     窗口包含一个时间线节点、一个主图标节点与若干附加图标节点，按屏幕尺寸缩放并定位；
///     图标加载完成后通过回调列表通知窗口，全部加载完成前窗口不参与绘制。
/// </summary>
[GenerateInterop(isInherited: true)]
[Inherits<AddonScreenInfoChild>]
[StructLayout(LayoutKind.Explicit, Size = 680)]
public unsafe partial struct AddonScreenInfo
{
    /// <summary>
    ///     构造时取得的界面舞台单例。
    /// </summary>
    [FieldOffset(576)]
    public AtkStage* Stage;

    /// <summary>
    ///     构造时清零、已分析的窗口方法均未读写的保留值。
    /// </summary>
    [FieldOffset(584)]
    public uint Unk248;

    /// <summary>
    ///     窗口所属的父窗口，销毁时清除父窗口槽位中记录的自身指针，父窗口不可见时不参与绘制。
    /// </summary>
    [FieldOffset(592)]
    public AtkUnitBase* ParentAddon;

    /// <summary>
    ///     窗口在父窗口槽位中的索引，-1 表示尚无槽位。
    /// </summary>
    [FieldOffset(600)]
    public int ParentAddonSlot;

    /// <summary>
    ///     构造时置为 -1 的保留值。
    /// </summary>
    [FieldOffset(604)]
    public int Unk25C;

    /// <summary>
    ///     节点 1，作为窗口缩放与定位的参照节点。
    /// </summary>
    [FieldOffset(608)]
    public AtkResNode* ReferenceNode;

    /// <summary>
    ///     承载入场、退场与闪烁动画的时间线节点；位置因窗口而异，取节点 1 或节点 2。
    /// </summary>
    [FieldOffset(616)]
    public AtkResNode* TimelineNode;

    /// <summary>
    ///     图标加载完成回调的上下文参数，同时作为界面值第 8 项缓存；不小于 0 时触发界面事件 22 与 47。
    /// </summary>
    [FieldOffset(624)]
    public int IconContext;

    /// <summary>
    ///     参照节点的宽度，用于按屏幕尺寸缩放窗口。
    /// </summary>
    [FieldOffset(628)]
    public int Width;

    /// <summary>
    ///     参照节点的高度，用于按屏幕尺寸缩放窗口。
    /// </summary>
    [FieldOffset(632)]
    public int Height;

    /// <summary>
    ///     主图标节点，位置因窗口而异，取节点 2 或节点 6。
    /// </summary>
    [FieldOffset(640)]
    public AtkImageNode* MainImageNode;

    /// <summary>
    ///     等待纹理加载完成的图标列表。
    /// </summary>
    [FieldOffset(648)]
    public TextureLoadList PendingTextureLoads;

    /// <summary>
    ///     构造时清零、已分析的窗口方法均未读写的保留值。
    /// </summary>
    [FieldOffset(672)]
    public uint Unk2A0;

    /// <summary>
    ///     等待纹理加载完成的图标列表。<br />
    ///     列表对象前八个字节为分配器，节点指针与节点数量分别位于其后。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public unsafe struct TextureLoadList
    {
        /// <summary>
        ///     图标加载列表。
        /// </summary>
        [FieldOffset(8)]
        public StdList<TextureLoadCallback> Entries;
    }

    /// <summary>
    ///     单个图标的纹理加载回调。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public unsafe struct TextureLoadCallback
    {
        /// <summary>
        ///     等待纹理加载的图标节点。
        /// </summary>
        [FieldOffset(0)]
        public AtkImageNode* ImageNode;

        /// <summary>
        ///     图标节点当前使用的纹理资源。
        /// </summary>
        [FieldOffset(8)]
        public AtkUldAsset* Asset;

        /// <summary>
        ///     纹理加载完成后调用的回调函数地址，参数依次为图标节点、纹理资源与上下文。
        /// </summary>
        [FieldOffset(16)]
        public nint Callback;

        /// <summary>
        ///     回调的上下文参数，即窗口自身。
        /// </summary>
        [FieldOffset(24)]
        public ulong Context;
    }
}
