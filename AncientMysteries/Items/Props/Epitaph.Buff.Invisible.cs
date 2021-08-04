using AncientMysteries.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class Epitaph_Buff_Invisible : Equipment
    {
        public Waiter waiter = new(480);


        public Epitaph_Buff_Invisible(float xpos, float ypos) : base(xpos, ypos)
        {
            canPickUp = false;
            visible = false;
        }

        public override void Update()
        {
            base.Update();
            if (_equippedDuck is Duck equippedDuck)
            {
                if (!waiter.Tick())
                {
                    ApplyBuff(equippedDuck, true);
                }
                else
                {
                    ApplyBuff(equippedDuck, false);
                    Level.Remove(this);
                }
            }
            else
            {
                Level.Remove(this);
            }
        }

        public override void UnEquip()
        {
            base.UnEquip();
            if (_equippedDuck != null)
            {
                ApplyBuff(_equippedDuck, false);
            }
        }

        public void ApplyBuff(Duck duck, bool enable)
        {
            if (enable)
            {
                if (duck.localDuck)
                {
                    duck.alpha = 0.5f;
                    duck.material = new MaterialSelection();
                }
                else
                {
                    duck.visible = false;
                }
            }
            else
            {
                if (duck.localDuck)
                {
                    duck.material = null;
                    duck.alpha = 1f;
                }
                else
                {
                    duck.visible = true;
                }
            }
        }
    }
}
