using System;

namespace DescImgGenerator
{
    public static class Helper
    {
        public static SKRect crect(float left, float top, float width, float height) => new(left, top, left + width, top + height);

        public static SKBitmap ScaleTexTo(SKBitmap bitmap, SKRect rect, float maxRatio = 3)
        {
            float ratio = rect.Height / bitmap.Height;
            if (ratio > maxRatio) ratio = maxRatio;
            SKBitmap result = new((int)MathF.Round(bitmap.Width * ratio), (int)MathF.Round(bitmap.Height * ratio));
            bitmap.ScalePixels(result, SKFilterQuality.None);
            return result;
        }

        public static SKRect CalculateDisplayRect(SKRect dest, SKBitmap bitmap, BitmapAlignment horizontal, BitmapAlignment vertical)
        {
            float bmpWidth = bitmap.Width, bmpHeight = bitmap.Height;
            float x = 0, y = 0;

            switch (horizontal)
            {
                case BitmapAlignment.Center:
                    x = (dest.Width - bmpWidth) / 2;
                    break;
                case BitmapAlignment.End:
                    x = dest.Width - bmpWidth;
                    break;
                default: break;
            }

            switch (vertical)
            {
                case BitmapAlignment.Center:
                    y = (dest.Height - bmpHeight) / 2;
                    break;
                case BitmapAlignment.End:
                    y = dest.Height - bmpHeight;
                    break;
                default: break;
            }

            x += dest.Left;
            y += dest.Top;

            return new SKRect(x, y, x + bmpWidth, y + bmpHeight);
        }
    }
}
