namespace DescImgGenerator
{
    static class Program
    {
        public const int itemPadding = 4;
        public const int itemWidth = 209;
        public const int itemHeight = 200;

        public const int canvasMaxWidth = 628;
        public const int canvasMaxHeight = 10240;

        static void Main(string[] args)
        {
            LoadAssembly("AncientMysteries.dll");
            ScanModItems();
            var sur = BuildImage(out SKRectI rect);
            sur.Snapshot().Encode(SKEncodedImageFormat.Png, 100).SaveTo(File.OpenWrite("A:\\test.png"));
        }

        public static SKSurface BuildImage(out SKRectI rect)
        {
            int x = 0, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(628, 10240));
            var canvas = surface.Canvas;
            foreach (var item in ModItems)
            {
                #region Move Y if needed
                if (x > canvasMaxWidth)
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
            canvas.DrawRect(rect, new SKPaint()
            {
                Color = new SKColor(25, 102, 61),
            });
            canvas.DrawBitmap(item.bitmap, rect);
        }
    }
}
