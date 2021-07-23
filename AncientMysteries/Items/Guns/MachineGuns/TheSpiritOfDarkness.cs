using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.english, "The Spirit Of Darkness", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class TheSpiritOfDarkness : AMGun
    {
        public TheSpiritOfDarkness(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Gun_TheSpiritOfDarkness);
            ammo = 1;
            _ammoType = new AT_Shadow()
            {
                range = 360f,
                accuracy = 0.7f,
                penetration = 1f
            };
        }

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            TheSpiritOfDarkness_ThingBullet b1 = new(barrelPosition + new Vec2(3,10), new Vec2(3, 0), duck, true);
            TheSpiritOfDarkness_ThingBullet b2 = new(barrelPosition + new Vec2(3, 10), new Vec2(3, 0), duck, false) ;
            Level.Add(b1);
            Level.Add(b2);
        }

        public override void Update()
        {
            base.Update();

        }
    }
}
