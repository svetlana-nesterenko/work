using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class HistoryRecordWithSumm : HistoryRecord
    {
        public double Cost;

        public HistoryRecordWithSumm(bool incoming, bool success, DateTime start, DateTime end, string number) : base(incoming, success, start, end, number)
        {
        }

        public HistoryRecordWithSumm(bool incoming, bool success, DateTime start, DateTime end, string number, double cost)
            : base(incoming, success, start, end, number)
        {
            Cost = cost;
        }
    }
}
