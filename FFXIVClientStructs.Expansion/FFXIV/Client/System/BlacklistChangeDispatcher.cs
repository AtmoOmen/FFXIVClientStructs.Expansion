namespace FFXIVClientStructs.Expansion.FFXIV.Client.System;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 8)]
public unsafe partial struct BlacklistChangeDispatcher
{
    [VirtualFunction(1)]
    public partial void DispatchChange
    (
        BlacklistChange* change
    );
}
