using AncientMysteries.Particles;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Skull : AmmoType
    {
        public SpriteMap _spriteMap;

        public AT_Skull()
        {
            accuracy = 0.9f;
            range = 5000f;
            penetration = 1f;
            bulletSpeed = 3f;
            bulletLength = 0;
            sprite = _spriteMap = TexHelper.ModSpriteMap("Skull.png", 25, 14, true);
            _spriteMap.AddAnimation("loop", 0.3f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            bulletColor = Color.Transparent;
            if (owner._offDir == -1)
            {
                sprite = _spriteMap = TexHelper.ModSpriteMap("Skull.png", 25, 14, true);
            }
            if (owner._offDir == 1)
            {
                sprite = _spriteMap = TexHelper.ModSpriteMap("SkullR.png", 25, 14, true);
            }
            return base.FireBullet(position, owner, angle, firedFrom);
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            if (!b.isLocal)
            {
                return;
            }
            if (destroyed)
            {
                ATMissileShrapnel aTMissileShrapnel = new ATMissileShrapnel();
                aTMissileShrapnel.MakeNetEffect(b.position);
                Random generator = null;
                if (Network.isActive && b.isLocal)
                {
                    generator = Rando.generator;
                    Rando.generator = new Random(NetRand.currentSeed);
                }
                List<Bullet> list = new List<Bullet>();
                for (int i = 0; i < 12; i++)
                {
                    float num = (float)i * 30f + Rando.Float(10f);
                    aTMissileShrapnel = new ATMissileShrapnel();
                    aTMissileShrapnel.range = 5f + Rando.Float(5f);
                    Vec2 value = new Vec2((float)Math.Cos(Maths.DegToRad(num)), (float)Math.Sin(Maths.DegToRad(num)));
                    Bullet bullet = new Bullet(b.x + value.x * 8f, b.y - value.y * 8f, aTMissileShrapnel, num);
                    bullet.firedFrom = b;
                    list.Add(bullet);
                    Level.Add(bullet);
                    Level.Add(CustomSpark.New(b.x + Rando.Float(-8f, 8f), b.y + Rando.Float(-8f, 8f), value + new Vec2(Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f)), Color.DarkGreen));
                    var smoke = SmallSmoke.New(b.x + value.x * 8f + Rando.Float(-8f, 8f), b.y + value.y * 8f + Rando.Float(-8f, 8f));
                    Level.Add(smoke);
                    smoke.sprite.color *= 0.8f;
                }
                if (Network.isActive && b.isLocal)
                {
                    NMFireGun msg = new NMFireGun(null, list, 0, rel: false, 4);
                    Send.Message(msg, NetMessagePriority.ReliableOrdered);
                    list.Clear();
                }
                if (Network.isActive && b.isLocal)
                {
                    Rando.generator = generator;
                }
                foreach (Window item in Level.CheckCircleAll<Window>(b.position, 15f))
                {
                    if (Network.isActive && b.isLocal)
                    {
                        Thing.Fondle(item, DuckNetwork.localConnection);
                    }
                    if (Level.CheckLine<Block>(b.position, item.position, item) == null)
                    {
                        item.Destroy(new DTImpact(b));
                    }
                }
                foreach (PhysicsObject item2 in Level.CheckCircleAll<PhysicsObject>(b.position, 30f))
                {
                    if (Network.isActive && b.isLocal && b.owner == null)
                    {
                        Thing.Fondle(item2, DuckNetwork.localConnection);
                    }
                    if ((item2.position - b.position).length < 15f)
                    {
                        item2.Destroy(new DTImpact(b));
                    }
                    item2.sleeping = false;
                    item2.vSpeed -= 1f;
                }
            }
            base.OnHit(destroyed, b);
        }
    }
}
