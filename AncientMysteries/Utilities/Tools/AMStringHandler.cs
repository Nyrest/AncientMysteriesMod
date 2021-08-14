using System.Buffers;
using System.Globalization;
#nullable enable

namespace AncientMysteries.Utilities
{
    [InterpolatedStringHandler]
    public ref struct AMStringHandler
    {
        public static string AMStr(ref AMStringHandler stringHandler) => stringHandler.ToStringAndClear();

        private const int MinimumArrayPoolLength = 256;

        private readonly IFormatProvider? _provider;

        private char[]? _arrayToReturnToPool;

        private Span<char> _chars;

        private int _pos;

        private readonly bool _hasCustomFormatter;

        public ReadOnlySpan<char> Text => _chars.Slice(0, _pos);

        public AMStringHandler(int literalLength, int formattedCount)
        {
            _provider = null;
            _chars = (_arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount)));
            _pos = 0;
            _hasCustomFormatter = false;
        }

        public AMStringHandler(int literalLength, int formattedCount, IFormatProvider? provider)
        {
            _provider = provider;
            _chars = (_arrayToReturnToPool = ArrayPool<char>.Shared.Rent(GetDefaultLength(literalLength, formattedCount)));
            _pos = 0;
            _hasCustomFormatter = provider != null && HasCustomFormatter(provider);
        }

        public AMStringHandler(int literalLength, int formattedCount, IFormatProvider? provider, Span<char> initialBuffer)
        {
            _provider = provider;
            _chars = initialBuffer;
            _arrayToReturnToPool = null;
            _pos = 0;
            _hasCustomFormatter = provider != null && HasCustomFormatter(provider);
        }

        public AMStringHandler(Span<char> initialBuffer)
        {
            _provider = null;
            _chars = initialBuffer;
            _arrayToReturnToPool = null;
            _pos = 0;
            _hasCustomFormatter = false;
        }

        public void AppendLiteral(string value)
        {
            if (value.AsSpan().TryCopyTo(_chars.Slice(_pos)))
            {
                _pos += value.Length;
            }
            else
            {
                GrowThenCopyString(value);
            }
        }

        public void AppendChar(char value)
        {
            if (_pos == _chars.Length) Grow();
            _chars[_pos++] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendLiteralNoGrow(ReadOnlySpan<char> value)
        {
            value.TryCopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }

        public void AppendFormatted<T>(T value)
        {
            if (value is null) return;
            if (typeof(T) == typeof(Color))
            {
                AppendDGColorString(in Unsafe.As<T, Color>(ref value));
            }
            else if (typeof(T) == typeof(char))
            {
                AppendChar(Unsafe.As<T, char>(ref value));
            }
            else
            {
                AppendLiteral(value.ToString());
            }
        }

        public void AppendFormatted(ReadOnlySpan<char> value)
        {
            if (value.TryCopyTo(_chars.Slice(_pos)))
            {
                _pos += value.Length;
            }
            else
            {
                GrowThenCopySpan(value);
            }
        }

        public void AppendDGColorString(in Color color)
        {
            if ((_chars.Length - _pos) < 13)
                Grow(13);
            _chars[_pos++] = '|';
            // Fuck NetFx
            AppendLiteralNoGrow(color.r.ToString().AsSpan());
            _chars[_pos++] = ',';
            AppendLiteralNoGrow(color.g.ToString().AsSpan());
            _chars[_pos++] = ',';
            AppendLiteralNoGrow(color.b.ToString().AsSpan());
            _chars[_pos++] = '|';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetDefaultLength(int literalLength, int formattedCount)
            => Math.Max(256, literalLength + formattedCount * 11);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasCustomFormatter(IFormatProvider provider)
            => provider.GetType() != typeof(CultureInfo) && provider.GetFormat(typeof(ICustomFormatter)) != null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void GrowThenCopyString(string value)
        {
            Grow(value.Length);
            value.AsSpan().CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void GrowThenCopySpan(ReadOnlySpan<char> value)
        {
            Grow(value.Length);
            value.CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int additionalChars)
        {
            GrowCore((uint)(_pos + additionalChars));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow()
        {
            GrowCore((uint)(_chars.Length + 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // but reuse this grow logic directly in both of the above grow routines
        private void GrowCore(uint requiredMinCapacity)
        {
            // We want the max of how much space we actually required and doubling our capacity (without going beyond the max allowed length). We
            // also want to avoid asking for small arrays, to reduce the number of times we need to grow, and since we're working with unsigned
            // ints that could technically overflow if someone tried to, for example, append a huge string to a huge string, we also clamp to int.MaxValue.
            // Even if the array creation fails in such a case, we may later fail in ToStringAndClear.

            uint newCapacity = Math.Max(requiredMinCapacity, Math.Min((uint)_chars.Length * 2, 1073741791u));
            int arraySize = (int)Clamp(newCapacity, MinimumArrayPoolLength, int.MaxValue);

            char[] newArray = ArrayPool<char>.Shared.Rent(arraySize);
            _chars.Slice(0, _pos).CopyTo(newArray);

            char[]? toReturn = _arrayToReturnToPool;
            _chars = _arrayToReturnToPool = newArray;

            if (toReturn is not null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint Clamp(uint value, uint min, uint max)
                => value < min ? min : value > max ? max : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // used only on a few hot paths
        internal void Clear()
        {
            char[]? toReturn = _arrayToReturnToPool;
            this = default; // defensive clear
            if (toReturn is not null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
        }

        public override string ToString()
        {
            return Text.ToString();
        }

        public string ToStringAndClear()
        {
            string result = Text.ToString();
            Clear();
            return result;
        }
    }
}
