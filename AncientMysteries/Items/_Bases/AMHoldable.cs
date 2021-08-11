namespace AncientMysteries.Items
{
    public abstract class AMHoldable : Holdable, IAMLocalizable
    {
        protected AMHoldable(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = GetLocalizedName(AMLocalization.Current);
            editorTooltip = GetLocalizedDescription(AMLocalization.Current);
        }

        /// <summary>
        /// Use this when collisionSize different with frame size
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SetBox(float w, float h)
        {
            collisionOffset = -(center = new Vec2(w / 2, h / 2));
            collisionSize = new Vec2(w, h);
        }

        public abstract string GetLocalizedDescription(Lang lang);

        public abstract string GetLocalizedName(Lang lang);
    }
}