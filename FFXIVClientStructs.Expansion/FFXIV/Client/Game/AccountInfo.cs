using FFXIVClientStructs.STD;

namespace FFXIVClientStructs.Expansion.FFXIV.Client.Game;

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 48)]
public unsafe partial struct AccountInfo
{
    [FieldOffset(0)]
    public AccountInfoState State;

    [FieldOffset(8)]
    public ulong AccountID;

    [FieldOffset(16)]
    public StdVector<AccountInfoCharacter> Characters;

    [FieldOffset(40)]
    public byte IsSaving;

    [FieldOffset(41)]
    public byte SaveResult;

    [MemberFunction("48 8B 05 ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC 83 39")]
    public static partial AccountInfo* Instance();
}
