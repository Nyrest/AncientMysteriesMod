namespace AncientMysteries.Items.Guns.MachineGuns
{
    [EditorGroup(g_rifles)]
    [MetaImage(t_)]
    [MetaInfo(Lang.english, "Iridescence", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    public sealed partial class Iridescence : AMGun
    {
        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "流光溢彩",
            _ => "Iridescence",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "彩虹，一条架向天堂的桥梁。",
            _ => "Rainbow. A bridge to the heaven.",
        };

        public Iridescence(float xval, float yval) : base(xval, yval)
        {
            ammo = 127;
            _ammoType = new Iridescence_AmmoType();
            _type = "gun";
            this.ReadyToRunWithFrames(t_Gun_RainbowGun);
            _barrelOffsetTL = new Vec2(33f, 6f);
            BarrelSmoke.color = Color.White;
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
