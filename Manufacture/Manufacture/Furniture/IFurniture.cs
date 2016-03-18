using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Manufacture.Furniture
{
    public interface IFurniture
    {
        string Name { get; set; }
        float Weight { get; set; }
        float Size { get; set; }
        List<Materials.MaterialSpecification> Materials { get; set; }

        string GenerateOrder();
    }
}