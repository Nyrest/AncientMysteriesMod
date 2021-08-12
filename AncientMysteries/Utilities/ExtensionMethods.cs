namespace AncientMysteries.Utilities
{
    public static class ExtensionMethods
    {
        public static Color Add(this in Color color, in Color with, byte alpha = 255)
        {
            return new Color(color.r + with.r, color.g + with.g, color.b + with.b);
        }

        public static float FaceAngleDegressLeftOrRight(this Thing thing)
        {
            return thing.offDir == 1 ? 0 : 180;
        }

        /// <summary>
        /// Use this when collisionSize different with frame size
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void SetBox(this Thing thing, float w, float h)
        {
            thing.collisionOffset = -(thing.center = new Vec2(w / 2, h / 2));
            thing.collisionSize = new Vec2(w, h);
        }
    }
}