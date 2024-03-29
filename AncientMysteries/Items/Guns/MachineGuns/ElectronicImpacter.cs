﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Rifles)]
    [MetaImage(tex_Gun_ElectronicImpacter)]
    [MetaInfo(Lang.Default, "Electronic Impacter", "But, why is it green?")]
    [MetaInfo(Lang.schinese, "雷电撞击", "但是，它为什么是绿色的呢？")]
    [MetaType(MetaType.Gun)]
    public sealed partial class ElectronicImpacter : AMGun
    {
        public ElectronicImpacter(float xval, float yval) : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new ElectronicImpacter_AmmoType();
            _type = "gun";
            this.ReadyToRunWithFrames(tex_Gun_ElectronicImpacter);
            _flare.color = Color.Transparent;
            BarrelSmokeFuckOff();
            _fireRumble = RumbleIntensity.Kick;
            _barrelOffsetTL = new Vec2(35f, 10f);
            _fireSound = "laserRifle";
            _fireSoundPitch = 0.9f;
            _fireWait = 0.9f;
            _kickForce = 0f;
            _fullAuto = true;
            _bulletColor = Color.Lime;
        }
    }
}