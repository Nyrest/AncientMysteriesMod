using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_IceCream)]
    [MetaInfo(Lang.Default, "Ice Cream Gun", "todo")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class IceCreamGun : AMThingBulletGun
    {
        public IceCreamGun(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(tex_Gun_IceCream);
            ammo = 200;
            _fireWait = 2;
            _fullAuto = true;
        }

        public override IEnumerable<AMThingBulletBase> FireThingBullets(float shootAngleDeg)
        {
            yield return new TheSpiritOfDarkness_ThingBullet(GetBarrelPosition(new Vec2(27, 11)), duck, true, Maths.DegToRad(shootAngleDeg));
        }
    }
}
