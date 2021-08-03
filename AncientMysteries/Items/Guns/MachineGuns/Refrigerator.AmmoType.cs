using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class Refrigerator_AmmoType : AMAmmoType
    {
        public Refrigerator_AmmoType()
        {
            range = 500;
            rangeVariation = 30;
            bulletSpeed = 23;
            speedVariation = 2;
            sprite = tex_Bullet_Refrigerator.ModSprite();
            HideTail();
        }
    }
}
