namespace FFXIVClientStructs.Expansion.FFXIV.Client.System;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 24)]
public unsafe partial struct BlacklistManager
{
    [StaticAddress("48 8B 0D ?? ?? ?? ?? 48 8D 54 24 ?? 48 83 C1 ?? 48 8B 01 FF 50 ?? 48 8B 5C 24", 3, isPointer: true)]
    public static partial BlacklistManager* Instance();

    [FieldOffset(16)]
    public BlacklistChangeDispatcher ChangeDispatcher;
}
