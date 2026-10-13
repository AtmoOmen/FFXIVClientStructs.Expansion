namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record OwnMemberInfo
(
    string Name,
    string TypeName,
    int    FieldOffset,
    bool   IsMethod,
    string MethodSignature
);
