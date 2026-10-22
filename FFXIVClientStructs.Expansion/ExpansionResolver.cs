using System.Collections.Concurrent;
using System.Diagnostics;

namespace FFXIVClientStructs.Expansion;

/// <summary>
///     仅用于本程序集新增地址的独立签名解析器。
/// </summary>
public sealed class ExpansionResolver
{
    private readonly HashSet<Address> addresses = [];

    private nint baseAddress;
    private nint targetSpace;
    private int  targetLength;

    private int textSectionOffset;
    private int textSectionSize;

    /// <summary>
    ///     获取当前已登记的全部原生地址。
    /// </summary>
    public IReadOnlyCollection<Address> Addresses => addresses;

    /// <summary>
    ///     登记一条原生地址。
    /// </summary>
    /// <param name="address">待登记的原生地址。</param>
    public void Register
    (
        Address address
    ) =>
        addresses.Add(address);

    /// <summary>
    ///     注销一条原生地址。
    /// </summary>
    /// <param name="address">待注销的原生地址。</param>
    public void Unregister
    (
        Address address
    ) =>
        addresses.Remove(address);

    /// <summary>
    ///     清空全部已登记的原生地址。
    /// </summary>
    public void Reset() =>
        addresses.Clear();

    /// <summary>
    ///     扫描客户端模块副本，解析全部尚未解析的原生地址。
    /// </summary>
    /// <param name="moduleCopyPointer">用于签名扫描的模块副本基址，为零时使用主模块基址。</param>
    /// <exception cref="InvalidOperationException">无法访问进程模块。</exception>
    public unsafe void Resolve
    (
        nint moduleCopyPointer
    )
    {
        var module = Process.GetCurrentProcess().MainModule ??
                     throw new InvalidOperationException("[FFXIVClientStructs.Expansion] 无法访问进程模块");

        baseAddress = module.BaseAddress;
        targetSpace = moduleCopyPointer == 0 ?
                          baseAddress :
                          moduleCopyPointer;
        targetLength = module.ModuleMemorySize;

        SetupSections();

        var remainingAddresses = addresses.Where(address => address.Value == 0).ToArray();
        var remainingCount     = remainingAddresses.Length;

        if (remainingCount == 0)
            return;

        var addressValues = (nint*)NativeMemory.AllocZeroed((nuint)remainingCount * (nuint)sizeof(nint));
        var buckets       = new BucketAddressEntry*[256];
        var bucketData    = new ulong*[256];
        var bucketLengths = new int[256];

        try
        {
            var grouped = remainingAddresses
                          .Select((address, index) => new { Address = address, GlobalIndex = index })
                          .GroupBy(item => (byte)item.Address.Bytes[0]);

            foreach (var group in grouped)
            {
                var firstByte = group.Key;
                var entries   = group.ToArray();

                bucketLengths[firstByte] = entries.Length;

                var ulongCount = entries.Sum(entry => entry.Address.Bytes.Length) * 2;

                buckets[firstByte]    = (BucketAddressEntry*)NativeMemory.Alloc((nuint)entries.Length * (nuint)sizeof(BucketAddressEntry));
                bucketData[firstByte] = (ulong*)NativeMemory.Alloc((nuint)ulongCount                  * sizeof(ulong));

                var dataPtr = bucketData[firstByte];

                for (var i = 0; i < entries.Length; i++)
                {
                    var entry    = entries[i];
                    var bytesLen = entry.Address.Bytes.Length;

                    buckets[firstByte][i] = new BucketAddressEntry
                    {
                        ValuePtr     = &addressValues[entry.GlobalIndex],
                        BytesPtr     = dataPtr,
                        MaskPtr      = dataPtr + bytesLen,
                        BytesLen     = bytesLen,
                        AddressIndex = entry.GlobalIndex
                    };

                    fixed (ulong* sourceBytes = entry.Address.Bytes)
                    fixed (ulong* sourceMask = entry.Address.Mask)
                    {
                        Buffer.MemoryCopy(sourceBytes, dataPtr,            bytesLen * sizeof(ulong), bytesLen * sizeof(ulong));
                        Buffer.MemoryCopy(sourceMask,  dataPtr + bytesLen, bytesLen * sizeof(ulong), bytesLen * sizeof(ulong));
                    }

                    dataPtr += bytesLen * 2;
                }
            }

            var partitioner = Partitioner.Create(0, textSectionSize, textSectionSize / Environment.ProcessorCount);
            var textPointer = (byte*)targetSpace + textSectionOffset;

            Parallel.ForEach
            (
                partitioner,
                (range, loopState) =>
                {
                    for (var location = range.Item1; !loopState.IsStopped && location < range.Item2; location++)
                    {
                        var currentByte = textPointer[location];
                        var bucket      = buckets[currentByte];

                        if (bucket == null)
                            continue;

                        var bucketLength = bucketLengths[currentByte];

                        if (bucketLength == 0)
                            continue;

                        var targetLocation = (ulong*)(textPointer + location);

                        for (var i = 0; i < bucketLength; i++)
                        {
                            ref readonly var target = ref bucket[i];

                            if (*target.ValuePtr != 0)
                                continue;

                            int count;

                            for (count = 0; count < target.BytesLen; count++)
                            {
                                if ((target.MaskPtr[count] & target.BytesPtr[count]) != (target.MaskPtr[count] & targetLocation[count]))
                                    break;
                            }

                            if (count != target.BytesLen)
                                continue;

                            var outLocation     = location;
                            var originalAddress = remainingAddresses[target.AddressIndex];

                            foreach (var relativeOffset in originalAddress.RelativeFollowOffsets)
                            {
                                var offset = *(int*)(textPointer + outLocation + relativeOffset);
                                outLocation = outLocation + relativeOffset + 4 + offset;
                            }

                            var finalAddress = baseAddress + textSectionOffset + outLocation;

                            if (Interlocked.CompareExchange(ref *target.ValuePtr, finalAddress, 0) == 0)
                            {
                                if (Interlocked.Decrement(ref remainingCount) == 0)
                                {
                                    loopState.Stop();
                                    return;
                                }
                            }
                        }
                    }
                }
            );

            for (var i = 0; i < remainingAddresses.Length; i++)
            {
                var foundAddress = addressValues[i];

                if (foundAddress != 0)
                    remainingAddresses[i].Value = foundAddress;
            }
        }
        finally
        {
            NativeMemory.Free(addressValues);

            for (var i = 0; i < 256; i++)
            {
                if (buckets[i] != null)
                    NativeMemory.Free(buckets[i]);

                if (bucketData[i] != null)
                    NativeMemory.Free(bucketData[i]);
            }
        }
    }

    private unsafe void SetupSections()
    {
        ReadOnlySpan<byte> moduleBytes = new(baseAddress.ToPointer(), targetLength);

        var ntNewOffset = BitConverter.ToInt32(moduleBytes.Slice(0x3C, 4));
        var ntHeader    = moduleBytes[ntNewOffset..];

        var fileHeader     = ntHeader[4..];
        var numSections    = BitConverter.ToInt16(ntHeader.Slice(6, 2));
        var optionalHeader = fileHeader[20..];
        var sectionHeader  = optionalHeader[240..];
        var sectionCursor  = sectionHeader;

        for (var i = 0; i < numSections; i++)
        {
            var sectionName = BitConverter.ToInt64(sectionCursor);

            if (sectionName == 0x747865742E)
            {
                textSectionOffset = BitConverter.ToInt32(sectionCursor.Slice(12, 4));
                textSectionSize   = BitConverter.ToInt32(sectionCursor.Slice(8,  4));

                break;
            }

            sectionCursor = sectionCursor[40..];
        }
    }

    private unsafe struct BucketAddressEntry
    {
        public nint*  ValuePtr;
        public ulong* BytesPtr;
        public ulong* MaskPtr;
        public int    BytesLen;
        public int    AddressIndex;
    }
}
