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
        public static Type[] amTypes;

        public DebugItemBox(float xpos, float ypos, Type c = null) : base(xpos, ypos, c)
        {
            _editorName = "|DGPURPLE|Debug Item Box";
            randomSpawn = true;
        }

        public override void Update()
        {
            base.Update();
            if (amTypes is null)
            {
                amTypes = Editor.Placeables.AllTypes.Where(x => x.Assembly == amAssembly).ToArray();
            }
            contains = amTypes[Rando.Int(amTypes.Length - 1)];
        }
    }
}
