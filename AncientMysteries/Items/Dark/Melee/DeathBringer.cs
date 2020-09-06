using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Dark.Melee
{
    [EditorGroup(topAndSeries + "Dark|Melee")]
    public sealed class DeathBringer : AMMelee
    {
        public float cooldown = -2;

        public DeathBringer(float xval, float yval)
            : base(xval, yval)
        {
            this.ReadyToRunMap("DeathBringer.png", 26, 32);
            _pitchOffset = -0.7f;
        }

        public override void Update()
        {
            base.Update();
            if (cooldown < 0)
            {
                cooldown += 0.1f;
            }
            else cooldown = 0;
        }

        public override void OnPressAction()
        {
            AT_Skull type = new AT_Skull();
            base.OnPressAction();
            if (duck != null && cooldown == 0)
            {
                cooldown = -2;
                if ((_crouchStance && _jabStance && !_swinging) || (!_crouchStance && !_swinging && _swing < 0.1f))
                {
                    if (base.duck.offDir != 1)
                    {
                        Level.Add(new Bullet_Skull(base.duck.x, base.duck.y, type, -180, true, base.duck, rbound: false, 300f));
                    }
                    else
                    {
                        Level.Add(new Bullet_Skull(base.duck.x, base.duck.y, type, 0, false, base.duck, rbound: false, 300f));
                    }
                }
            }
        }
    }
}
