namespace AncientMysteries
{
    public static class GTool
    {
        public static readonly BitmapFont _biosFont = new("biosFont", 8);

        public static void DrawTopText(this Thing thing, string text, Color color, float yOffset, InputProfile inputProfile = null)
        {
            var input = inputProfile ?? (thing as Duck)?.inputProfile;
            float fontWidth = _biosFont.GetWidth(text, false, input);
            _biosFont.Draw(text, new Vec2(thing.position.x - fontWidth / 2, thing.top - 12 + yOffset), color, 1, input);
        }

        public static void DrawTopProgressCenterTop(Vec2 position, float progress, Color bgColor, Color fillColor, Color border, float borderWidth, float yOffset, float width = 60, float height = 20, Depth depth = default)
        {
            Graphics.DrawRect(new Rectangle(position.x - width / 2, position.y + yOffset, width, height), bgColor, depth, true);
            Graphics.DrawRect(
                new Rectangle(position.x - width / 2, position.y + yOffset, width * Math.Min(progress, 1), height)
                , fillColor, depth + 1, true);
            Graphics.DrawRect(new Rectangle(position.x - width / 2, position.y + yOffset, width, height), border, depth + 2, false, borderWidth);
        }
    }
}
