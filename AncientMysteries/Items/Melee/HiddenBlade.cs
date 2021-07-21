namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Melees)]
    [MetaImage(tex_Melee_HiddenBlade_Out)]
    [MetaInfo(Lang.english, "Hidden Blade", "「Nothing is true, everything is permitted.」")]
    [MetaInfo(Lang.schinese, "袖箭", "「万事皆虚，万事皆允。」")]
    [MetaType(MetaType.Melee)]
    public partial class HiddenBlade : AMHoldable
    {
        public StateBinding bladeOutBinding = new StateBinding(nameof(bladeOut));

        private bool _bladeOut;
        public bool bladeOut
        {
            get => _bladeOut;
            set
            {
                if (_bladeOut != value)
                {
                    _bladeOut = value;
                    this.ReadyToRun(value ? tex_Melee_HiddenBlade_Out : tex_Melee_HiddenBlade);
                }
            }
        }

        public HiddenBlade(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Melee_HiddenBlade);
            hideLeftWing = true;
            hideRightWing = true;
            ignoreHands = true;
            _holdOffset = new Vec2(-8, 4);
        }

        public override void Update()
        {
            base.Update();
            if (!held)
            {
                bladeOut = false;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            bladeOut = true;
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            bladeOut = false;
        }
    }
}
