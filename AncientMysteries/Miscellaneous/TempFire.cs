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
    public sealed class TempFire : Thing
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
        public TempFire(float xpos, float ypos, bool doWait = true, Thing tOwner = null) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap("cross.png", 18, 25);
            this.graphic = this._sprite;
            this._sprite.speed = 0.6f;
            base.xscale = 0.5f;
            base.yscale = base.xscale;
            this.center = new Vec2(18f, 18f);
            base.depth = 1f;
            fireAngle = tOwner._offDir == 1 ? 0 : 180;
            t = tOwner;
            if (!doWait)
            {
                this._wait = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
            if (timer >= 30 && removing == false)
            {
                Bullet b = new Bullet_BigFB(this.x, this.y, new AT_BigFB(), fireAngle, t, false, 400);
                b.color = Color.Orange;
                Level.Add(b);
                timer = 0;
                timer2++;
            }
            if (timer2 == 8 && removing == false)
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
