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

        public float cos = 0;

        public float a = 3;
        public TheSpiritOfDarkness_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck, bool goingUp) : base(pos, 320, int.MaxValue, initSpeed, safeDuck)
        {
            BulletTailColor = Color.Purple;
            this.ReadyToRun(tex_Bullet_TSOD);
            _goingUp = goingUp;
        }

        public override void Update()
        {
            base.Update();
            cos += 0.2f;
            if (_goingUp) y += (float)Math.Cos(cos) * a;
            else y += -(float)Math.Cos(cos) * a;
        }
    }
}
