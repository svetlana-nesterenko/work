using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public interface ITariffPlan
    {
        string Name { get; }
        double Cost { get; }
        double Tax { get; }

        IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords);
    }
}
