namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Class represents tariff plan in which call cost is calculated as (minutes cost * call duration).
    /// </summary>
    /// <seealso cref="BillingSystem.Classes.Tariffs.TariffPlan" />
    public class TariffStandart : TariffPlan
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TariffStandart"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="tax">The tax.</param>
        public TariffStandart(string name, double cost, int tax)
            : base(name, cost, tax)
        {
        }

        /// <summary>
        /// Calculates the call cost as (minutes cost * call duration).
        /// </summary>
        /// <param name="historyRecords">The history records.</param>
        /// <returns></returns>
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
    }
}
