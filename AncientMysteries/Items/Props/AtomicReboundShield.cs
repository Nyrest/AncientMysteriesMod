namespace AncientMysteries.Items
{
    [EditorGroup(group_Props_functional)]
    [MetaImage(tex_Holdable_ReboundShield)]
    [MetaInfo(Lang.Default, "Rebound Shield", "It may also rebound the chain letters.")]
    [MetaInfo(Lang.schinese, "反弹盾", "也许还能为你反弹诅咒连环信。")]
    [MetaType(MetaType.Props)]
    public partial class ReboundShield : AMGun, IPlatform
    {
        public ReboundShield(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            this.ReadyToRunWithFrames(tex_Holdable_ReboundShield);
            thickness = 100f;
            weight = 10f;
            flammable = 0f;
            physicsMaterial = PhysicsMaterial.Metal;
            _holdOffset = new Vec2(-5, 0);
        }

        public override void Update()
        {
            solid = true;
            base.Update();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.ammo is null) return base.Hit(bullet, hitPos);
            if (bullet.ammo.penetration < thickness)
            {
                SFX.Play("ting", 0.8f, Rando.Float(-0.4f, 0.4f));
                bullet.ReverseTravel();
            }
            return base.Hit(bullet, hitPos);
        }

        public override void ApplyKick()
        {
        }

        public override void PressAction()
        {
        }

        public override void Fire()
        {
        }
    }
}