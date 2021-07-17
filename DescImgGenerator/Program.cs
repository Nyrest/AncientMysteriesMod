using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AncientMysteries;
using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using DuckGame;
using FastGenericNew;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using SkiaSharp;
using static HarmonyLib.AccessTools;

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

        static Program()
        {
            Hooks.InitHooks();
        }

        static void Main(string[] args)
        {
            foreach (var item in modAssembly.GetTypes())
            {
                if (item.IsAbstract || !typeof(IAMLocalizable).IsAssignableFrom(item)) continue;
                
                var thing = TypeNew.GetCreateInstance<Thing, float, float>(item, typeof(float), typeof(float))(0, 0);
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

    public static class Hooks
    {
        public static Harmony harmony = new Harmony("descImgGen");

        public static Tex2D blank;

        public static void InitHooks()
        {
            blank = FastNew<Tex2D, Texture2D, string, short>.CreateInstance(FastNew<Texture2D>.CreateInstance(), string.Empty, 0);
            harmony.Patch(Method(
                typeof(Content),
                "Load",
                new Type[] { typeof(string) }).MakeGenericMethod(typeof(Tex2D)), new HarmonyMethod(Method(typeof(Hooks), nameof(EmptyTex))));

            harmony.Patch(Method(
                typeof(TeamSelect2),
                "Enabled"), new HarmonyMethod(Method(typeof(Hooks), nameof(NotEnabled))));
            harmony.PatchAll();
        }

        public static bool EmptyTex(ref Tex2D __result, string name)
        {
            __result = blank;
            return false;
        }


        public static bool NotEnabled(ref bool __result, string id, bool ignoreTeamSelect = false)
        {
            __result = false;
            return false;
        }
    }
}
