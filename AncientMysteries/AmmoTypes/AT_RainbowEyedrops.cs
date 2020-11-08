using AncientMysteries.Bullets;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_RainbowEyedrops : AmmoType
    {
        public AT_RainbowEyedrops()
        {
            accuracy = 0.93f;

            bulletSpeed = 9f;
            rangeVariation = 0f;
            speedVariation = 0.5f;
            range = 1500f;

            penetration = 2f;
            weight = 5f;

            bulletLength = 3000;
            this.affectedByGravity = true;

        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            var result = base.FireBullet(position, owner, angle, firedFrom);
            result.color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            return result;
        }
    }
}
