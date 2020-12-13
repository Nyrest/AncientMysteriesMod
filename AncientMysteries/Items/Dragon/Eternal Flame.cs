using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using DuckGame;
using System.Collections.Generic;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Dragon.Melee
{
    [EditorGroup(g_melees)]
    public sealed class EternalFlame : AMMelee
    {
        public float cooldown = 0;
        public float cooldown2 = 0;
        public float cooldown3 = 0;
        public bool _quacked;

        public EternalFlame(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunMap("eF.png", 9, 25);
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
                if ( _quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                {
                    if (this.owner._offDir == -1)
                    {
                        ExplosionPart ins = new ExplosionPart(owner.x, owner.y, true);
                        ins.xscale *= 0.7f;
                        ins.yscale *= 0.7f;
                        Level.Add(ins);
                        SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
                        Thing bulletOwner = this.owner;
                        IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(owner.position, 14f);
                        foreach (MaterialThing t2 in things)
                        {
                            if (t2 != bulletOwner)
                            {
                                t2.Destroy(new DTShot(new Bullet_FB(owner.x, owner.y, new AT_FB(), -1, this.owner, false, 1)));
                            }
                        }
                        owner.hSpeed += -700;
                        cooldown2 = -15;
                    }
                    else
                    {
                        ExplosionPart ins = new ExplosionPart(owner.x, owner.y, true);
                        ins.xscale *= 0.7f;
                        ins.yscale *= 0.7f;
                        Level.Add(ins);
                        SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
                        Thing bulletOwner = this.owner;
                        IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(owner.position, 14f);
                        foreach (MaterialThing t2 in things)
                        {
                            if (t2 != bulletOwner)
                            {
                                t2.Destroy(new DTShot(new Bullet_FB(owner.x, owner.y, new AT_FB(), -1, this.owner, false, 1)));
                            }
                        }
                        owner.hSpeed += 700;
                        cooldown2 = -15;
                    }
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
                if (base.duck.offDir != 1)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Bullet b = new Bullet_FB(base.duck.x, base.duck.y, new AT_FB(), -180 + Rando.Float(-5, 5), this.owner, false, 250f + Rando.Float(-75, 75));
                        firedBullets.Add(b);
                        Level.Add(b);
                        cooldown3 = -10;
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Bullet b = new Bullet_FB(base.duck.x, base.duck.y, new AT_FB(), 0 + Rando.Float(-5, 5), this.owner, false, 250f + Rando.Float(-75, 75));
                        firedBullets.Add(b);
                        Level.Add(b);
                        cooldown3 = -10;
                    }
                }
            }
        }
    }
}
