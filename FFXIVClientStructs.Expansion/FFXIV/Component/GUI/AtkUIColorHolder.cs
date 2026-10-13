namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[GenerateInterop]
[Inherits<FFXIVClientStructs.FFXIV.Component.GUI.AtkUIColorHolder>]
[StructLayout(LayoutKind.Explicit, Size = 160)]
public unsafe partial struct AtkUIColorHolder
{
    /// <summary>
    /// 根据已经加载的 UIColor 表重建颜色与主题颜色。调用前必须确保颜色表已经就绪。
    /// </summary>
    [MemberFunction("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 54 41 55 41 56 41 57 48 83 EC ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 44 24 ?? 48 8B 19")]
    public partial void LoadColors();

    [MemberFunction("E8 ?? ?? ?? ?? B0 ?? C6 83 ?? ?? ?? ?? ?? 48 8B 5C 24 ?? 48 8B 74 24 ?? 48 83 C4 ?? 5F C3 80 BB")]
    public partial void SetActiveColorThemeType
    (
        byte themeType
    );
}
