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
    public class RandomAMItem : Holdable
    {
        public static Type[] amTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && typeof(Holdable).IsAssignableFrom(x)).ToArray();

        public RandomAMItem()
        {
            this.ReadyToRun(Texs.HatBoring3Pickup);
            _editorName = "|DGPURPLE|Random";
        }

        public override void Initialize()
        {
            base.Initialize();
            if (Level.current is Editor || (Network.isActive && !this.isServerForObject)) return;
            var fuckingType = amTypes[Rando.Int(amTypes.Length - 1)];
            if (Editor.CreateThing(fuckingType) is PhysicsObject newThing)
            {
                newThing.x = base.x;
                newThing.y = this.y;
                newThing.vSpeed = -2f;
                newThing.spawnAnimation = true;
                newThing.isSpawned = true;
                newThing.offDir = offDir;
                Level.Add(newThing);
            }
            Level.Remove(this);
        }
    }
}
