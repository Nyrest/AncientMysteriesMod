using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    [EditorGroup(e_isekai_ror)]
    public class SoldiersSyringe : RoREquipmentBase
    {
        public SoldiersSyringe(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override void Update()
        {
            base.Update();
            if (duck?.gun is Gun gun && gun._fireWait > 0)
            {
                gun._fireWait -= 0.015f;
            }
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "Soldier's Syringe",
            _ => "士兵的注射器",
        };
    }
}
