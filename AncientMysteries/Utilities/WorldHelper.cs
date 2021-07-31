using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public static class WorldHelper
    {
        public static int DestroyBlocksRadius(Vec2 pPosition, float pRadius, Thing culprit, bool pExplode = false, bool destoryWindows = true, bool destoryPhyObjs = true, bool explodeMakeFire = false)
        {
            if (destoryWindows)
            {
                foreach (Window w in Level.CheckCircleAll<Window>(pPosition, pRadius - 20f))
                {
                    Thing.Fondle(w, DuckNetwork.localConnection);
                    if (Level.CheckLine<Block>(pPosition, w.position, w) == null)
                    {
                        w.Destroy(new DTImpact(culprit));
                    }
                }
            }
            if (destoryPhyObjs)
            {
                foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(pPosition, pRadius + 30f))
                {
                    if (p == culprit) continue;
                    if (culprit.isLocal && culprit.owner == null)
                    {
                        Thing.Fondle(p, DuckNetwork.localConnection);
                    }
                    if ((p.position - pPosition).length < 30f)
                    {
                        p.Destroy(new DTImpact(culprit));
                    }
                    p.sleeping = false;
                    p.vSpeed = -2f;
                }
            }
            int idd = 0;
            HashSet<ushort> idx = new HashSet<ushort>();
            foreach (BlockGroup block2 in Level.CheckCircleAll<BlockGroup>(pPosition, pRadius))
            {
                if (block2 == null)
                {
                    continue;
                }
                BlockGroup group = block2;
                new List<Block>();
                foreach (Block bl in group.blocks)
                {
                    if (!Collision.Circle(pPosition, pRadius - 22f, bl.rectangle))
                    {
                        continue;
                    }
                    bl.shouldWreck = true;
                    if (bl is AutoBlock && !(bl as AutoBlock).indestructable)
                    {
                        idx.Add((bl as AutoBlock).blockIndex);
                        if (pExplode && idd % 10 == 0)
                        {
                            Level.Add(new ExplosionPart(bl.x, bl.y));
                            if(explodeMakeFire)
                            {
                                Level.Add(SmallFire.New(bl.x, bl.y, Rando.Float(-2f, 2f), Rando.Float(-2f, 2f)));
                            }
                        }
                        idd++;
                    }
                }
                group.Wreck();
            }
            foreach (Block block in Level.CheckCircleAll<Block>(pPosition, pRadius - 22f))
            {
                if (block is AutoBlock && !(block as AutoBlock).indestructable)
                {
                    block.skipWreck = true;
                    block.shouldWreck = true;
                    idx.Add((block as AutoBlock).blockIndex);
                    if (pExplode)
                    {
                        if (idd % 10 == 0)
                        {
                            Level.Add(new ExplosionPart(block.x, block.y));
                            Level.Add(SmallFire.New(block.x, block.y, Rando.Float(-2f, 2f), Rando.Float(-2f, 2f)));
                        }
                        idd++;
                    }
                }
                else if (block is Door || block is VerticalDoor)
                {
                    Level.Remove(block);
                    block.Destroy(new DTRocketExplosion(null));
                }
            }
            if (Network.isActive && (culprit.isLocal || culprit.isServerForObject) && idx.Count > 0)
            {
                Send.Message(new NMDestroyBlocks(idx));
            }
            foreach (ILight item in Level.current.things[typeof(ILight)])
            {
                item.Refresh();
            }
            return idx.Count;
        }
    }
}
