using DuckGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.AmmoTypes
{
    public class _ImplicitDefaultAmmoType : AmmoType
    {
        public static readonly _ImplicitDefaultAmmoType Instance = new();

#if DEBUG
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            Debugger.Break();
            return base.FireBullet(position, owner, angle, firedFrom);
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            Debugger.Break();
            base.OnHit(destroyed, b);
        }
#endif
    }
}
