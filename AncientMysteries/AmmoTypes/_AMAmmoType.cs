namespace AncientMysteries.AmmoTypes
{
    public abstract class AMAmmoType : AmmoType
    {
        
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            var bullet = base.FireBullet(position, owner, angle, firedFrom);
            bullet.color = bulletColor;
            return bullet;
        }
    }
}
