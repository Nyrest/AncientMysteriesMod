using AncientMysteries.DeathTypes;

namespace AncientMysteries.Items.Props
{
    [EditorGroup(p_functional)]
    public class DeathFlower : AMHoldable
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "一朵可爱的FAFA",
            _ => "Wonder Flower",
        };

        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "看起来很美味，吃了它！",
            _ => "Looks delicious. Eat it!",
        };

        public DeathFlower(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(t_Props_DeathFlower);
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if(duck != null)
            {
                duck.Scream();
                duck.Kill(new DT_NoReason());
                Level.Remove(this);
            }
        }
    }
}
