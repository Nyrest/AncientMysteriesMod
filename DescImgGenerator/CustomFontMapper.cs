namespace DescImgGenerator
{
    public class CustomFontMapper : FontMapper
    {
        public static readonly SKTypeface typeface;
        public static readonly SKTypeface typefaceLight;

        static CustomFontMapper()
        {
            typeface = SKTypeface.FromFile("NotoSansCJKsc-Regular.otf");
            typefaceLight = SKTypeface.FromFile("NotoSansCJKsc-Light.otf");
        }

        public override SKTypeface TypefaceFromStyle(IStyle style, bool ignoreFontVariants)
        {
            if (style.FontWeight < 400)
                return typefaceLight;
            return typeface;
        }
    }
}