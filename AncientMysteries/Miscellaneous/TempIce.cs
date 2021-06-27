using System.Collections.Generic;

namespace AncientMysteries.Items.Miscellaneous
{
    public sealed class TempIce : PhysicsObject
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

        public StateBinding _progressBinding = new(nameof(progress));

        public TempIce(float xpos, float ypos, bool doWait = true, Thing tOwner = null) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap("icySpirit.png", 34, 34);
            this._sprite.AddAnimation("loop", 0.2f, true, new int[]
            {
        0,
        1,
        2,
        3,
            });
            this._sprite.SetAnimation("loop");
            this.graphic = this._sprite;
            this._sprite.speed = 0.6f;
            xscale = 0.5f;
            yscale = xscale;
            this.center = new Vec2(17f, 17f);
            depth = 1f;
            t = tOwner;
            solid = false;
            if (!doWait)
            {
                this._wait = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
            var firedBullets = new List<Bullet>(1);
            if (timer >= 5 && removing == false)
            {
                Bullet b1 = new Bullet_Icicle(this.x, this.y, new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    sprite = this.ModSprite("icicle.png"),
                    bulletLength = 0,
                }, fireAngle, t, false, 400);
                Bullet b2 = new Bullet_Icicle(this.x, this.y, new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    sprite = this.ModSprite("icicle.png"),
                    bulletLength = 0,
                }, fireAngle + 90, t, false, 400);
                Bullet b3 = new Bullet_Icicle(this.x, this.y, new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    sprite = this.ModSprite("icicle.png"),
                    bulletLength = 0,
                }, fireAngle + 180, t, false, 400);
                Bullet b4 = new Bullet_Icicle(this.x, this.y, new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    sprite = this.ModSprite("icicle.png"),
                    bulletLength = 0,
                }, fireAngle + 270, t, false, 400);
                firedBullets.Add(b1);
                firedBullets.Add(b2);
                firedBullets.Add(b3);
                firedBullets.Add(b4);
                Level.Add(b1);
                Level.Add(b2);
                Level.Add(b3);
                Level.Add(b4);
                SFX.Play("goody", 0.4f, Rando.Float(0.2f, 0.4f));
                timer = 0;
                timer2++;
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
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
                this.Removed();
            }
            this.alpha = progress;
            timer++;
            fireAngle = Rando.Float(0, 360);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
