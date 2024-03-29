﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools)]
    [MetaImage(tex_Block_Barrier)]
    [MetaInfo(Lang.Default, "Barrier", "Blocks everything.")]
    [MetaInfo(Lang.schinese, "屏障", "简单来说就是空气墙。")]
    [MetaType(MetaType.MapTools)]
    public sealed partial class Barrier : AMMapToolBlock
    {
        public Barrier(float x, float y) : base(x, y)
        {
            this.ReadyToRun(tex_Block_Barrier);
            _canFlip = false;
            _visibleInGame = false;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet != null)
            {
                if (bullet != null)
                {
                    bullet.hitArmor = true;
                }
                bullet.OnCollide(hitPos, this, willBeStopped: true);
                ExitHit(bullet, hitPos);
                Level.Remove(bullet);
            }
            return false;
        }
    }
}