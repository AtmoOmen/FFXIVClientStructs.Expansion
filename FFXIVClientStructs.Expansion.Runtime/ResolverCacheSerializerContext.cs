using System.Text.Json.Serialization;

namespace FFXIVClientStructs.Expansion.Runtime;

[JsonSerializable(typeof(ResolverCache))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class ResolverCacheSerializerContext : JsonSerializerContext {
}
