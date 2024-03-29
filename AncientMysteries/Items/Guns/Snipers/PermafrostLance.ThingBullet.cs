﻿namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBullet : AMThingBulletLinar
    {
        public PermafrostLance_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck, int powerLevel) : base(pos, 1750, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBullet);
            GravityEnabled = true;
            GravityMax = 0.15f;
            GravityIncrement = 0.07f;
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Removed()
        {
            PermafrostExplosion exp = new(x, y, false);
            Level.Add(exp);
            SFX.PlaySynchronized("glassBreak", 1, 1);
            base.Removed();
        }
    }
}