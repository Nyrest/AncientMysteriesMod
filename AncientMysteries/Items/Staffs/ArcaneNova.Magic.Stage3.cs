﻿using AncientMysteries.DestroyTypes;

namespace AncientMysteries.Items
{
    public class ArcaneNova_Magic_Stage3 : AMThingBulletCircular
    {
        public ArcaneNova_Magic_Stage3(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 90, float.PositiveInfinity, initSpeed, safeDuck)
        {
            var _spriteMap = this.ReadyToRunWithFrames(tex_Bullet_NovaFrame2, 25, 25);
            _spriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            BulletRadius = 12.5f;
        }

        public override ColorTrajectory GetTrajectory() => null;

        public override void BulletRemove()
        {
            NovaExp n = new(x, y, true);
            n.xscale *= 2.25f;
            n.yscale *= 2.25f;
            Level.Add(n);
            SFX.PlaySynchronized("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            if (isServerForObject)
            {
                IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(position, 25f);
                foreach (MaterialThing t2 in things)
                {
                    if (t2 != BulletSafeDuck)
                    {
                        t2.Destroy(new DT_ThingBullet(this));
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    var bullet = new ArcaneNova_Magic_Stage4(position, GetBulletVecDeg(Rando.Float(0, 360), 6), BulletSafeDuck);
                    Level.Add(bullet);
                }
            }
            base.BulletRemove();
        }
    }
}