using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class MetaImageAttribute : Attribute
    {
        /// <summary>
        /// Specify single or multiple frames for DescImgGeneator
        /// </summary>
        /// <param name="texture">Texture Reference</param>
        /// <param name="frameWidth">Frame Width (-1 to full width)</param>
        /// <param name="frameHeight">Frame Height (-1 to full height)</param>
        /// <param name="frames">Frames to present (null or empty to use 1)</param>
        public MetaImageAttribute(string texture, int frameWidth = -1, int frameHeight = -1, params int[] frames) { }
    }
}
