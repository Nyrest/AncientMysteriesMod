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
        public Func<Vec2> PositionProvider { get; protected set; }

        public abstract void Update();

        public abstract void Draw();
    }
}
