namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_Oblivion)]
    [MetaInfo(Lang.Default, "Oblivion", "A perfect combination of technology and magic.")]
    [MetaInfo(Lang.schinese, "遗忘", "科技与魔法的完美结合。")]
    [MetaType(MetaType.Magic)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class Oblivion : AMStaff
    {
        public bool isRed = true;

        private bool _quacked;
        public Oblivion(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            this.ReadyToRun(tex_Staff_Oblivion);
            SetBox(17, 35);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.016f;
            _fireWait = 0.5f;
            _holdOffset = new(-5, -5);
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                if (isRed)
                {
                    var bullet = new Oblivion_ThingBulletRed(firePos, GetBulletVecDeg(owner.offDir == 1 ? Rando.Float(-5, 5) : Rando.Float(175, 185), 2.8f), duck);
                    Level.Add(bullet);
                }
                else
                {
                    var bullet = new Oblivion_ThingBulletBlue(firePos, GetBulletVecDeg(owner.offDir == 1 ? Rando.Float(-5, 5) : Rando.Float(175, 185), 2.8f), duck);
                    Level.Add(bullet);
                }
                SFX.PlaySynchronized("laserRifle", 5, Rando.Float(0.8f, 1f));
            }
        }

        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                if (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                {
                    isRed = !isRed;
                    SFX.Play("swipe", 1f, 0.8f);
                }
            }
            if (isRed)
            {
                this.ReadyToRun(tex_Staff_Oblivion);
            }
            else
            {
                this.ReadyToRun(tex_Staff_OblivionBlue);
            }
        }
    }
}