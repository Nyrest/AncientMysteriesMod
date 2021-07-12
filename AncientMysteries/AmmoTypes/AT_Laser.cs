namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Laser : AMAmmoType
    {
        public AT_Laser()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 1f;
            rangeVariation = 20f;
            bulletThickness = 2f;
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
