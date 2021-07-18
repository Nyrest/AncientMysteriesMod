namespace AncientMysteries.Items
{
    [EditorGroup(group_DEBUG)]
    public class RandomAMItem : Holdable
    {
        public static Type[] amTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && typeof(Holdable).IsAssignableFrom(x)).ToArray();

        public static readonly string[] awesomeGuys = new[]
        {
            tex_Hat_Boring3Pickup,
            tex_Hat_DevastatorPickup,
            tex_Hat_GUDZHPickup,
        };

        public RandomAMItem()
        {
            this.ReadyToRunWithFrames(Rand.Choose(awesomeGuys));
            _editorName = "|DGPURPLE|Random";
        }

        public override void Initialize()
        {
            base.Initialize();
            if (Level.current is Editor || (Network.isActive && !isServerForObject)) return;
            var fuckingType = amTypes[Rando.Int(amTypes.Length - 1)];
            if (Activator.CreateInstance(fuckingType, Editor.GetConstructorParameters(fuckingType)) is PhysicsObject newThing)
            {
                newThing.x = x;
                newThing.y = y;
                newThing.vSpeed = -2f;
                newThing.spawnAnimation = true;
                newThing.isSpawned = true;
                newThing.offDir = offDir;
                Level.Add(newThing);
            }
            Level.Remove(this);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}