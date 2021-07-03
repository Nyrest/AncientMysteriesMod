using System.Diagnostics;

namespace AncientMysteries.Utilities.Collections
{
    public sealed class DictSlim<TKey, TValue> where TKey : IEquatable<TKey>
    {
        #region Fields
        private static readonly Entry[] InitialEntries = new Entry[1];

        private static readonly int[] SizeOneIntArray = new int[1];
        // 1-based index into _entries; 0 means empty
        private int[] _buckets;

        private int _count;
        private Entry[] _entries;

        // 0-based index into _entries of head of free chain: -1 means empty
        private int _freeList = -1;

        #endregion Fields

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue GetOrAddValueRef(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            uint entriesLength = (uint)_entries.Length;
            int bucketIndex = key.GetHashCode() & (_buckets.Length - 1);
            for (int i = _buckets[bucketIndex] - 1;
                    (uint)i < entriesLength; i = _entries[i].next)
            {
                ref Entry entry = ref _entries[i];
                if (key.Equals(entry.key))
                    return ref entry.value;

            }
            return ref AddKey(key, bucketIndex);
        }


        public ref TValue AddKey(TKey key)
        {
            int bucketIndex = key.GetHashCode() & (_buckets.Length - 1);
            int entryIndex;
            if (_freeList != -1)
            {
                entryIndex = _freeList;
                _freeList = -3 - _entries[_freeList].next;
            }
            else
            {
                if (_count == _entries.Length || _entries.Length == 1)
                {
                    _entries = Resize();
                    bucketIndex = key.GetHashCode() & (_buckets.Length - 1);
                    // entry indexes were not changed by Resize
                }
                entryIndex = _count;
            }
            ref Entry entry = ref _entries[entryIndex];
            entry.key = key;
            entry.next = _buckets[bucketIndex] - 1;
            entry.hashCode = (uint)key.GetHashCode();
            _buckets[bucketIndex] = entryIndex + 1;
            _count++;
            return ref entry.value;
        }

        public ref TValue AddKey(TKey key, int bucketIndex)
        {
            int entryIndex;
            if (_freeList != -1)
            {
                entryIndex = _freeList;
                _freeList = -3 - _entries[_freeList].next;
            }
            else
            {
                if (_count == _entries.Length || _entries.Length == 1)
                {
                    _entries = Resize();
                    bucketIndex = key.GetHashCode() & (_buckets.Length - 1);
                }
                entryIndex = _count;
            }

            ref Entry entry = ref _entries[entryIndex];
            entry.key = key;
            entry.next = _buckets[bucketIndex] - 1;
            _buckets[bucketIndex] = entryIndex + 1;
            _count++;
            return ref entry.value;
        }

        private Entry[] Resize()
        {
            const uint int32Max = int.MaxValue;
            int count = _count;
            int newSize = _entries.Length * 2;
            if ((uint)newSize > int32Max)
                ThrowInvalidOperationException();
            [MethodImpl(MethodImplOptions.NoInlining)]
            static void ThrowInvalidOperationException() => throw new InvalidOperationException("Arg_HTCapacityOverflow");

            var newEntries = new Entry[newSize];
            new Span<Entry>(_entries).CopyTo(new Span<Entry>(newEntries, 0, count));

            var newBuckets = new int[newEntries.Length];
            while (count-- > 0)
            {
                int bucketIndex = newEntries[count].key.GetHashCode() & (newBuckets.Length - 1);
                newEntries[count].next = newBuckets[bucketIndex] - 1;
                newBuckets[bucketIndex] = count + 1;
            }

            _buckets = newBuckets;
            _entries = newEntries;

            return newEntries;
        }

        [DebuggerDisplay("({key}, {value})->{next}")]
        public struct Entry
        {
            #region Fields

            /// <summary>
            /// Key
            /// </summary>
            public TKey key;

            /// <summary>
            /// 0-based index of next entry in chain: -1 means end of chain
            /// also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
            /// so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
            /// </summary>
            public int next;

            /// <summary>
            /// Value
            /// </summary>
            public TValue value;

            public uint hashCode;
            #endregion Fields
        }
    }
}
