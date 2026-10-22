using NativeAtkResNode = FFXIVClientStructs.FFXIV.Component.GUI.AtkResNode;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<NativeAtkResNode>]
[StructLayout(LayoutKind.Explicit, Size = 0xC0)]
public unsafe partial struct AtkResNode
{
    /// <summary>
    ///     按锚点标志把尺寸变更传播到本节点、其子节点与兄弟节点。
    /// </summary>
    /// <param name="oldWidth">变更前的宽度。</param>
    /// <param name="oldHeight">变更前的高度。</param>
    /// <param name="newWidth">变更后的宽度。</param>
    /// <param name="newHeight">变更后的高度。</param>
    /// <param name="forceResize">强制把本节点尺寸直接替换为新尺寸，跳过锚点计算。</param>
    [MemberFunction("40 53 41 57 48 83 EC ?? F3 0F 10 15")]
    public partial void ApplyResizeAnchors
    (
        ushort oldWidth,
        ushort oldHeight,
        ushort newWidth,
        ushort newHeight,
        bool   forceResize
    );
}
