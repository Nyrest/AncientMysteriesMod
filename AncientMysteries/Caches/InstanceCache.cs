using AncientMysteries.Helpers;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries
{
    public static class InstanceCache<T> where T : new()
    {
        public static readonly T instance = FastNew<T>.CreateInstance();
    }
}
