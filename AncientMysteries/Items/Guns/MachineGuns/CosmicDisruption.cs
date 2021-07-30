namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_WTF)]
    [MetaImage(tex_Gun_CosmicDisruption)]
    [MetaInfo(Lang.Default, "Cosmic Disruption", "Cheating is no longer needed with this.")]
    [MetaInfo(Lang.schinese, "寰宇星怒", "外挂和这把枪你只需要一个。")]
    [MetaType(MetaType.Gun)]
    public sealed partial class CosmicDisruption : AMGun
    {
        public CosmicDisruption(float xval, float yval) : base(xval, yval)
        {
            ammo = byte.MaxValue;
            _ammoType = new CosmicDisruption_AmmoType();
            _type = "gun";
            this.ReadyToRunWithFrames(tex_Gun_CosmicDisruption);
            _barrelOffsetTL = new Vec2(40f, 7f);
            BarrelSmoke.color = Color.White;
            _fireRumble = RumbleIntensity.Kick;
            SetBarrelFlare(tex_Effect_DarkFlare);
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