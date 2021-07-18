using System;

namespace DescImgGenerator
{
    public record Item(LocalizedText name, LocalizedText description, SKBitmap bitmap, int order) : IComparable<Item>
    {
        private SKBitmap? _scaled;
        private SKRect _scaleRect;
        private readonly object _scaleLock = new();
        public SKBitmap GetScaledBitmap(SKRect rect)
        {
            lock (_scaleLock)
            {
                if (_scaleRect != rect) _scaled = null;
                _scaleRect = rect;
                return _scaled is not null ? _scaled : (_scaled = ScaleTexTo(bitmap, rect));
            }
        }

        public int CompareTo(Item? other) => other is not null ? order.CompareTo(other.order) : 1;
    }
}
