namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Snipers)]
    [MetaImage(tex_Gun_ShadowDance)]
    [MetaInfo(Lang.Default, "Permafrost Lance", "Cold-hearted.")]
    [MetaInfo(Lang.schinese, "极冻长枪", "冰冷无情。")]
    [MetaType(MetaType.Gun)]
    public sealed partial class PermafrostLance : AMGun
    {
        public int n = 0;
        public PermafrostLance(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 1600,
                penetration = 3.5f,
            };
            this.ReadyToRunWithFrames(tex_Gun_PermafrostLance);
            _flare.color = Color.AliceBlue;
            BarrelSmoke.color = Color.Blue;
            _fireRumble = RumbleIntensity.Medium;
            _barrelOffsetTL = new Vec2(34f, 6f);
            _fireSound = "laserBlast";
            _fireSoundPitch = -0.8f;
            _kickForce = 0f;
            _holdOffset = new Vec2(0f, 0f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }

        public override void Fire()
        {
            //base.Fire();
        }

        public override void OnPressAction()
        {
            n = 0;
            base.OnPressAction();
        }

        public override void OnReleaseAction()
        {
            if (n <= 60)
            {
                PermafrostLance_ThingBullet b = new(barrelPosition, barrelVector * 20, duck);
                Level.Add(b);
                ApplyKick();
                SFX.PlaySynchronized("sniper", 1, 0.3f);
            }
            else
            {
                PermafrostLance_ThingBulletCharged b = new(barrelPosition, barrelVector * 25, duck);
                Level.Add(b);
                ApplyKick();
                SFX.PlaySynchronized("laserRifle");
            }
            n = 0;
            base.OnReleaseAction();
        }

        public override void OnHoldAction()
        {
            n++;;
            base.OnHoldAction();
        }
    }
}