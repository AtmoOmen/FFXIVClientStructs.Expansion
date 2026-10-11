
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace FFXIVClientStructs.Expansion.Runtime;

/// <summary>
/// Represents a compact array of bits stored in unmanaged memory.
/// </summary>
public readonly unsafe struct BitArray(byte* ptr, int bitCount) {
   
   
   
    public byte* Pointer => ptr;

   
   
   
    public int BitCount => bitCount;

   
   
   
    public int ByteLength => (BitCount + 7) / 8;

   
   
   
    public int PopCount {
        get {
            int popCount = 0;

            int fullBytes = BitCount / 8;
            for (int i = 0; i < fullBytes; i++)
                popCount += BitOperations.PopCount(ptr[i]);

            int remainingBits = BitCount & 7;
            if (remainingBits > 0)
                popCount += BitOperations.PopCount((byte)(ptr[fullBytes] & ((1 << remainingBits) - 1)));

            return popCount;
        }
    }

   
   
   
   
   
   
   
   
    public bool this[int index] {
        get => Get(index);
        set => Set(index, value);
    }

   
   
   
   
   
   
   
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Get(int index) {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, bitCount, nameof(index));

        (uint byteIndex, uint bitOffset) = Math.DivRem((uint)index, 8);
        return (ptr[byteIndex] & (1 << (int)bitOffset)) != 0;
    }

   
   
   
   
   
   
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index, bool value) {
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, bitCount, nameof(index));

        (uint byteIndex, uint bitOffset) = Math.DivRem((uint)index, 8);

        ref byte segment = ref ptr[byteIndex];
        byte bitMask = (byte)(1 << (int)bitOffset);
        if (value) {
            segment |= bitMask;
        } else {
            segment &= (byte)~bitMask;
        }
    }

   
   
   
   
   
   
   
   
   
   
   
   
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(int index, out bool value) {
        if ((uint)index >= (uint)bitCount) {
            value = false;
            return false;
        }

        (uint byteIndex, uint bitOffset) = Math.DivRem((uint)index, 8);
        value = (ptr[byteIndex] & (1 << (int)bitOffset)) != 0;
        return true;
    }

   
   
   
   
   
   
   
   
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TrySet(int index, bool value) {
        if ((uint)index >= (uint)bitCount) {
            return false;
        }

        (uint byteIndex, uint bitOffset) = Math.DivRem((uint)index, 8);

        ref byte segment = ref ptr[byteIndex];
        byte bitMask = (byte)(1 << (int)bitOffset);
        if (value) {
            segment |= bitMask;
        } else {
            segment &= (byte)~bitMask;
        }

        return true;
    }

   
   
   
    public BitArrayEnumerator GetEnumerator() => new(this);

   
   
   
    public ref struct BitArrayEnumerator(BitArray bitArray) {
        private int _index = -1;

       
       
       
        public readonly (int Index, bool Value) Current => (_index, bitArray[_index]);

       
       
       
       
        public bool MoveNext() => ++_index < bitArray.BitCount;

       
       
       
        public void Reset() {
            _index = -1;
        }
    }
}
