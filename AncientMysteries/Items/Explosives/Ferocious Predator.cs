using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Explosives
{
    [EditorGroup(g_explosives)]
    public sealed class FerociousPredator : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Ferocious Predator",
        };

        public StateBinding _fireAngleBinding = new StateBinding("_fireAngle");

        public StateBinding _aimAngleBinding = new StateBinding("_aimAngle");

        public StateBinding _aimAngleShakeBinding = new StateBinding("_aimAngle");

        public StateBinding _aimWaitBinding = new StateBinding("_aimWait");

        public StateBinding _aimingBinding = new StateBinding("_aiming");

        public StateBinding _cooldownBinding = new StateBinding("_cooldown");

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
                return base.angle + _aimAngle +
                    (_fireAngle == 0 ? 0 :
                    Maths.DegToRad(Rando.Float(-1, 1) * (90 / _fireAngle)) * offDir);
            }
            set
            {
                _angle = value;
            }
        }

        public FerociousPredator(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 6;
            _type = "gun";
            this.ReadyToRunMap("ferociousPredator.png", 32, 16);
            _barrelOffsetTL = new Vec2(32f, 7f);
            _fireSound = "pistol";
            _kickForce = 3f;
            _holdOffset = new Vec2(4f, 0f);
            _ammoType = new AT_FerociousPredator();
            _fireSound = "deepMachineGun";
            _bulletColor = Color.White;
        }

        public override void Update()
        {
            base.Update();
            if (_aiming && _aimWait <= 0f && _fireAngle < 90f)
            {
                _fireAngle += _fireAngle < 30 ? 1 : ((90 / _fireAngle) * 1f);
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
