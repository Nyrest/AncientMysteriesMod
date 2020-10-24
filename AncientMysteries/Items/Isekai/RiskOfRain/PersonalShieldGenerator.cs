﻿using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    public class PersonalShieldGenerator : RoREquipmentBase
    {
        public PersonalShieldGenerator(float xpos, float ypos) : base(xpos, ypos)
        {

        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "个人护盾生成器",
            _ => "Personal Shield Generator",
        };
    }
}
