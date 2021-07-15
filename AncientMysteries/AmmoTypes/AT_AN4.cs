namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_AN4 : AMAmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteWithFrames(t_Bullet_Nova4, 14, 6, true);

        public AT_AN4()
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
            _spriteMap.CenterOrigin();
            bulletType = typeof(Bullet_AN4);
        }
    }
}
