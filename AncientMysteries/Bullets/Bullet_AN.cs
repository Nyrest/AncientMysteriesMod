﻿using AncientMysteries.AmmoTypes;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN : Bullet
    {
        public Bullet_AN(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
        }
        public override void DoTerminate()
        {
            base.DoTerminate();
            NovaExp ins = new NovaExp(travelEnd.x, travelEnd.y, true);
            ins.xscale *= 3f;
            ins.yscale *= 3f;
            Level.Add(ins);
            SFX.Play("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            for (int i = 0; i < 10; i++)
            {
                Level.Add(new Bullet_FB(travelEnd.x, travelEnd.y, new AT_FB(), Rando.Float(0, 360), owner, false, 275, false, true));
            }
        }
    }
}