namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.english, "The Spirit Of Darkness", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class TheSpiritOfDarkness : AMGun
    {
        public Waiter w = new(20);
        public TheSpiritOfDarkness(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Gun_TheSpiritOfDarkness);
            ammo = 1;
            _ammoType = new AT_Shadow()
            {
                range = 360f,
                accuracy = 0.7f,
                penetration = 1f
            };

        }

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            if (w.Tick())
            {
                TheSpiritOfDarkness_ThingBullet b1 = new(GetBarrelPosition(new Vec2(27, 11)), GetBulletVecDeg(owner.FaceAngleDegressLeftOrRight(),3), duck, true);
                TheSpiritOfDarkness_ThingBullet b2 = new(GetBarrelPosition(new Vec2(27, 11)), GetBulletVecDeg(owner.FaceAngleDegressLeftOrRight(), 3), duck, false);
                Level.Add(b1);
                Level.Add(b2);
                SFX.PlaySynchronized("laserRifle",1,0.5f);
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}