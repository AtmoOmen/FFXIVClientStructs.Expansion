namespace FFXIVClientStructs.Expansion;

/// <summary>
///     管理本程序集全部新增原生地址的注册与解析。
/// </summary>
public static class ExpansionInterop
{
    private static readonly Lock              SyncRoot = new();
    private static readonly ExpansionResolver Resolver = new();

    /// <summary>
    ///     注册本程序集声明的全部原生地址并完成解析，未能解析的地址仅记录错误，不中断初始化。
    /// </summary>
    /// <param name="moduleCopyPointer">用于签名扫描的模块副本基址。</param>
    /// <param name="logError">由调用方提供的错误日志输出方法，接收包含未解析地址名称的完整错误消息。</param>
    public static void Initialize
    (
        nint           moduleCopyPointer,
        Action<string> logError
    )
    {
        lock (SyncRoot)
        {
            Addresses.Register(Resolver);

            Resolver.Resolve(moduleCopyPointer);

            var unresolved = Resolver.Addresses.Where(address => address.Value == 0)
                                     .Select(address => address.Name)
                                     .ToArray();

            if (unresolved.Length != 0)
                logError($"[FFXIVClientStructs.Expansion] 未能解析以下地址：\n{string.Join("\n", unresolved)}");
        }
    }

    /// <summary>
    ///     清除本程序集声明的原生地址的解析结果并注销登记。
    /// </summary>
    public static void Uninitialize()
    {
        lock (SyncRoot)
        {
            foreach (var address in Resolver.Addresses)
                address.Value = 0;

            Addresses.Unregister(Resolver);

            Resolver.Reset();
        }
    }
}
