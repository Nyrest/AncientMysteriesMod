using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_Refrigerator)]
    [MetaInfo(Lang.Default, "Refrigerator", "todo")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class Refrigerator : AMThingBulletGun
    {
        public StateBinding backpackBinding = new StateBinding(nameof(backpack));
        public Refrigerator_Backpack backpack;

        public Refrigerator(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(tex_Gun_Refrigerator);
            ammo = 200;
            _fireWait = 2;
            _fullAuto = true;
        }

        public override void Update()
        {
            if (backpack == null && !(Level.current is Editor) && isServerForObject)
            {
                backpack = new Refrigerator_Backpack(x, y, this)
                {
                    visible = false,
                };
                Level.Add(backpack);
            }
            base.Update();
            if (backpack != null && isServerForObject)
            {
                if (duck is Duck d)
                {
                    if(!d.HasEquipment(backpack))
                    {
                        d.Equip(backpack);
                    }
                    backpack.visible = true;
                }
                else
                {
                    if (backpack?.equippedDuck is Duck equippedDuck)
                    {
                        equippedDuck.Unequip(backpack);
                    }
                    backpack.UnEquip();
                    backpack.visible = false;
                }
            }
        }

        public override IEnumerable<AMThingBulletBase> FireThingBullets(float shootAngleDeg)
        {
            yield return new TheSpiritOfDarkness_ThingBullet(GetBarrelPosition(new Vec2(27, 11)), duck, true, Maths.DegToRad(shootAngleDeg));
        }
    }
}
