using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items{
    public class BloodyMarquis_Crystal : AMThing
    {
        public StateBinding positionBinding = new CompressedVec2Binding(nameof(position));
        public StateBinding alphaBinding = new CompressedFloatBinding(nameof(alpha));

        public BloodyMarquis_Crystal(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Props_BloodyCrystal);
        }

        public override void Update()
        {
            base.Update();
            alpha -= 0.03f;
            if(alpha<=0)
            {
                Level.Remove(this);
            }
        }
    }
}
