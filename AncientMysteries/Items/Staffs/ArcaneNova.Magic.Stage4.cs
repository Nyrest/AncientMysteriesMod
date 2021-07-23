namespace AncientMysteries.Items
{
    public class ArcaneNova_Magic_Stage4 : AMThingBulletLinar
    {
        public ArcaneNova_Magic_Stage4(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 80, float.PositiveInfinity, initSpeed, safeDuck)
        {
            this.ReadyToRunWithFrames(tex_Bullet_Nova4, 14, 6, true);
            BulletTailColor = Color.MediumPurple;
        }
    }
}