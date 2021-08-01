namespace AncientMysteries.Items
{
    public abstract class AMThing : Thing
    {
        public AMThing(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        protected AMThing This() { return this; }
    }
}