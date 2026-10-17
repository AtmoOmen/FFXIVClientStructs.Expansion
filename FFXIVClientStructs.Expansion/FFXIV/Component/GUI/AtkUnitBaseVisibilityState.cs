namespace FFXIVClientStructs.Expansion.FFXIV.Component.GUI;

[Flags]
public enum AtkUnitBaseVisibilityState : byte
{
    None = 0,

    TransitionComplete = 1 << 0,

    Show = 1 << 1,

    Hide = 1 << 2
}
