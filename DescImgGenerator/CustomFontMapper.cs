namespace DescImgGenerator
{
    public class CustomFontMapper : FontMapper
    {
        public static readonly SKTypeface typeface;
        public static readonly SKTypeface typefaceDemiLight;


        static CustomFontMapper()
        {
            typeface = SKTypeface.FromFile("NotoSansCJKsc-Regular.otf");
            typefaceDemiLight = SKTypeface.FromFile("NotoSansCJKsc-Light.otf");
        }

        public override SKTypeface TypefaceFromStyle(IStyle style, bool ignoreFontVariants)
        {
            if (style.FontWeight < 400)
                return typefaceDemiLight;
            return typeface;
        }
    }
}