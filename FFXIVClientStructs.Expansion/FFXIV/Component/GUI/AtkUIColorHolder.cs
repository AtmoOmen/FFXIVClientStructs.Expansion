using NativeAtkUIColorHolder = FFXIVClientStructs.FFXIV.Component.GUI.AtkUIColorHolder;

namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<NativeAtkUIColorHolder>]
[StructLayout(LayoutKind.Explicit, Size = 160)]
public unsafe partial struct AtkUIColorHolder
{
    /// <summary>
    /// 根据已经加载的 UIColor 表重建颜色与主题颜色。调用前必须确保颜色表已经就绪。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B 19")]
    public partial void LoadColors();

    /// <summary>
    /// 设置活动颜色主题，并请求重新加载 UIColor 表。
    /// </summary>
    [MemberFunction("40 57 48 83 EC ?? 83 79 ?? ?? 48 8B F9 74 ?? 38 51")]
    public partial void SetActiveColorThemeType
    (
        byte themeType
    );
}
