namespace AncientMysteries.Bullets
{
    public class Bullet_CubicBlast : Bullet
    {
        public bool fired = false;

        public Bullet_CubicBlast(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void Update()
        {
            base.Update();
            foreach (Duck d in Level.CheckCircleAll<Duck>(start, 80))
            {
                if (d != _owner && fired == false)
                {
                    fired = true;
                    var firedBullets = new List<Bullet>(1);
                    var bullet = Make.Bullet<AT_Current>(start, _owner, -Maths.PointDirection(start, d.position), this);
                    SFX.PlaySynchronized("deadTVLand");
                    firedBullets.Add(bullet);
                    Level.Add(bullet);
                }
            }
        }
    }
}