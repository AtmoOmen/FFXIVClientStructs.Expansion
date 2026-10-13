using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using FFXIVClientStructs.Expansion.Generators.Extensions;
using FFXIVClientStructs.Expansion.Generators.Helpers;
using FFXIVClientStructs.Expansion.Generators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

public sealed partial class InteropGenerator {

    private static StructInfo ParseStructInfo(INamedTypeSymbol structSymbol, AttributeData generateInteropAttributeData, CancellationToken token) {
        ExtraInheritedStructInfo? extraInheritedStructInfo = null;
        generateInteropAttributeData.TryGetConstructorArgument(0, out bool isInherited);
        ParseMethods(structSymbol,
            token,
            isInherited,
            out EquatableArray<MemberFunctionInfo> memberFunctions,
            out EquatableArray<VirtualFunctionInfo> virtualFunctions,
            out EquatableArray<StaticAddressInfo> staticAddresses,
            out EquatableArray<StringOverloadInfo> stringOverloads,
            out EquatableArray<MethodInfo> extraPublicMethods);
        token.ThrowIfCancellationRequested();
        ParseFields(structSymbol, token, isInherited, out EquatableArray<FixedSizeArrayInfo> fixedSizeArrays, out EquatableArray<BitFieldInfo> bitFields, out EquatableArray<FieldInfo> publicFields);
        token.ThrowIfCancellationRequested();
        ParseOwnMembers(structSymbol, fixedSizeArrays, token, out EquatableArray<OwnMemberInfo> ownMembers);
        token.ThrowIfCancellationRequested();
        SignatureInfo? virtualTableSignatureInfo = null;
        uint? virtualTableFunctionCount = null;
        if (structSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.VirtualTableAttribute, out AttributeData? virtualTableAttribute)) {
            if (virtualTableAttribute.TryGetConstructorArgument(0, out string? signature)) {
                if (virtualTableAttribute.ConstructorArguments[1].Kind == TypedConstantKind.Array &&
                    virtualTableAttribute.TryGetMultiValueConstructorArgument(1, out ImmutableArray<ushort>? multipleOffsets)) {
                    virtualTableSignatureInfo = new SignatureInfo(signature, multipleOffsets.Value);
                } else if (virtualTableAttribute.TryGetConstructorArgument(1, out ushort? singleOffset)) {
                    ImmutableArray<ushort> values = [singleOffset.Value];
                    virtualTableSignatureInfo = new SignatureInfo(signature, values);
                }
            }
            if (virtualTableAttribute.TryGetConstructorArgument(2, out uint? functionCount)) {
                virtualTableFunctionCount = functionCount == 0 ? null : functionCount;
            }
        }

        using ImmutableArrayBuilder<InheritanceInfo> inheritanceInfoBuilder = new();
        foreach (AttributeData attributeData in structSymbol.GetAttributes()) {
            if (attributeData.AttributeClass is not { } attributeSymbol) continue;
            if (!attributeSymbol.HasFullyQualifiedMetadataName(InteropTypeNames.InheritsAttribute)) continue;
            if (attributeData.ConstructorArguments.Length != 1 ||
                !attributeData.TryGetConstructorArgument(0, out int? parentOffset))
                continue;
            if (attributeSymbol.TypeArguments.Length != 1) continue;

            InheritanceInfo inheritanceInfo = new(
                attributeSymbol.TypeArguments[0].GetNameWithContainingTypeAndNamespace(),
                parentOffset.Value
            );
            inheritanceInfoBuilder.Add(inheritanceInfo);
        }
        using ImmutableArrayBuilder<string> hierarchy = new();

        for (INamedTypeSymbol? parent = structSymbol;
             parent is not null;
             parent = parent.ContainingType) {
            hierarchy.Add(parent.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
        }
        int? structSize = null;

        if (structSymbol.TryGetAttributeWithFullyQualifiedMetadataName("System.Runtime.InteropServices.StructLayoutAttribute", out AttributeData? structLayoutAttributeData)) {
            structLayoutAttributeData.TryGetNamedArgument("Size", out structSize);
        }
        if (isInherited) {
            ParseProperties(structSymbol, token, out EquatableArray<PropertyInfo> publicProperties);
            extraInheritedStructInfo = new ExtraInheritedStructInfo(
                publicFields,
                extraPublicMethods,
                publicProperties);
        }
        return new StructInfo(
            structSymbol.GetNameWithContainingTypeAndNamespace(),
            structSymbol.ContainingNamespace.GetNameWithContainingTypeAndNamespace(),
            hierarchy.ToImmutable(),
            memberFunctions,
            virtualFunctions,
            staticAddresses,
            stringOverloads,
            virtualTableSignatureInfo,
            virtualTableFunctionCount,
            fixedSizeArrays,
            bitFields,
            inheritanceInfoBuilder.ToImmutable(),
            structSize,
            ownMembers,
            false,
            extraInheritedStructInfo);
    }

