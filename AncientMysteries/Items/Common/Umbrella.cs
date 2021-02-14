using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Common
{
    [EditorGroup(guns)]
    public class Umbrella : AMNotGun, IPlatform
    {
        //public StateBinding _openedBinding = new StateBinding("_opened");

        public bool _opened = false;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Umbrella",
        };

        public Umbrella(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunMap(Texs.Umbrella_closed);
            thickness = 1.5f;
            flammable = 1f;
            physicsMaterial = PhysicsMaterial.Rubber;
            _holdOffset = new Vec2(-4, -7);
        }

        public override void Update()
        {
            base.Update();
            if (duck is Duck d)
            {
                if (!_opened)
                {
                    _opened = true;
                    this.ReadyToRunMap(Texs.Umbrella_open);
                }
                this.handAngle = 0;
                if (duck.crouch || this._triggerHeld)
                {
                    this.handAngle = -1.56f * -this.offDir;
                    _holdOffset = new Vec2(-10 , -5);
                    handOffset = new Vec2(0, -4);
                }
                else
                {
                    _holdOffset = new Vec2(-4, -7);
                    handOffset = Vec2.Zero;
                }
            }
            else
            {
                if (_opened)
                {
                    _opened = false;
                    this.ReadyToRunMap(Texs.Umbrella_closed);
                }
            }
        }
    }
}
