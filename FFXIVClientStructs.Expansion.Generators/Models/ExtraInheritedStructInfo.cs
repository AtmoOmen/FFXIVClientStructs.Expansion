using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Helpers;

namespace FFXIVClientStructs.Expansion.Generators.Models;

internal sealed record ExtraInheritedStructInfo
(
    EquatableArray<FieldInfo>    PublicFields,
    EquatableArray<MethodInfo>   PublicMethods,
    EquatableArray<PropertyInfo> PublicProperties,
    EquatableArray<MethodInfo>   MemberFunctionDelegates
)
{
    /// <summary>
    ///     外部继承结构中具有公开成员函数指针属性的方法。
    /// </summary>
    public EquatableArray<MethodInfo> MemberFunctionPointers { get; init; } = ImmutableArray<MethodInfo>.Empty;
}
