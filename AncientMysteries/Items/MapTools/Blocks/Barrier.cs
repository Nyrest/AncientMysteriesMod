using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(grouptopGroupName)]
    public sealed class Barrier : AMBlock
    {
        public Barrier(float x, float y) : base(x, y)
        {
            this.ReadyToRun(tex_Block_Barrier);
            _canFlip = false;
            _visibleInGame = false;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            bullet.OnCollide(hitPos, this, willBeStopped: true);
            ExitHit(bullet, hitPos);
            bullet.Removed();
            return false;
        }
    }
}
