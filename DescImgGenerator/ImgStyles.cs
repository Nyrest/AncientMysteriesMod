namespace DescImgGenerator
{
    public static class ImgStyles
    {
        public const int itemMargin = 3;
        public const int itemPadding = 6;
        public const int itemWidth = 314;
        public const int itemHeight = 140;

        public const int labelHeight = 75;

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

        public static Style labelStyle = new()
        {
            TextColor = new SKColor(240, 239, 235),
            FontSize = 33f,
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

        public static SKPaint labelLine = new()
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

        public static string GetMetaTypeLabel(Lang lang, MetaType metaType) => lang switch
        {
            Lang.schinese => metaType switch
            {
                MetaType.Error => "Error (WTF?)",
                MetaType.Gun => "武器",
                MetaType.Magic => "魔法",
                MetaType.Melee => "近战武器",
                MetaType.Equipment => "装备",
                MetaType.Throwable => "投掷物",
                MetaType.Props => "小道具",
                MetaType.Decoration => "装饰品",
                MetaType.Developer => "开发者",
                _ => throw new NotImplementedException(),
            },
            _ => metaType switch
            {
                MetaType.Error => "Error (WTF?)",
                MetaType.Gun => "Weapons",
                MetaType.Magic => "Magics",
                MetaType.Melee => "Melees",
                MetaType.Equipment => "Equipments",
                MetaType.Throwable => "Throwables",
                MetaType.Props => "Props",
                MetaType.Decoration => "Decorations",
                MetaType.Developer => "Developers",
                _ => throw new NotImplementedException(),
            },
        };
    }
}