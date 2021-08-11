namespace AncientMysteries.Utilities
{
    public static class GraphicsExtensions
    {
        private const float DefaultWidth = 17f;
        private const float DefaultHeight = 4.5f;
        private const float DefaultBorderWidth = 1f;

        public static void DrawProgressBar(this Vec2 pos, Rectangle rect, float progress, Color back, Color fore, Color border, float borderWidth = 2f)
        {
            Graphics.DrawRect(rect, border);
            var innerRect = new Rectangle(rect.x + borderWidth, rect.y + borderWidth, rect.width - borderWidth * 2, rect.height - borderWidth * 2);
            Graphics.DrawRect(innerRect, back);
            innerRect.width *= progress;
            Graphics.DrawRect(innerRect, fore);
        }

        public static void DrawProgressBarTop(
            this Thing thing,
            float progress,
            Color back,
            Color fore,
            Color border,
            float width = DefaultWidth,
            float height = DefaultHeight,
            float borderWidth = DefaultBorderWidth,
            float yOffset = 1f)
        {
            var center = thing.collisionCenter;
            Rectangle rect = new Rectangle(center.x - width / 2, thing.top - height - yOffset, width, height);
            DrawProgressBar(new Vec2(thing.collisionCenter.x, thing.bottom), rect, progress, back, fore, border, borderWidth);
        }

        public static void DrawProgressBarBottom(
            this Thing thing,
            float progress,
            Color back,
            Color fore,
            Color border,
            float width = DefaultWidth,
            float height = DefaultHeight,
            float borderWidth = DefaultBorderWidth,
            float yOffset = 1f)
        {
            var center = thing.collisionCenter;
            Rectangle rect = new Rectangle(center.x - width / 2, thing.bottom + height + yOffset, width, height);
            DrawProgressBar(new Vec2(thing.collisionCenter.x, thing.bottom), rect, progress, back, fore, border, borderWidth);
        }
    }
}