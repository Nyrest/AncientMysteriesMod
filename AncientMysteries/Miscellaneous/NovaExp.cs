using DuckGame;

namespace AncientMysteries.Items.Miscellaneous
{
    public sealed class NovaExp : Thing
    {
        public SpriteMap _sprite;
        public bool _smoked;
        public int _smokeFrame;
        public float _wait;
        public NovaExp(float xpos, float ypos, bool doWait = true) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap("crystalExplosionP.png", 36, 36);
            this._sprite.AddAnimation("loop", 1f, false, new int[]
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
            this._sprite.SetAnimation("loop");
            this.graphic = this._sprite;
            this._sprite.speed = 0.6f;
            base.xscale = 0.5f;
            base.yscale = base.xscale;
            this.center = new Vec2(18f, 18f);
            base.depth = 1f;
            if (!doWait)
            {
                this._wait = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
