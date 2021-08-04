namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.Default, "Neon Striker", "Those bright things gonna shine through the world.")]
    [MetaInfo(Lang.schinese, "霓虹打击", "这些明亮的小东西将要照亮整个世界。")]
    [MetaType(MetaType.Gun)]
    public partial class NeonStriker : AMGun
    {
        public NeonStriker(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Gun_NeonStriker);
            ammo = 30;
            _fireWait = 1.1f;
            _fullAuto = true;
            _flare.color = Color.Blue;
            _barrelOffsetTL = new Vec2(22,5.5f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Fire()
        {
            if (_wait != 0 || !isServerForObject) return;
            _wait = _fireWait;
            if (ammo <= 0)
            {
                DoAmmoClick();
                return;
            }
            NeonStriker_ThingBullet_Blue b1 = new(GetBarrelPosition(new Vec2(22, 5.5f)), GetBulletVecDeg(Maths.PointDirection(Vec2.Zero,barrelVector),6,1,0.85f), duck, true);
            NeonStriker_ThingBullet_Purple b2 = new(GetBarrelPosition(new Vec2(22, 5.5f)), GetBulletVecDeg(Maths.PointDirection(Vec2.Zero, barrelVector), 6, 1, 0.85f), duck, true);
            Level.Add(b1);
            Level.Add(b2);
            SFX.PlaySynchronized("laserRifle", 1, Rando.Float(-0.9f,-0.4f));
            ApplyKick();
            ammo--;
            if (duck?.profile != null)
                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Kick, RumbleDuration.Pulse, RumbleFalloff.None));
        }
    }
}