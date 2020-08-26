using AncientMysteries.Localization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Items.Rainbow
{
    public class ChromaticStreamer : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "棱彩流光",
            _ => "Chromatic Streamer",
        };

        public ChromaticStreamer(float xval, float yval) : base(xval, yval)
        {
        }
    }
}
