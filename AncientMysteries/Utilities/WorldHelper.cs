namespace AncientMysteries.Utilities
{
    public static class WorldHelper
    {
        public static int DestroyBlocksRadius(Vec2 pPosition, float pRadius, Thing culprit, bool pExplode = false, bool destroyWindows = true, bool destroyPhyObjs = true, bool explodeMakeFire = false)
        {
            if (destroyWindows)
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
            if (destroyPhyObjs)
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
                foreach (Block bl in group.blocks)
                {
                    if (!Collision.Circle(pPosition, pRadius - 22f, bl.rectangle))
                    {
                        continue;
                    }
                    bl.shouldWreck = true;
                    if (bl is AutoBlock {indestructable: false} block)
                    {
                        idx.Add(block.blockIndex);
                        if (pExplode && idd % 10 == 0)
                        {
                            Level.Add(new ExplosionPart(block.x, block.y));
                            if (explodeMakeFire)
                            {
                                Level.Add(SmallFire.New(block.x, block.y, Rando.Float(-2f, 2f), Rando.Float(-2f, 2f)));
                            }
                        }
                        idd++;
                    }
                }
                group.Wreck();
            }
            foreach (Block block in Level.CheckCircleAll<Block>(pPosition, pRadius - 22f))
            {
                if (block is AutoBlock {indestructable: false} autoBlock)
                {
                    autoBlock.skipWreck = true;
                    autoBlock.shouldWreck = true;
                    idx.Add(autoBlock.blockIndex);
                    if (pExplode)
                    {
                        if (idd % 10 == 0 && explodeMakeFire)
                        {
                            Level.Add(new ExplosionPart(autoBlock.x, autoBlock.y));
                            Level.Add(SmallFire.New(autoBlock.x, autoBlock.y, Rando.Float(-2f, 2f), Rando.Float(-2f, 2f)));
                        }
                        idd++;
                    }
                }
                else if (block is Door or VerticalDoor)
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