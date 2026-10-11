
using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FFXIVClientStructs.Expansion.Generators.Helpers;

/// <summary>
///     Extensions for <see cref="EquatableArray{T}" />.
/// </summary>
internal static class EquatableArray {
   
   
   
   
   
   
    public static EquatableArray<T> AsEquatableArray<T>(this ImmutableArray<T> array)
        where T : IEquatable<T> => new(array);
}

/// <summary>
///     An imutable, equatable array. This is equivalent to <see cref="ImmutableArray{T}" /> but with value equality
///     support.
/// </summary>
/// <typeparam name="T">The type of values in the array.</typeparam>
/// <param name="array">The input <see cref="ImmutableArray{T}" /> to wrap.</param>
internal readonly struct EquatableArray<T>(ImmutableArray<T> array) : IEquatable<EquatableArray<T>>, IEnumerable<T>
    where T : IEquatable<T> {
   
   
   
    private readonly T[]? _array = Unsafe.As<ImmutableArray<T>, T[]?>(ref array);

   
   
   
   
   
    public ref readonly T this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref AsImmutableArray().ItemRef(index);
    }

   
   
   
    public bool IsEmpty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsImmutableArray().IsEmpty;
    }

   
   
   
    public bool IsDefaultOrEmpty {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsImmutableArray().IsDefaultOrEmpty;
    }

   
   
   
    public int Length {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsImmutableArray().Length;
    }

   
    public bool Equals(EquatableArray<T> array) => AsSpan().SequenceEqual(array.AsSpan());

   
    public override bool Equals(object? obj) => obj is EquatableArray<T> array && Equals(this, array);

   
    public override unsafe int GetHashCode() {
        if (_array is not T[] array) {
            return 0;
        }

        HashCode hashCode = default;

        if (typeof(T) == typeof(byte)) {
            ReadOnlySpan<T> span = array;
            ref T? r0 = ref MemoryMarshal.GetReference(span)!;
            ref byte r1 = ref Unsafe.As<T, byte>(ref r0);

            fixed (byte* p = &r1) {
                ReadOnlySpan<byte> bytes = new(p, span.Length);

                hashCode.AddBytes(bytes);
            }
        } else {
            foreach (T item in array) {
                hashCode.Add(item);
            }
        }

        return hashCode.ToHashCode();
    }

   
   
   
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ImmutableArray<T> AsImmutableArray() => Unsafe.As<T[]?, ImmutableArray<T>>(ref Unsafe.AsRef(in _array));

   
   
   
   
   
    public static EquatableArray<T> FromImmutableArray(ImmutableArray<T> array) => new(array);

   
   
   
   
    public ReadOnlySpan<T> AsSpan() => AsImmutableArray().AsSpan();

   
   
   
   
    public T[] ToArray() => [.. AsImmutableArray()];

   
   
   
   
    public ImmutableArray<T>.Enumerator GetEnumerator() => AsImmutableArray().GetEnumerator();

   
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)AsImmutableArray()).GetEnumerator();

   
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)AsImmutableArray()).GetEnumerator();

   
   
   
   
    public static implicit operator EquatableArray<T>(ImmutableArray<T> array) => FromImmutableArray(array);

   
   
   
   
    public static implicit operator ImmutableArray<T>(EquatableArray<T> array) => array.AsImmutableArray();

   
   
   
   
   
   
    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right) => left.Equals(right);

   
   
   
   
   
   
    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right) => !left.Equals(right);
}
