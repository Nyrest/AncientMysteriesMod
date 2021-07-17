using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace DescImgGenerator
{
    static class Program
    {
        public static Assembly modAssembly;

        public const int itemPadding = 4;
        public const int itemWidth = 314;
        public const int itemHeight = 200;

        public const int canvasMaxWidth = 628;
        public const int canvasMaxHeight = 10240;

        public static List<Item> Items;

        public record Item(string name, string description, SKBitmap bitmap);

        public static byte[] modRawAssembly;

        static void Main(string[] args)
        {
            var sur = BuildImage(out SKRectI rect);
            sur.Snapshot().Encode(SKEncodedImageFormat.Png, 100).SaveTo(File.OpenWrite("A:\\test.png"));
        }

        public static SKBitmap GetItemBitmap(string item, int frameWidth = -1, int frameHeight = -1, params int[] frames)
        {
            
            return null;
        }

        public static SKSurface BuildImage(out SKRectI rect)
        {
            int x = 0, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(628, 10240));
            var canvas = surface.Canvas;
            foreach (var item in Items)
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
                Color = SKColors.Red,
            }
            );
        }
    }
}
