namespace AncientMysteries.AmmoTypes
{
    public class _ImplicitDefaultAmmoType : AMAmmoType
    {
        public static readonly _ImplicitDefaultAmmoType Instance = new();

#if DEBUG
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            // This shouldn't happen.
            // Set a AmmoType for the gun.
            Debugger.Break();
            return base.FireBullet(position, owner, angle, firedFrom);
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            // This shouldn't happen.
            // Set a AmmoType for the gun.
            Debugger.Break();
            base.OnHit(destroyed, b);
        }
#endif
    }
}
