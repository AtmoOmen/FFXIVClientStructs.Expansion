using FFXIVClientStructs.Expansion.Generators.Helpers;
using Microsoft.CodeAnalysis;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal record BitFieldInfo(
    string FieldName,
    string Name,
    string Type,
    string BackingType,
    int Index,
    int Length,
    Accessibility Accessibility,
    bool IsPartial,
    bool HasGetter,
    bool HasSetter,
    bool IsReadonlyGetter,
    EquatableArray<string> InheritableAttributes
);
