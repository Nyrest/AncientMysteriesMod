namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_VoidPiercer)]
    [MetaInfo(Lang.Default, "Void Piercer", "Traveling through the void, nothing can escape from you.")]
    [MetaInfo(Lang.schinese, "虚空穿刺者", "穿行于虚无之中，无人能够逃脱你所带来的死亡。")]
    [MetaType(MetaType.Gun)]
    public sealed partial class VoidPiercer : AMGun
    {
        public VoidPiercer(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 360f,
                accuracy = 0.7f,
                penetration = 1f
            };
            this.ReadyToRun(tex_Gun_VoidPiercer);
            _barrelOffsetTL = new Vec2(20f, 4f);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _fireRumble = RumbleIntensity.None;
            _fireSound = "laserRifle";
            _fireWait = 0.6f;
            _fireSoundPitch = -0.5f;
            _kickForce = 0.25f;
            _fullAuto = true;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.02f;
            _holdOffset = new Vec2(-2.5f, 0.2f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}