using AncientMysteries.AmmoTypes;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Bullets
{
    public class Bullet_CubicBlast : Bullet
    {


        public Bullet_CubicBlast(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }
        public override void Update()
        {
            base.Update();
            int count = 15;
            foreach(Duck d in Level.CheckCircleAll<Duck>(this.position,50))
            {
                if (d != DuckNetwork.localConnection.profile.duck && Network.isActive && count >=15)
                {
                    count = 0;
                    Level.Add(new Bullet_Current(this.x,this.y,new AT_Current(),Maths.PointDirection(this.position,d.position),this.owner,true,400,false,true));
                }
            }
            count++;
        }
    }
}
