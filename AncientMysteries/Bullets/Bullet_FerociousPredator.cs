﻿namespace AncientMysteries.Bullets
{
    public class Bullet_FerociousPredator : Bullet
    {
        public Bullet_FerociousPredator(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            for (int repeat = 0; repeat < 6; repeat++)
            {
                ExplosionPart ins = new(pos.x - 8f + Rando.Float(32f), pos.y - 8f + Rando.Float(32f))
                {
                    xscale = 3f,
                    yscale = 3f
                };
                Level.Add(ins);
            }
            SFX.Play("explode");
            Vec2 bPos = pos;
            bPos -= travelDirNormalized;

            NetHelper.NmFireGun(null, list =>
            {
                for (int i = 0; i < 24; i++)
                {
                    float dir = (i * 30f) - 10f + Rando.Float(20f);
                    Bullet bullet = Make.Bullet<ATGrenadeLauncherShrapnel>(bPos, owner, dir, this);
                    list.Add(bullet);
                }
            });
            IEnumerable<Window> windows = Level.CheckCircleAll<Window>(position, 20f);
            foreach (Window w in windows)
            {
                if (Level.CheckLine<Block>(position, w.position, w) == null)
                {
                    w.Destroy(new DTImpact(this));
                }
            }
        }
    }
}