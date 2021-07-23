using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public partial class TheSpiritOfDarkness_ThingBullet : AMThingBulletLinar
    {
        public bool _goingUp = false;
        public TheSpiritOfDarkness_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck, bool goingUp) : base(pos, 320, 2, initSpeed, safeDuck)
        {
            BulletTailColor = Color.Purple;
            this.ReadyToRun(tex_Bullet_TSOD);
            _goingUp = goingUp;
        }

        public override void Update()
        {
            base.Update();
            if (_goingUp) y += (float)Math.Sin(2*x) * 3;
            else y += -(float)Math.Sin(2 * x) * 3;
        }
    }
}
