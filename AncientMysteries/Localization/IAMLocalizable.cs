﻿namespace AncientMysteries.Localization
{
    /// <summary>
    /// Prepare for Duck Game Localization Update
    /// </summary>
    public interface IAMLocalizable
    {
        public string GetLocalizedName(AMLang lang);

        public string GetLocalizedDescription(AMLang lang);
    }
}
