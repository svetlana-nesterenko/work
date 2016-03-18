using Manufacture.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Furniture
{
    public abstract class Bed : Furniture, IBed
    {
        public int SeatPlacesNumber { get; set; }

        public override string GenerateOrder()
        {
            string orderContent = base.GenerateOrder();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("***************************************************");
            stringBuilder.AppendLine(orderContent);
            stringBuilder.AppendLine("Additional information:");
            stringBuilder.AppendLine(String.Format("SeatPlacesNumber: {0}", this.SeatPlacesNumber));
            
            return stringBuilder.ToString();
        }
    }
}