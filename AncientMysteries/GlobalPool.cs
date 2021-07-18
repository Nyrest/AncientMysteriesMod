using AncientMysteries.Helpers;

namespace AncientMysteries
{
    public static class GlobalPool<T>
    {
        public static T[] _array = new T[4];

        public static int _size;

        public static T Rent()
        {
            int index = _size - 1;
            if ((uint)index < (uint)_array.Length)
            {
                T result = _array[_size = index];
                _array[index] = default!;
                return result;
            }
            return GenericNew<T>.CreateInstance();
        }

        public static void Return(T item)
        {
            if ((uint)_size < (uint)_array.Length)
            {
                _array[_size++] = item;
            }
            else
            {
                ResizeThenReturn(item);
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            static void ResizeThenReturn(T item)
            {
                Array.Resize(ref _array, 2 * _array.Length);
                _array[_size++] = item;
            }
        }
    }
}