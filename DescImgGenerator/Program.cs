using System.Xml;
using Topten.RichTextKit.Editor;

namespace DescImgGenerator
{
    static class Program
    {
        public const int itemMargin = 8;
        public const int itemPadding = 4;
        public const int itemWidth = 314;
        public const int itemHeight = 140;

        public const int itemFrameMargin = 8;

        public const int canvasMaxWidth = 628;
        public const int canvasMaxHeight = 10240;

        public static Lang currentLang = Lang.english;

        public static Style nameStyle = new()
        {
            TextColor = SKColors.White,
            FontSize = 16,
            FontWeight = 500,
            LetterSpacing = 1,
        };

        public static Style descStyle = new()
        {
            TextColor = SKColors.LightSeaGreen,
            FontSize = 13,
        };

        static void Main(string[] args)
        {
            LoadAssembly("AncientMysteries.dll");
            ScanModItems();
            var sur = BuildImage(out SKRectI rect);
            sur.Snapshot(rect).Encode(SKEncodedImageFormat.Png, 100).SaveTo(File.OpenWrite("A:\\test.png"));
        }

        public static SKSurface BuildImage(out SKRectI rect)
        {
            int x = 0, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(628, 10240));
            var canvas = surface.Canvas;
            foreach (var item in ModItems)
            {
                #region Move Y if needed
                if ((x + itemWidth) > canvasMaxWidth)
                {
                    x = 0;
                    y += itemHeight;
                }
                #endregion
                var itemRect = new SKRect(x + itemPadding, y + itemPadding, x + itemWidth - itemPadding, y + itemHeight - itemPadding);
                DrawItem(canvas, item, itemRect);
                #region Move X
                x += itemWidth;
                #endregion
            }
            surface.Flush();
            rect = new SKRectI(0, 0, canvasMaxWidth, y + itemHeight);
            return surface;
        }

        public static void DrawItem(SKCanvas canvas, Item item, SKRect rect)
        {
            DrawItemBackground(canvas, rect);
            SKRect padded = new(rect.Left + itemPadding, rect.Top + itemPadding, rect.Right - itemPadding, rect.Bottom - itemPadding);
            float imageHeight = padded.Height * 0.4f;
            float nameHeight = padded.Height * 0.2f;
            float descHeight = padded.Height * 0.4f;
            SKRect imageRect = crect(padded.Left, padded.Top, padded.Width, imageHeight);
            SKRect nameRect = crect(padded.Left, padded.Top + imageHeight, padded.Width, nameHeight);
            SKRect descRect = crect(padded.Left, padded.Top + imageHeight + nameHeight, padded.Width, descHeight);
            canvas.DrawBitmap(ScaleTexTo(item.bitmap, imageRect), imageRect.Left, imageRect.Top);
            #region Draw Name
            RichString name = new RichString()
            {
                MaxWidth = nameRect.Width,
            }.FontFamily("Microsoft YaHei").FontSize(18).TextColor(SKColors.White)
            .Add(item.name.GetText(currentLang));
            name.MaxLines = 1;
            name.Paint(canvas, new SKPoint(nameRect.Left + 2, nameRect.Top + 5));
            #endregion
            RichString desc = new RichString()
            {
                MaxWidth = nameRect.Width,
                MaxHeight = null,
            }.FontFamily("Microsoft YaHei").FontSize(13).TextColor(SKColors.LightGreen)
            .Add(item.description.GetText(currentLang));
            desc.MaxLines = 20;
            desc.Paint(canvas, new SKPoint(descRect.Left + 3, descRect.Top + 5));
        }

        public static void DrawItemBackground(SKCanvas canvas, SKRect rect)
        {
            canvas.DrawRect(rect, new SKPaint()
            {
                Color = new SKColor(24, 26, 27),
            });
            canvas.DrawRect(rect, new SKPaint()
            {
                Color = new SKColor(45, 100, 97),
                StrokeWidth = 2,
                Style = SKPaintStyle.Stroke,
            });
        }
    }
}
