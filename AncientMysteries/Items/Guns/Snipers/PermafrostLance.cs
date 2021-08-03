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

        public Waiter fireWaiter = new(30);

        public bool canFire = false;

        public int ammoCount = 8;
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
            if (fireWaiter.Tick())
            {
                canFire = true;
                fireWaiter.Reset();
                fireWaiter.Pause();
            }
            if (owner == null && ammoCount <= 0 && grounded)
            {
                canPickUp = false;
                weight = 0.01f;
                alpha -= 0.2f;
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
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
            if (n <= 60 && canFire && ammoCount > 0)
            {
                PermafrostLance_ThingBullet b = new(barrelPosition, barrelVector * 20, duck);
                Level.Add(b);
                ApplyKick();
                SFX.PlaySynchronized("sniper", 1, 0.3f);
                canFire = false;
                fireWaiter.Resume();
                ammoCount--;
            }
            else if (canFire && ammoCount > 0)
            {
                PermafrostLance_ThingBulletCharged b = new(barrelPosition, barrelVector * 20, duck);
                Level.Add(b);
                ApplyKick();
                SFX.PlaySynchronized("laserRifle");
                canFire = false;
                fireWaiter.Resume();
                ammoCount--;
            }
            else if (ammoCount <= 0)
            {
                DoAmmoClick();
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