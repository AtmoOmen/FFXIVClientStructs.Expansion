using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Extensions;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

public sealed partial class InteropGenerator
{
    private static string RenderInheritedStructInfo
    (
        StructInfo                 structInfo,
        ImmutableArray<StructInfo> inheritedStructs,
        string                     generatorNamespace,
        Dictionary<string, string> sharedPointerBases,
        CancellationToken          token
    )
    {
        using IndentedTextWriter writer = new();
        writer.WriteLine("");

        if (structInfo.Namespace.Length > 0)
        {
            writer.WriteLine($"namespace {structInfo.Namespace};");
            writer.WriteLine();
        }

        for (var i = structInfo.Hierarchy.Length - 1; i >= 0; i--)
        {
            writer.WriteLine($"unsafe partial struct {structInfo.Hierarchy[i]}");
            writer.WriteLine("{");
            writer.IncreaseIndent();
        }

        if (inheritedStructs.Any())
            RenderInheritance(structInfo, inheritedStructs, generatorNamespace, sharedPointerBases, token, writer);

        for (var i = 0; i < structInfo.Hierarchy.Length; i++)
        {
            writer.DecreaseIndent();
            writer.WriteLine("}");
        }

        return writer.ToString();
    }

    private static void RenderInheritance
    (
        StructInfo                 structInfo,
        ImmutableArray<StructInfo> inheritedStructs,
        string                     generatorNamespace,
        Dictionary<string, string> sharedPointerBases,
        CancellationToken          token,
        IndentedTextWriter         writer
    )
    {
        using ImmutableArrayBuilder<(StructInfo inheritedStruct, string path, int offset)> resolvedInheritanceOrderBuilder = new();

        var hasPrimaryVirtualFunctions = false;

        ResolveInheritanceOrder(structInfo, string.Empty, 0, 0, 0, inheritedStructs, resolvedInheritanceOrderBuilder, ref hasPrimaryVirtualFunctions);

        var resolvedInheritanceOrder = resolvedInheritanceOrderBuilder.ToImmutable();
        resolvedInheritanceOrder =
        [
            .. resolvedInheritanceOrder.Where(item => !item.inheritedStruct.IsExternal), .. resolvedInheritanceOrder.Where(item => item.inheritedStruct.IsExternal)
        ];

        token.ThrowIfCancellationRequested();

        HashSet<string>                    alreadyWrittenParents = [];
        HashSet<string>                    alreadyWrittenBases   = [];
        HashSet<(int Offset, string Name)> writtenFields         = [];
        HashSet<string>                    writtenMethods        = [];
        HashSet<string>                    writtenMembers        = [];

        foreach (var (inheritedStruct, path, offset) in resolvedInheritanceOrder)
        {
            if (structInfo.InheritedStructs.Any
                    (inheritanceInfo => inheritanceInfo.InheritedTypeName == inheritedStruct.FullyQualifiedMetadataName) &&
                !alreadyWrittenParents.Contains(inheritedStruct.FullyQualifiedMetadataName))
            {
                if (!HasOwnParentField(structInfo, inheritedStruct, offset))
                {
                    var fieldName = ResolveParentFieldName(structInfo, inheritedStruct, offset);
                    writer.WriteLine
                    (
                        $"""/// <summary>Inherited parent class accessor for <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see></summary>"""
                    );
                    writer.WriteLine("[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]");
                    writer.WriteLine
                    (
                        $"[global::System.Runtime.InteropServices.FieldOffsetAttribute({offset})] public {inheritedStruct.FullyQualifiedMetadataName} {fieldName};"
                    );
                }

                alreadyWrittenParents.Add(inheritedStruct.FullyQualifiedMetadataName);
            }

            foreach (var field in inheritedStruct.ExtraInheritedStructInfo!.PublicFields)
            {
                if (HasOwnMemberNameOrOffset(structInfo, field.Name, offset + field.Offset))
                    continue;
                if (!writtenFields.Add((offset + field.Offset, field.Name)))
                    continue;

                var adjustedFieldName = alreadyWrittenBases.Contains(inheritedStruct.FullyQualifiedMetadataName) ?
                                            $"{path.Replace(".", "_")}_{field.Name}" :
                                            field.Name;
                writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{field.Name}" />""");
                writer.WriteLine
                (
                    $"""/// <remarks>Field inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
                );

                foreach (var inheritedAttribute in field.InheritableAttributes)
                {
                    writer.WriteLine($"{inheritedAttribute}");
                }

                if (field.Type is "bool" or "global::System.Boolean" &&
                    !field.InheritableAttributes.Any
                        (attribute => attribute.StartsWith("[global::System.Runtime.InteropServices.MarshalAsAttribute", StringComparison.Ordinal)))
                {
                    writer.WriteLine("[global::System.Runtime.InteropServices.MarshalAsAttribute(global::System.Runtime.InteropServices.UnmanagedType.U1)]");
                }

                writer.WriteLine("[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]");
                writer.WriteLine
                (
                    $"[global::System.Runtime.InteropServices.FieldOffsetAttribute({offset + field.Offset})] public {(field.IsReadOnly ? "readonly " : "")}{field.Type} {adjustedFieldName};"
                );
            }

            alreadyWrittenBases.Add(inheritedStruct.FullyQualifiedMetadataName);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, path, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.MemberFunctions.IsEmpty)
                RenderInheritedMemberFunctions(structInfo, inheritedStruct, path, writer, writtenMethods);
        }

        token.ThrowIfCancellationRequested();
        if (hasPrimaryVirtualFunctions)
            RenderInheritedVirtualTable(structInfo, resolvedInheritanceOrder, writer);

        if (resolvedInheritanceOrder.Any
            (item => item.offset == 0 &&
                     (!item.inheritedStruct.MemberFunctions.IsEmpty ||
                      !(item.inheritedStruct.ExtraInheritedStructInfo?.MemberFunctionPointers.IsEmpty ?? true))
            ))
            RenderInheritedMemberFunctionPointers(structInfo, resolvedInheritanceOrder, generatorNamespace, sharedPointerBases, writer);

        var hasInheritedDelegates = resolvedInheritanceOrder.Any
        (item =>
             item.offset == 0 &&
             (!item.inheritedStruct.MemberFunctions.IsEmpty  ||
              !item.inheritedStruct.VirtualFunctions.IsEmpty ||
              !(item.inheritedStruct.ExtraInheritedStructInfo?.MemberFunctionDelegates.IsEmpty ?? true))
        );
        if (hasInheritedDelegates)
            RenderInheritedDelegateTypes(structInfo, resolvedInheritanceOrder, writer);

        foreach (var (inheritedStruct, path, offset) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.VirtualFunctions.IsEmpty)
                RenderInheritedVirtualFunctions(structInfo, inheritedStruct, path, offset, writer, writtenMethods);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, path, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.ExtraInheritedStructInfo!.PublicMethods.IsEmpty)
                RenderInheritedPublicMethods(structInfo, inheritedStruct, path, writer, writtenMethods);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, path, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.ExtraInheritedStructInfo!.PublicProperties.IsEmpty)
                RenderInheritedPublicProperties(structInfo, inheritedStruct, path, writer, writtenMembers);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, path, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.BitFields.IsEmpty)
                RenderInheritedBitFieldProperties(structInfo, inheritedStruct, path, writer, writtenMembers);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, _, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.StringOverloads.IsEmpty)
                RenderStringOverloads(inheritedStruct, writer);
        }

        token.ThrowIfCancellationRequested();

        foreach (var (inheritedStruct, path, _) in resolvedInheritanceOrder)
        {
            if (!inheritedStruct.FixedSizeArrays.IsEmpty)
                RenderInheritedFixedSizeArrayAccessors(structInfo, inheritedStruct, path, writer);
        }

        token.ThrowIfCancellationRequested();
    }

    private static int ResolveInheritanceOrder
    (
        StructInfo                                                                   structInfo,
        string                                                                       path,
        int                                                                          absoluteOffset,
        int                                                                          curParentOffset,
        int                                                                          index,
        ImmutableArray<StructInfo>                                                   inheritedStructs,
        ImmutableArrayBuilder<(StructInfo inheritedStruct, string path, int offset)> resolvedInheritanceOrder,
        ref bool                                                                     hasPrimaryVirtualFunctions
    )
    {
        var processed = 0;

        foreach (var inheritanceInfo in structInfo.InheritedStructs)
        {
            if (index >= inheritedStructs.Length)
            {
                return 0;
            }

            if (inheritanceInfo.ParentOffset != 0)
                absoluteOffset = curParentOffset + inheritanceInfo.ParentOffset;

            var currentStruct = inheritedStructs[index + processed];
            var baseName      = ResolveParentFieldName(structInfo, currentStruct, absoluteOffset);
            var newPath = path == string.Empty ?
                              baseName :
                              path + "." + baseName;

            processed += 1;

            processed += ResolveInheritanceOrder
            (
                currentStruct,
                newPath,
                absoluteOffset,
                absoluteOffset,
                index + processed,
                inheritedStructs,
                resolvedInheritanceOrder,
                ref hasPrimaryVirtualFunctions
            );

            resolvedInheritanceOrder.Add((currentStruct, newPath, absoluteOffset));
            if (absoluteOffset == 0 && !currentStruct.VirtualFunctions.IsEmpty)
                hasPrimaryVirtualFunctions = true;

            absoluteOffset += currentStruct.Size!.Value;
        }

        return processed;
    }

    private static void RenderInheritedMemberFunctions
        (StructInfo structInfo, StructInfo inheritedStruct, string path, IndentedTextWriter writer, HashSet<string> writtenMethods)
    {
        foreach (var (methodInfo, _) in inheritedStruct.MemberFunctions)
        {
            if (methodInfo.Name == "Ctor")
                continue;
            if (HasOwnMethod(structInfo, methodInfo))
                continue;
            if (!writtenMethods.Add(GetMethodKey(methodInfo)))
                continue;
            writer.WriteLine
                ($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{methodInfo.Name}({methodInfo.GetParameterTypeStringForCref()})" />""");
            writer.WriteLine
            (
                $"""/// <remarks>Method inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );
            writer.WriteLine
                ("[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");

            foreach (var inheritedAttribute in methodInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            if (methodInfo.IsStatic)
            {
                writer.WriteLine
                (
                    $"{methodInfo.GetDeclarationStringWithoutPartial()} => {methodInfo.ReturnTypeCast}{inheritedStruct.FullyQualifiedMetadataName}.{methodInfo.Name}({methodInfo.GetParameterNamesStringWithGeneric()});"
                );
            }
            else
            {
                writer.WriteLine
                    ($"{methodInfo.GetDeclarationStringWithoutPartial()} => {path}.{methodInfo.Name}({methodInfo.GetParameterNamesStringWithGeneric()});");
            }
        }
    }

    private static void RenderInheritedVirtualTable
        (StructInfo structInfo, ImmutableArray<(StructInfo inheritedStruct, string path, int offset)> resolvedInheritanceOrder, IndentedTextWriter writer)
    {
        if (!structInfo.HasVirtualTable())
            writer.WriteLine("[global::System.Runtime.InteropServices.StructLayoutAttribute(global::System.Runtime.InteropServices.LayoutKind.Explicit)]");
        writer.WriteLine($"public unsafe partial struct {structInfo.Name}VirtualTable");

        using (writer.WriteBlock())
        {
            foreach (var (inheritedStruct, _, offset) in resolvedInheritanceOrder)
            {
                if (offset != 0)
                    continue;

                foreach (var virtualFunctionInfo in inheritedStruct.VirtualFunctions)
                {
                    var functionPointerType =
                        $"delegate* unmanaged <{structInfo.Name}*, {virtualFunctionInfo.MethodInfo.GetParameterTypeStringWithTrailingTypeNoGenerics()}{virtualFunctionInfo.MethodInfo.ReturnTypeOrVoid}>";
                    foreach (var inheritedAttribute in virtualFunctionInfo.MethodInfo.InheritableAttributes)
                        writer.WriteLine(inheritedAttribute);
                    writer.WriteLine
                    (
                        $"[global::System.Runtime.InteropServices.FieldOffsetAttribute({virtualFunctionInfo.Index * 8})] public {functionPointerType} {virtualFunctionInfo.MethodInfo.NameNonGeneric};"
                    );
                }
            }
        }

        if (!structInfo.HasVirtualTable())
        {
            writer.WriteLine("[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]");
            writer.WriteLine($"[global::System.Runtime.InteropServices.FieldOffsetAttribute(0)] public {structInfo.Name}VirtualTable* VirtualTable;");
        }
    }

    private static void RenderInheritedMemberFunctionPointers
    (
        StructInfo                                                            structInfo,
        ImmutableArray<(StructInfo inheritedStruct, string path, int offset)> resolvedInheritanceOrder,
        string                                                                generatorNamespace,
        Dictionary<string, string>                                            sharedPointerBases,
        IndentedTextWriter                                                    writer
    )
    {
        HashSet<string> writtenPointers =
        [
            .. structInfo.MemberFunctions.Select(function => function.MethodInfo.NameNonGeneric)
        ];

        using IndentedTextWriter members = new();

        foreach (var (inheritedStruct, _, offset) in resolvedInheritanceOrder)
        {
            if (offset != 0)
                continue;

            var methods = inheritedStruct.MemberFunctions.Select(function => function.MethodInfo)
                                         .Concat(inheritedStruct.ExtraInheritedStructInfo?.MemberFunctionPointers ?? ImmutableArray<MethodInfo>.Empty);

            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name == "Ctor"            ||
                    HasOwnMethod(structInfo, methodInfo) ||
                    !writtenPointers.Add(methodInfo.NameNonGeneric))
                    continue;

                var thisPtrType = methodInfo.IsStatic ?
                                      string.Empty :
                                      "TSelf*, ";
                var functionPointerType =
                    $"delegate* unmanaged <{thisPtrType}{methodInfo.GetParameterTypeStringWithTrailingTypeNoGenerics()}{methodInfo.ReturnTypeOrVoid}>";

                members.WriteLine($"/// <inheritdoc cref=\"{inheritedStruct.FullyQualifiedMetadataName}.MemberFunctionPointers.{methodInfo.NameNonGeneric}\" />");

                foreach (var inheritedAttribute in methodInfo.InheritableAttributes)
                    members.WriteLine(inheritedAttribute);

                members.WriteLine
                (
                    $"public static {functionPointerType} {methodInfo.NameNonGeneric} => ({functionPointerType}){inheritedStruct.FullyQualifiedMetadataName}.MemberFunctionPointers.{methodInfo.NameNonGeneric};"
                );
            }
        }

        var source = members.ToString();

        if (!sharedPointerBases.TryGetValue(source, out var baseName))
        {
            baseName = $"InheritedMemberFunctionPointers{sharedPointerBases.Count}";
            sharedPointerBases.Add(source, baseName);
        }

        writer.WriteLine
        (
            $"public unsafe abstract partial class MemberFunctionPointers : global::{generatorNamespace}.{baseName}<{structInfo.Name}>"
        );

        using (writer.WriteBlock())
        {
            if (structInfo.MemberFunctions.IsEmpty)
            {
                writer.WriteLine("private MemberFunctionPointers()");
                writer.WriteLine("{");
                writer.WriteLine("}");
            }
        }
    }

    private static string RenderInheritedMemberFunctionPointerBases(string generatorNamespace, Dictionary<string, string> sharedPointerBases)
    {
        using IndentedTextWriter writer = new();

        writer.WriteLine($"namespace {generatorNamespace};");

        foreach (var pair in sharedPointerBases)
        {
            writer.WriteLine("/// <summary>为具有相同继承成员的原生结构提供共享的成员函数指针。</summary>");
            writer.WriteLine("/// <typeparam name=\"TSelf\">接收原生调用的派生结构类型。</typeparam>");
            writer.WriteLine($"public unsafe abstract class {pair.Value}<TSelf> where TSelf : unmanaged");

            using (writer.WriteBlock())
            {
                writer.WriteLine($"private protected {pair.Value}()");
                writer.WriteLine("{");
                writer.WriteLine("}");
                writer.WriteLine(pair.Key);
            }
        }

        return writer.ToString();
    }

    private static void RenderInheritedDelegateTypes
        (StructInfo structInfo, ImmutableArray<(StructInfo inheritedStruct, string path, int offset)> resolvedInheritanceOrder, IndentedTextWriter writer)
    {
        HashSet<(string Name, int Arity)> writtenDelegates =
        [
            .. structInfo.MemberFunctions.Select(function => function.MethodInfo)
                         .Concat(structInfo.VirtualFunctions.Select(function => function.MethodInfo))
                         .Select(method => (method.NameNonGeneric, method.TypeArguments.Length))
        ];

        writer.WriteLine("public static partial class Delegates");

        using (writer.WriteBlock())
        {
            foreach (var (inheritedStruct, _, offset) in resolvedInheritanceOrder)
            {
                if (offset != 0)
                    continue;

                var methods = inheritedStruct.MemberFunctions.Select(function => function.MethodInfo)
                                             .Concat(inheritedStruct.ExtraInheritedStructInfo?.MemberFunctionDelegates ?? ImmutableArray<MethodInfo>.Empty)
                                             .Concat(inheritedStruct.VirtualFunctions.Select(function => function.MethodInfo));

                foreach (var methodInfo in methods)
                {
                    if (!writtenDelegates.Add((methodInfo.NameNonGeneric, methodInfo.TypeArguments.Length)))
                        continue;

                    RenderDelegateTypeForMethod(structInfo.Name, methodInfo, writer);
                }
            }
        }
    }

    private static void RenderInheritedVirtualFunctions
        (StructInfo structInfo, StructInfo inheritedStruct, string path, int offset, IndentedTextWriter writer, HashSet<string> writtenMethods)
    {
        foreach (var (methodInfo, _) in inheritedStruct.VirtualFunctions)
        {
            if (offset != 0 && methodInfo.Name == "Dtor") continue;
            if (HasOwnMethod(structInfo, methodInfo))
                continue;
            if (!writtenMethods.Add(GetMethodKey(methodInfo)))
                continue;
            writer.WriteLine
            (
                $"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{methodInfo.NameXmlEscaped}({methodInfo.GetParameterTypeStringForCref()})" />"""
            );
            writer.WriteLine
            (
                $"""/// <remarks>Method inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );
            writer.WriteLine
                ("[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");

            foreach (var inheritedAttribute in methodInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine
                ($"{methodInfo.GetDeclarationStringWithoutPartial()} => {path}.{methodInfo.Name}({methodInfo.GetParameterNamesStringWithGeneric()});");
        }
    }

    private static void RenderInheritedPublicMethods
        (StructInfo structInfo, StructInfo inheritedStruct, string path, IndentedTextWriter writer, HashSet<string> writtenMethods)
    {
        foreach (var methodInfo in inheritedStruct.ExtraInheritedStructInfo!.PublicMethods)
        {
            if (methodInfo.Name == "Ctor")
                continue;
            if (HasOwnMethod(structInfo, methodInfo))
                continue;
            if (!writtenMethods.Add(GetMethodKey(methodInfo)))
                continue;
            writer.WriteLine
            (
                $"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{methodInfo.Name.Replace('<', '{').Replace('>', '}')}({methodInfo.GetParameterTypeStringForCref()})" />"""
            );
            writer.WriteLine
            (
                $"""/// <remarks>Method inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );
            writer.WriteLine
                ("[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");

            foreach (var inheritedAttribute in methodInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            if (methodInfo.IsStatic)
            {
                writer.WriteLine
                (
                    $"{methodInfo.GetDeclarationStringWithoutPartial()} => {inheritedStruct.FullyQualifiedMetadataName}.{methodInfo.Name}({methodInfo.GetParameterNamesStringWithGeneric()});"
                );
            }
            else
            {
                writer.WriteLine
                    ($"{methodInfo.GetDeclarationStringWithoutPartial()} => {path}.{methodInfo.Name}({methodInfo.GetParameterNamesStringWithGeneric()});");
            }
        }
    }

    private static void RenderInheritedPublicProperties
        (StructInfo structInfo, StructInfo inheritedStruct, string path, IndentedTextWriter writer, HashSet<string> writtenMembers)
    {
        foreach (var propertyInfo in inheritedStruct.ExtraInheritedStructInfo!.PublicProperties)
        {
            if (HasOwnMemberName(structInfo, propertyInfo.Name))
                continue;
            if (!writtenMembers.Add(propertyInfo.Name))
                continue;
            writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{propertyInfo.Name}" />""");
            writer.WriteLine
            (
                $"""/// <remarks>Property inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );

            foreach (var inheritedAttribute in propertyInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine($"public {propertyInfo.RefKind.GetStringPrefix()}{propertyInfo.Type} {propertyInfo.Name}");

            using (writer.WriteBlock())
            {
                if (propertyInfo.Get)
                {
                    writer.WriteLine($"get => {propertyInfo.RefKind.GetStringPrefix()}{path}.{propertyInfo.Name};");
                }

                if (propertyInfo.Set)
                {
                    writer.WriteLine($"set => {path}.{propertyInfo.Name} = value;");
                }
            }
        }
    }

    private static void RenderInheritedBitFieldProperties
        (StructInfo structInfo, StructInfo inheritedStruct, string path, IndentedTextWriter writer, HashSet<string> writtenMembers)
    {
        foreach (var bitFieldInfo in inheritedStruct.BitFields)
        {
            if (bitFieldInfo.IsPartial)
                continue;
            if (HasOwnMemberName(structInfo, bitFieldInfo.Name))
                continue;
            if (!writtenMembers.Add(bitFieldInfo.Name))
                continue;

            writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{bitFieldInfo.Name}" />""");
            writer.WriteLine
            (
                $"""/// <remarks>Property inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );

            foreach (var inheritedAttribute in bitFieldInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine($"public {bitFieldInfo.Type} {bitFieldInfo.Name}");

            using (writer.WriteBlock())
            {
                if (bitFieldInfo.HasGetter)
                {
                    writer.WriteLine($"get => {path}.{bitFieldInfo.Name};");
                }

                if (bitFieldInfo.HasSetter)
                {
                    writer.WriteLine($"set => {path}.{bitFieldInfo.Name} = value;");
                }
            }
        }
    }

    private static void RenderInheritedFixedSizeArrayAccessors(StructInfo structInfo, StructInfo inheritedStruct, string path, IndentedTextWriter writer)
    {
        foreach (var fixedSizeArrayInfo in inheritedStruct.FixedSizeArrays)
        {
            if (HasOwnMemberName(structInfo, fixedSizeArrayInfo.GetPublicFieldName()))
                continue;
            if (structInfo.FixedSizeArrays.Any(ownArray => ownArray.GetPublicFieldName() == fixedSizeArrayInfo.GetPublicFieldName()))
                continue;
            writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{fixedSizeArrayInfo.GetPublicFieldName()}" />""");
            writer.WriteLine
            (
                $"""/// <remarks>Field inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
            );

            foreach (var inheritedAttribute in fixedSizeArrayInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine
            (
                $"[global::System.Diagnostics.CodeAnalysis.UnscopedRefAttribute] public Span<{fixedSizeArrayInfo.Type}> {fixedSizeArrayInfo.GetPublicFieldName()} => {path}.{fixedSizeArrayInfo.FieldName};"
            );

            if (fixedSizeArrayInfo.IsString)
            {
                writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{fixedSizeArrayInfo.GetPublicFieldName()}" />""");
                writer.WriteLine
                (
                    $"""/// <remarks>Field inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
                );

                foreach (var inheritedAttribute in fixedSizeArrayInfo.InheritableAttributes)
                {
                    writer.WriteLine(inheritedAttribute);
                }

                writer.WriteLine($"public string {fixedSizeArrayInfo.GetPublicFieldName()}String");

                using (writer.WriteBlock())
                {
                    switch (fixedSizeArrayInfo.Type)
                    {
                        case "byte":
                        {
                            writer.WriteLine
                            (
                                $"get => global::System.Text.Encoding.UTF8.GetString(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref {path}.{fixedSizeArrayInfo.FieldName}[0])));"
                            );
                            writer.WriteLine("set");

                            using (writer.WriteBlock())
                            {
                                writer.WriteLine($"if (global::System.Text.Encoding.UTF8.GetByteCount(value) > {fixedSizeArrayInfo.Size} - 1)");

                                using (writer.WriteBlock())
                                {
                                    writer.WriteLine
                                    (
                                        $"""InteropGenerator.Runtime.ThrowHelper.ThrowStringSizeTooLarge("{fixedSizeArrayInfo.GetPublicFieldName()}String", {fixedSizeArrayInfo.Size});"""
                                    );
                                }

                                writer.WriteLine($"global::System.Text.Encoding.UTF8.GetBytes(value.AsSpan(), {path}.{fixedSizeArrayInfo.FieldName});");
                                writer.WriteLine($"{path}.{fixedSizeArrayInfo.FieldName}[{fixedSizeArrayInfo.Size - 1}] = 0;");
                            }
                        }
                            break;
                        case "char":
                        {
                            writer.WriteLine
                            (
                                $"get => new string((char*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref {path}.{fixedSizeArrayInfo.FieldName}[0]));"
                            );
                            writer.WriteLine("set");

                            using (writer.WriteBlock())
                            {
                                writer.WriteLine($"if (value.Length > {fixedSizeArrayInfo.Size} - 1)");

                                using (writer.WriteBlock())
                                {
                                    writer.WriteLine
                                    (
                                        $"""InteropGenerator.Runtime.ThrowHelper.ThrowStringSizeTooLarge("{fixedSizeArrayInfo.GetPublicFieldName()}String", {fixedSizeArrayInfo.Size});"""
                                    );
                                }

                                writer.WriteLine($"value.CopyTo({path}.{fixedSizeArrayInfo.FieldName});");
                                writer.WriteLine($"{path}.{fixedSizeArrayInfo.FieldName}[{fixedSizeArrayInfo.Size - 1}] = '\\0';");
                            }
                        }
                            break;
                    }
                }
            }

            if (fixedSizeArrayInfo.IsBitArray)
            {
                writer.WriteLine($"""/// <inheritdoc cref="{inheritedStruct.FullyQualifiedMetadataName}.{fixedSizeArrayInfo.GetPublicFieldName()}" />""");
                writer.WriteLine
                (
                    $"""/// <remarks>Field inherited from parent class <see cref="{inheritedStruct.FullyQualifiedMetadataName}">{inheritedStruct.Name}</see>.</remarks>"""
                );

                foreach (var inheritedAttribute in fixedSizeArrayInfo.InheritableAttributes)
                {
                    writer.WriteLine(inheritedAttribute);
                }

                writer.WriteLine($"public BitArray {fixedSizeArrayInfo.GetPublicFieldName()}BitArray => {path}.{fixedSizeArrayInfo.GetPublicFieldName()}BitArray;");
            }
        }
    }

