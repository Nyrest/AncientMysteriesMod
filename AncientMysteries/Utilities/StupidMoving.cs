using System.Collections.Generic;

namespace AncientMysteries.Utilities
{
    public static class StupidMoving
    {
        public static void DuckMoveTo(Duck duck, Vec2 position, float maxSpeed = 3)
        {
            if (duck?.CanMove() == false && (duck.crouch || duck.sliding))
                return;

            if (duck.y - 2 > position.y && !duck.HasJumpModEquipment())
            {
                duck._jumpValid = 4;
                if (!duck.grounded && duck.crouch)
                {
                    duck.skipPlatFrames = 10;
                }
            }

            duck.hSpeed = MathHelper.Clamp((position - duck.position).x, -maxSpeed, maxSpeed);

            if (Level.CheckLine<Window>(duck.position, duck.position + new Vec2(20, 0) * duck.offDir) != null && Level.CheckLine<Block>(duck.position, duck.position + new Vec2(20, 5) * duck.offDir) == null)
            {
                duck.crouch = true;
                duck.sliding = true;
            }
        }

        public static void ThingMoveTo(Thing thing, Vec2 position, float speed = 3)
        {
            if (thing == null)
                return;
            if (thing.y < position.y && thing is MaterialThing materialThing)
            {
                IEnumerable<IPlatform> plats = Level.CheckLineAll<IPlatform>(thing.bottomLeft + new Vec2(1f, 1f), thing.bottomRight + new Vec2(-1f, 1f));
                if (plats.FirstOrDefault((IPlatform p) => p is Block) == null)
                {
                    foreach (IPlatform plat in plats)
                    {
                        if (plat is Block)
                        {
                            break;
                        }
                        if (plat is MaterialThing t)
                        {
                            materialThing.clip.Add(t);
                            /*
                            if (Level.CheckPoint<IPlatform>(
                                t.topLeft + new Vec2(-2f, 2f)) is MaterialThing left && left is Block)
                            {
                                materialThing.clip.Add(left);
                            }
                            if (Level.CheckPoint<IPlatform>(
                                t.topRight + new Vec2(2f, 2f)) is MaterialThing right && right is Block)
                            {
                                materialThing.clip.Add(right);
                            }
                             */
                            IPlatform left = Level.CheckPoint<IPlatform>(t.topLeft + new Vec2(-2f, 2f));
                            if (left != null && left is MaterialThing && !(left is Block))
                            {
                                materialThing.clip.Add(left as MaterialThing);
                            }
                            IPlatform right = Level.CheckPoint<IPlatform>(t.topRight + new Vec2(2f, 2f));
                            if (right != null && right is MaterialThing && !(right is Block))
                            {
                                materialThing.clip.Add(right as MaterialThing);
                            }
                            thing.vSpeed += 1;
                            thing.y += 2;
                            materialThing.grounded = false;
                        }
                    }
                }
            }
            Vec2 anglevec = Vec2.Clamp(new Vec2(position.x - thing.x, thing.y - position.y), new Vec2(-speed), new Vec2(speed));
            Vec2 tmp = Maths.AngleToVec((float)Math.Atan(anglevec.y / anglevec.x)) * speed;
            thing.hSpeed = anglevec.x < 0 ? -tmp.x : tmp.x;
            thing.vSpeed = anglevec.x < 0 ? -tmp.y : tmp.y;
            if (Level.CheckRect<Window>(thing.topLeft, thing.bottomRight) is Window window)
            {
                window.Destroy();
            }
        }

        public static void ThingMoveToVertically(Thing thing, Vec2 position, float speed = 3)
        {
            if (thing == null)
                return;
            if (thing.y < position.y && thing is MaterialThing materialThing)
            {
                IEnumerable<IPlatform> plats = Level.CheckLineAll<IPlatform>(thing.bottomLeft + new Vec2(1f, 1f), thing.bottomRight + new Vec2(-1f, 1f));
                if (plats.FirstOrDefault((IPlatform p) => p is Block) == null)
                {
                    foreach (IPlatform plat in plats)
                    {
                        if (plat is Block)
                        {
                            break;
                        }
                        if (plat is MaterialThing t)
                        {
                            materialThing.clip.Add(t);
                            /*
                            if (Level.CheckPoint<IPlatform>(
                                t.topLeft + new Vec2(-2f, 2f)) is MaterialThing left && left !is Block)
                            {
                                materialThing.clip.Add(left);
                            }
                            if (Level.CheckPoint<IPlatform>(
                                t.topRight + new Vec2(2f, 2f)) is MaterialThing right && right !is Block)
                            {
                                materialThing.clip.Add(right);
                            }
                             */
                            IPlatform left = Level.CheckPoint<IPlatform>(t.topLeft + new Vec2(-2f, 2f));
                            if (left != null && left is MaterialThing && !(left is Block))
                            {
                                materialThing.clip.Add(left as MaterialThing);
                            }
                            IPlatform right = Level.CheckPoint<IPlatform>(t.topRight + new Vec2(2f, 2f));
                            if (right != null && right is MaterialThing && !(right is Block))
                            {
                                materialThing.clip.Add(right as MaterialThing);
                            }
                            thing.vSpeed = 1;
                            thing.y += 2;
                            materialThing.grounded = false;
                        }
                    }
                }
            }
            Vec2 tmp = position - thing.position;
            thing.hSpeed = Maths.Clamp(tmp.x, -speed, speed);
            thing.vSpeed = Maths.Clamp(tmp.y, -speed, speed);
        }
    }
}
