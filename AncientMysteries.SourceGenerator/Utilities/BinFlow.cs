using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace AncientMysteries.SourceGenerator.Utilities
{
    public readonly ref struct BinFlow
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        private readonly Stream stream;

        private readonly byte[] buffer4;

        public long Position
        {
            readonly get => stream.Position;
            set => stream.Position = value;
        }

        public long Length
        {
            readonly get => stream.Length;
            set => stream.SetLength(value);
        }

        public readonly long Remainder
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => stream.Length - stream.Position;
        }

        public BinFlow(Stream stream)
        {
            this.stream = stream;
            this.buffer4 = new byte[4];
        }

        #region Int
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteInt(int value)
        {
            Unsafe.CopyBlock(ref buffer4[0], ref Unsafe.As<int, byte>(ref value), 4);
            stream.Write(buffer4, 0, 4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryReadInt(out int value)
        {
            if (Remainder < 4)
            {
                value = 0;
                return false;
            }
            stream.Read(buffer4, 0, 4);
            value = Unsafe.ReadUnaligned<int>(ref buffer4[0]);
            return true;
        }
        #endregion

        #region Unmanaged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUnmanaged<T>(T value) where T : unmanaged
        {
            Unsafe.CopyBlock(ref buffer4[0], ref Unsafe.As<T, byte>(ref value), (uint)Unsafe.SizeOf<T>());
            stream.Write(buffer4, 0, Unsafe.SizeOf<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryReadUnmanaged<T>(out T value) where T : unmanaged
        {
            if (Remainder < Unsafe.SizeOf<T>())
            {
                value = default;
                return false;
            }
            byte[] buffer = new byte[Unsafe.SizeOf<T>()];
            stream.Read(buffer, 0, Unsafe.SizeOf<T>());
            value = Unsafe.ReadUnaligned<T>(ref buffer[0]);
            return true;
        }
        #endregion

        #region Byte
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteByte(byte value)
        {
            stream.WriteByte(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryReadByte(out byte value)
        {
            if (Remainder == 0)
            {
                value = default;
                return false;
            }
            value = (byte)stream.ReadByte();
            return true;
        }
        #endregion

        #region Bytes
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBytes(byte[] value)
        {
            WriteInt(value.Length);
            stream.Write(value, 0, value.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadBytes(out byte[] value)
        {
            if (TryReadInt(out int length) && (uint)length <= Remainder)
            {
                value = new byte[length];
                stream.Read(value, 0, length);
                return true;
            }
            else
            {
                value = null!;
                return false;
            }
        }
        #endregion

        #region String
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteString(string value)
        {
            WriteBytes(encoding.GetBytes(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadString(out string value)
        {
            if (TryReadBytes(out byte[] bytes))
            {
                value = encoding.GetString(bytes);
                return true;
            }
            value = null!;
            return false;
        }
        #endregion

        public void MarkHereAsEnd()
        {
            Length = Position;
        }
    }
}
