namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_CubicBlast : AMAmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap(t_CubicBlast, 8, 8, true);

        public AT_CubicBlast()
        {
            accuracy = 0.6f;
            range = 600f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 2;
            speedVariation = 0.5f;
            bulletLength = 50;
            bulletColor = Color.LightYellow;
            bulletThickness = 2;
            sprite = _spriteMap;
            _spriteMap.AddAnimation("loop", 0.3f, true, 0, 1, 2, 3, 4);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
        }

    }
}
