using DuckGame;

namespace AncientMysteries
{
    public static class GTool
    {
        public static readonly BitmapFont _biosFont = new BitmapFont("biosFont", 8);

        public static void DrawTopText(this Thing thing, string text, Color color, float yOffset, InputProfile inputProfile = null)
        {
            var input = inputProfile ?? (thing as Duck)?.inputProfile;
            float fontWidth = _biosFont.GetWidth(text, false, input);
            _biosFont.Draw(text, new Vec2(thing.position.x - fontWidth / 2, thing.top - 12 + yOffset), color, 1, input);
        }
    }
}
