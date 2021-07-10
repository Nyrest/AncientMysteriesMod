namespace AncientMysteries.Items.Snipers
{
    [EditorGroup(g_snipers)]
    public sealed class ShadowDance : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Shadow Dance",
        };

        public ShadowDance(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 1600,
                penetration = 3.5f,
            };
            this.ReadyToRunMap("shadowDance.png");
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _barrelOffsetTL = new Vec2(34f, 6f);
            _fireSound = "laserBlast";
            _fireSoundPitch = -0.8f;
            _kickForce = 0f;
            _holdOffset = new Vec2(3f, 0f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}
