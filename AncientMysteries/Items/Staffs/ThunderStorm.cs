﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_ThunderStorm, 13, 36)]
    [MetaInfo(Lang.Default, "Thunderstorm", "May the lightning drives away the darkness!")]
    [MetaInfo(Lang.schinese, "雷暴", "以雷霆击碎黑暗！")]
    [MetaType(MetaType.Magic)]
    public sealed partial class Thunderstorm : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public Thunderstorm(float xval, float yval) : base(xval, yval)
        {
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_ThunderStorm, 13, 36);
            _barrelOffsetTL = new Vec2(6, 3);
            _spriteMap.AddAnimation("loop", 0.1f, true, 0, 1, 2);
            _spriteMap.SetAnimation("loop");
            _castSpeed = 0.009f;
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            float speed = Rando.Float(2, 4);
            if (_castTime >= 0.95f)
            {
                SFX.Play("sniper", 0.9f, -0.4f);
                    ThunderStorm_ThingBullet bullet = new(
                        firePos,
                        GetBulletVecDeg(owner.FaceAngleDegressLeftOrRight() + Rando.Float(-10, 10), speed, 0.5f, 1f),
                        duck);
                    Level.Add(bullet);
            }
        }
    }
}