﻿using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AncientMysteries.Particles
{
    public sealed class CustomSpark : PhysicsParticle, IFactory
    {
        public static int kMaxSparks = 64;

        public static CustomSpark[] _sparks = new CustomSpark[kMaxSparks];

        public static int _lastActiveSpark = 0;

        public float _killSpeed = 0.03f;

        public StateBinding _colorBinding = new StateBinding("_color");

        public Color _color;

        public static CustomSpark New(float xpos, float ypos, Vec2 hitAngle, Color color, float killSpeed = 0.02f)
        {
            CustomSpark spark = null;
            if (_sparks[_lastActiveSpark] == null)
            {
                spark = new CustomSpark();
                _sparks[_lastActiveSpark] = spark;
            }
            else
            {
                spark = _sparks[_lastActiveSpark];
            }
            _lastActiveSpark = (_lastActiveSpark + 1) % kMaxSparks;
            spark.ResetProperties();
            spark.Init(xpos, ypos, hitAngle, color, killSpeed);
            spark.globalIndex = Thing.GetGlobalIndex();
            return spark;
        }

        public CustomSpark()
            : base(0f, 0f)
        {
        }

        public void DrawGlow()
        {
        }

        public void Init(float xpos, float ypos, Vec2 hitAngle, Color color, float killSpeed = 0.02f)
        {
            _color = color;
            position.x = xpos;
            position.y = ypos;
            hSpeed = (0f - hitAngle.x) * 2f * (Rando.Float(1f) + 0.3f);
            vSpeed = (0f - hitAngle.y) * 2f * (Rando.Float(1f) + 0.3f) - Rando.Float(2f);
            _bounceEfficiency = 0.6f;
            base.depth = 0.9f;
            _killSpeed = killSpeed;
        }

        public override void Update()
        {
            base.alpha -= _killSpeed;
            if (base.alpha < 0f)
            {
                Level.Remove(this);
            }
            base.Update();
        }

        public override void Draw()
        {
            Vec2 dir = base.velocity.normalized;
            float speed = base.velocity.length * 2f;
            Vec2 end = position + dir * speed;
            Vec2 intersect;
            Block touch = Level.CheckLine<Block>(position, end, out intersect);
            Graphics.DrawLine(position, (touch != null) ? intersect : end, _color * base.alpha, 0.5f, base.depth);
        }
    }
}