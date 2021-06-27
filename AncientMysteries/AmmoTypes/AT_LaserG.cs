namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_LaserG : AmmoType
    {
        public AT_LaserG()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 1f;
            rangeVariation = 20f;
            this.bulletThickness = 2f;
            affectedByGravity = true;
            this.bulletSpeed = 4f;
            //this.bulletType = typeof(Bullet_Electronic);
            //this.sprite = TexHelper.ModSprite("ElectronicStar.png");
            //this.sprite.CenterOrigin();
            //bulletLength = 10;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.Lime;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
