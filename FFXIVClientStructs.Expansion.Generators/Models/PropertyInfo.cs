using FFXIVClientStructs.Expansion.Generators.Helpers;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record PropertyInfo(
    string Name,
    string Type,
    RefKind RefKind,
    bool Get,
    bool Set,
    EquatableArray<string> InheritableAttributes);
