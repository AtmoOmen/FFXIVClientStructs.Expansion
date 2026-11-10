using FFXIVClientStructs.Expansion.Generators.Extensions;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

public sealed partial class InteropGenerator
{
    private static string RebindInheritedType(string type, StructInfo inheritedStruct, string targetType)
    {
        var inheritedType = $"global::{inheritedStruct.FullyQualifiedMetadataName}";
        if (type.IndexOf(inheritedType, StringComparison.Ordinal) < 0)
            return type;

        var syntax = SyntaxFactory.ParseTypeName(type);
        var references = syntax.DescendantNodesAndSelf().OfType<NameSyntax>()
                               .Where
                               (name => name.ToString() == inheritedType &&
                                        !(name.Parent is QualifiedNameSyntax qualifiedName && qualifiedName.Left == name)
                               );
        return syntax.ReplaceNodes
        (
            references,
            (_, _) => SyntaxFactory.ParseName(targetType)
        ).ToString();
    }

    private static MethodInfo RebindInheritedMethod(MethodInfo method, StructInfo inheritedStruct, string targetType) =>
        method with
        {
            ReturnType = RebindInheritedType(method.ReturnType, inheritedStruct, targetType)
        };

    private static string ConvertInheritedValue(string expression, string sourceType, string targetType)
    {
        if (sourceType == targetType)
            return expression;

        if (SyntaxFactory.ParseTypeName(targetType) is PointerTypeSyntax or FunctionPointerTypeSyntax)
            return $"({targetType}){expression}";

        if (SyntaxFactory.ParseTypeName(sourceType) is QualifiedNameSyntax
            {
                Left: var containingNamespace,
                Right: GenericNameSyntax { Identifier.ValueText: "Span" or "ReadOnlySpan", TypeArgumentList.Arguments: var sourceArguments }
            }                                                  &&
            containingNamespace.ToString() == "global::System" &&
            SyntaxFactory.ParseTypeName(targetType) is QualifiedNameSyntax
            {
                Right: GenericNameSyntax { TypeArgumentList.Arguments: var targetArguments }
            })
            return $"global::System.Runtime.InteropServices.MemoryMarshal.Cast<{sourceArguments[0]}, {targetArguments[0]}>({expression})";

        return $"global::System.Runtime.CompilerServices.Unsafe.BitCast<{sourceType}, {targetType}>({expression})";
    }

    private static void RenderInheritedMethodForwarder
    (
        StructInfo         structInfo,
        StructInfo         inheritedStruct,
        MethodInfo         method,
        string             path,
        IndentedTextWriter writer
    )
    {
        var forwardedMethod = RebindInheritedMethod(method, inheritedStruct, $"global::{structInfo.FullyQualifiedMetadataName}");
        var arguments = string.Join
        (
            ", ",
            method.Parameters.Select
            (parameter => (parameter.RefKind == RefKind.RefReadOnlyParameter ?
                               "in " :
                               parameter.RefKind.GetStringPrefix()) +
                          parameter.Name
            )
        );
        var receiver = method.IsStatic ?
                           inheritedStruct.FullyQualifiedMetadataName :
                           path;
        var invocation = $"{receiver}.{method.Name}({arguments})";
        var result     = ConvertInheritedValue(invocation, method.ReturnType, forwardedMethod.ReturnType);

        writer.WriteLine($"{forwardedMethod.GetDeclarationStringWithoutPartial()} => {result};");
    }
}
