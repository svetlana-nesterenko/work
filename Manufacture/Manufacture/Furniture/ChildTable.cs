using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manufacture.Furniture
{
    public class ChildTable : Table, IChild
    {
        public string CertificateNumber { get; set; }

        public override string GenerateOrder()
        {
            string orderContent = base.GenerateOrder();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(orderContent);

            stringBuilder.AppendLine("Additional information:");
            stringBuilder.AppendLine(String.Format("CertificateNumber: {0}", this.CertificateNumber));

            return stringBuilder.ToString();
        }
    }
}
