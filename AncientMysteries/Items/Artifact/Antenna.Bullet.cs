namespace AncientMysteries.Items
{
    public class AntennaBullet : AMThingBulletLinar
    {
        public AntennaBullet(Vec2 pos, Duck safeDuck, Vec2 pointAngle) : base(pos, 800, 2, Vec2.Zero, safeDuck)
        {
            alpha = 0f;
            graphic = this.ReadyToRun(tex_Bullet_Antenna);
            graphic.CenterOrigin();
            this.angle = CalcBulletAngleRadian(pointAngle);
        }

        public override void UpdateAngle()
        {
            // Angle is controlled by Antenna this time
            if (!IsMoving) return;
            base.UpdateAngle();
        }
    }
}