    private static string ResolveParentFieldName(StructInfo structInfo, StructInfo inheritedStruct, int offset)
    {
        foreach (var ownMember in structInfo.OwnMembers)
        {
            if (!ownMember.IsMethod             &&
                ownMember.FieldOffset == offset &&
                ownMember.TypeName    == inheritedStruct.FullyQualifiedMetadataName)
                return ownMember.Name;
        }

        return inheritedStruct.Name == structInfo.Name ?
                   inheritedStruct.Name + "Base" :
                   inheritedStruct.Name;
    }

    private static bool HasOwnParentField(StructInfo structInfo, StructInfo inheritedStruct, int offset) =>
        structInfo.OwnMembers.Any
        (ownMember => !ownMember.IsMethod             &&
                      ownMember.FieldOffset == offset &&
                      ownMember.TypeName    == inheritedStruct.FullyQualifiedMetadataName
        );

    private static bool HasOwnMemberNameOrOffset(StructInfo structInfo, string name, int offset) =>
        structInfo.OwnMembers.Any
        (ownMember => ownMember.Name == name ||
                      (!ownMember.IsMethod && ownMember.FieldOffset == offset)
        );

    private static bool HasOwnMemberName(StructInfo structInfo, string name) =>
        structInfo.OwnMembers.Any(ownMember => ownMember.Name == name);

    private static bool HasOwnMethod(StructInfo structInfo, MethodInfo methodInfo)
    {
        var parameterSignature = GetParameterSignature(methodInfo);

        return structInfo.OwnMembers.Any
        (ownMember => ownMember.IsMethod                           &&
                      ownMember.Name            == methodInfo.Name &&
                      ownMember.MethodSignature == parameterSignature
        );
    }

    private static string GetParameterSignature(MethodInfo methodInfo) =>
        string.Join(", ", methodInfo.Parameters.Select(parameter => parameter.RefKind.GetStringPrefix() + parameter.Type));

    private static string GetMethodKey(MethodInfo methodInfo) =>
        methodInfo.Name + "(" + GetParameterSignature(methodInfo) + ")";
}
