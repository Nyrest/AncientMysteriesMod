namespace AncientMysteries.Items.Equipments.Back
{
    [EditorGroup(e_developer)]
#warning Texture Todo
    [MetaImage(tex_Bullet_NovaFrame)]
    [MetaInfo(Lang.english, "Angel Wings", "「あぁ〜麻婆豆腐〜♪〜♪」")]
    [MetaInfo(Lang.schinese, "天使之翼", null)]
    public partial class AngelWings : AMEquipment
    {
        public bool isFlying = false;

        public int timeFlied = 0;

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public AngelWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _spriteMap = this.ReadyToRunWithFrames(t_Equipment_DemonWings, 28, 14);
            _spriteMap.AddAnimation("loop", 0.18f, true, 0, 1, 2, 1);
            _spriteMap.SetAnimation("loop");
            wearOffset = new(-0.5f, -1);
            _equippedDepth = -2;
        }

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "天使之翼",
            _ => "Angel Wings",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            _ => "「あぁ〜麻婆豆腐〜♪〜♪」",
        };

        public override void OnPressAction()
        {
            base.OnPressAction();
            isFlying = true;
        }

        public override void OnReleaseAction()
        {
            base.OnPressAction();
            isFlying = false;
        }

        public override void Update()
        {
            base.Update(); 
            if (owner != null)
            {
                timeFlied++;
                owner.vSpeed += -0.1f;
            }
            if (owner != null && timeFlied > 300)
            {
                timeFlied = 0;
            }
        }
    }
}