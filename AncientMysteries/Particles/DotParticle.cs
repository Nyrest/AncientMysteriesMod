namespace AncientMysteries.Particles
{
    public class DotParticle : Thing, IFactory
    {
        public const int kMaxWagCharge = 128;

        public static DotParticle[] _sparks = new DotParticle[kMaxWagCharge];

        public static int _lastActiveWagCharge = 0;

        public Func<Vec2> _target;

        public float life = 1f;

        public float lifeFadeSpeed;

        public Color color;

        public static DotParticle New(float xpos, float ypos, Func<Vec2> target, in Color color, float lifeFadeSpeed = 0.02f)
        {
            DotParticle spark = null;
            if (_sparks[_lastActiveWagCharge] == null)
            {
                spark = new DotParticle();
                _sparks[_lastActiveWagCharge] = spark;
            }
            else
            {
                spark = _sparks[_lastActiveWagCharge];
            }
            _lastActiveWagCharge = (_lastActiveWagCharge + 1) % kMaxWagCharge;
            spark.ResetProperties();
            spark.Init(xpos, ypos, target, color, lifeFadeSpeed);
            spark.globalIndex = GetGlobalIndex();
            return spark;
        }

        private SpriteMap _airFire;

        private float _airFireScale;

        private float _spinSpeed;

        private DotParticle()
        {
            _airFire = new SpriteMap("airFire", 16, 16);
            _airFire.AddAnimation("burn", 0.2f + Rando.Float(0.2f), true, 0, 1, 2, 1);
            _airFire.center = new Vec2(8f, 8f);
        }

        private void Init(float xpos, float ypos, Func<Vec2> target, in Color color, float fadeSpeed)
        {
            hSpeed = Rando.Float(-1f, 1f);
            vSpeed = Rando.Float(-1f, 1f);
            position.x = xpos;
            position.y = ypos;
            lifeFadeSpeed = fadeSpeed;
            depth = 0.9f;
            life = 1f;
            this.color = color;
            _target = target;

            _airFire.SetAnimation("burn");
            _airFire.imageIndex = Rando.Int(2);
            _airFire.color = Color.Orange * (0.8f + Rando.Float(0.2f));
            _airFire.globalIndex = GetGlobalIndex();
            _airFireScale = 0f;
            _spinSpeed = 0.1f + Rando.Float(0.1f);

            alpha = 1f;
        }

        public override void Update()
        {
            var targetPos = _target();
            Vec2 travel = position - targetPos;
            float len = travel.lengthSq;
            if (len is < 64f or > 4096f)
            {
                alpha -= 0.08f;
            }
            hSpeed = Lerp.Float(hSpeed, (0f - travel.x) * 0.7f, 0.15f);
            vSpeed = Lerp.Float(vSpeed, (0f - travel.y) * 0.7f, 0.15f);
            position.x += hSpeed;
            position.y += vSpeed;
            position.x = Lerp.Float(position.x, targetPos.x, 0.16f);
            position.y = Lerp.Float(position.y, targetPos.y, 0.16f);
            hSpeed *= Math.Min(1f, len / 128f + 0.25f);
            vSpeed *= Math.Min(1f, len / 128f + 0.25f);
            life -= lifeFadeSpeed;
            if (life < 0f)
            {
                alpha -= 0.08f;
            }

            #region Air Fire

            if (_airFireScale < 1.2f)
            {
                _airFireScale += 0.15f;
            }
            if (Rand.Bool())
            {
                _airFireScale -= 0.3f;
                if (_airFireScale < 0.9f)
                {
                    _airFireScale = 0.9f;
                }
                _spinSpeed -= 0.01f;
                if (_spinSpeed < 0.05f)
                {
                    _spinSpeed = 0.05f;
                }
            }

            _airFire.depth = depth - 1;
            _airFire.alpha = 0.5f;
            _airFire.angle += hSpeed * _spinSpeed;

            #endregion Air Fire

            if (alpha < 0f)
            {
                Level.Remove(this);
            }
            base.Update();
        }

        public override void Draw()
        {
            Vec2 dir = velocity.normalized;
            float speed = velocity.length * 2f;
            Vec2 end = position + dir * speed;
            Graphics.DrawLine(col: color * alpha, p1: position, p2: end, width: 1f, depth: depth);
        }
    }
}