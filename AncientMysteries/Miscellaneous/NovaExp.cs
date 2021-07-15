namespace AncientMysteries.Items.Miscellaneous
{
    public sealed class NovaExp : ExplosionPart
    {
        public SpriteMap _sprite;
        public bool _smoked;
        public int _smokeFrame;
        public float _wait;
        public NovaExp(float xpos, float ypos, bool doWait = true) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunWithFrames(t_Effect_CrystalExplosionPurple, 36, 36);
            _sprite.AddAnimation("loop", 1f, false, new int[]
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
            });
            _sprite.SetAnimation("loop");
            graphic = _sprite;
            _sprite.speed = 0.6f;
            xscale = 0.5f;
            yscale = xscale;
            center = new Vec2(18f, 18f);
            depth = 1f;
            if (!doWait)
            {
                _wait = 0f;
            }
        }
    }
}
