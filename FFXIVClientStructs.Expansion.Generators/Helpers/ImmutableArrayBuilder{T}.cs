
using System.Collections;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace FFXIVClientStructs.Expansion.Generators.Helpers;

/// <summary>
///     A helper type to build sequences of values with pooled buffers.
/// </summary>
/// <typeparam name="T">The type of items to create sequences for.</typeparam>
internal struct ImmutableArrayBuilder<T> : IDisposable {
   
   
   
    private static readonly ObjectPool<Writer> SharedObjectPool = new(static () => new Writer());

   
   
   
    private Writer? _writer;

   
   
   
    public ImmutableArrayBuilder() {
        _writer = SharedObjectPool.Allocate();
    }

   
   
   
    public readonly ReadOnlySpan<T> WrittenSpan {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _writer!.WrittenSpan;
    }

   
   
   
    public readonly int Count {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _writer!.Count;
    }

   
   
   
   
   
   
   
   
   
    public readonly Span<T> Advance(int requestedSize) => _writer!.Advance(requestedSize);

   
    public readonly void Add(T item) {
        _writer!.Add(item);
    }

   
   
   
   
    public readonly void AddRange(ReadOnlySpan<T> items) {
        _writer!.AddRange(items);
    }

   
    public readonly void Clear() {
        _writer!.Clear();
    }

   
   
   
   
   
    public readonly void Insert(int index, T item) {
        _writer!.Insert(index, item);
    }

   
   
   
   
   
   
   
    public readonly IEnumerable<T> AsEnumerable() => _writer!;

   
    public readonly ImmutableArray<T> ToImmutable() {
        T[]? array = _writer!.WrittenSpan.ToArray();

        return Unsafe.As<T[], ImmutableArray<T>>(ref array);
    }

   
    public readonly T[] ToArray() => _writer!.WrittenSpan.ToArray();

   
    public readonly override string ToString() => _writer!.WrittenSpan.ToString();

   
    public void Dispose() {
        Writer? writer = _writer;

        _writer = null;

        if (writer is not null) {
            writer.Clear();

            SharedObjectPool.Free(writer);
        }
    }

   
   
   
    private sealed class Writer : IList<T>, IReadOnlyList<T> {
       
       
       
        private T[] _array;

       
       
       
        private int _index;

       
       
       
        public Writer() {
            if (typeof(T) == typeof(char)) {
                _array = new T[1024];
            } else {
                _array = new T[8];
            }

            _index = 0;
        }

       
        public ReadOnlySpan<T> WrittenSpan {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(_array, 0, _index);
        }

       
        public int Count => _index;

       
        bool ICollection<T>.IsReadOnly => true;

       
        T IList<T>.this[int index] {
            get => WrittenSpan[index];
            set => throw new NotSupportedException();
        }

       
        public void Add(T value) {
            EnsureCapacity(1);

            _array[_index++] = value;
        }

       
        public void Insert(int index, T item) {
            if (index < 0 || index > _index) {
                ImmutableArrayBuilder.ThrowArgumentOutOfRangeExceptionForIndex();
            }

            EnsureCapacity(1);

            if (index < _index) {
                Array.Copy(_array, index, _array, index + 1, _index - index);
            }

            _array[index] = item;
            _index++;
        }

       
       
       
        public void Clear() {
            if (typeof(T) != typeof(byte) &&
                typeof(T) != typeof(char) &&
                typeof(T) != typeof(int)) {
                _array.AsSpan(0, _index).Clear();
            }

            _index = 0;
        }

       
        int IList<T>.IndexOf(T item) => Array.IndexOf(_array, item, 0, _index);

       
        void IList<T>.RemoveAt(int index) {
            throw new NotSupportedException();
        }

       
        bool ICollection<T>.Contains(T item) => Array.IndexOf(_array, item, 0, _index) >= 0;

       
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            Array.Copy(_array, 0, array, arrayIndex, _index);
        }

       
        bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

       
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            T?[] array = _array;
            int length = _index;

            for (var i = 0; i < length; i++) {
                yield return array[i]!;
            }
        }

       
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

       
        T IReadOnlyList<T>.this[int index] => WrittenSpan[index];

       
        public Span<T> Advance(int requestedSize) {
            EnsureCapacity(requestedSize);

            Span<T> span = _array.AsSpan(_index, requestedSize);

            _index += requestedSize;

            return span;
        }

       
        public void AddRange(ReadOnlySpan<T> items) {
            EnsureCapacity(items.Length);

            items.CopyTo(_array.AsSpan(_index));

            _index += items.Length;
        }

       
        public void Clear(ReadOnlySpan<T> items) {
            _index = 0;
        }

       
       
       
       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureCapacity(int requestedSize) {
            if (requestedSize > _array.Length - _index) {
                ResizeBuffer(requestedSize);
            }
        }

       
       
       
       
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ResizeBuffer(int sizeHint) {
            int minimumSize = _index + sizeHint;
            int requestedSize = Math.Max(_array.Length * 2, minimumSize);

            var newArray = new T[requestedSize];

            Array.Copy(_array, newArray, _index);

            _array = newArray;
        }
    }
}

/// <summary>
///     Private helpers for the <see cref="ImmutableArrayBuilder{T}" /> type.
/// </summary>
file static class ImmutableArrayBuilder {
   
   
   
    public static void ThrowArgumentOutOfRangeExceptionForIndex() {
        throw new ArgumentOutOfRangeException("index");
    }
}
