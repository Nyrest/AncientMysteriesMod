namespace AncientMysteries.Items
{
    public abstract class AMNotGun : AMGun
    {
        protected AMNotGun(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
        }

        public override void ApplyKick() { }

        public override void Fire() { }
    }
}
