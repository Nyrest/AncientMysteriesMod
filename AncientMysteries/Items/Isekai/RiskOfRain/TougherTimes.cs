﻿using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    public class TougherTimes : RoREquipmentBase
    {
        public TougherTimes(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "Tougher Times",
            _ => "最艰难的时光",
        };
    }
}
