namespace AncientMysteries.Items.Melee
{
    [EditorGroup(g_melees)]
    public sealed partial class DeathBringer : AMMelee
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "死亡收割者",
            _ => "Death Bringer",
        };
        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "用起来没有想象中的好",
            _ => "Looks cool, but not cool actually",
        };

        public float cooldown = -2;

        public DeathBringer(float xval, float yval)
            : base(xval, yval)
        {
            this.ReadyToRunWithFrames(t_Melee_DeathBringer, 26, 32);
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
            base.OnPressAction();
            /*
            AT_Skull type = new AT_Skull();
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
            */
        }
    }
}
