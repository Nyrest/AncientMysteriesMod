using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AncientMysteries.Items
{
    public abstract class AMNotGun : AMGun
    {
        protected AMNotGun(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
        }

        public override void ApplyKick() { }

        public override void Fire() { }
    }
}
