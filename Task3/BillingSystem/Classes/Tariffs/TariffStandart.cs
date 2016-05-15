namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    public class TariffStandart : TariffPlan
    {
        public override IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords)
        {
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (HistoryRecord hr in historyRecords)
            {
                double callCost = 0;
                if (!hr.incoming && hr.success)
                {
                    double duration = hr.end.Subtract(hr.start).TotalMinutes;
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
