namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_LaserG2 : AMAmmoType
    {
        public AT_LaserG2()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 100000f;
            rangeVariation = 20f;
            bulletThickness = 2f;
            bulletSpeed = 0.5f;
            //this.bulletType = typeof(Bullet_Electronic);
            //this.sprite = TexHelper.ModSprite("ElectronicStar.png");
            //this.sprite.CenterOrigin();
            //bulletLength = 10;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            bulletColor = Color.Lime;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
