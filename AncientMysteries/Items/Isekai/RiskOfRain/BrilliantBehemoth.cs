using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Isekai.RiskOfRain
{
    [EditorGroup(e_isekai_ror)]
    public class BrilliantBehemoth : RoREquipmentBase
    {
        public BrilliantBehemoth(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = this.ModSpriteMap("hatHatty.png", 32, 32, true);
        }

        public override void Update()
        {
            base.Update();
            if (duck is Duck d)
                foreach (Bullet b in Hooks.removedThings.Where(x => x is Bullet))
                {
                    if (!b.isLocal || b.owner != d)
                    {
                        continue;
                    }
                    var pos = b.travelEnd;
                    ATMissileShrapnel shrap = new ATMissileShrapnel();
                    shrap.MakeNetEffect(b.position);
                    Random rand = null;
                    if (Network.isActive && b.isLocal)
                    {
                        rand = Rando.generator;
                        Rando.generator = new Random(NetRand.currentSeed);
                    }
                    List<Bullet> firedBullets = new List<Bullet>();
                    for (int i = 0; i < 12; i++)
                    {
                        float dir = (float)i * 30f - 10f + Rando.Float(20f);
                        shrap = new ATMissileShrapnel();
                        shrap.range = 15f + Rando.Float(5f);
                        Vec2 shrapDir = new Vec2((float)Math.Cos(Maths.DegToRad(dir)), (float)Math.Sin(Maths.DegToRad(dir)));
                        Bullet bullet = new Bullet(b.x + shrapDir.x * 8f, b.y - shrapDir.y * 8f, shrap, dir);
                        bullet.firedFrom = b;
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                        Level.Add(Spark.New(b.x + Rando.Float(-8f, 8f), b.y + Rando.Float(-8f, 8f), shrapDir + new Vec2(Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f))));
                        Level.Add(SmallSmoke.New(b.x + shrapDir.x * 8f + Rando.Float(-8f, 8f), b.y + shrapDir.y * 8f + Rando.Float(-8f, 8f)));
                    }
                    if (Network.isActive && b.isLocal)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 0, rel: false, 4);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                    if (Network.isActive && b.isLocal)
                    {
                        Rando.generator = rand;
                    }
                    IEnumerable<Window> windows = Level.CheckCircleAll<Window>(b.position, 30f);
                    foreach (Window w in windows)
                    {
                        if (b.isLocal)
                        {
                            Thing.Fondle(w, DuckNetwork.localConnection);
                        }
                        if (Level.CheckLine<Block>(b.position, w.position, w) == null)
                        {
                            w.Destroy(new DTImpact(b));
                        }
                    }
                    IEnumerable<PhysicsObject> phys = Level.CheckCircleAll<PhysicsObject>(b.position, 70f);
                    foreach (PhysicsObject p in phys)
                    {
                        if (b.isLocal && b.owner == null)
                        {
                            Thing.Fondle(p, DuckNetwork.localConnection);
                        }
                        if ((p.position - b.position).length < 30f)
                        {
                            p.Destroy(new DTImpact(b));
                        }
                        p.sleeping = false;
                        p.vSpeed = -2f;
                    }
                    HashSet<ushort> idx = new HashSet<ushort>();
                    IEnumerable<BlockGroup> blokzGroup = Level.CheckCircleAll<BlockGroup>(b.position, 50f);
                    foreach (BlockGroup block2 in blokzGroup)
                    {
                        if (block2 == null)
                        {
                            continue;
                        }
                        BlockGroup group = block2;
                        new List<Block>();
                        foreach (Block bl in group.blocks)
                        {
                            if (Collision.Circle(b.position, 28f, bl.rectangle))
                            {
                                bl.shouldWreck = true;
                                if (bl is AutoBlock)
                                {
                                    idx.Add((bl as AutoBlock).blockIndex);
                                }
                            }
                        }
                        group.Wreck();
                    }
                    IEnumerable<Block> blokz = Level.CheckCircleAll<Block>(b.position, 28f);
                    foreach (Block block in blokz)
                    {
                        if (block is AutoBlock)
                        {
                            block.skipWreck = true;
                            block.shouldWreck = true;
                            if (block is AutoBlock)
                            {
                                idx.Add((block as AutoBlock).blockIndex);
                            }
                        }
                        else if (block is Door || block is VerticalDoor)
                        {
                            Level.Remove(block);
                            block.Destroy(new DTRocketExplosion(null));
                        }
                    }
                    if (Network.isActive && b.isLocal && idx.Count > 0)
                    {
                        Send.Message(new NMDestroyBlocks(idx));
                    }
                }
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Brilliant Behemoth",
        };
    }
}
