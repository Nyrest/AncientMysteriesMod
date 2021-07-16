using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Guns.MachineGuns
{
    public class ElectronicImpacter_AmmoType : AMAmmoType
    {
        public ElectronicImpacter_AmmoType()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 10f;
            rangeVariation = 20f;
            bulletThickness = 2f;
            bulletColor = Color.Lime;
            bulletSpeed = 40f;
        }
    }
}
