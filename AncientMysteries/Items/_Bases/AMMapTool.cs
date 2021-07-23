namespace AncientMysteries.Items
{
    public abstract partial class AMMapTool : AMThing, IAMLocalizable
    {
        public AMMapTool(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = GetLocalizedName(LocalizationHelper.DefaultLang);
        }

        public abstract string GetLocalizedDescription(Lang lang);

        public abstract string GetLocalizedName(Lang lang);
    }
}