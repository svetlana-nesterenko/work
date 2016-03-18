using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Furniture
{
    public interface IBed : IFurniture
    {
        int SeatPlacesNumber { get; set; }
    }
}