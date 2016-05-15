namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    public class TariffSpecial : TariffPlan
    {
        public int Limit;
        public int Discount { get; set; }

        public override IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords)
        {
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (HistoryRecord hr in historyRecords)
            {
                double callCost = 0;
                if (!hr.incoming && hr.success)
                {
                    double duration = hr.end.Subtract(hr.start).TotalMinutes;
                    
                    if (duration > Limit)
                    {
                        callCost = (duration - Limit)*(Cost - Cost/100*Discount);
                        callCost += Limit*Cost;
                    }
                    else
                    {
                        callCost = Cost*duration;
                    }
                }
                list.Add(new HistoryRecordWithSumm(hr.incoming, hr.success, hr.start, hr.end, hr.number, callCost));
            }

            return list;
        }

        public TariffSpecial(string name, double cost, double tax, int limitNonDiscountMinutesPerCall, int discount)
            : base(name, cost, tax)
        {
            Limit = limitNonDiscountMinutesPerCall;
            Discount = discount;
        }
    }
}
