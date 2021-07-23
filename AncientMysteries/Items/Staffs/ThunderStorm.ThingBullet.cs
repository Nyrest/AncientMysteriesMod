namespace AncientMysteries.Items
{
    public class ThunderStorm_ThingBullet : AMThingBulletLinar
    {
        public Waiter fireWait = new(15);
        public SpriteMap _spriteMap;

        public ThunderStorm_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 600, 1, initSpeed, safeDuck)
        {
            _spriteMap = this.ReadyToRunWithFrames(tex_Bullet_CubicBlast, 8, 8, true);
            _spriteMap.AddAnimation("loop", 0.3f, true, 0, 1, 2, 3, 4);
            _spriteMap.SetAnimation("loop");
            graphic = _spriteMap;
        }

        public override void Update()
        {
            base.Update();
            if (fireWait.Tick())
            {
                foreach (Duck d in Level.CheckCircleAll<Duck>(position, 80))
                {
                    if (d != BulletSafeDuck)
                    {
                        NetHelper.NmFireGun(list =>
                        {
                            var bullet = Make.Bullet<AT_Current>(position, null, -Maths.PointDirection(position, d.position));
                            list.Add(bullet);
                            SFX.Play("deadTVLand");
                        });
                    }
                }
            }
            _spriteMap.angle += 0.02f;
        }

        public override void Removed()
        {
            base.Removed();
            foreach (Duck d in Level.current.things[typeof(Duck)])
            {
                if (d == BulletSafeDuck) return;

                if (d != BulletSafeDuck && d != null)
                {
                    NetHelper.NmFireGun(list =>
                    {
                        var bullet = Make.Bullet<AT_Current>(position, null, -Maths.PointDirection(position, d.position));
                        list.Add(bullet);
                        SFX.Play("deadTVLand");
                    });
                }
            }
        }
    }
}