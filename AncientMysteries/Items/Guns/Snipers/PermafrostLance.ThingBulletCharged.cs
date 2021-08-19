namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBulletCharged : AMThingBulletLinar
    {
        public bool blasted;

        public PermafrostLance_ThingBulletCharged(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 1750, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBulletCharged);
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Update()
        {
            base.Update();
            BlastCamera();
        }

        public override void BulletOnHit(MaterialThing thing, ref bool willStop)
        {
            base.BulletOnHit(thing, ref willStop);
            if (willStop)
            {
                Blast(BlastHitDir.Random360);
                blasted = true;
            }
        }

        public override void Removed()
        {
            SFX.PlaySynchronized("laserBlast", 1, 1);
            base.Removed();
        }

        public void BlastCamera()
        {

            if (x < Level.current.camera.left)
            {
                Blast(BlastHitDir.HitLeft);
                blasted = true;
            }
            else if (x > Level.current.camera.right)
            {
                Blast(BlastHitDir.HitRight);
                blasted = true;
            }
            else if (y > Level.current.camera.bottom)
            {
                Blast(BlastHitDir.HitBottom);
                blasted = true;
            }
            else if (y < Level.current.camera.top)
            {
                Blast(BlastHitDir.HitTop);
                blasted = true;
            }
        }

        public void Blast(BlastHitDir hitDir)
        {
            if (blasted || !isServerForObject) return;
#if !DEBUG
            const
#endif
            float bulletSpeed = 3f;
#if !DEBUG
            const
#endif
            float speedVariation = 1f;
#if !DEBUG
            const
#endif
            float accuracy = 0.9f;

            float baseDeg = hitDir switch
            {
                BlastHitDir.HitLeft => 0,
                BlastHitDir.HitTop => 90,
                BlastHitDir.HitRight => 180,
                BlastHitDir.HitBottom => 270,
                BlastHitDir.Random360 => 0,
                _ => throw new Exception(),
            };
            for (int i = 0; i < 10; i++)
            {
                PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(baseDeg, bulletSpeed, speedVariation, hitDir == BlastHitDir.Random360 ? 0 : accuracy), BulletSafeDuck);
                Level.Add(b);
                Level.Remove(this);
            }
        }

        public enum BlastHitDir
        {
            HitLeft,
            HitTop,
            HitRight,
            HitBottom,
            Random360,
        }
    }
}