using System.Collections.Generic;

namespace AncientMysteries.Items.Explosives
{
    public class ViscousAcidLiquor_Bullet : Bullet
    {
        public ViscousAcidLiquor_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            if (!willBeStopped) return;
            SFX.PlaySynchronized("ignite",1,0.7f);
            for (int i = 0; i < 6; i++)
            {
                SmallSmoke smallSmoke = SmallSmoke.New(pos.x + Rando.Float(-5f, 5f), pos.y + Rando.Float(-5f, 5f), 0.8f, 4);
                smallSmoke.vSpeed = Rando.Float(0f, -0.5f);
                float num4 = smallSmoke.xscale = smallSmoke.yscale = Rando.Float(0.2f, 0.7f);
                Level.Add(smallSmoke);
            }
            DestroyRadius(pos, 40, this);

        }

        public static int DestroyRadius(Vec2 pPosition, float pRadius, Thing pBullet)
        {
            foreach (Window w in Level.CheckCircleAll<Window>(pPosition, pRadius - 20f))
            {
                Thing.Fondle(w, DuckNetwork.localConnection);
                if (Level.CheckLine<Block>(pPosition, w.position, w) == null)
                {
                    w.Destroy(new DTImpact(pBullet));
                }
            }
            foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(pPosition, pRadius + 30f))
            {
                if (pBullet.isLocal && pBullet.owner == null)
                {
                    Thing.Fondle(p, DuckNetwork.localConnection);
                }
                if ((p.position - pPosition).length < 30f)
                {
                    p.Destroy(new DTImpact(pBullet));
                }
                p.sleeping = false;
                p.vSpeed = -2f;
            }
            HashSet<ushort> idx = new();
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
                }
                else if (block is Door or VerticalDoor)
                {
                    Level.Remove(block);
                    block.Destroy(new DTRocketExplosion(null));
                }
            }
            if (Network.isActive && (pBullet.isLocal || pBullet.isServerForObject) && idx.Count > 0)
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
