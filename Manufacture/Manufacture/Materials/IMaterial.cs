using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Materials
{
    public interface IMaterial
    {
        MaterialColor Color { get; set; }
        MaterialSort Sort { get; set; }
        float Price { get; set; }
    }
}