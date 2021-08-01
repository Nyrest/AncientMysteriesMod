namespace AncientMysteries.Items
{
    public abstract class AMEquipmentWings : AMEquipment
    {
        public StateBinding isFlyingBinding = new(nameof(isFlying));

        public bool isFlying = false;

        public int timeFlied = 0;

        public SpriteMap _wingsSpriteMap = null!;

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
            if (this._equippedDuck is not Duck _equippedDuck) return;
            if (!_equippedDuck.grounded && _equippedDuck.InputDown(trigger_Jump))
            {
                isFlying = true;
            }
            else if (_equippedDuck.grounded)
            {
                isFlying = false;
            }

            _wingsSpriteMap.SetAnimation(isFlying ? "loop" : "idle");
            if (_equippedDuck is Duck equippedDuck)
            {
                timeFlied++;
                equippedDuck.vSpeed += -0.1f;

                if (isFlying)
                {
                    PhysicsObject propel = _equippedDuck;
                    if (equippedDuck._trapped != null)
                    {
                        propel = equippedDuck._trapped;
                    }
                    else if (equippedDuck.ragdoll?.part1 != null)
                    {
                        propel = equippedDuck.ragdoll.part1;
                    }
                    propel.velocity = GetFlyDir() * 3.5f;
                    equippedDuck.gravMultiplier = 0;
                }
                equippedDuck.gravMultiplier = 1;
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
                Vec2 dir = new(0f, 0f);
                if (_equippedDuck.InputDown(trigger_Left))
                {
                    dir.x -= 1f;
                }
                if (_equippedDuck.InputDown(trigger_Right))
                {
                    dir.x += 1f;
                }
                if (_equippedDuck.InputDown(trigger_Up) || _equippedDuck.InputDown(trigger_Jump))
                {
                    dir.y -= 1f;
                }
                if (_equippedDuck.InputDown(trigger_Down))
                {
                    dir.y += 1f;
                }
                return dir;
            }
        }
    }
}