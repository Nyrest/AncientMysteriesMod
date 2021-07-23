namespace AncientMysteries.Items
{
    public abstract class AMDecoration : AMThing, IAMLocalizable
    {
        protected AMDecoration(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public virtual string GetLocalizedDescription(Lang lang) => string.Empty;

        public virtual string GetLocalizedName(Lang lang) => string.Empty;
    }
}