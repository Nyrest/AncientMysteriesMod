using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    public class SoldiersSyringe : RoREquipmentBase
    {
        public SoldiersSyringe(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "Soldier's Syringe",
            _ => "士兵的注射器",
        };
    }
}
