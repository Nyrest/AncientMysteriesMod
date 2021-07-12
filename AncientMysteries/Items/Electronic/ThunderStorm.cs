using System.Collections.Generic;

namespace AncientMysteries.Items.Electronic
{
    [EditorGroup(g_staffs)]
    public sealed class Thunderstorm : AMStaff
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Thunderstorm",
        };

        public StateBinding _animationFrameBinding = new StateBinding(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public int r;
        public Thunderstorm(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AT_CubicBlast();
            _spriteMap = this.ReadyToRunMap(t_ThunderStorm, 13, 36);
            _barrelOffsetTL = new Vec2(6, 3);
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
            r = Rando.Int(3, 5);
            int count = _castTime >= 0.95f ? r : 1;
            float speed = _castTime >= 0.95f ? 4 : 1.5f;
            if (_castTime >= 0.95f)
            {
                SFX.Play("sniper", 0.9f, -0.4f);
            }
            else
            {
                SFX.Play("shotgunFire2", 0.7f, 0.9f);
            }
            var firedBullets = new List<Bullet>(count);
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
