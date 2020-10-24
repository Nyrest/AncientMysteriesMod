using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using AncientMysteries.Localization.Enums;
using AncientMysteries.Utilities;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Miscellaneous
{
    public sealed class TempCrystal : Thing
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

        public StateBinding _progressBinding = new StateBinding(nameof(progress));

        public TempCrystal(float xpos, float ypos, bool doWait = true, Thing tOwner = null) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap("crystal.png", 17, 36);
            this._sprite.AddAnimation("loop", 0.2f, true, new int[]
            {
        0,
        1,
        2,
            });
            this._sprite.SetAnimation("loop");
            this.graphic = this._sprite;
            this._sprite.speed = 0.6f;
            base.xscale = 0.5f;
            base.yscale = base.xscale;
            this.center = new Vec2(8.5f, 18f);
            base.depth = 1f;
            t = tOwner;
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
                for (int i = 0; i < 2; i++)
                {
                    Bullet b1 = new Bullet_Laser(this.x + Rando.Float(-r, r), this.y - 200f + Rando.Float(-r/2,r/2), /*new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    bulletLength = 3,
                }*/new AT_Laser(), Rando.Float(-100f - Convert.ToSingle(r/3.5f), Convert.ToSingle(-80 + r/3.5f)), t, false, 400);
                    b1.color = Color.Yellow;
                    firedBullets.Add(b1);
                    Level.Add(b1);
                    ExplosionPart ins = new ExplosionPart(b1.travelStart.x, b1.travelStart.y, true);
                    ins.xscale *= 0.2f;
                    ins.yscale *= 0.2f;
                    Level.Add(ins);
                }
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new NMFireGun(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
                timer = 0;
                timer2++;
                r += 4f;
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
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
