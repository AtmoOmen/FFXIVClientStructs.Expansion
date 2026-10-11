
using System.Runtime.CompilerServices;

#pragma warning disable RS1035

namespace FFXIVClientStructs.Expansion.Generators.Helpers;

/// <summary>
///     <para>
///         Generic implementation of object pooling pattern with predefined pool size limit. The main purpose
///         is that limited number of frequently used objects can be kept in the pool for further recycling.
///     </para>
///     <para>
///         Notes:
///         <list type="number">
///             <item>
///                 It is not the goal to keep all returned objects. Pool is not meant for storage. If there
///                 is no space in the pool, extra returned objects will be dropped.
///             </item>
///             <item>
///                 It is implied that if object was obtained from a pool, the caller will return it back in
///                 a relatively short time. Keeping checked out objects for long durations is ok, but
///                 reduces usefulness of pooling. Just new up your own.
///             </item>
///         </list>
///     </para>
///     <para>
///         Not returning objects to the pool in not detrimental to the pool's work, but is a bad practice.
///         Rationale: if there is no intent for reusing the object, do not use pool - just use "new".
///     </para>
/// </summary>
/// <typeparam name="T">The type of objects to pool.</typeparam>
/// <param name="factory">The input factory to produce <typeparamref name="T" /> items.</param>
/// <remarks>
///     The factory is stored for the lifetime of the pool. We will call this only when pool needs to
///     expand. compared to "new T()", Func gives more flexibility to implementers and faster than "new T()".
/// </remarks>
/// <param name="size">The pool size to use.</param>
internal sealed class ObjectPool<T>(Func<T> factory, int size)
    where T : class {
   
   
   
    private readonly Element[] _items = new Element[size - 1];

   
   
   
   
    private T? _firstItem;

   
   
   
   
    public ObjectPool(Func<T> factory)
        : this(factory, Environment.ProcessorCount * 2) {
    }

   
   
   
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Allocate() {
        T? item = _firstItem;

        if (item is null || item != Interlocked.CompareExchange(ref _firstItem, null, item)) {
            item = AllocateSlow();
        }

        return item;
    }

   
   
   
   
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Free(T obj) {
        if (_firstItem is null) {
            _firstItem = obj;
        } else {
            FreeSlow(obj);
        }
    }

   
   
   
   
    [MethodImpl(MethodImplOptions.NoInlining)]
    private T AllocateSlow() {
        foreach (ref Element element in _items.AsSpan()) {
            T? instance = element.Value;

            if (instance is not null) {
                if (instance == Interlocked.CompareExchange(ref element.Value, null, instance)) {
                    return instance;
                }
            }
        }

        return factory();
    }

   
   
   
   
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void FreeSlow(T obj) {
        foreach (ref Element element in _items.AsSpan()) {
            if (element.Value is null) {
                element.Value = obj;

                break;
            }
        }
    }

   
   
   
    private struct Element {
       
       
       
        internal T? Value;
    }
}
