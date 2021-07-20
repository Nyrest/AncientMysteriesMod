using AncientMysteries.DeathTypes;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Props_functional)]
    [MetaImage(tex_Props_DeathFlower, 9, 14, 0, 1)]
    [MetaInfo(Lang.english, "Wonder Flower", "Looks delicious. Eat it!")]
    [MetaInfo(Lang.schinese, "一朵可爱的FAFA", "看起来很美味，吃了它！")]
    [MetaType(MetaType.Props)]
    public partial class DeathFlower : AMHoldable
    {
        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "一朵可爱的FAFA",
            _ => "Wonder Flower",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "看起来很美味，吃了它！",
            _ => "Looks delicious. Eat it!",
        };

        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public DeathFlower(float xval, float yval) : base(xval, yval)
        {
            _spriteMap = this.ReadyToRunWithFrames(tex_Props_DeathFlower, 9, 14);
            AnimationFrame = 0;
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if (duck != null && AnimationFrame == 0)
            {
                duck.Scream();
                duck.Kill(new DT_NoReason());
                SFX.PlayMod(snd_FloweyLaugh, 1, -0.3f);
                AnimationFrame = 1;
            }
        }
    }
}