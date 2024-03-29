﻿namespace AncientMysteries.Items
{
    public class PrimordialLibram_ThingBullet_Flower : AMThingBulletLinar
    {
        public Waiter waiter = new(10);

        public PrimordialLibram_ThingBullet_Flower(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 300, 3f, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_Flower);
            angleDegrees = Rando.Float(0, 360);
            alpha = 0;
        }

        public override ColorTrajectory GetTrajectory() => null;

        public override void Removed()
        {
            base.Removed();
            if (isServerForObject)
            {
                for (int i = 0; i < 10; i++)
                {
                    PrimordialLibram_ThingBullet_Leaf bullet = new(position, GetBulletVecDeg(Rando.Float(0, 360), 4), BulletSafeDuck);
                    Level.Add(bullet);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            angle += 30;
            MathHelper.Min(alpha += 0.04f, 1);
        }

        public override bool BulletCanHit(MaterialThing thing)
        {
            if (thing is Boring3)
            {
                return false;
            }
            return base.BulletCanHit(thing);
        }
    }
}