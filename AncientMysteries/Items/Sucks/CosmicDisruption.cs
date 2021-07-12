namespace AncientMysteries.Items.Sucks
{
    [EditorGroup(g_wtf)]
    public sealed class CosmicDisruption : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Cosmic Disruption",
        };

        public CosmicDisruption(float xval, float yval) : base(xval, yval)
        {
            ammo = byte.MaxValue;
            _ammoType = new CosmicDisruption_AmmoType();
            _type = "gun";
            this.ReadyToRunMap(t_CosmicDisruption);
            _barrelOffsetTL = new Vec2(40f, 7f);
            BarrelSmoke.color = Color.White;
            _fireSound = "laserRifle";
            _fireWait = 0f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.2f;
            _fullAuto = true;
            loseAccuracy = 1f;
            maxAccuracyLost = 1f;
            _holdOffset = new Vec2(3f, 0.2f);
            _bulletColor = Color.Red;
        }

        public override void Update()
        {
            ammo = byte.MaxValue;
            ammoType.bulletColor = Color.Blue;
            base.Update();
            base.Update();
        }
    }
}
