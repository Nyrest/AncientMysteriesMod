using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;

namespace AncientMysteries.Items
{
    public abstract class AMHoldable : Holdable, IAMLocalizable
    {
        protected AMHoldable(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public abstract string GetLocalizedName(AMLang lang);
    }
}
