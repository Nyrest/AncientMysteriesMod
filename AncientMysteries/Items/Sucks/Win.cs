namespace AncientMysteries.Items.Sucks
{
    public class Win : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Win",
        };

        public Win(float xval, float yval) : base(xval, yval)
        {
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if (owner is Duck ownerDuck)
            {
                foreach (Duck d in Level.current.things[typeof(Duck)])
                {
                    if (d.team != ownerDuck.team)
                    {
                        d.Kill(new DTImpact(this));
                    }
                }
            }
        }
    }
}
