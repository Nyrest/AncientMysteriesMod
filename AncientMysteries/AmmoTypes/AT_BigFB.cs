namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_BigFB : AmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap("firebally.png", 25, 12, true);
        public AT_BigFB()
        {
            accuracy = 0.8f;
            range = 300f;
            penetration = 1f;
            rangeVariation = 10f;
            combustable = true;
            bulletSpeed = 0.5f;
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_BigFB);
            this.sprite = _spriteMap;
            bulletThickness = 4f;
            bulletLength = 5f;
            bulletColor = Color.Orange;
            _spriteMap.AddAnimation("loop", 0.4f, true, 0, 1, 2);
            _spriteMap.SetAnimation("loop");
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
