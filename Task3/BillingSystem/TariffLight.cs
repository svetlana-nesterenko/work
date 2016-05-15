using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class TariffLight : TariffPlan
    {
        public int Limit { get; set; }
        public int Discount { get; set; }
        public override IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords)
        {
            int minutesCounter = 0;
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (HistoryRecord hr in historyRecords)
            {
                double callCost = 0;
                if (!hr.incoming)
                {
                    int duration = hr.end.Subtract(hr.start).Seconds;
                    minutesCounter += duration;

                    if (minutesCounter <= Limit)
                    {
                        callCost = (Cost - Cost/100*Discount)*duration;
                    }
                    else
                    {
                        int nonDiscountMinutes = (minutesCounter - Limit);
                        int discountMinutes = duration - nonDiscountMinutes;
                        callCost = nonDiscountMinutes*Cost + discountMinutes*(Cost - Cost/100*Discount);
                    }
                }
                list.Add(new HistoryRecordWithSumm(hr.incoming, hr.success, hr.start, hr.end, hr.number, callCost));
            }

            return list;
        }

        public TariffLight(string name, double cost, double tax, int minutesLimitPerMonth, int discountPercent)
            : base(name, cost, tax)
        {
            Limit = minutesLimitPerMonth;
            Discount = discountPercent;
        }
    }
}
