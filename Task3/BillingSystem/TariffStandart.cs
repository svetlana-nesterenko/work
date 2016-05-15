using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class TariffStandart : TariffPlan
    {
        public override IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords)
        {
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (HistoryRecord hr in historyRecords)
            {
                double callCost = 0;
                if (!hr.incoming)
                {
                    int duration = hr.end.Subtract(hr.start).Seconds;
                    callCost = Cost*duration;
                }
                list.Add(new HistoryRecordWithSumm(hr.incoming, hr.success, hr.start, hr.end, hr.number, callCost));
            }

            return list;
        }

        public TariffStandart(string name, double cost, int tax)
            : base(name, cost, tax)
        {
        }
    }
}
