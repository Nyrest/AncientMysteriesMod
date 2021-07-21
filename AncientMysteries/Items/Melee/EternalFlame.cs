namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Melees)]
    [MetaImage(tex_Melee_EternalFlame)]
    [MetaInfo(Lang.english, "Eternal Flame", "Make your enemies burn into charred bones in the blink of an eye")]
    [MetaInfo(Lang.schinese, "永恒烈焰", "让你的敌人在顷刻间化为焦黑的尸骨")]
    [MetaType(MetaType.Melee)]
    public sealed partial class EternalFlame : AMMelee
    {
        public float cooldown = 0;
        public float cooldown2 = 0;
        public float cooldown3 = 0;
        public bool _quacked;

        public EternalFlame(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(tex_Melee_EternalFlame, 9, 25);
        }

        public override void Update()
        {
            base.Update();
            if (cooldown < 0)
            {
                cooldown += 0.1f;
            }
            else cooldown = 0;
            if (cooldown2 < 0)
            {
                cooldown2 += 0.1f;
            }
            else cooldown2 = 0;
            if (cooldown3 < 0)
            {
                cooldown3 += 0.1f;
            }
            else cooldown3 = 0;
            if (duck != null && cooldown2 == 0)
            {
                if (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                {
                    ExplosionPart ins = new(owner.x, owner.y, true);
                    ins.xscale *= 0.7f;
                    ins.yscale *= 0.7f;
                    Level.Add(ins);
                    SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
                    Thing bulletOwner = owner;
                    IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(owner.position, 14f);
                    foreach (MaterialThing t2 in things)
                    {
                        if (t2 != bulletOwner)
                        {
                            t2.Destroy(new DTIncinerate(this));
                        }
                    }
                    owner.hSpeed += 700 * owner._offDir;
                    cooldown2 = -15;
                }
            }
            else
            {
                _quacked = false;
            }
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if (duck != null && cooldown3 == 0)
            {
                var firedBullets = new List<Bullet>(10);
                if (duck.offDir != 1)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Bullet b = Make.Bullet<EternalFlame_AmmoType>(duck.position, owner, -180 + Rando.Float(-5, 5), this);
                        firedBullets.Add(b);
                        Level.Add(b);
                        cooldown3 = -10;
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Bullet b = Make.Bullet<EternalFlame_AmmoType>(duck.position, owner, 0 + Rando.Float(-5, 5), this);
                        firedBullets.Add(b);
                        Level.Add(b);
                        cooldown3 = -10;
                    }
                }
            }
        }
    }
}