namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Class represents tariff plan that has limit for a current call. 
    /// The minutes beyond the limit is calculated with discont, other minutes is calculated as (minutes cost * call duration).
    /// </summary>
    /// <seealso cref="BillingSystem.Classes.Tariffs.TariffPlan" />
    public class TariffSpecial : TariffPlan
    {
        /// <summary>
        /// The limit for a current call.
        /// </summary>
        public int Limit;

        /// <summary>
        /// Gets or sets the discount for minutes beyond the limit.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public int Discount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TariffSpecial"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="tax">The tax.</param>
        /// <param name="limitNonDiscountMinutesPerCall">The limit non discount minutes per call.</param>
        /// <param name="discount">The discount.</param>
        public TariffSpecial(string name, double cost, double tax, int limitNonDiscountMinutesPerCall, int discount)
            : base(name, cost, tax)
        {
            Limit = limitNonDiscountMinutesPerCall;
            Discount = discount;
        }

        /// <summary>
        /// Calculates the call cost. 
        /// The minutes beyond the limit is calculated with discont, other minutes is calculated as (minutes cost * call duration).
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
    }
}
