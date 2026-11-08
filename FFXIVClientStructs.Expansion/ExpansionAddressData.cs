using System.IO.Compression;

namespace FFXIVClientStructs.Expansion;

internal readonly struct ExpansionAddressData
{
    private ExpansionAddressData(Address address, string signature)
    {
        Address   = address;
        Signature = signature;
    }

    internal Address Address { get; }

    internal string Signature { get; }

    internal static ExpansionAddressData[] Load(byte[] data)
    {
        using var stream       = new MemoryStream(data, false);
        using var decompressor = new DeflateStream(stream, CompressionMode.Decompress);
        using var reader       = new BinaryReader(decompressor);

        var addresses = new ExpansionAddressData[reader.ReadInt32()];

        for (var addressIndex = 0; addressIndex < addresses.Length; addressIndex++)
        {
            var name            = reader.ReadString();
            var signature       = reader.ReadString();
            var paddedSignature = reader.ReadString();
            var offsets         = new ushort[reader.ReadInt32()];

            for (var offsetIndex = 0; offsetIndex < offsets.Length; offsetIndex++)
                offsets[offsetIndex] = reader.ReadUInt16();

            var wordCount = reader.ReadInt32();
            var bytes     = new ulong[wordCount];
            var mask      = new ulong[wordCount];

            for (var wordIndex = 0; wordIndex < wordCount; wordIndex++)
            {
                bytes[wordIndex] = reader.ReadUInt64();
                mask[wordIndex]  = reader.ReadUInt64();
            }

            addresses[addressIndex] = new ExpansionAddressData(new Address(name, paddedSignature, offsets, bytes, mask, 0), signature);
        }

        return addresses;
    }
}
