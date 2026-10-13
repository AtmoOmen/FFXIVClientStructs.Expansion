namespace FFXIVClientStructs.Expansion;

public static class ExpansionInterop
{
    private static readonly Lock SyncRoot = new();

    private static readonly HashSet<Address> OwnedAddresses = [];

    public static void Initialize
    (
        nint moduleCopyPointer
    )
    {
        lock (SyncRoot)
        {
            var resolver = Resolver.GetInstance;
            resolver.Setup(moduleCopyPointer);

            HashSet<Address> registered = [.. resolver.Addresses];

            Addresses.Register();
            OwnedAddresses.Clear();
            OwnedAddresses.UnionWith(resolver.Addresses.Where(address => !registered.Contains(address)));
            resolver.Resolve();

            var unresolved = OwnedAddresses.Where(address => address.Value == 0).Select(address => address.Name).ToArray();
            if (unresolved.Length != 0)
                throw new InvalidOperationException($"Unresolved native addresses: {string.Join(", ", unresolved)}");
        }
    }

    public static void Uninitialize()
    {
        lock (SyncRoot)
        {
            foreach (var address in OwnedAddresses)
                address.Value = 0;

            OwnedAddresses.Clear();

            Addresses.Unregister();
        }
    }
}