    private static void ParseMethods(INamedTypeSymbol structSymbol, CancellationToken token, bool isInherited,
        out EquatableArray<MemberFunctionInfo> memberFunctions,
        out EquatableArray<VirtualFunctionInfo> virtualFunctions,
        out EquatableArray<StaticAddressInfo> staticAddresses,
        out EquatableArray<StringOverloadInfo> stringOverloads,
        out EquatableArray<MethodInfo> extraPublicMethods) {

        using ImmutableArrayBuilder<MemberFunctionInfo> memberFunctionsBuilder = new();
        using ImmutableArrayBuilder<VirtualFunctionInfo> virtualFunctionBuilder = new();
        using ImmutableArrayBuilder<StaticAddressInfo> staticAddressesBuilder = new();
        using ImmutableArrayBuilder<StringOverloadInfo> stringOverloadsBuilder = new();
        using ImmutableArrayBuilder<MethodInfo> extraPublicMethodsBuilder = new();

        foreach (IMethodSymbol methodSymbol in structSymbol.GetMembers().OfType<IMethodSymbol>()) {
            MethodInfo? methodInfo = null;
            if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.MemberFunctionAttribute, out AttributeData? mfAttribute)) {
                var relativeOffsets = ImmutableArray<ushort>.Empty;
                if (!mfAttribute.TryGetConstructorArgument(0, out string? signature))
                    continue;
                if (mfAttribute.ConstructorArguments.Length == 2) {
                    if (mfAttribute.ConstructorArguments[1].Kind == TypedConstantKind.Array &&
                        mfAttribute.TryGetMultiValueConstructorArgument(1, out ImmutableArray<ushort>? multipleOffsets))
                        relativeOffsets = multipleOffsets.Value;
                    else if (mfAttribute.TryGetConstructorArgument(1, out ushort? singleOffset))
                        relativeOffsets = [singleOffset.Value];
                    else
                        continue;
                }

                if (!TryParseMethod(methodSymbol, isInherited, token, out methodInfo))
                    continue;

                MemberFunctionInfo memberFunctionInfo = new(
                    methodInfo,
                    new SignatureInfo(signature, relativeOffsets));

                memberFunctionsBuilder.Add(memberFunctionInfo);
            } else if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.VirtualFunctionAttribute, out AttributeData? vfAttribute)) {
                if (vfAttribute.ConstructorArguments.Length != 1 ||
                    !vfAttribute.TryGetConstructorArgument(0, out uint? index))
                    continue;

                if (!TryParseMethod(methodSymbol, isInherited, token, out methodInfo))
                    continue;

                VirtualFunctionInfo virtualFunctionInfo = new(
                    methodInfo,
                    index.Value);

                virtualFunctionBuilder.Add(virtualFunctionInfo);
            } else if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.StaticAddressAttribute, out AttributeData? saAttribute)) {
                if (saAttribute.ConstructorArguments.Length != 3 ||
                    !saAttribute.TryGetConstructorArgument(0, out string? signature) ||
                    !saAttribute.TryGetConstructorArgument(2, out bool? isPointer))
                    continue;

                ImmutableArray<ushort> relativeOffsets;

                if (saAttribute.ConstructorArguments[1].Kind == TypedConstantKind.Array &&
                    saAttribute.TryGetMultiValueConstructorArgument(1, out ImmutableArray<ushort>? multipleOffsets)) {
                    relativeOffsets = multipleOffsets.Value;
                } else if (saAttribute.TryGetConstructorArgument(1, out ushort? singleOffset)) {
                    relativeOffsets = [singleOffset.Value];
                } else {
                    continue;
                }

                if (!TryParseMethod(methodSymbol, isInherited, token, out methodInfo))
                    continue;

                StaticAddressInfo staticAddressInfo = new(
                    methodInfo,
                    new SignatureInfo(signature, relativeOffsets),
                    isPointer.Value);

                staticAddressesBuilder.Add(staticAddressInfo);
            } else if (isInherited && methodSymbol.DeclaredAccessibility == Accessibility.Public) {
                if (!TryParseMethod(methodSymbol, isInherited, token, out methodInfo))
                    continue;

                extraPublicMethodsBuilder.Add(methodInfo);
            }
            if (methodSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.GenerateStringOverloadsAttribute, out _)) {
                if (methodInfo is null) {
                    if (!TryParseMethod(methodSymbol, isInherited, token, out methodInfo))
                        continue;
                }

                using ImmutableArrayBuilder<string> ignoredParametersBuilder = new();

                foreach (IParameterSymbol parameterSymbol in methodSymbol.Parameters) {
                    if (parameterSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.StringIgnoreAttribute, out _))
                        ignoredParametersBuilder.Add(parameterSymbol.Name);
                }

                EquatableArray<string> attributes = isInherited ? methodInfo.InheritableAttributes : ParseInheritedAttributes(methodSymbol, token);

                StringOverloadInfo stringOverloadInfo = new(
                    methodInfo,
                    ignoredParametersBuilder.ToImmutable(),
                    attributes);

                stringOverloadsBuilder.Add(stringOverloadInfo);

            }

            token.ThrowIfCancellationRequested();
        }
        memberFunctions = memberFunctionsBuilder.ToImmutable();
        virtualFunctions = virtualFunctionBuilder.ToImmutable();
        staticAddresses = staticAddressesBuilder.ToImmutable();
        stringOverloads = stringOverloadsBuilder.ToImmutable();
        extraPublicMethods = extraPublicMethodsBuilder.ToImmutable();
    }

    private static bool TryParseMethod(IMethodSymbol methodSymbol, bool isInherited, CancellationToken token, [NotNullWhen(true)] out MethodInfo? methodInfo) {
        if (!methodSymbol.TryGetSyntaxNode(token, out MethodDeclarationSyntax? methodSyntax)) {
            methodInfo = null;
            return false;
        }

        var constraints = string.Empty;
        ImmutableArrayBuilder<string> typeArguments = new();

        if (methodSymbol.TypeParameters.Any()) {
            ImmutableArray<SymbolDisplayPart> symbolDisplayParts = methodSymbol.ToDisplayParts(new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeConstraints));
            constraints = string.Join("", symbolDisplayParts[1..].Select(t => {
                if (t.Kind != SymbolDisplayPartKind.InterfaceName) return t.ToString();
                var typeArguments = ((INamedTypeSymbol)t.Symbol!).TypeArguments;
                return typeArguments.Length <= 0 ? t.ToString() : $"{t.ToString()}<{string.Join(", ", typeArguments.Select(t => t.Name))}>";
            }));
        }

        EquatableArray<string> inheritableAttributes = ParseInheritedAttributes(methodSymbol, token);

        methodInfo =
            new MethodInfo(
                methodSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                methodSyntax.Modifiers.ToString(),
                methodSymbol.ReturnType.GetFullyQualifiedName(),
                constraints,
                methodSymbol.IsStatic,
                methodSymbol.Parameters.Select(p => ParseParameter(p, isInherited, token)).ToImmutableArray(),
                inheritableAttributes,
                methodSymbol.TypeParameters.Select(t => t.Name).ToImmutableArray()
            );

        return true;
    }

    private static ParameterInfo ParseParameter(IParameterSymbol parameterSymbol, bool isInherited, CancellationToken token) => new(
        parameterSymbol.Name,
        parameterSymbol.Type.GetFullyQualifiedName(),
        parameterSymbol.GetDefaultValueString(),
        parameterSymbol.RefKind,
        isInherited ? ParseInheritedAttributes(parameterSymbol, token) : null);

    private static void ParseFields(INamedTypeSymbol structSymbol, CancellationToken token, bool isInherited,
        out EquatableArray<FixedSizeArrayInfo> fixedSizeArrays, out EquatableArray<BitFieldInfo> bitFields, out EquatableArray<FieldInfo> publicFields) {

        using ImmutableArrayBuilder<FixedSizeArrayInfo> fixedSizeArrayBuilder = new();
        using ImmutableArrayBuilder<BitFieldInfo> bitFieldBuilder = new();
        using ImmutableArrayBuilder<FieldInfo> publicFieldBuilder = new();

        foreach (IFieldSymbol fieldSymbol in structSymbol.GetMembers().OfType<IFieldSymbol>()) {
            if (fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName(InteropTypeNames.FixedSizeArrayAttribute, out AttributeData? fixedSizeArrayAttributeData)) {
                if (fieldSymbol.Type is not INamedTypeSymbol fieldTypeSymbol)
                    continue;

                if (!fieldTypeSymbol.IsGenericType || fieldTypeSymbol.TypeArguments.Length != 1)
                    continue;
                if (fieldTypeSymbol.Name.Length < 14)
                    continue;
                if (!int.TryParse(fieldTypeSymbol.Name[14..], out int size))
                    continue;

                if (!fixedSizeArrayAttributeData.TryGetConstructorArgument(0, out bool isString) ||
                    !fixedSizeArrayAttributeData.TryGetConstructorArgument(1, out bool isBitArray) ||
                    !fixedSizeArrayAttributeData.TryGetConstructorArgument(2, out int bitCount))
                    continue;

                EquatableArray<string> inheritableAttributes = ParseInheritedAttributes(fieldSymbol, token);

                FixedSizeArrayInfo fixedSizeArrayInfo = new(
                    fieldSymbol.Name,
                    fieldTypeSymbol.TypeArguments[0].GetFullyQualifiedName(),
                    size,
                    isString,
                    isBitArray,
                    bitCount,
                    inheritableAttributes
                );

                fixedSizeArrayBuilder.Add(fixedSizeArrayInfo);
            }

            foreach (AttributeData attributeData in fieldSymbol.GetAttributes()) {
                if (attributeData.AttributeClass is not { } attributeSymbol)
                    continue;

                if (!attributeSymbol.HasFullyQualifiedMetadataName(InteropTypeNames.BitFieldAttribute))
                    continue;

                if (!attributeData.TryGetConstructorArgument(0, out string? name) ||
                    !attributeData.TryGetConstructorArgument(1, out int index) ||
                    !attributeData.TryGetConstructorArgument(2, out int length))
                    continue;

                if (attributeSymbol.TypeArguments.Length != 1)
                    continue;
                Accessibility accessibility = Accessibility.Public;
                bool isPartial = false;
                bool hasGetter = true;
                bool hasSetter = true;
                bool isReadonlyGetter = true;
                foreach (IPropertySymbol propertySymbol in structSymbol.GetMembers().OfType<IPropertySymbol>()) {
                    if (propertySymbol.Name != name)
                        continue;

                    accessibility = propertySymbol.DeclaredAccessibility;
                    isPartial = propertySymbol.IsPartialDefinition;
                    hasGetter = propertySymbol.GetMethod != null;
                    hasSetter = !propertySymbol.IsReadOnly && propertySymbol.SetMethod != null;
                    isReadonlyGetter = propertySymbol.GetMethod?.IsReadOnly ?? false;

                    break;
                }

                string typeName = attributeSymbol.TypeArguments[0].GetFullyQualifiedName();

                EquatableArray<string> inheritableAttributes = ParseInheritedAttributes(fieldSymbol, token);

                BitFieldInfo bitFieldInfo = new(
                    fieldSymbol.Name,
                    name!,
                    typeName,
                    fieldSymbol.Type.GetFullyQualifiedName(),
                    index,
                    length,
                    accessibility,
                    isPartial,
                    hasGetter,
                    hasSetter,
                    isReadonlyGetter,
                    inheritableAttributes
                );

                bitFieldBuilder.Add(bitFieldInfo);
            }

            if (isInherited && fieldSymbol.DeclaredAccessibility == Accessibility.Public) {
                if (!fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("System.Runtime.InteropServices.FieldOffsetAttribute", out AttributeData? fieldOffsetAttributeData))
                    continue;

                if (!fieldOffsetAttributeData.TryGetConstructorArgument(0, out int fieldOffset))
                    continue;

                EquatableArray<string> inheritableAttributes = ParseInheritedAttributes(fieldSymbol, token);

                FieldInfo fieldInfo = new(
                    fieldSymbol.Name,
                    fieldSymbol.Type.GetFullyQualifiedName(),
                    fieldOffset,
                    inheritableAttributes,
                    fieldSymbol.IsReadOnly);

                publicFieldBuilder.Add(fieldInfo);
            }
            token.ThrowIfCancellationRequested();
        }

        fixedSizeArrays = fixedSizeArrayBuilder.ToImmutable();
        bitFields = bitFieldBuilder.ToImmutable();
        publicFields = publicFieldBuilder.ToImmutable();
    }

    private static void ParseProperties(INamedTypeSymbol structSymbol, CancellationToken token, out EquatableArray<PropertyInfo> publicProperties) {
        using ImmutableArrayBuilder<PropertyInfo> publicPropertiesBuilder = new();

        foreach (IPropertySymbol propertySymbol in structSymbol.GetMembers().OfType<IPropertySymbol>()) {
            if (propertySymbol.DeclaredAccessibility != Accessibility.Public ||
                propertySymbol.IsIndexer) {
                continue;
            }

            EquatableArray<string> inheritableAttributes = ParseInheritedAttributes(propertySymbol, token);

            PropertyInfo propertyInfo = new(
                propertySymbol.Name,
                propertySymbol.Type.GetFullyQualifiedName(),
                propertySymbol.RefKind,
                propertySymbol.GetMethod is not null,
                propertySymbol.SetMethod is not null,
                inheritableAttributes
            );

            publicPropertiesBuilder.Add(propertyInfo);

            token.ThrowIfCancellationRequested();
        }

        publicProperties = publicPropertiesBuilder.ToImmutable();
    }

    private static void ParseOwnMembers(INamedTypeSymbol structSymbol, EquatableArray<FixedSizeArrayInfo> fixedSizeArrays, CancellationToken token, out EquatableArray<OwnMemberInfo> ownMembers) {
        using ImmutableArrayBuilder<OwnMemberInfo> ownMembersBuilder = new();

        foreach (ISymbol memberSymbol in structSymbol.GetMembers()) {
            token.ThrowIfCancellationRequested();

            switch (memberSymbol) {
                case IFieldSymbol fieldSymbol:
                    if (fieldSymbol.IsStatic ||
                        !fieldSymbol.TryGetAttributeWithFullyQualifiedMetadataName("System.Runtime.InteropServices.FieldOffsetAttribute", out AttributeData? fieldOffsetAttributeData) ||
                        !fieldOffsetAttributeData.TryGetConstructorArgument(0, out int fieldOffset))
                        continue;

                    ownMembersBuilder.Add(new OwnMemberInfo(fieldSymbol.Name, fieldSymbol.Type.GetNameWithContainingTypeAndNamespace(), fieldOffset, false, string.Empty));

                    break;
                case IMethodSymbol methodSymbol:
                    if (methodSymbol.MethodKind != MethodKind.Ordinary)
                        continue;

                    ownMembersBuilder.Add(new OwnMemberInfo(methodSymbol.Name, string.Empty, -1, true, GetMethodSignature(methodSymbol)));

                    break;
                case IPropertySymbol propertySymbol:
                    if (propertySymbol.IsIndexer)
                        continue;

                    ownMembersBuilder.Add(new OwnMemberInfo(propertySymbol.Name, propertySymbol.Type.GetNameWithContainingTypeAndNamespace(), -1, false, string.Empty));

                    break;
            }
        }

        if (structSymbol.TryGetAttributeWithFullyQualifiedMetadataName("FFXIVClientStructs.Attributes.AgentAttribute", out _) ||
            structSymbol.TryGetAttributeWithFullyQualifiedMetadataName("FFXIVClientStructs.Attributes.InfoProxyAttribute", out _))
            ownMembersBuilder.Add(new OwnMemberInfo("Instance", string.Empty, -1, true, string.Empty));

        foreach (FixedSizeArrayInfo fixedSizeArrayInfo in fixedSizeArrays) {
            string publicFieldName = fixedSizeArrayInfo.GetPublicFieldName();

            ownMembersBuilder.Add(new OwnMemberInfo(publicFieldName, string.Empty, -1, false, string.Empty));
            ownMembersBuilder.Add(new OwnMemberInfo(publicFieldName + "String", string.Empty, -1, false, string.Empty));
            ownMembersBuilder.Add(new OwnMemberInfo(publicFieldName + "BitArray", string.Empty, -1, false, string.Empty));
        }

        ownMembers = ownMembersBuilder.ToImmutable();
    }

    private static string GetMethodSignature(IMethodSymbol methodSymbol) =>
        string.Join(", ", methodSymbol.Parameters.Select(parameter => parameter.RefKind.GetStringPrefix() + parameter.Type.GetFullyQualifiedName()));

    private static EquatableArray<string> ParseInheritedAttributes(ISymbol symbol, CancellationToken token) {
        ImmutableArrayBuilder<string> inheritedAttributes = new();

        foreach (AttributeData attributeData in symbol.GetAttributes()) {
            if (attributeData.AttributeClass?.GetFullyQualifiedMetadataName() is not { } attributeName ||
                InteropTypeNames.UninheritableAttributes.Contains(attributeName))
                continue;
            if (attributeData.ApplicationSyntaxReference is not { } syntaxReference)
                continue;

            SyntaxNode originalAttributeSyntax = syntaxReference.GetSyntax(token);
            AttributeArgumentListSyntax? argumentListSyntax = originalAttributeSyntax.ChildNodes().OfType<AttributeArgumentListSyntax>().FirstOrDefault();

            inheritedAttributes.Add(argumentListSyntax is not null ? $"[global::{attributeName}{argumentListSyntax.ToString()}]" : $"[global::{attributeName}]");
        }
        return inheritedAttributes.ToImmutable();
    }
}
