namespace AncientMysteries.Items.Guns.Snipers
{
    [EditorGroup(g_snipers)]
    [MetaImage(t_)]
    [MetaInfo(Lang.english, "Shadow Dance", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    public sealed partial class ShadowDance : AMGun
    {
        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "影舞者",
            _ => "Shadow Dance",
        };
        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "瞄准，开火！",
            _ => "Aim, fire!",
        };
        public ShadowDance(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 1600,
                penetration = 3.5f,
            };
            this.ReadyToRunWithFrames(t_Gun_ShadowDance);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _barrelOffsetTL = new Vec2(34f, 6f);
            _fireSound = "laserBlast";
            _fireSoundPitch = -0.8f;
            _kickForce = 0f;
            _holdOffset = new Vec2(3f, 0f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}
