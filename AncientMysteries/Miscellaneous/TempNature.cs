using System.Collections.Generic;

namespace AncientMysteries.Items.Miscellaneous
{
    public sealed class TempNature : PhysicsObject
    {
        public SpriteMap _sprite;
        public bool _smoked;
        public int _smokeFrame;
        public float _wait;
        public int timer = 0;
        public int timer2 = 0;
        public float fireAngle;
        public Thing t;
        public float progress = 0;
        public bool removing = false;
        public float r = 0;

        public StateBinding _progressBinding = new(nameof(progress));

        public TempNature(float xpos, float ypos, bool doWait = true, Thing tOwner = null) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap(t_Crystal, 17, 36);
            _sprite.AddAnimation("loop", 0.2f, true, new int[]
            {
        0,
        1,
        2,
            });
            _sprite.SetAnimation("loop");
            graphic = _sprite;
            _sprite.speed = 0.6f;
            xscale = 0.5f;
            yscale = xscale;
            center = new Vec2(8.5f, 18f);
            depth = 1f;
            t = tOwner;
            solid = false;
            if (!doWait)
            {
                _wait = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 5 && removing == false)
            {
                var firedBullets = new List<Bullet>(1);
                for (int i = 0; i < 2; i++)
                {
                    Bullet b1 = new Bullet_LaserG(x, y, /*new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    bulletLength = 3,
                }*/new AT_LaserG(), Rando.Float(100f + Convert.ToSingle(r / 3.5f), Convert.ToSingle(80 - r / 3.5f)), t, false, 400)
                    {
                        color = Color.Green
                    };
                    firedBullets.Add(b1);
                    Level.Add(b1);
                    ExplosionPart ins = new(b1.travelStart.x, b1.travelStart.y, true);
                    ins.xscale *= 0.2f;
                    ins.yscale *= 0.2f;
                    Level.Add(ins);
                }
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
                timer = 0;
                timer2++;
                r += 2f;
            }
            if (timer2 == 60 && removing == false)
            {
                removing = true;
                progress = 1;
            }
            if (removing == false)
            {
                progress += 0.04f;
            }
            else
            {
                progress -= 0.04f;
            }
            if (progress < 0f)
            {
                Removed();
            }
            alpha = progress;
            timer++;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
