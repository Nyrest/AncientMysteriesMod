namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_OceanicRage)]
    [MetaInfo(Lang.Default, "Oceanic Rage", "Let it unleash the fury of the sea.")]
    [MetaInfo(Lang.schinese, "深洋狂怒", "让它释放深海的怒火。")]
    [MetaType(MetaType.Magic)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class OceanicRage : AMStaff
    {
        public OceanicRage(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            this.ReadyToRun(tex_Staff_OceanicRage);
            SetBox(15, 34);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.007f;
            _fireWait = 0.5f;
            _holdOffset = new(-5, -6);
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                var bullet = new OceanicRage_ThingBullet(firePos, GetBulletVecDeg(owner.offDir == 1 ? Rando.Float(-5,5) : Rando.Float(175, 185), 0.7f), duck);
                Level.Add(bullet);
                SFX.PlaySynchronized("largeSplash", 5, Rando.Float(-0.5f, -0.3f));
            }
        }
    }
}