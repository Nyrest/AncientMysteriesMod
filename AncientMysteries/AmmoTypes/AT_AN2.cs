namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_AN2 : AMAmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap(t_NovaFrm2, 25, 25, true);

        public AT_AN2()
        {
            accuracy = 1f;
            range = 275f;
            penetration = 200000f;
            bulletSpeed = 6;
            speedVariation = 0.5f;
            bulletLength = 0;
            bulletColor = Color.LightYellow;
            bulletThickness = 2;
            sprite = _spriteMap;
            _spriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
            bulletType = typeof(Bullet_AN2);
        }

    }
}
