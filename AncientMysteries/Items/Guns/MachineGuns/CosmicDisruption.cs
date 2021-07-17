namespace AncientMysteries.Items.Guns.MachineGuns
{
    [EditorGroup(g_wtf)]
    public sealed partial class CosmicDisruption : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "寰宇星怒",
            _ => "Cosmic Disruption",
        };

        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "外挂和这把枪你只需要一个。",
            _ => "Cheating is no longer needed with this.",
        };

        public CosmicDisruption(float xval, float yval) : base(xval, yval)
        {
            ammo = byte.MaxValue;
            _ammoType = new CosmicDisruption_AmmoType();
            _type = "gun";
            this.ReadyToRunWithFrames(t_Gun_CosmicDisruption);
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
            base.Update();
        }
    }
}
