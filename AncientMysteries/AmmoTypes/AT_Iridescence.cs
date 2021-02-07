using AncientMysteries.Bullets;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Iridescence : AmmoType
    {
        public AT_Iridescence()
        {
            accuracy = 1f;
            range = 800f;
            penetration = 2f;
            rangeVariation = 10f;
            bulletLength = 3000;
            combustable = true;
            this.bulletType = typeof(AT_Iridescence_Bullet);
            bulletColor = Color.Red;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            var result = base.FireBullet(position, owner, angle, firedFrom);

            result.color = this.bulletColor = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            return result;
        }

        public class AT_Iridescence_Bullet : Bullet
        {
            public AT_Iridescence_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
            {
                color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            }
        }

        // TODO: FIX lost color after teleported
        // Reason: Stupid Duck Game is not allow to override AmmoType.GetBullet
    }
}
