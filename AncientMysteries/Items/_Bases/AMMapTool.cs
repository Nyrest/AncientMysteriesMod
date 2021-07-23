namespace AncientMysteries.Items
{
    public abstract partial class AMMapTool : AMThing, IAMLocalizable
    {
        public AMMapTool(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public abstract string GetLocalizedDescription(Lang lang);

        public abstract string GetLocalizedName(Lang lang);
    }
}