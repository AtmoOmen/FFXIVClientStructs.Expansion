using NativeAtkTextNode = FFXIVClientStructs.FFXIV.Component.GUI.AtkTextNode;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<NativeAtkTextNode>]
[StructLayout(LayoutKind.Explicit, Size = 384)]
public unsafe partial struct AtkTextNode
{
    /// <summary>
    /// 释放旧字体缓存，并根据当前文字和绘制参数重新生成缓存。
    /// </summary>
    [MemberFunction("48 85 C9 0F 84 ?? ?? ?? ?? 56 48 83 EC ?? 48 89 5C 24 ?? 48 8B F1")]
    public partial void RebuildFontCache();
}
