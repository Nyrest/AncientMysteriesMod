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
    public class CeremonialDagger : RoREquipmentBase
    {
        public CeremonialDagger(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "Ceremonial Dagger",
            _ => "仪式用匕首",
        };
    }
}
