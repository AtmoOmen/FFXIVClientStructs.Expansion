using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace FFXIVClientStructs.Expansion.Generators.Generator;

[Generator]
public sealed class InstanceGenerator : IIncrementalGenerator
{
    public void Initialize
    (
        IncrementalGeneratorInitializationContext context
    )
    {
        Register(context, "FFXIVClientStructs.Attributes.AgentAttribute",     "Agent", "AgentModule", "GetAgentByInternalId", "AgentId");
        Register(context, "FFXIVClientStructs.Attributes.InfoProxyAttribute", "Info",  "InfoModule",  "GetInfoProxyById",     "InfoProxyId");
    }

    private static void Register
    (
        IncrementalGeneratorInitializationContext context,
        string                                    attributeName,
        string                                    category,
        string                                    module,
        string                                    getter,
        string                                    idType
    )
    {
        var targets = context.SyntaxProvider.ForAttributeWithMetadataName
        (
            attributeName,
            static (node, _) => node is StructDeclarationSyntax,
            static (attributeContext, _) =>
            {
                var symbol = (INamedTypeSymbol)attributeContext.TargetSymbol;
                var id     = (uint)attributeContext.Attributes[0].ConstructorArguments[0].Value!;
                return (Namespace: symbol.ContainingNamespace.ToDisplayString(), symbol.Name, Id: id);
            }
        );

        context.RegisterSourceOutput
        (
            targets,
            (sourceContext, target) =>
            {
                var source = $$"""
                               using FFXIVClientStructs.FFXIV.Client.UI.{{category}};
                               using NativeModule = FFXIVClientStructs.FFXIV.Client.UI.{{category}}.{{module}};
                               namespace {{target.Namespace}};
                               public unsafe partial struct {{target.Name}}
                               {
                                   public static {{target.Name}}* Instance()
                                   {
                                       var module = NativeModule.Instance();
                                       return module == null ? null : ({{target.Name}}*)module->{{getter}}(({{idType}}){{target.Id}});
                                   }
                               }
                               """;
                sourceContext.AddSource($"{target.Namespace}.{target.Name}.Instance.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        );
    }
}
