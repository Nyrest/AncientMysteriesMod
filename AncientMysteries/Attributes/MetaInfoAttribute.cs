using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class MetaInfoAttribute : Attribute
    {
        /// <summary>
        /// Specify localized name and description for DescImgGeneator
        /// </summary>
        /// <param name="lang">Language</param>
        /// <param name="name">Localized Name</param>
        /// <param name="description">Localized Description. (Use \n to make line break)</param>
        public MetaInfoAttribute(Lang lang, string name, string description) { }
    }
}
