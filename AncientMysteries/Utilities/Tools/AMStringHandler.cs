using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace AncientMysteries.Utilities.Tools
{
    [InterpolatedStringHandler]
    public ref struct AMStringHandler
    {
        private readonly IFormatProvider? _provider;

        private char[]? _arrayToReturnToPool;

        private Span<char> _chars;

        private int _pos;

        private readonly bool _hasCustomFormatter;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                AppendDGColorString(ref Unsafe.As<T, Color>(ref value));
            }
            else
            {
                AppendLiteral(value.ToString());
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void AppendDGColorString(ref Color color)
        {
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
        internal static int GetDefaultLength(int literalLength, int formattedCount)
            => Math.Max(256, literalLength + formattedCount * 11);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool HasCustomFormatter(IFormatProvider provider)
            => provider.GetType() != typeof(CultureInfo) && provider.GetFormat(typeof(ICustomFormatter)) != null;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void GrowThenCopyString(string value)
        {
            Grow(value.Length);
            value.AsSpan().CopyTo(_chars.Slice(_pos));
            _pos += value.Length;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int additionalChars)
        {
            GrowCore((uint)(_pos + additionalChars));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void GrowCore(uint requiredMinCapacity)
        {
            uint value = Math.Max(requiredMinCapacity, Math.Min((uint)(_chars.Length * 2), 1073741791u));
            int minimumLength = (int)Clamp(value, 256u, 2147483647u);
            char[] array = ArrayPool<char>.Shared.Rent(minimumLength);
            _chars.Slice(0, _pos).CopyTo(array);
            char[]? arrayToReturnToPool = _arrayToReturnToPool;
            _chars = _arrayToReturnToPool = array;
            if (arrayToReturnToPool != null)
            {
                ArrayPool<char>.Shared.Return(arrayToReturnToPool);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static uint Clamp(uint value, uint min, uint max)
            => value < min ? min : value > max ? max : value;
        }
    }
}
