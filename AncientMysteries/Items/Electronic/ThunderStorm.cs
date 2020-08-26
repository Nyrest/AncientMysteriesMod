using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Electronic
{
    [EditorGroup(topAndSeries + "Electronic")]
    public sealed class Thunderstorm : AMStaff
    {
        public StateBinding _animationFrameBinding = new StateBinding("animationFrame");

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }


        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "雷爆", // 
            _ => "Thunderstorm",
        };


        public Thunderstorm(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AT_CubicBlast();
            _spriteMap = this.ReadyToRunMap("ThunderStorm.png", 13, 36);
            this._barrelOffsetTL = new Vec2(6, 3);
            _spriteMap.AddAnimation("loop", 0.1f, true, 0, 1, 2);
            _spriteMap.SetAnimation("loop");
        }


        public float fuck = 0;
        public override void Update()
        {
            base.Update();
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            int count = _castTime >= 0.95f ? Rando.Int(7, 9) : 1;
            float speed = _castTime >= 0.95f ? 4 : 1.5f;
            if (_castTime >= 0.95f)
            {
                SFX.Play("sniper", 0.9f, -0.4f);
            }
            else
            {
                SFX.Play("shotgunFire2", 0.7f, 0.9f);
            }
            for (int i = 0; i < count; i++)
            {
                ammoType.bulletSpeed = speed;
                Bullet_CubicBlast bullet = new Bullet_CubicBlast(firePos.x, firePos.y, ammoType, offDir == 1 ? 0 : 180, this);
                bullet.color = ammoType.bulletColor;
                firedBullets.Add(bullet);
                Level.Add(bullet);
                bulletFireIndex += 1;
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new NMFireGun(this, firedBullets, bulletFireIndex, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
