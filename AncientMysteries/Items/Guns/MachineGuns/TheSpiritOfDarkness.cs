namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.english, "The Spirit Of Darkness", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class TheSpiritOfDarkness : AMGun
    {
        public TheSpiritOfDarkness(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(tex_Gun_TheSpiritOfDarkness);
            ammo = 1;
            _fireWait = 5;
            _fullAuto = true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Fire()
        {
            if (_wait != 0) return;
            _wait = _fireWait;
            TheSpiritOfDarkness_ThingBullet b1 = new(GetBarrelPosition(new Vec2(27, 11)), GetBulletVecDeg(owner.FaceAngleDegressLeftOrRight(), 3), duck, true);
            TheSpiritOfDarkness_ThingBullet b2 = new(GetBarrelPosition(new Vec2(27, 11)), GetBulletVecDeg(owner.FaceAngleDegressLeftOrRight(), 3), duck, false);
            Level.Add(b1);
            Level.Add(b2);
            SFX.PlaySynchronized("laserRifle", 1, -0.5f);
            if (duck?.profile != null)
                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Kick, RumbleDuration.Pulse, RumbleFalloff.None));
        }
    }
}