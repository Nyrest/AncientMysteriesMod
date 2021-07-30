using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns)]
    [MetaImage(tex_Gun_Umbra)]
    [MetaInfo(Lang.english, "Gaster Blaster Test", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class aGasterBlasterTest : AMGun
    {
        public aGasterBlasterTest(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            this.ReadyToRun(tex_Gun_Umbra);
        }

        public override void Fire()
        {
            base.Fire();
            
            //var a = new GasterBlaster(x, y) {angle = };
        }
    }
}
