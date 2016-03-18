using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manufacture.Materials
{
    public abstract class Material
    {
        public MaterialColor Color { get; set; }
        public MaterialSort Sort { get; set; }
        public float Price { get; set; }
    }
}
