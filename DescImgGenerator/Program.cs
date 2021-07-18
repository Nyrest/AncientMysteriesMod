using System;
using System.Text;
using System.Threading.Tasks;

namespace DescImgGenerator
{
    internal static class Program
    {
        public static Lang[] languages = new[]
        {
            Lang.english,
            Lang.schinese
        };

        private static void Main(string[] args)
        {
            string saveTo = args.Length == 0 ? ".\\" : args[0] + Path.DirectorySeparatorChar;
            FontMapper.Default = new CustomFontMapper();
            LoadAssembly("AncientMysteries.dll");
            ScanModItems();
            Parallel.ForEach(languages, lang =>
            {
                var sur = BuildImage(lang, out SKRectI rect);
                sur.Canvas.Flush();
                sur.Flush();
                using var snapshot = sur.Snapshot(rect);
                using var encodedData = snapshot.Encode(SKEncodedImageFormat.Png, 100);
                using var fileStream = File.OpenWrite($"{saveTo}desc_{lang}.png");
                encodedData.SaveTo(fileStream);
            });
        }

        public static SKSurface BuildImage(Lang lang, out SKRectI rect)
        {
            object _lock = new();
            int x = itemMargin, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(canvasMaxWidth, canvasMaxHeight));
            var canvas = surface.Canvas;
            foreach (var item in ModItems)
            {
                #region Move Y if needed

                if ((x + itemWidth + itemMargin) > canvasMaxWidth)
                {
                    x = itemMargin;
                    y += itemHeight + itemMargin + 1;
                }

                #endregion Move Y if needed

                var itemRect = new SKRect(x, y, x + itemWidth, y + itemHeight);
                DrawItem(canvas, item, lang, itemRect);

                #region Move X

                x += itemWidth + itemMargin + 1;

                #endregion Move X
            }
            rect = new SKRectI(0, 0, canvasMaxWidth, y + itemHeight + itemMargin);
            return surface;
        }

        public static void DrawItem(SKCanvas canvas, Item item, Lang lang, SKRect rect)
        {
            Console.WriteLine($"[{lang}] {item.name.GetText(lang)}");
            DrawItemBackground(canvas, rect);
            SKRect padded = new(rect.Left + itemPadding, rect.Top + itemPadding, rect.Right - itemPadding, rect.Bottom);
            float imageHeight = padded.Height * 0.4f;
            float nameHeight = padded.Height * 0.2f;
            float descHeight = padded.Height * 0.4f;
            SKRect imageRect = crect(padded.Left, padded.Top, padded.Width, imageHeight);
            SKRect nameRect = crect(padded.Left + 3, padded.Top + imageHeight, padded.Width - 5, nameHeight);
            SKRect descRect = crect(padded.Left + 3, padded.Top + imageHeight + nameHeight, padded.Width - 5, descHeight);
            var scaled = item.GetScaledBitmap(imageRect);
            var imageDestRect = CalculateDisplayRect(imageRect, scaled, BitmapAlignment.Start, BitmapAlignment.Center);
            canvas.DrawBitmap(scaled, imageDestRect.Left + 5, imageDestRect.Top);

            #region Draw Name

            RichString name = new RichString()
            {
                MaxWidth = nameRect.Width,
                DefaultStyle = nameStyle,
            }.Add(item.name.GetText(lang));
            name.MaxLines = 1;
            name.Paint(canvas, new SKPoint(nameRect.Left , nameRect.Top + 4), paintOptions);

            #endregion Draw Name

            RichString desc = new RichString()
            {
                MaxWidth = nameRect.Width,
                MaxHeight = null,
                DefaultStyle = descStyle,
            }.Add(item.description.GetText(lang));
            desc.MaxLines = 20;
            desc.Paint(canvas, new SKPoint(descRect.Left, descRect.Top + 3), paintOptions);
        }
    }
}