using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Materials
{
    public class Metal : Material, IMaterial
    {
        public MetalType Type { get; set; }
    }
}
