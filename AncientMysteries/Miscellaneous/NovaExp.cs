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
    public sealed class NovaExp : Thing
    {
        public SpriteMap _sprite;
        public bool _smoked;
        public int _smokeFrame;
        public float _wait;
        public NovaExp(float xpos, float ypos, bool doWait = true) : base(xpos, ypos)
        {
            _sprite = this.ReadyToRunMap("crystalExplosion.png", 36, 32);
            this._sprite.AddAnimation("loop", 1f, false, new int[]
            {
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15,
        16,
        17,
            });
            this._sprite.SetAnimation("loop");
            this.graphic = this._sprite;
            this._sprite.speed = 0.4f + Rando.Float(0.2f);
            base.xscale = 0.5f + Rando.Float(0.5f);
            base.yscale = base.xscale;
            this.center = new Vec2(32f, 32f);
            this._wait = Rando.Float(1f);
            this._smokeFrame = Rando.Int(1, 3);
            base.depth = 1f;
            this.vSpeed = Rando.Float(-0.2f, -0.4f);
            if (!doWait)
            {
                this._wait = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
