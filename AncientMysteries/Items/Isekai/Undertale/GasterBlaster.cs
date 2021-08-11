namespace AncientMysteries.Items
{
    public partial class GasterBlaster : AMThing
    {
        public StateBinding positionBinding = new CompressedVec2Binding(nameof(position));
        public StateBinding angleBinding = new(nameof(angle));
        public StateBinding fireWaitBinding = new(nameof(fireWait));
        public Vec2 targetPosition;
        public int fireWait;
        private SpriteMap spriteMap;

        private const float Range = 1200;

        public GasterBlaster(float xpos, float ypos) : base(xpos, ypos)
        {
            fireWait = 60;
            spriteMap = this.ReadyToRunWithFrames(tex_Bullet_GasterBlaster, 44, 58);
        }

        public override void Update()
        {
            base.Update();
            fireWait--;
            spriteMap.frame = fireWait switch
            {
                < 5 => 3,
                < 30 => 2,
                < 40 => 1,
                _ => 0,
            };
        }

        public override void Draw()
        {
            base.Draw();
            if (fireWait <= 5)
            {
                Graphics.DrawLine(position, GetFireEnd(), Color.White, 20);
            }
        }

        public Vec2 GetFireEndOffset() => Maths.AngleToVec(angle) * Range;

        public Vec2 GetFireEnd() => position + GetFireEndOffset();
    }
}