namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Class represents tariff plan that has limit for a month. 
    /// The minutes within the limit is calculated with discont, other minutes is calculated as (minutes cost * call duration).
    /// </summary>
    /// <seealso cref="BillingSystem.Classes.Tariffs.TariffPlan" />
    public class TariffLight : TariffPlan
    {
        /// <summary>
        /// Gets or sets the limit of minutes for a month.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the discount for minutes within the limit.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public int Discount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TariffLight"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="tax">The tax.</param>
        /// <param name="minutesLimitPerMonth">The minutes limit per month.</param>
        /// <param name="discountPercent">The discount percent.</param>
        public TariffLight(string name, double cost, double tax, int minutesLimitPerMonth, int discountPercent)
            : base(name, cost, tax)
        {
            Limit = minutesLimitPerMonth;
            Discount = discountPercent;
        }

        /// <summary>
        /// Calculates the call cost. 
        /// The minutes within the limit is calculated with discont, other minutes is calculated as (minutes cost * call duration).
        /// </summary>
        /// <param name="historyRecords">The history records.</param>
        /// <returns></returns>
        public override IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords)
        {
            double minutesCounter = 0;
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (HistoryRecord hr in historyRecords)
            {
                double callCost = 0;
                if (!hr.incoming && hr.success)
                {
                    double duration = hr.end.Subtract(hr.start).TotalMinutes;
                    minutesCounter += duration;

                    if (minutesCounter <= Limit)
                    {
                        callCost = (Cost - Cost/100*Discount)*duration;
                    }
                    else
                    {
                        double nonDiscountMinutes = (minutesCounter - Limit);
                        double discountMinutes = duration - nonDiscountMinutes;
                        callCost = nonDiscountMinutes*Cost + discountMinutes*(Cost - Cost/100*Discount);
                    }
                }
                list.Add(new HistoryRecordWithSumm(hr.incoming, hr.success, hr.start, hr.end, hr.number, callCost));
            }

            return list;
        }
    }
}
