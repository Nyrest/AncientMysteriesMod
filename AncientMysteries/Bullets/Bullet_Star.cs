using AncientMysteries.AmmoTypes;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Bullets
{
    public class Bullet_Star : Bullet
    {
        private Texture2D _beem;

        private float _thickness;

        public int n = 0;

        public Bullet_Star(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = this.ModTex2D("holyStar.png");
        }

        public override void Update()
        {
            base.Update();
            n++;
            if (n == 5)
            {
                Level.Add(new Bullet_Star2(start.x, start.y, new AT_Star2(), _angle + 3.14f, owner, false, 80, false, true));
                n = 0;
            }
            /*foreach (Thing t in Level.CheckCircleAll<Thing>(this.position,10))
            {
                if (t != Level.CheckCircleAll<Thing>(DuckNetwork.localConnection.profile.duck.position,20))
                {
                    Level.Add(SmallFire.New(t.x, t.y, 0, 0));
                }
            }*/
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
        }
    }
}
