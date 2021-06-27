namespace AncientMysteries.Utilities
{
    public static class ExtensionMethods
    {
        public static Color Add(this in Color color, in Color with, byte alpha = 255)
        {
            return new Color(color.r + with.r, color.g + with.g, color.b + with.b);
        }
    }
}
