using Manufacture.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Furniture
{
    public abstract class Cabinet : Furniture, ICabinet
    {
        public int SectionsNumber { get; set; }

        public override string GenerateOrder()
        {
            string orderContent = base.GenerateOrder();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("***************************************************");
            stringBuilder.AppendLine(orderContent);
            stringBuilder.AppendLine("Additional information:");
            stringBuilder.AppendLine(String.Format("SectionsNumber: {0}", this.SectionsNumber));
            return stringBuilder.ToString();
        }
    }
}