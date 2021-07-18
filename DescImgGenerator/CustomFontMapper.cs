using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescImgGenerator
{
    public class CustomFontMapper : FontMapper
    {
        public static readonly SKTypeface typeface;

        static CustomFontMapper()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DescImgGenerator.NotoSansCJKsc-Regular.otf")!)
            {
                typeface = SKTypeface.FromStream(stream);
            }
        }

        public override SKTypeface TypefaceFromStyle(IStyle style, bool ignoreFontVariants)
        {
            return typeface;
        }
    }
}
