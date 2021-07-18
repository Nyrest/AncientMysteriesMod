namespace AncientMysteries
{
    public static class TexHelper
    {
        public static Dictionary<string, CachedTextureInfo> cache = new(ModTextureCount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CachedTextureInfo GetInfo(string name)
        {
            return cache.TryGetValue(name, out var result) ? result : LoadInfo();
            [MethodImpl(MethodImplOptions.NoInlining)]
            CachedTextureInfo LoadInfo()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ModSpriteWithFrames(this string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite ModSprite(this string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new Sprite(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ModSpriteWithFrames(this string spriteMapName, int frameWidth = -1, int frameHeight = -1, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, frameWidth == -1 ? info.frameWidth : frameWidth, frameHeight == -1 ? info.frameHeight : frameHeight);

            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ModSpriteWithFrames(this Thing thing, string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite ModSprite(this Thing thing, string spriteMapName, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new Sprite(info.texture, info.frameWidth, info.frameHeight);
            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ModSpriteWithFrames(this Thing thing, string spriteMapName, int frameWidth = -1, int frameHeight = -1, bool centerOrigin = false)
        {
            var info = GetInfo(spriteMapName);
            var result = new SpriteMap(info.texture, frameWidth == -1 ? info.frameWidth : frameWidth, frameHeight == -1 ? info.frameHeight : frameHeight);

            if (centerOrigin)
                result.CenterOrigin();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadyToRunSizeOnly(this Thing thing, string spriteMapName)
        {
            var info = GetInfo(spriteMapName);
            int w = info.frameWidth, h = info.frameHeight;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite ReadyToRun(this Thing thing, string spriteMapName)
        {
            var info = GetInfo(spriteMapName);
            int w = info.frameWidth, h = info.frameHeight;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
            return thing.graphic = new Sprite(info.texture, w, h);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ReadyToRunWithFrames(this Thing thing, string spriteMapName, int frameWidth = -1, int frameHeight = -1)
        {
            var info = GetInfo(spriteMapName);
            if (thing.graphic is SpriteMap spriteMap && spriteMap.texture == info.texture)
            {
                return spriteMap;
            }
            int w = frameWidth == -1 ? info.frameWidth : frameWidth, h = frameHeight == -1 ? info.frameHeight : frameHeight;
            SpriteMap result = new(info.texture, w, h);
            thing.graphic = result;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteMap ReadyToRunWithFrames(this Thing thing, string spriteMapName, Vec2 scale, int frameWidth = -1, int frameHeight = -1)
        {
            var info = GetInfo(spriteMapName);
            int w = (int)Math.Ceiling((frameWidth == -1 ? info.frameWidth : frameWidth) * scale.x);
            int h = (int)Math.Ceiling((frameHeight == -1 ? info.frameHeight : frameHeight) * scale.y);
            SpriteMap result = new(info.texture, w, h);
            thing.graphic = result;
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tex2D ModTex2D(this Thing thing, string textureName)
        {
            return GetInfo(textureName).texture;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tex2D ModTex2D(string textureName)
        {
            return GetInfo(textureName).texture;
        }

        public sealed class CachedTextureInfo
        {
            public readonly string fullName;

            public readonly int frameWidth, frameHeight;

            public readonly Tex2D texture;

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