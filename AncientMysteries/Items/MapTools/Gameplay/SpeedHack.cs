using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools_Gameplay)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.Default, "Speed Hack", "Boost Game Speed to 1.5x.")]
    [MetaInfo(Lang.schinese, "变速齿轮", "加倍游戏速度至原本 1.5x。")]
    [MetaType(MetaType.MapTools)]
    public sealed partial class SpeedHack : AMMapToolGameplay
    {
        public bool half;
        public SpeedHack(float xpos, float ypos) : base(xpos, ypos)
        {

        }

        public override void Update()
        {
            base.Update();
            if (half)
            {
                foreach (var item in Level.current.things[typeof(Thing)])
                {
                    if (item.GetType() != typeof(SpeedHack))
                        item.DoUpdate();
                }
                half = false;
            }
            else
            {
                half = true;
                foreach (var item in Level.current.things[typeof(Thing)])
                {
                    if (item.GetType() != typeof(SpeedHack))
                        item.DoUpdate();
                }
            }
        }
    }
}
