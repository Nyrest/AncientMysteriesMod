using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescImgGenerator
{
    public static class ImgStyles
    {
        public const int itemMargin = 8;
        public const int itemPadding = 4;
        public const int itemWidth = 314;
        public const int itemHeight = 140;

        public const int frameMargin = 4;

        public const int canvasMaxWidth = 628;
        public const int canvasMaxHeight = 10240;

        public static Style nameStyle = new Style()
        {
            TextColor = SKColors.White,
            FontSize = 16,
            LetterSpacing = 1,
        };

        public static Style descStyle = new()
        {
            TextColor = SKColors.LightGray,
            FontSize = 13.8f,
        };

        public static TextPaintOptions paintOptions = new()
        {
            IsAntialias = true,
            LcdRenderText = true,
        };
    }
}
