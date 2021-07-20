using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public abstract partial class AMThrowable : AMGun
    {
        protected AMThrowable(float xval, float yval) : base(xval, yval)
        {
        }
    }
}
