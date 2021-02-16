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
        public static Type[] amTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(Holdable).IsAssignableFrom(x)).ToArray();
        public static List<TypeProbPair> probPairs = new List<TypeProbPair>(amTypes.Select(x => new TypeProbPair() { probability = 1, type = x }));
        public static FieldInfo fuck = typeof(ItemSpawner).GetField("_possible", BindingFlags.NonPublic | BindingFlags.Instance);

        public DebugItemBox(float xpos, float ypos, Type c = null) : base(xpos, ypos, c)
        {
            _editorName = "|DGPURPLE|Debug Item Box";
            randomSpawn = true;
            fuck.SetValue(this, probPairs);
        }
        public override void DoInitialize()
        {
            fuck.SetValue(this, probPairs);
            base.DoInitialize();
            fuck.SetValue(this, probPairs);
        }

        public override void Update()
        {
            fuck.SetValue(this, probPairs);
            base.Update();
            contains = amTypes[Rando.Int(amTypes.Length - 1)];
        }
    }
}
