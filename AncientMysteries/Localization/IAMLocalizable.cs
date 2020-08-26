using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Localization
{
    /// <summary>
    /// Prepare for Duck Game Localization Update
    /// </summary>
    public interface IAMLocalizable
    {
        public string GetLocalizedName(AMLang lang);
    }
}
