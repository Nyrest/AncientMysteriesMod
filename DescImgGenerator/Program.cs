using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AncientMysteries;
using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using DuckGame;
using SkiaSharp;

namespace DescImgGenerator
{
    static class Program
    {
        public static Assembly modAssembly = typeof(AncientMysteriesMod).Assembly;
        public static List<Item> allTheFucks = new();

        public const int itemPadding = 4;
        public const int itemWidth = 314;
        public const int itemHeight = 200;

        public const int canvasMaxWidth = 628;
        public const int canvasMaxHeight = 10240;

        public static AMLang currentLang = AMLang.english;

        public record Item(Thing thing, IAMLocalizable localizable, SKBitmap bitmap);

        static void Main(string[] args)
        {
            foreach (var item in modAssembly.GetTypes())
            {
                if (item.IsAbstract || !typeof(IAMLocalizable).IsAssignableFrom(item)) continue;
                // var thing = (Thing)Activator.CreateInstance(item, new object[] { 0, 0 });
                Thing thing = null;
                allTheFucks.Add(new Item(thing, (IAMLocalizable)thing, GetItemBitmap(thing)));
            }
            var sur = BuildImage(out SKRectI rect);
            sur.Snapshot().Encode(SKEncodedImageFormat.Png, 100).SaveTo(File.OpenWrite("A:\\test.png"));
        }

        public static SKBitmap GetItemBitmap(Thing item)
        {
            var graphic = item.graphic;
            var tex = graphic.texture;
            var rawBitmap = new SKBitmap(tex.w, tex.h);
            rawBitmap.SetPixels(Unsafe.As<SKColor, IntPtr>(ref MemoryMarshal.GetReference(ToSKColors(tex))));
            return rawBitmap;
            static Span<SKColor> ToSKColors(Tex2D tex)
            {
                Color[] data = tex.GetData();
                var colors = new SKColor[tex.width * tex.height];
                int pos = 0;
                for (int y = 0; y < tex.height; y++)
                {
                    for (int x = 0; x < tex.width; x++)
                    {
                        var dgColor = data[x + y * tex.width];
                        byte b = dgColor.b;
                        byte g = dgColor.g;
                        byte r = dgColor.r;
                        byte a = dgColor.a;
                        colors[pos++] = new SKColor(r, g, b, a);
                    }
                }
                return colors.AsSpan();
            }
        }

        public static SKSurface BuildImage(out SKRectI rect)
        {
            int x = 0, y = 0;
            var surface = SKSurface.Create(new SKImageInfo(628, 10240));
            var canvas = surface.Canvas;
            foreach (var item in allTheFucks)
            {
                #region Move Y if needed
                if (x > canvasMaxWidth)
                {
                    x = 0;
                    y += itemHeight;
                }
                #endregion
                var itemRect = new SKRect(x + itemPadding, y + itemPadding, x + itemWidth - itemPadding, y + itemHeight - itemPadding);
                DrawItem(canvas, item.thing, item.localizable, itemRect);
                #region Move X
                x += itemWidth;
                #endregion
            }
            surface.Flush();
            rect = new SKRectI(0, 0, canvasMaxWidth, y + itemHeight);
            return surface;
        }

        public static void DrawItem(SKCanvas canvas, Thing thing, IAMLocalizable localizable, SKRect rect)
        {
            string name = localizable?.GetLocalizedName(currentLang);
            string description = localizable?.GetLocalizedDescription(currentLang);
            canvas.DrawRect(rect, new SKPaint()
            {
                Color = SKColors.Red,
            }
            );
        }
    }
}
