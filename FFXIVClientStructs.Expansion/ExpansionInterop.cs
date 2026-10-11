using FFXIVClientStructs.Expansion.Interop.Generated;
using FFXIVClientStructs.Expansion.Runtime;

namespace FFXIVClientStructs.Expansion;

public static class ExpansionInterop
{
    private static readonly object SyncRoot = new();

    public static void Initialize
    (
        nint moduleCopyPointer
    )
    {
        lock (SyncRoot)
        {
            var resolver = Resolver.GetInstance;
            resolver.Setup(moduleCopyPointer);
            Addresses.Register();
            resolver.Resolve();

            var unresolved = resolver.Addresses.Where(address => address.Value == 0).Select(address => address.Name).ToArray();
            if (unresolved.Length != 0)
                throw new InvalidOperationException($"Unresolved native addresses: {string.Join(", ", unresolved)}");
        }
    }

    public static void Uninitialize()
    {
        lock (SyncRoot)
        {
            var resolver = Resolver.GetInstance;
            foreach (var address in resolver.Addresses)
                address.Value = 0;
            Addresses.Unregister();
        }
    }
}
