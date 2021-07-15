namespace AncientMysteries.Items.Explosives
{
    [EditorGroup(g_explosives)]
    public sealed class ViscousAcidLiquor : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Viscous Acid Liquor",
        };

        public StateBinding _fireAngleBinding = new("_fireAngle");

        public StateBinding _aimAngleBinding = new("_aimAngle");

        public StateBinding _aimAngleShakeBinding = new("_aimAngle");

        public StateBinding _aimWaitBinding = new("_aimWait");

        public StateBinding _aimingBinding = new("_aiming");

        public StateBinding _cooldownBinding = new("_cooldown");

        public float _fireAngle;

        public float _aimAngle;

        public float _aimWait;

        public bool _aiming;

        public float _cooldown;

        public float _aimAngleShake;

        public override float angle
        {
            get
            {
                return base.angle + _aimAngle;
            }
            set
            {
                _angle = value;
            }
        }

        public ViscousAcidLiquor(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 15;
            _type = "gun";
            this.ReadyToRun(t_Gun_ViscousAcidLiquor);
            _barrelOffsetTL = new Vec2(29, 6);
            _fireSound = "pistol";
            _kickForce = 2f;
            _holdOffset = new Vec2(4f, 0f);
            _ammoType = new ViscousAcidLiquor_AmmoType();
            _fireSound = "deepMachineGun";
        }

        public override void Update()
        {
            base.Update();
            if (_aiming && _aimWait <= 0f && _fireAngle < 90f)
            {
                _fireAngle += _fireAngle < 30 ? 1 : (90 / _fireAngle * 1f);
            }
            if (_aimWait > 0f)
            {
                _aimWait -= 0.9f;
            }
            if (_cooldown > 0f)
            {
                _cooldown -= 0.1f;
            }
            else
            {
                _cooldown = 0f;
            }
            if (owner != null)
            {
                _aimAngle = 0f - Maths.DegToRad(_fireAngle);
                if (offDir < 0)
                {
                    _aimAngle = 0f - _aimAngle;
                }
            }
            else
            {
                _aimWait = 0f;
                _aiming = false;
                _aimAngle = 0f;
                _fireAngle = 0f;
            }
            if (_raised)
            {
                _aimAngle = 0f;
            }
        }

        public override void OnPressAction()
        {
            if (_cooldown == 0f)
            {
                if (ammo > 0)
                {
                    _aiming = true;
                    _aimWait = 1f;
                }
                else
                {
                    SFX.Play("click");
                }
            }
        }

        public override void OnReleaseAction()
        {
            if (_cooldown == 0f && ammo > 0)
            {
                _aiming = false;
                Fire();
                _cooldown = 1f;
                angle = 0f;
                _fireAngle = 0f;
            }
        }
    }
}
