namespace AncientMysteries.Utilities
{
    public static class HSL
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color RandomRGB()
        {
            return FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
        }

        public static Color Hue(float hue) => FromHslFloat(hue, 1, 0.5f);

        public static Color Hue(float hue, float lightness) => FromHslFloat(hue, 1, lightness);

        public static Color FromHslFloat(float h, float s, float l, float alpha = 1f)
        {
            if (l == 0)
                return Color.Black;
            else if (s <= 0.001f)
            {
                int grayScale = (byte)(255 * l);
                return new Color(grayScale, grayScale, grayScale, alpha);
            }
            float num = l < 0.5f
                ? (l * (1f + s))
                : (l + s - (s * l));
            float v = (2f * l) - num;
            float red = HslToRgb(v, num, h + 0.333333334f);
            float green = HslToRgb(v, num, h);
            float blue = HslToRgb(v, num, h - 0.333333334f);
            return new Color((byte)(255 * red), (byte)(255 * green), (byte)(255 * blue), (byte)(255 * alpha));
            static float HslToRgb(float v1, float v2, float vH)
            {
                // Division is slower than multiplication in .NET CLR
                if (vH < 0f)
                    vH += 1f;
                else if (vH > 1f)
                    vH -= 1f;
                return 6f * vH < 1f
                    ? v1 + ((v2 - v1) * 6f * vH)
                    : 2f * vH < 1f
                    ? v2
                    : 3f * vH < 2f
                    ? v1 + ((v2 - v1) * ((2 / 3f) - vH) * 6f)
                    : v1;
            }
        }
    }
}