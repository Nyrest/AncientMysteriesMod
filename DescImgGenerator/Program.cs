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
            int x = itemMargin, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(canvasMaxWidth, canvasMaxHeight));
            var canvas = surface.Canvas;
            foreach (var groupItems in ModItems.GroupBy(x => x.metaType))
            {
                // Draw Label

                #region Draw Label

                var metaType = groupItems.First().metaType;
                x = 0;
                if (y != 0)
                    y += itemHeight + itemMargin + 1;
                DrawLabel(canvas, metaType, lang, SKRect.Create(x, y, canvasMaxWidth, labelHeight));
                x = itemMargin;
                y += labelHeight + itemMargin * 2;

                #endregion Draw Label

                foreach (var item in groupItems.OrderBy(x => x.name.GetText(Lang.english)).OrderBy(x => x.order))
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
            }
            rect = new SKRectI(0, 0, canvasMaxWidth, y + itemHeight + itemMargin);
            return surface;
        }

        public static void DrawLabel(SKCanvas canvas, MetaType metaType, Lang lang, SKRect rect)
        {
            var lineR = rect;
            lineR.Inflate(-(canvasMaxWidth * 0.8f), -(itemMargin * 2.4f));
            //top
            //canvas.DrawLine(new SKPoint(lineR.Left, lineR.Top), new SKPoint(lineR.Right, lineR.Top), labelLine);
            //bottom
            canvas.DrawLine(new SKPoint(lineR.Left, lineR.Bottom), new SKPoint(lineR.Right, lineR.Bottom), labelLine);

            string labelName = GetMetaTypeLabel(lang, metaType);
            RichString desc = new RichString()
            {
                DefaultStyle = labelStyle,
                MaxLines = 1,
            }.Add(labelName);
            const int offsetY = 3;
            desc.Paint(canvas, new SKPoint(rect.MidX - desc.MeasuredWidth / 2, rect.MidY - desc.MeasuredHeight / 2 - offsetY), paintOptions);
        }

        public static void DrawItem(SKCanvas canvas, Item item, Lang lang, SKRect rect)
        {
            Console.Out.WriteLine($"[{lang}] {item.name.GetText(lang)}".AsSpan());
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
            name.Paint(canvas, new SKPoint(nameRect.Left, nameRect.Top + 4), paintOptions);

            #endregion Draw Name

            RichString desc = new RichString()
            {
                MaxWidth = nameRect.Width,
                MaxHeight = null,
                DefaultStyle = descStyle,
            }.Add(item.description.GetText(lang));
            desc.MaxLines = 20;
            desc.Paint(canvas, new SKPoint(descRect.Left, descRect.Top + 1), paintOptions);
        }
    }
}