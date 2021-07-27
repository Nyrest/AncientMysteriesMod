namespace AncientMysteries.Items
{
    public class PrimordialLibram_ThingBullet_Flower : AMThingBulletLinar
    {
        public PrimordialLibram_ThingBullet_Flower(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 300, 3, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_Flower);
            angleDegrees = Rando.Float(0, 360);
            BulletTail = false;
            alpha = 0;
        }

        public override void Removed()
        {
            base.Removed();
            if (isServerForObject)
            {
                for (int i = 0; i < 10; i++)
                {
                    PrimordialLibram_ThingBullet_Leaf bullet = new(position, GetBulletVecDeg(Rando.Float(0, 360), 4), BulletSafeDuck);
                    Level.Add(bullet);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            angle += 30;
            MathHelper.Min(alpha += 0.04f, 1);
        }
    }
}