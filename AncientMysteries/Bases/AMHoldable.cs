namespace AncientMysteries.Bases
{
    public abstract class AMHoldable : Holdable, IAMLocalizable
    {
        protected AMHoldable(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public abstract string GetLocalizedDescription(AMLang lang);
        public abstract string GetLocalizedName(AMLang lang);
    }
}
