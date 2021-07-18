namespace AncientMysteries.Localization
{
    /// <summary>
    /// Prepare for Duck Game Localization Update
    /// </summary>
    public interface IAMLocalizable
    {
        public string GetLocalizedName(Lang lang);

        public string GetLocalizedDescription(Lang lang);
    }
}