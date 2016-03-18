using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Materials
{
    public class Wood : Material, IMaterial
    {
        public WoodType Type { get; set; }
    }
}