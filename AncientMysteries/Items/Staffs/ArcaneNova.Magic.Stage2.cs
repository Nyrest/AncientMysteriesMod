namespace AncientMysteries.Items.Staffs
{
    public class ArcaneNova_Magic_Stage2 : AMThing
    {
        public StateBinding _positionBinding = new CompressedVec2Binding(nameof(position), int.MaxValue, isvelocity: false, doLerp: true);

        public StateBinding _travelBinding = new CompressedVec2Binding(nameof(_travelAngleRadian), 20);

        private float _travelAngleRadian;


    }

}
