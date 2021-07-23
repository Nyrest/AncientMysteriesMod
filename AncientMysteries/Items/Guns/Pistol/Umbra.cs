using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Pistols)]
    [MetaImage(tex_Gun_Umbra)]
    [MetaInfo(Lang.english, "Umbra", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class Umbra : AMGun
    {
        public Umbra(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 410f,
                accuracy = 0.9f,
                penetration = 2.5f,
            };
            this.ReadyToRun(tex_Gun_Umbra);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _fireRumble = RumbleIntensity.None;
            _barrelOffsetTL = new Vec2(17, 5);
            _fullAuto = false;
            _fireRumble = RumbleIntensity.Kick;
            _fireSound = "laserRifle";
            _fireWait = 1.6f;
            _fireSoundPitch = -0.6f;
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}
