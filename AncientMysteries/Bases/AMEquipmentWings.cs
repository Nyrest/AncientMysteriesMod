namespace AncientMysteries.Bases
{
    public abstract class AMEquipmentWings : AMEquipment
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public StateBinding isFlyingBinding = new(nameof(isFlying));

        public bool isFlying = false;

        public int timeFlied = 0;

        public SpriteMap _wingsSpriteMap;

        public byte AnimationFrame
        {
            get => (byte)_wingsSpriteMap._frame;
            set => _wingsSpriteMap._frame = value;
        }

        protected AMEquipmentWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _jumpMod = true;
        }

        public override void Update()
        {
            base.Update();
            _wingsSpriteMap.SetAnimation(isFlying ? "loop" : "idle");
            if (_equippedDuck is Duck equippedDuck)
            {
                timeFlied++;
                equippedDuck.vSpeed += -0.1f;
            }
            if (_equippedDuck is null || timeFlied > 300)
            {
                timeFlied = 0;
            }
        }

        public Vec2 GetFlyDir()
        {
            if (_equippedDuck.inputProfile.leftStick.length > 0.1f)
            {
                return new Vec2(_equippedDuck.inputProfile.leftStick.x, 0f - _equippedDuck.inputProfile.leftStick.y);
            }
            else
            {
                Vec2 dir = new Vec2(0f, 0f);
                if (_equippedDuck.inputProfile.Down("LEFT"))
                {
                    dir.x -= 1f;
                }
                if (_equippedDuck.inputProfile.Down("RIGHT"))
                {
                    dir.x += 1f;
                }
                if (_equippedDuck.inputProfile.Down("UP"))
                {
                    dir.y -= 1f;
                }
                if (_equippedDuck.inputProfile.Down("DOWN"))
                {
                    dir.y += 1f;
                }
                return dir;
            }
        }
    }
}