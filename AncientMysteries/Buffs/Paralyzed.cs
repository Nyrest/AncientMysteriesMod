using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Buffs
{
    public partial class Paralyzed : Equipment
    {
        public Waiter waiter = new Waiter(180);
        public Paralyzed(float xpos, float ypos) : base(xpos, ypos)
        {
            
        }

        public override void Update()
        {
            base.Update();
            if (_equippedDuck != null)
            {
                _equippedDuck.immobilized = true;
                if (waiter.Tick())
                {
                    _equippedDuck.immobilized = false;
                    Level.Remove(this);
                }
            }
            else
            {
                Level.Remove(this);
            }
        }
    }
}
