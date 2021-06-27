namespace AncientMysteries.Items.FutureTech
{
    [EditorGroup(guns)]
    public class ReboundShield : AMGun, IPlatform
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Rebound Shield",
        };

        public ReboundShield(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            this.ReadyToRunMap(Texs.ReboundShield);
            thickness = 100f;
            weight = 10f;
            flammable = 0f;
            physicsMaterial = PhysicsMaterial.Metal;
            _holdOffset = new Vec2(-5, 0);
        }

        public override void Update()
        {
            solid = true;
            base.Update();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.ammo is null) return base.Hit(bullet, hitPos);
            if (bullet.ammo.penetration < this.thickness)
            {
                SFX.Play("ting", 0.8f, Rando.Float(-0.4f, 0.4f));
                bullet.ReverseTravel();
            }
            return base.Hit(bullet, hitPos);
        }

        public override void ApplyKick() { }

        public override void PressAction() { }

        public override void Fire() { }
    }
}
