namespace AncientMysteries.Items._DEBUG
{
    [EditorGroup(_debugGroup)]
    public class RandomAMItem : Holdable
    {
        public static Type[] amTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && typeof(Holdable).IsAssignableFrom(x)).ToArray();

        public RandomAMItem()
        {
            this.ReadyToRun(Rand.Bool() ? t_HatBoring3Pickup : t_HatHattyPickup);
            _editorName = "|DGPURPLE|Random";
        }

        public override void Initialize()
        {
            base.Initialize();
            if (Level.current is Editor || (Network.isActive && !this.isServerForObject)) return;
            var fuckingType = amTypes[Rando.Int(amTypes.Length - 1)];
            if (Activator.CreateInstance(fuckingType, Editor.GetConstructorParameters(fuckingType)) is PhysicsObject newThing)
            {
                newThing.x = x;
                newThing.y = this.y;
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
