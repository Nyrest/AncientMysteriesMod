using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    public class BrilliantBehemoth : RoREquipmentBase
    {
        public BrilliantBehemoth(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "完美巨兽",
            _ => "Brilliant Behemoth",
        };
    }
}
