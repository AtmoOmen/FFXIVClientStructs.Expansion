using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record StringOverloadInfo(
    MethodInfo MethodInfo,
    EquatableArray<string> IgnoredParameters,
    EquatableArray<string> InheritableAttributes);
