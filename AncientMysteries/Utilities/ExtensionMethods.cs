namespace AncientMysteries.Utilities
{
    public static class ExtensionMethods
    {
        public static Color Add(this in Color color, in Color with, byte alpha = 255)
        {
            return new Color(color.r + with.r, color.g + with.g, color.b + with.b);
        }

        public static int FaceAngleDegressLeftOrRight(this Thing thing)
        {
            if (thing.offDir == 1)
                return 0;
            else return 180;
        }
    }
}
