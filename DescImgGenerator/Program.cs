using System.Threading.Tasks;

namespace DescImgGenerator
{
    static class Program
    {
        public static Lang[] languages = new[]
        {
            Lang.english,
            Lang.schinese
        };

        static void Main(string[] args)
        {
            string saveTo = args.Length == 0 ? ".\\" : args[0] + Path.DirectorySeparatorChar;
            FontMapper.Default = new CustomFontMapper();
            LoadAssembly("AncientMysteries.dll");
            ScanModItems();
            Parallel.ForEach(languages, lang =>
            {
                var sur = BuildImage(lang, out SKRectI rect);
                sur.Snapshot(rect).Encode(SKEncodedImageFormat.Png, 100).SaveTo(File.OpenWrite($"{saveTo}desc_{lang}.png"));
            });
        }

        public static SKSurface BuildImage(Lang lang, out SKRectI rect)
        {
            object _lock = new();
            int x = 0, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(canvasMaxWidth, canvasMaxHeight));
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
                DrawItem(canvas, item, lang, itemRect);
                #region Move X
                x += itemWidth;
                #endregion
            }
            surface.Flush();
            rect = new SKRectI(0, 0, canvasMaxWidth, y + itemHeight);
            return surface;
        }

        public static void DrawItem(SKCanvas canvas, Item item, Lang lang, SKRect rect)
        {
            DrawItemBackground(canvas, rect);
            SKRect padded = new(rect.Left + itemPadding, rect.Top + itemPadding, rect.Right - itemPadding, rect.Bottom - itemPadding);
            float imageHeight = padded.Height * 0.4f;
            float nameHeight = padded.Height * 0.2f;
            float descHeight = padded.Height * 0.4f;
            SKRect imageRect = crect(padded.Left, padded.Top, padded.Width, imageHeight);
            SKRect nameRect = crect(padded.Left, padded.Top + imageHeight, padded.Width, nameHeight);
            SKRect descRect = crect(padded.Left, padded.Top + imageHeight + nameHeight, padded.Width, descHeight);
            using var scaled = ScaleTexTo(item.bitmap, imageRect);
            var imageDestRect = CalculateDisplayRect(imageRect, scaled, BitmapAlignment.Start, BitmapAlignment.Center);
            canvas.DrawBitmap(scaled, imageDestRect.Left + 5, imageDestRect.Top);
            #region Draw Name
            RichString name = new RichString()
            {
                MaxWidth = nameRect.Width,
                DefaultStyle = nameStyle,
            }.Add(item.name.GetText(lang));
            name.MaxLines = 1;
            name.Paint(canvas, new SKPoint(nameRect.Left + 2, nameRect.Top + 5), paintOptions);
            #endregion
            RichString desc = new RichString()
            {
                MaxWidth = nameRect.Width,
                MaxHeight = null,
                DefaultStyle = descStyle,
            }.Add(item.description.GetText(lang));
            desc.MaxLines = 20;
            desc.Paint(canvas, new SKPoint(descRect.Left + 3, descRect.Top + 5), paintOptions);

        }
    }
}
