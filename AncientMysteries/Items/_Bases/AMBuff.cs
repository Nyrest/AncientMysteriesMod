using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{

    public abstract class AMBuff : AMThing
    {
        public StateBinding duckBinding = new(nameof(duck));
        public Duck duck { get; private set; }

        protected AMBuff() : base(0, 0)
        {
        }
    }
}
