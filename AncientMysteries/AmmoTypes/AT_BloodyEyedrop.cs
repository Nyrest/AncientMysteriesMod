using AncientMysteries.Bullets;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_BloodyEyedrop : AmmoType
    {
        public AT_BloodyEyedrop()
        {
            accuracy = 0.93f;

            bulletSpeed = 9f;
            rangeVariation = 0f;
            speedVariation = 0f;
            range = 3000f;

            penetration = 2f;
            weight = 5f;

            immediatelyDeadly = true;
            deadly = false;

            bulletLength = 3000;
            this.affectedByGravity = true;
            combustable = true;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = HSL.FromHslFloat(Rando.Float(1), Rando.Float(0.1f, 0.9f), Rando.Float(0.45f, 0.65f));
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
