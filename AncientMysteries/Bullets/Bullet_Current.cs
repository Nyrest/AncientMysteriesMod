using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace AncientMysteries.Bullets
{
    public class Bullet_Current : Bullet
    {
        public Texture2D _beem;

        protected float _thickness;

        public Bullet_Current(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false)
            : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = TexHelper.ModTex2D(tex_Bullet_LaserBeam);
        }

        public override void Draw()
        {
            if (_tracer || !(_bulletDistance > 0.1f))
            {
                return;
            }

            if (base.gravityAffected)
            {
                if (prev.Count < 1)
                {
                    return;
                }

                int num = (int)Math.Ceiling((drawdist - startpoint) / 8f);
                Vec2 p = prev.Last();
                for (int i = 0; i < num; i++)
                {
                    Vec2 pointOnArc = GetPointOnArc(i * 8);
                    Graphics.DrawTexturedLine(_beem, pointOnArc, p, color * (1f - (float)i / (float)num) * base.alpha, ammo.bulletThickness, 0.9f);
                    if (!(pointOnArc == prev.First()))
                    {
                        p = pointOnArc;
                        if (i == 0 && ammo.sprite != null && !doneTravelling)
                        {
                            ammo.sprite.depth = 1f;
                            ammo.sprite.angleDegrees = 0f - Maths.PointDirection(Vec2.Zero, travelDirNormalized);
                            Graphics.Draw(ammo.sprite, p.x, p.y);
                        }

                        continue;
                    }

                    break;
                }

                return;
            }

            float length = (drawStart - drawEnd).length;
            float num2 = 0f;
            float num3 = 1f / (length / 8f);
            float num4 = 0f;
            float num5 = 8f;
            while (true)
            {
                bool flag = false;
                if (num2 + num5 > length)
                {
                    num5 = length - Maths.Clamp(num2, 0f, 99f);
                    flag = true;
                }

                num4 += num3;
                Graphics.DrawTexturedLine(_beem, drawStart + travelDirNormalized * num2, drawStart + travelDirNormalized * (num2 + num5), Color.White * num4, _thickness, 0.6f);
                if (!flag)
                {
                    num2 += 8f;
                    continue;
                }

                break;
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            reboundBulletsCreated++;
            Bullet.isRebound = true;
            Bullet_Current laserBullet = new(pos.x, pos.y, ammo, dir, null, rebound, rng);
            Bullet.isRebound = false;
            laserBullet.firedFrom = base.firedFrom;
            laserBullet.lastReboundSource = lastReboundSource;
            laserBullet.isLocal = isLocal;
            laserBullet.connection = base.connection;
            reboundCalled = true;
            Level.current.AddThing(laserBullet);
            Level.current.AddThing(new LaserReboundYellow(pos.x, pos.y));
        }
    }
}