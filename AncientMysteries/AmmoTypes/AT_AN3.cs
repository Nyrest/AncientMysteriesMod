namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_AN3 : AMAmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteWithFrames(tex_Bullet_NovaFrame3, 18, 18, true);

        public AT_AN3()
        {
            accuracy = 1f;
            range = 80f;
            penetration = int.MaxValue;
            rangeVariation = 10f;
            bulletSpeed = 6;
            speedVariation = 0.5f;
            bulletLength = 0;
            bulletColor = Color.LightYellow;
            bulletThickness = 2;
            sprite = _spriteMap;
            _spriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
            bulletType = typeof(Bullet_AN3);
        }

    }
}
