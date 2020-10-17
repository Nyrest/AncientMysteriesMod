using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Stuff.Props
{
    [EditorGroup(s_props)]
    public class MeltingMeat : CookedDuck
    {
        public SmallFire fire;

        public MeltingMeat(float xpos, float ypos) : base(xpos, ypos)
        {
            this.graphic.color = new Color(250, 130, 130);
        }

        public override void Initialize()
        {
            base.Initialize();
            Level.Add(fire = SmallFire.New(x, y, 0f, 0f, false, this, true, this, false));
        }

        public override void Update()
        {
            base.Update();
            if (fire != null && fire.life <= 5)
            {
                fire.SuckLife(-10);
            }
        }
    }
}
