using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AncientMysteries
{
    public static class TexHelper
    {
        public static Dictionary<string, CachedTextureInfo> cache = new Dictionary<string, CachedTextureInfo>(10);

        public static CachedTextureInfo GetInfo(string name)
        {
            if (cache.TryGetValue(name, out var result))
                return result;
            else
            {
                string fullName = Mod.GetPath<AncientMysteriesMod>(name);
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(fullName))
                {
                    cache.Add(name, result = new CachedTextureInfo(fullName, image.Width, image.Height));
                }
                return result;
            }
        }

        #region
        public static SpriteMap ModSpriteMap(string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        public static Sprite ModSprite(string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new Sprite(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        public static SpriteMap ModSpriteMap(string spriteMapName, int frameWidth = -1, int frameHeight = -1, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, frameWidth == -1 ? info.frameWidth : frameWidth, frameHeight == -1 ? info.frameHeight : frameHeight);

            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }
        #endregion

        public static SpriteMap ModSpriteMap(this Thing thing, string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        public static Sprite ModSprite(this Thing thing, string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new Sprite(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        public static SpriteMap ModSpriteMap(this Thing thing, string spriteMapName, int frameWidth = -1, int frameHeight = -1, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, frameWidth == -1 ? info.frameWidth : frameWidth, frameHeight == -1 ? info.frameHeight : frameHeight);

            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        public static Sprite ReadyToRun(this Thing thing, string spriteMapName)
        {
            var info = GetInfo(spriteMapName);
            int w = info.frameWidth, h = info.frameHeight;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
            return thing.graphic = new Sprite(info.texture, w, h);
        }

        public static SpriteMap ReadyToRunMap(this Thing thing, string spriteMapName, int frameWidth = -1, int frameHeight = -1)
        {
            var info = GetInfo(spriteMapName);
            int w = frameWidth == -1 ? info.frameWidth : frameWidth, h = frameHeight == -1 ? info.frameHeight : frameHeight;
            SpriteMap result =  new SpriteMap(info.texture, w, h);
            thing.graphic = result;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
            return result;
        }

        public static Tex2D ModTex2D(this Thing thing, string textureName)
        {
            return GetInfo(textureName).texture;
        }

        public static Tex2D ModTex2D(string textureName)
        {
            return GetInfo(textureName).texture;
        }

        public sealed class CachedTextureInfo
        {
            public string fullName;

            public int frameWidth, frameHeight;

            public Tex2D texture;

            public CachedTextureInfo(string fullName, int frameWidth, int frameHeight)
            {
                this.fullName = fullName;
                this.frameWidth = frameWidth;
                this.frameHeight = frameHeight;
                texture = Content.Load<Tex2D>(fullName);
            }
        }
    }
}
