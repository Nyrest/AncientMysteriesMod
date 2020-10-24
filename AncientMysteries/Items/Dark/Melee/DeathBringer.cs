using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using DuckGame;
using System.Collections.Generic;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Dark.Melee
{
    [EditorGroup(g_melees)]
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
                    var firedBullets = new List<Bullet>(1);
                    if (base.duck.offDir != 1)
                    {
                        Bullet b = new Bullet_Skull(base.duck.x, base.duck.y, type, -180, true, base.duck, rbound: false, 300f);
                        firedBullets.Add(b);
                        Level.Add(b);
                    }
                    else
                    {
                        Bullet b = new Bullet_Skull(base.duck.x, base.duck.y, type, -180, true, base.duck, rbound: false, 300f);
                        firedBullets.Add(b);
                        Level.Add(b);
                    }
                }
            }
        }
    }
}
