namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Rifles)]
    [MetaImage(tex_Gun_RainbowGun)]
    [MetaInfo(Lang.Default, "Iridescence", "Rainbow. A bridge to the heaven.")]
    [MetaInfo(Lang.schinese, "流光溢彩", "彩虹，一条架向天堂的桥梁。")]
    [MetaType(MetaType.Gun)]
    public sealed partial class Iridescence : AMGun
    {
        public Iridescence(float xval, float yval) : base(xval, yval)
        {
            ammo = 127;
            _ammoType = new Iridescence_AmmoType();
            _type = "gun";
            this.ReadyToRunWithFrames(tex_Gun_RainbowGun);
            _barrelOffsetTL = new Vec2(33f, 6f);
            BarrelSmoke.color = Color.White;
            _fireRumble = RumbleIntensity.Kick;
            _fireSound = "laserRifle";
            _fireWait = 0.6f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.02f;
            _holdOffset = new Vec2(-2.5f, 0.2f);
        }

        public override void Update()
        {
            var color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            ammoType.bulletColor = color;
            _flare.color = color;
            base.Update();
        }
    }
}