using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.True
{
    public class ArcaneNova_Magic_Stage1 : AMThing
    {
        public StateBinding _positionBinding = new CompressedVec2Binding(nameof(position), int.MaxValue, isvelocity: false, doLerp: true);

        public StateBinding _travelBinding = new CompressedVec2Binding(nameof(_travelAngleRadius), 20);

        private float _travelAngleRadius;

        public override void OnGhostObjectAdded()
        {
            base.OnGhostObjectAdded();
        }
    }
}
