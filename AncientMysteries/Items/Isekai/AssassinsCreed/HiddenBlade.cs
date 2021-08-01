namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Melees)]
    [MetaImage(tex_Melee_HiddenBlade, 8, 16, 0, 1)]
    [MetaInfo(Lang.Default, "Hidden Blade", "「Nothing is true, everything is permitted.」")]
    [MetaInfo(Lang.schinese, "袖剑", "「万物皆虚，万事皆允。」")]
    [MetaType(MetaType.Melee)]
    public partial class HiddenBlade : AMHoldable
    {
        public StateBinding bladeOutBinding = new(nameof(bladeOut));

        private bool _bladeOut;
        private readonly SpriteMap _spriteMap;
        public bool bladeOut
        {
            get => _bladeOut;
            set
            {
                if (_bladeOut != value)
                {
                    _bladeOut = value;
                    _spriteMap.frame = value ? 1 : 0;
                }
            }
        }

        public HiddenBlade(float xpos, float ypos) : base(xpos, ypos)
        {
            _spriteMap = this.ReadyToRunWithFrames(tex_Melee_HiddenBlade, 8, 16);
            SetBox(8, 10);
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