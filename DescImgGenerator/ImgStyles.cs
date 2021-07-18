namespace DescImgGenerator
{
    public static class ImgStyles
    {
        public const int itemMargin = 3;
        public const int itemPadding = 6;
        public const int itemWidth = 314;
        public const int itemHeight = 140;

        public const int frameMargin = 4;

        public const int canvasMaxWidth = 638;
        public const int canvasMaxHeight = 10240;

        public static Style nameStyle = new()
        {
            TextColor = new SKColor(238, 66, 102),
            FontSize = 16,
            LetterSpacing = 1,
        };

        public static Style descStyle = new()
        {
            TextColor = new SKColor(240, 239, 235),
            FontWeight = 300,
            FontSize = 13.7f,
        };

        public static TextPaintOptions paintOptions = new()
        {
            IsAntialias = true,
            LcdRenderText = true,
        };

        public static SKPaint bgFill = new()
        {
            Color = new SKColor(24, 26, 27),
        };

        public static SKPaint bgBorder = new()
        {
            Color = new SKColor(93, 101, 178),
            StrokeWidth = 2,
            Style = SKPaintStyle.Stroke,
        };

        public static void DrawItemBackground(SKCanvas canvas, SKRect rect)
        {
            canvas.DrawRect(rect, bgFill);
            canvas.DrawRect(rect, bgBorder);
        }
    }
}