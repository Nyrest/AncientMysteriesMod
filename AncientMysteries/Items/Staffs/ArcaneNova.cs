﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_ArcaneNova)]
    [MetaInfo(Lang.Default, "Arcane Nova", "A staff fulfilled with mysteries from the universe.")]
    [MetaInfo(Lang.schinese, "奥术新星", "一把充满了宇宙奥秘的法杖。")]
    [MetaType(MetaType.Magic)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class ArcaneNova : AMStaff
    {
        public ArcaneNova(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            this.ReadyToRun(tex_Staff_ArcaneNova);
            SetBox(14, 38);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.006f;
            _fireWait = 0.5f;
            _holdOffset = new(-5, -6);
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                var bullet = new ArcaneNova_Magic_Stage2(firePos, GetBulletVecDeg(owner.offDir == 1 ? 0 : 180, 7.5f), duck);
                Level.Add(bullet);
                SFX.PlaySynchronized("laserBlast", 5, -0.2f);
            }
        }
    }
}