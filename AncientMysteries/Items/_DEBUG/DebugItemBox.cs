using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items._DEBUG
{
    [EditorGroup(_debugGroup)]
    public class DebugItemBox : ItemSpawner
    {
        public static Type[] amTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && typeof(Holdable).IsAssignableFrom(x)).ToArray();

        public DebugItemBox(float xpos, float ypos, Type c = null) : base(xpos, ypos, c)
        {
            _editorName = "|DGPURPLE|Debug Item Box";
            randomSpawn = true;
        }

        public override void Update()
        {
            base.Update();
            if (Network.isActive && !Network.isServer) return;
            contains = amTypes[Rando.Int(amTypes.Length - 1)];
        }

        public override void SpawnItem()
        {
            _spawnWait = 0f;
            if (Network.isActive && base.isServerForObject)
            {
                Send.Message(new NMItemSpawned(this));
            }
            if (Network.isActive && !Network.isServer) return;
            var fuckingType = amTypes[Rando.Int(amTypes.Length - 1)];
            PhysicsObject newThing = (PhysicsObject)Editor.CreateThing(fuckingType);
            if (newThing != null)
            {
                newThing.x = base.x;
                newThing.y = base.top + (newThing.y - newThing.bottom) - 6f;
                newThing.vSpeed = -2f;
                newThing.spawnAnimation = true;
                newThing.isSpawned = true;
                newThing.offDir = offDir;
                Level.Add(newThing);
                if (_seated)
                {
                    SetHoverItem(newThing as Holdable);
                }
            }
        }
    }
}
