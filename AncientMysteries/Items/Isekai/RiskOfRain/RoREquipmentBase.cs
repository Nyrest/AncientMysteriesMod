using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    public abstract class RoREquipmentBase : AMEquipment
    {
        public RoREquipmentBase(float xpos, float ypos) : base(xpos, ypos)
        {
            _isArmor = false;
            _canCrush = false;
            _destroyable = false;
            _knockOffOnHit = false;
        }
    }
}
