namespace AncientMysteries.Items
{
    public abstract partial class AMMapTool : AMThing, IAMLocalizable
    {
        public AMMapTool(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_MapTools_Swirl);
            _editorName = GetLocalizedName(LocalizationHelper.DefaultLang);
            editorTooltip = GetLocalizedDescription(LocalizationHelper.DefaultLang);
            _visibleInGame = false;
        }

        public abstract string GetLocalizedDescription(Lang lang);

        public abstract string GetLocalizedName(Lang lang);
    }
}