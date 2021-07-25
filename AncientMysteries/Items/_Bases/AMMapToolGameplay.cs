namespace AncientMysteries.Items
{
    public abstract partial class AMMapToolGameplay : AMMapTool
    {
        protected AMMapToolGameplay(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_MapTools_Swirl);
            _visibleInGame = false;
        }
    }
}