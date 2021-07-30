using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Isekai)]
    [MetaImage(tex_Gun_IceFireContainer)]
    [MetaInfo(Lang.Default, "Dance Ice And Fire", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    partial class DanceIceAndFire : AMHoldable
    {
        public DanceIceAndFire(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Gun_IceFireContainer);
        }
    }
}
