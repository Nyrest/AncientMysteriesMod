using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items._DEBUG
{
    [EditorGroup(_debugGroup)]
    public class DebugItemBox : ItemSpawner
    {
        private static readonly Assembly amAssembly = Assembly.GetExecutingAssembly();
        public static Type[] amTypes = Editor.Placeables.AllTypes.Where(x => x.Assembly == amAssembly).ToArray();

        public DebugItemBox(float xpos, float ypos, Type c = null) : base(xpos, ypos, c)
        {
            randomSpawn = true;
        }

        public override void Update()
        {
            base.Update();
            contains = amTypes[Rando.Int(amTypes.Length - 1)];
        }
    }
}
