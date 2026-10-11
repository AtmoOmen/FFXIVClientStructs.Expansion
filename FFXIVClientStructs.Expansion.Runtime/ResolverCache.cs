using System.Collections.Concurrent;

namespace FFXIVClientStructs.Expansion.Runtime;

internal sealed record ResolverCache(string Version, ConcurrentDictionary<string, long> Cache);
