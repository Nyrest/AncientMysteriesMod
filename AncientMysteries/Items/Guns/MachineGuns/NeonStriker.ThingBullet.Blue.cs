using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class NeonStriker_ThingBullet_Blue : AMThingBulletLinar
    {
        public Waiter w = new(2);

        public bool _canMultiply;
        public NeonStriker_ThingBullet_Blue(Vec2 pos, Vec2 initSpeed, Duck safeDuck, bool canMultiply) : base(pos, 1000, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_NeonLightBlue);
            _canMultiply = canMultiply;
            hasGravity = true;
            gravityIncrement = 0.2f;
            maxGravity = 1f;
        }

        public override void Update()
        {
            if (!_canMultiply)
            {
                alpha -= 0.1f;
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
            if (w.Tick() && _canMultiply)
            {
                NeonStriker_ThingBullet_Blue b = new(position, new Vec2(0.001f, 0), BulletSafeDuck, false);
                Level.Add(b);
            }
            base.Update();
        }
        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }
    }
}
