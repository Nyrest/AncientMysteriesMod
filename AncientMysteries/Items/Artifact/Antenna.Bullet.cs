using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Artifact
{
    class AntennaBullet : Thing
    {
        public Vec2 moveSpeed;

        public StateBinding moveBinding = new(nameof(moveSpeed));

        public bool isMoving = false;

        public StateBinding movingBinding = new(nameof(isMoving));

        public int aliveTime = 0;

        public StateBinding aliveTimeBinding = new(nameof(aliveTime));
        public AntennaBullet(Vec2 spd, Thing ownerr = null, float xval = 0, float yval = 0, Sprite sprite = null) : base(xval, yval, sprite)
        {
            moveSpeed = spd;
            alpha = 0f;
            angleDegrees = -Maths.PointDirection(new Vec2(0, 0), spd);
            graphic = TexHelper.ModSprite(t_Bullet_Antenna, true);
            owner = ownerr;
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
                position += moveSpeed;
            }

            if (aliveTime >= 120)
            {
                Level.Remove(this);
            }

            if (isMoving)
            {
                aliveTime++;
            }
            foreach (Duck d in Level.CheckRectAll<Duck>(base.topLeft, base.bottomRight))
            {
                if (isMoving == false)
                {
                    return;
                }
                if (d != owner)
                {
                    d.Destroy(new DTCrush(d));
                }
            }
        }
    }
}
