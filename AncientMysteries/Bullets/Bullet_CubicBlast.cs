using System.Collections.Generic;

namespace AncientMysteries.Bullets
{
    public class Bullet_CubicBlast : Bullet
    {
        public static int count = 0;
        public Bullet_CubicBlast(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
            foreach (Duck d in Level.CheckCircleAll<Duck>(position, 50))
            {
                if (d != owner &&  count >= 15)
                {
                    count = 0;
                    var firedBullets = new List<Bullet>(1);
                    for (int i = 0; i < 7; i++)
                    {
                        var bullet = new Bullet_Current(x, y, new AT_Current(), Maths.PointDirection(position, d.position), owner, true, 400, false, true);
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                    }
                }

            }
            count++;
        }
    }
}
