using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.UI;

/// <summary>
///     工房休息日设置界面的选择数量校验入口。
/// </summary>
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 688)]
public unsafe partial struct AddonMJICraftScheduleMaintenance
{
    /// <summary>
    ///     根据十四个休息日复选框更新确定按钮的启用状态。
    /// </summary>
    [MemberFunction("48 8B 81 ?? ?? ?? ?? 4C 8B D1 8B 90")]
    public partial void UpdateConfirmButton();
}
