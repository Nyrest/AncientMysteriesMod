using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class DanceIceAndFire_BulletThing : AMThing
    {
        public StateBinding linkedBallBinding = new(nameof(linkedBall));
        public DanceIceAndFire_BulletThing linkedBall;

        public StateBinding rollingBinding = new(nameof(rolling));
        public bool rolling;

        public DanceIceAndFire_BulletThing(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRunWithFrames(tex_Bullet_IceFireContainer);
        }

        public override void Update()
        {
            base.Update();
            if (linkedBall is null) return;
            if (linkedBall.rolling) rolling = false;

            if (rolling)
            {
            }
        }

        public void MakeRolling()
        {
            rolling = true;
            linkedBall.rolling = false;
        }
    }
}