using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record ExtraInheritedStructInfo(
    EquatableArray<FieldInfo> PublicFields,
    EquatableArray<MethodInfo> PublicMethods,
    EquatableArray<PropertyInfo> PublicProperties);
