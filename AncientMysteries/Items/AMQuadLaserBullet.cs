using AncientMysteries;

namespace DuckGame
{
    public class AMQuadLaserBullet : Thing, ITeleport
    {
        public StateBinding _positionBinding = new CompressedVec2Binding("position", int.MaxValue, isvelocity: false, doLerp: true);

        public StateBinding _travelBinding = new CompressedVec2Binding("travel", 20);

        private Vec2 _travel;

        public float timeAlive;

        public static readonly Tex2D t = TexHelper.ModTex2D(t_NovaFrm);

        public Vec2 travel
        {
            get
            {
                return _travel;
            }
            set
            {
                _travel = value;
            }
        }

        public AMQuadLaserBullet(float xpos, float ypos, Vec2 travel)
            : base(xpos, ypos)
        {
            _travel = travel;
            collisionOffset = new Vec2(-1f, -1f);
            _collisionSize = new Vec2(2f, 2f);
        }

        public override void Update()
        {
            timeAlive += 0.016f;
            position += _travel * 0.5f;
            if (base.isServerForObject && (base.x > Level.current.bottomRight.x + 200f || base.x < Level.current.topLeft.x - 200f))
            {
                Level.Remove(this);
            }
            base.Update();
        }

        public override void Draw()
        {
            Graphics.Draw(t, x, y);
            base.Draw();
        }
    }
}
