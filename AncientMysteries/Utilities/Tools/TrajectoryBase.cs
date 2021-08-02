using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public abstract record class TrajectoryBase
    {

        public abstract void Update();

        public abstract void Draw();

        public abstract void Reset();
    }
}
