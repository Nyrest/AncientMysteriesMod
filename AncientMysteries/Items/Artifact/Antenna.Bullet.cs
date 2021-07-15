using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Artifact
{
    class AntennaBullet : Thing, ITeleport
    {
        public StateBinding positionBinding = new(nameof(position));

        public Vec2 move;

        public StateBinding moveBinding = new(nameof(move));

        public bool isMoving => move != Vec2.Zero;

        public int aliveTime = 0;

        public StateBinding aliveTimeBinding = new(nameof(aliveTime));

        public StateBinding safeDuckBinding = new(nameof(safeDuck));
        public Duck safeDuck;

        public AntennaBullet(Vec2 anglePoint, Duck safeDuck, float xval = 0, float yval = 0) : base(xval, yval, null)
        {
            alpha = 0f;
            angleDegrees = -Maths.PointDirection(new Vec2(0, 0), anglePoint);
            graphic = TexHelper.ModSprite(t_Bullet_Antenna, true);
            this.safeDuck = safeDuck;
        }

        public override void Update()
        {
            base.Update();
            if (alpha < 1)
            {
                alpha += 0.016f;
            }
            else
            {
                alpha = 1;
            }

            if (isMoving)
            {
                position += move;
                aliveTime++;
            }

            if (aliveTime >= 120)
            {
                Level.Remove(this);
            }

            foreach (Duck d in Level.CheckRectAll<Duck>(base.topLeft, base.bottomRight))
            {
                if (isMoving == false)
                {
                    return;
                }
                if (d != safeDuck)
                {
                    d.Destroy(new DTCrush(d));
                }
            }
        }
    }
}
