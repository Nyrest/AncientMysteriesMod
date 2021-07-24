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

        public float cosInput = 0;

        public float amplitude = 3;
        public float fireAngleRadius;
        public StateBinding fireAngleBinding = new(nameof(fireAngleRadius));

        public override bool IsMoving => true;

        public TheSpiritOfDarkness_ThingBullet(Vec2 pos, Duck safeDuck, bool goingUp, float fireAngleRadius) : base(pos, 820, int.MaxValue, Vec2.Zero, safeDuck)
        {
            BulletTailColor = Color.Purple;
            this.ReadyToRun(tex_Bullet_TSOD);
            _goingUp = goingUp;
            BulletTailMaxSegments = 10;
            BulletCanCollideWhenNotMoving = true;
            this.fireAngleRadius = fireAngleRadius;
        }

        public override void Update()
        {
            cosInput += 0.2f;
            float offset = (float)Math.Cos(cosInput) * amplitude;
            var offsetVec = new Vec2(3, _goingUp ? offset : -offset);
            speed = offsetVec.Rotate(fireAngleRadius, Vec2.Zero);
            speed.y *= -1;
            //if (_goingUp) y += (float)Math.Cos(cosInput) * amplitude;
            //else y += -(float)Math.Cos(cosInput) * amplitude;
            base.Update();
        }

        public override void BulletRemove()
        {
            base.BulletRemove();
        }
    }
}
