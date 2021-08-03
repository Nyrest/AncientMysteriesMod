namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.Default, "The Spirit Of Darkness", "The twins accompanying. Not ever wither.")]
    [MetaInfo(Lang.schinese, "黑暗之灵", "双生随伴，永不凋零。")]
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
            if (_wait != 0 || !isServerForObject) return;
            _wait = _fireWait;
            var barrelAngleRad = this.barrelAngle;
            TheSpiritOfDarkness_ThingBullet b1 = new(GetBarrelPosition(new Vec2(27, 11)), duck, true, barrelAngleRad);
            TheSpiritOfDarkness_ThingBullet b2 = new(GetBarrelPosition(new Vec2(27, 11)), duck, false, barrelAngleRad);
            Level.Add(b1);
            Level.Add(b2);
            SFX.PlaySynchronized("laserRifle", 1, -0.5f);
            ApplyKick();
            if (duck?.profile != null)
                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Kick, RumbleDuration.Pulse, RumbleFalloff.None));
        }
    }
}