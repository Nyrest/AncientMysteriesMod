namespace AncientMysteries.Items.Staffs
{
    public class PrimordialLibram_ThingBullet_Leaf : AMThingBulletLinar
    {
        public PrimordialLibram_ThingBullet_Leaf(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 80, int.MaxValue, initSpeed, safeDuck)
        {
            this.ReadyToRun(t_Bullet_Leaf);
        }
    }
}
