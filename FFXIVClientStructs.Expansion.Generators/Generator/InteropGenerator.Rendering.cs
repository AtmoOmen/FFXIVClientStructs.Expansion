using System.Collections.Immutable;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

public sealed partial class InteropGenerator
{
    private static string RenderStructInfo
    (
        StructInfo        structInfo,
        CancellationToken token
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

        if (structInfo.Size.HasValue)
            writer.WriteLine($"public const int StructSize = {structInfo.Size};");

        if (structInfo.HasSignatures())
        {
            RenderAddresses(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (structInfo.HasVirtualTable())
        {
            RenderVirtualTable(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.MemberFunctions.IsEmpty || !structInfo.VirtualFunctions.IsEmpty)
        {
            RenderDelegateTypes(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.MemberFunctions.IsEmpty)
        {
            RenderMemberFunctions(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.VirtualFunctions.IsEmpty)
        {
            RenderVirtualFunctions(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.StaticAddresses.IsEmpty)
        {
            RenderStaticAddresses(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.StringOverloads.IsEmpty)
        {
            RenderStringOverloads(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.FixedSizeArrays.IsEmpty)
        {
            RenderFixedSizeArrayAccessors(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        if (!structInfo.BitFields.IsEmpty)
        {
            RenderBitFields(structInfo, writer);
            token.ThrowIfCancellationRequested();
        }

        for (var i = 0; i < structInfo.Hierarchy.Length; i++)
        {
            writer.DecreaseIndent();
            writer.WriteLine("}");
        }

        return writer.ToString();
    }

    private static void RenderAddresses
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine("public static class Addresses");

        using (writer.WriteBlock())
        {
            foreach (var mfi in structInfo.MemberFunctions)
            {
                writer.WriteLine(GetAddressString(structInfo, mfi.MethodInfo.NameNonGeneric, mfi.SignatureInfo));
            }

            foreach (var sai in structInfo.StaticAddresses)
            {
                writer.WriteLine(GetAddressString(structInfo, sai.MethodInfo.NameNonGeneric, sai.SignatureInfo));
            }

            if (structInfo.StaticVirtualTableSignature is not null)
            {
                writer.WriteLine(GetAddressString(structInfo, "StaticVirtualTable", structInfo.StaticVirtualTableSignature));
            }
        }
    }

    private static string GetAddressString
    (
        StructInfo    structInfo,
        string        signatureName,
        SignatureInfo signatureInfo
    )
    {
        var paddedSignature = signatureInfo.GetPaddedSignature();
        var relativeOffsets = signatureInfo.GetRelCallAndJumpAdjustedOffset();
        var offsets         = "new ushort[] {" + string.Join(", ", relativeOffsets) + "}";
        var groupedSig = paddedSignature.Replace("??", "00").Split()
                                        .Select((x, i) => new { Index = i, Value = x })
                                        .GroupBy(x => x.Index / 8 * 3)
                                        .Select(x => x.Select(v => v.Value))
                                        .Select(x => "0x" + string.Join(string.Empty, x.Reverse()));

        var ulongArraySignature = "new ulong[] {" + string.Join(", ", groupedSig) + "}";
        var groupedSigMask = paddedSignature.Split()
                                            .Select
                                            (s => s == "??" ?
                                                      "00" :
                                                      "FF"
                                            )
                                            .Select((x, i) => new { Index = i, Value = x })
                                            .GroupBy(x => x.Index / 8 * 3)
                                            .Select(x => x.Select(v => v.Value))
                                            .Select(x => "0x" + string.Join(string.Empty, x.Reverse()));

        var ulongArrayMask = "new ulong[] {" + string.Join(", ", groupedSigMask) + "}";

        return
            $"""public static readonly global::InteropGenerator.Runtime.Address {signatureName} = new global::InteropGenerator.Runtime.Address("{structInfo.FullyQualifiedMetadataName}.{signatureName}", "{paddedSignature}", {offsets}, {ulongArraySignature}, {ulongArrayMask}, 0);""";
    }

    private static void RenderVirtualTable
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        if (structInfo.VirtualTableFunctionCount is not null)
        {
            writer.WriteLine
            (
                $"[global::System.Runtime.InteropServices.StructLayoutAttribute(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = {structInfo.VirtualTableFunctionCount * 8})]"
            );
        }
        else
        {
            writer.WriteLine("[global::System.Runtime.InteropServices.StructLayoutAttribute(global::System.Runtime.InteropServices.LayoutKind.Explicit)]");
        }

        writer.WriteLine($"public unsafe partial struct {structInfo.Name}VirtualTable");

        using (writer.WriteBlock())
        {
            foreach (var vfi in structInfo.VirtualFunctions)
            {
                var functionPointerType =
                    $"delegate* unmanaged <{structInfo.Name}*, {vfi.MethodInfo.GetParameterTypeStringWithTrailingTypeNoGenerics()}{vfi.MethodInfo.ReturnTypeOrVoid}>";
                foreach (var attr in vfi.MethodInfo.InheritableAttributes)
                    writer.WriteLine(attr);
                writer.WriteLine
                (
                    $"[global::System.Runtime.InteropServices.FieldOffsetAttribute({vfi.Index * 8})] public {functionPointerType} {vfi.MethodInfo.NameNonGeneric};"
                );
            }
        }

        writer.WriteLine($"[global::System.Runtime.InteropServices.FieldOffsetAttribute(0)] public {structInfo.Name}VirtualTable* VirtualTable;");

        if (structInfo.StaticVirtualTableSignature is not null)
        {
            writer.WriteLine
                ($"public static {structInfo.Name}VirtualTable* StaticVirtualTablePointer => ({structInfo.Name}VirtualTable*)Addresses.StaticVirtualTable.Value;");
        }
    }

    private static void RenderDelegateTypes
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine("public static partial class Delegates");

        using (writer.WriteBlock())
        {
            foreach (var memberFunctionInfo in structInfo.MemberFunctions)
            {
                RenderDelegateTypeForMethod(structInfo.Name, memberFunctionInfo.MethodInfo, writer);
            }

            foreach (var virtualFunctionInfo in structInfo.VirtualFunctions)
            {
                RenderDelegateTypeForMethod(structInfo.Name, virtualFunctionInfo.MethodInfo, writer);
            }
        }
    }

    private static void RenderDelegateTypeForMethod
    (
        string             structType,
        MethodInfo         methodInfo,
        IndentedTextWriter writer
    )
    {
        if (methodInfo.ReturnType == "bool")
        {
            writer.WriteLine("[return:global::System.Runtime.InteropServices.MarshalAsAttribute(global::System.Runtime.InteropServices.UnmanagedType.U1)]");
        }

        string paramTypesAndNames;

        if (methodInfo.IsStatic)
        {
            paramTypesAndNames = methodInfo.GetParameterTypesAndNamesString();
        }
        else
        {
            paramTypesAndNames = $"{structType}* thisPtr";
            if (!methodInfo.Parameters.IsEmpty)
                paramTypesAndNames += $", {methodInfo.GetParameterTypesAndNamesString()}";
        }

        var methodModifiers = methodInfo.Modifiers.Replace(" partial", string.Empty).Replace(" static", string.Empty);
        foreach (var attr in methodInfo.InheritableAttributes)
            writer.WriteLine(attr);
        writer.WriteLine($"{methodModifiers} delegate {methodInfo.ReturnType} {methodInfo.Name}({paramTypesAndNames}){methodInfo.GenericConstraints};");
    }

    private static void RenderMemberFunctions
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine("public unsafe static class MemberFunctionPointers");

        using (writer.WriteBlock())
        {
            foreach (var mfi in structInfo.MemberFunctions)
            {
                var thisPtrType = mfi.MethodInfo.IsStatic ?
                                      string.Empty :
                                      $"{structInfo.Name}*, ";
                var functionPointerType =
                    $"delegate* unmanaged <{thisPtrType}{mfi.MethodInfo.GetParameterTypeStringWithTrailingTypeNoGenerics()}{mfi.MethodInfo.ReturnTypeOrVoid}>";
                writer.WriteLine
                (
                    $"public static {functionPointerType} {mfi.MethodInfo.NameNonGeneric} => ({functionPointerType}) {structInfo.Name}.Addresses.{mfi.MethodInfo.NameNonGeneric}.Value;"
                );
            }
        }

        foreach (var mfi in structInfo.MemberFunctions)
        {
            writer.WriteLine(mfi.MethodInfo.GetDeclarationString());

            using (writer.WriteBlock())
            {
                writer.WriteLine($"if (MemberFunctionPointers.{mfi.MethodInfo.NameNonGeneric} is null)");

                using (writer.WriteBlock())
                {
                    writer.WriteLine
                        ($"""InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("{structInfo.Name}.{mfi.MethodInfo.Name}", "{mfi.SignatureInfo.Signature}");""");
                }

                if (mfi.MethodInfo.IsStatic)
                {
                    writer.WriteLine
                        ($"{mfi.MethodInfo.GetReturnString()}MemberFunctionPointers.{mfi.MethodInfo.NameNonGeneric}({mfi.MethodInfo.GetParameterNamesString()});");
                }
                else
                {
                    var paramNames = string.Empty;
                    if (mfi.MethodInfo.Parameters.Any())
                        paramNames = ", " + mfi.MethodInfo.GetParameterNamesString();
                    writer.WriteLine
                    (
                        $"{mfi.MethodInfo.GetReturnString()}MemberFunctionPointers.{mfi.MethodInfo.Name}(({structInfo.Name}*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref this){paramNames});"
                    );
                }
            }
        }
    }

    private static void RenderVirtualFunctions
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        foreach (var virtualFunctionInfo in structInfo.VirtualFunctions)
        {
            writer.WriteLine
                ("[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");
            var paramNames = string.Empty;
            if (virtualFunctionInfo.MethodInfo.Parameters.Any())
                paramNames = ", " + virtualFunctionInfo.MethodInfo.GetParameterNamesString();
            writer.WriteLine
            (
                $"{virtualFunctionInfo.MethodInfo.GetDeclarationString()} => {virtualFunctionInfo.MethodInfo.ReturnTypeCast}VirtualTable->{virtualFunctionInfo.MethodInfo.NameNonGeneric}(({structInfo.Name}*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref this){paramNames});"
            );
        }
    }

    private static void RenderStaticAddresses
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine("public unsafe static class StaticAddressPointers");

        using (writer.WriteBlock())
        {
            foreach (var sai in structInfo.StaticAddresses)
            {
                var pointerText = sai.IsPointer ?
                                      "* p" :
                                      " ";
                var pointer = sai.IsPointer ?
                                  "*" :
                                  string.Empty;
                writer.WriteLine
                (
                    $"public static {sai.MethodInfo.ReturnType}{pointerText}p{sai.MethodInfo.Name} => ({sai.MethodInfo.ReturnType}{pointer}){structInfo.Name}.Addresses.{sai.MethodInfo.Name}.Value;"
                );
            }
        }

        foreach (var sai in structInfo.StaticAddresses)
        {
            writer.WriteLine(sai.MethodInfo.GetDeclarationString());
            var extraPointerText = sai.IsPointer ?
                                       "p" :
                                       string.Empty;
            var pointerReturnText = sai.IsPointer ?
                                        "*" :
                                        string.Empty;

            using (writer.WriteBlock())
            {
                writer.WriteLine($"if (StaticAddressPointers.{extraPointerText}p{sai.MethodInfo.Name} is null)");

                using (writer.WriteBlock())
                {
                    writer.WriteLine
                        ($"""InteropGenerator.Runtime.ThrowHelper.ThrowNullAddress("{structInfo.Name}.{sai.MethodInfo.Name}", "{sai.SignatureInfo.Signature}");""");
                }

                writer.WriteLine($"return {pointerReturnText}StaticAddressPointers.{extraPointerText}p{sai.MethodInfo.Name};");
            }
        }
    }

    private static void RenderStringOverloads
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        foreach (var stringOverloadInfo in structInfo.StringOverloads)
        {
            var methodInfo = stringOverloadInfo.MethodInfo;
            ImmutableArray<string> paramsToOverload =
            [
                .. methodInfo.Parameters.Where
                    (p => p.Type == "global::" + InteropTypeNames.CStringPointer && !stringOverloadInfo.IgnoredParameters.Contains(p.Name)).Select(p => p.Name)
            ];

            foreach (var inheritedAttribute in stringOverloadInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine("[global::System.Runtime.CompilerServices.SkipLocalsInitAttribute]");
            writer.WriteLine(methodInfo.GetDeclarationStringForStringOverload("string", paramsToOverload));

            using (writer.WriteBlock())
            {
                foreach (var overloadParamName in paramsToOverload)
                {
                    var strLenName = $"{overloadParamName}UTF8StrLen";

                    writer.WriteLine($"int {strLenName} = global::System.Text.Encoding.UTF8.GetByteCount({overloadParamName});");
                    writer.WriteLine($"Span<byte> {overloadParamName}Bytes = {strLenName} <= 511 ? stackalloc byte[512] : new byte[{strLenName} + 1];");
                    writer.WriteLine($"global::System.Text.Encoding.UTF8.GetBytes({overloadParamName}, {overloadParamName}Bytes);");
                    writer.WriteLine($"{overloadParamName}Bytes[{strLenName}] = 0;");
                }

                foreach (var overloadParamName in paramsToOverload)
                {
                    writer.WriteLine($"fixed (byte* {overloadParamName}Ptr = {overloadParamName}Bytes)");
                    writer.WriteLine("{");
                    writer.IncreaseIndent();
                }

                var paramNames = methodInfo.GetParameterNamesStringForStringOverload(paramsToOverload);
                writer.WriteLine($"{methodInfo.GetReturnString()}{methodInfo.Name}({paramNames});");

                foreach (var _ in paramsToOverload)
                {
                    writer.DecreaseIndent();
                    writer.WriteLine("}");
                }
            }

            foreach (var inheritedAttribute in stringOverloadInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine(methodInfo.GetDeclarationStringForStringOverload("ReadOnlySpan<byte>", paramsToOverload));

            using (writer.WriteBlock())
            {
                writer.WriteLine("byte zero = 0;");

                foreach (var overloadParamName in paramsToOverload)
                {
                    writer.WriteLine($"fixed (byte* {overloadParamName}Ptr = {overloadParamName})");
                    writer.WriteLine("{");
                    writer.IncreaseIndent();
                }

                var paramNames = methodInfo.GetParameterNamesStringForStringOverloadWithNullCheck(paramsToOverload);
                writer.WriteLine($"{methodInfo.GetReturnString()}{methodInfo.Name}({paramNames});");

                foreach (var _ in paramsToOverload)
                {
                    writer.DecreaseIndent();
                    writer.WriteLine("}");
                }
            }
        }
    }

    private static void RenderFixedSizeArrayAccessors
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        foreach (var fixedSizeArrayInfo in structInfo.FixedSizeArrays)
        {
            writer.WriteLine($"""/// <inheritdoc cref="{fixedSizeArrayInfo.FieldName}" />""");

            foreach (var inheritedAttribute in fixedSizeArrayInfo.InheritableAttributes)
            {
                writer.WriteLine(inheritedAttribute);
            }

            writer.WriteLine
            (
                $"[global::System.Diagnostics.CodeAnalysis.UnscopedRefAttribute] public Span<{fixedSizeArrayInfo.Type}> {fixedSizeArrayInfo.GetPublicFieldName()} => {fixedSizeArrayInfo.FieldName};"
            );

            if (fixedSizeArrayInfo.IsString)
            {
                writer.WriteLine($"""/// <inheritdoc cref="{fixedSizeArrayInfo.FieldName}" />""");

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
                            writer.WriteLine
                            (
                                $"get => global::System.Text.Encoding.UTF8.GetString(global::System.Runtime.InteropServices.MemoryMarshal.CreateReadOnlySpanFromNullTerminated((byte*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref {fixedSizeArrayInfo.FieldName}[0])));"
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

                                writer.WriteLine($"global::System.Text.Encoding.UTF8.GetBytes(value.AsSpan(), {fixedSizeArrayInfo.FieldName});");
                                writer.WriteLine($"{fixedSizeArrayInfo.FieldName}[{fixedSizeArrayInfo.Size - 1}] = 0;");
                            }

                            break;
                        case "char":
                        {
                            writer.WriteLine
                                ($"get => new string((char*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref {fixedSizeArrayInfo.FieldName}[0]));");
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

                                writer.WriteLine($"value.CopyTo({fixedSizeArrayInfo.FieldName});");
                                writer.WriteLine($"{fixedSizeArrayInfo.FieldName}[{fixedSizeArrayInfo.Size - 1}] = '\\0';");
                            }

                            break;
                        }
                    }
                }
            }

            if (fixedSizeArrayInfo.IsBitArray)
            {
                writer.WriteLine($"""/// <inheritdoc cref="{fixedSizeArrayInfo.FieldName}" />""");

                foreach (var inheritedAttribute in fixedSizeArrayInfo.InheritableAttributes)
                {
                    writer.WriteLine(inheritedAttribute);
                }

                writer.WriteLine
                (
                    $"public BitArray {fixedSizeArrayInfo.GetPublicFieldName()}BitArray => new BitArray((byte*)global::System.Runtime.CompilerServices.Unsafe.AsPointer(ref {fixedSizeArrayInfo.FieldName}[0]), {fixedSizeArrayInfo.BitCount});"
                );
            }
        }
    }

    private static string RenderResolverInitializer
    (
        ImmutableArray<StructInfo> structInfos,
        string                     generatorNamespace
    )
    {
        using IndentedTextWriter writer = new();
        writer.WriteLine("");
        writer.WriteLine($"namespace {generatorNamespace};");
        writer.WriteLine("#pragma warning disable 612, 618");
        writer.WriteLine("public static class Addresses");

        using (writer.WriteBlock())
        {
            writer.WriteLine("public static void Register(global::FFXIVClientStructs.Expansion.ExpansionResolver resolver)");

            using (writer.WriteBlock())
            {
                foreach (var sInfo in structInfos)
                {
                    foreach (var mfi in sInfo.MemberFunctions)
                    {
                        writer.WriteLine(GetAddToResolverString(sInfo, mfi.MethodInfo.NameNonGeneric));
                    }

                    foreach (var sai in sInfo.StaticAddresses)
                    {
                        writer.WriteLine(GetAddToResolverString(sInfo, sai.MethodInfo.NameNonGeneric));
                    }

                    if (sInfo.StaticVirtualTableSignature is not null)
                    {
                        writer.WriteLine(GetAddToResolverString(sInfo, "StaticVirtualTable"));
                    }
                }
            }

            writer.WriteLine("public static void Unregister(global::FFXIVClientStructs.Expansion.ExpansionResolver resolver)");

            using (writer.WriteBlock())
            {
                foreach (var sInfo in structInfos)
                {
                    foreach (var mfi in sInfo.MemberFunctions)
                    {
                        writer.WriteLine(GetRemoveFromResolverString(sInfo, mfi.MethodInfo.NameNonGeneric));
                    }

                    foreach (var sai in sInfo.StaticAddresses)
                    {
                        writer.WriteLine(GetRemoveFromResolverString(sInfo, sai.MethodInfo.NameNonGeneric));
                    }

                    if (sInfo.StaticVirtualTableSignature is not null)
                    {
                        writer.WriteLine(GetRemoveFromResolverString(sInfo, "StaticVirtualTable"));
                    }
                }
            }
        }

        writer.WriteLine("#pragma warning restore 612, 618");

        return writer.ToString();
    }

    private static string GetAddToResolverString
    (
        StructInfo structInfo,
        string     signatureName
    )
    {
        var namespaceString = string.IsNullOrEmpty(structInfo.Namespace) ?
                                  string.Empty :
                                  structInfo.Namespace + ".";
        var fullTypeName = $"global::{namespaceString}{string.Join(".", structInfo.Hierarchy.Reverse())}";
        return $"resolver.Register({fullTypeName}.Addresses.{signatureName});";
    }

    private static string GetRemoveFromResolverString
    (
        StructInfo structInfo,
        string     signatureName
    )
    {
        var namespaceString = string.IsNullOrEmpty(structInfo.Namespace) ?
                                  string.Empty :
                                  structInfo.Namespace + ".";
        var fullTypeName = $"global::{namespaceString}{string.Join(".", structInfo.Hierarchy.Reverse())}";
        return $"resolver.Unregister({fullTypeName}.Addresses.{signatureName});";
    }

    private static string RenderFixedArrayTypes
    (
        ImmutableArray<StructInfo> structInfos,
        string                     generatorNamespace
    )
    {
        using IndentedTextWriter writer         = new();
        HashSet<int>             generatedSizes = [];

        writer.WriteLine("");
        writer.WriteLine($"namespace {generatorNamespace};");

        foreach (var structInfo in structInfos)
        {
            foreach (var fixedSizeArrayInfo in structInfo.FixedSizeArrays)
            {
                if (generatedSizes.Contains(fixedSizeArrayInfo.Size))
                    continue;

                writer.WriteLine
                    ("[global::System.Runtime.InteropServices.StructLayoutAttribute(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]");
                writer.WriteLine($"[global::System.Runtime.CompilerServices.InlineArrayAttribute({fixedSizeArrayInfo.Size})]");
                writer.WriteLine($"internal struct FixedSizeArray{fixedSizeArrayInfo.Size}<T> where T : unmanaged");

                using (writer.WriteBlock())
                {
                    writer.WriteLine("private T _element0;");
                }

                generatedSizes.Add(fixedSizeArrayInfo.Size);
            }
        }

        return writer.ToString();
    }

    private static void RenderBitFields
    (
        StructInfo         structInfo,
        IndentedTextWriter writer
    )
    {
        foreach (var bitField in structInfo.BitFields)
        {
            foreach (var inheritedAttribute in bitField.InheritableAttributes)
                writer.WriteLine(inheritedAttribute);

            writer.WriteLine($"{SyntaxFacts.GetText(bitField.Accessibility)} {(bitField.IsPartial ? "partial " : "")}{bitField.Type} {bitField.Name}");

            using (writer.WriteBlock())
            {
                if (bitField.Type == "bool")
                {
                    if (bitField.HasGetter)
                        writer.WriteLine
                        (
                            $"{(bitField is { IsReadonlyGetter: true, HasSetter: true } ? "readonly " : "")}get => BitOps.GetBit<{bitField.BackingType}>({bitField.FieldName}, {bitField.Index});"
                        );
                    if (bitField.HasSetter)
                        writer.WriteLine($"set => {bitField.FieldName} = BitOps.SetBit<{bitField.BackingType}>({bitField.FieldName}, {bitField.Index}, value);");
                }
                else
                {
                    var mask = $"BitOps.CreateLowBitMask<{bitField.BackingType}>({bitField.Length})";
                    if (bitField.HasGetter)
                        writer.WriteLine
                        (
                            $"{(bitField is { IsReadonlyGetter: true, HasSetter: true } ? "readonly " : "")}get => ({bitField.Type})BitOps.GetBits<{bitField.BackingType}>({bitField.FieldName}, {bitField.Index}, {mask});"
                        );
                    if (bitField.HasSetter)
                        writer.WriteLine
                        (
                            $"set => {bitField.FieldName} = BitOps.SetBits<{bitField.BackingType}>({bitField.FieldName}, {bitField.Index}, {mask}, ({bitField.BackingType})value);"
                        );
                }
            }
        }
    }
}
