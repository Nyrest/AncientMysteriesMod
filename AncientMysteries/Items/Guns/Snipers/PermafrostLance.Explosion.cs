namespace AncientMysteries.Items
{
    public sealed class PermafrostExplosion : ExplosionPart
    {
        public SpriteMap _sprite;
        public bool _smoked;
        public int _smokeFrame;
        public float _wait;

        public PermafrostExplosion(float xpos, float ypos, bool doWait = true) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunWithFrames(tex_Effect_PermafrostExplosion, 15, 15);
            _sprite.AddAnimation("loop", 0.6f, false, new int[]
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            _sprite.SetAnimation("loop");
            graphic = _sprite;
            _sprite.speed = 0.6f;
            xscale = 2f;
            yscale = xscale;
            center = new Vec2(7.5f, 7.5f);
            depth = 0f;
            if (!doWait)
            {
                _wait = 0f;
            }
        }
    }
}