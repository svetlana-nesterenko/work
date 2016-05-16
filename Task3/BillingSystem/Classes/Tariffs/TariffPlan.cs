namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;
    using Interfaces;

    #endregion

    /// <summary>
    /// Class used for defined the base fields of different tariff plans.
    /// </summary>
    /// <seealso cref="BillingSystem.Interfaces.ITariffPlan" />
    public abstract class TariffPlan : ITariffPlan
    {
        #region Public Properties

        /// <summary>
        /// Gets the tariff plan name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
       
        /// <summary>
        /// Gets the tariff plan cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public double Cost { get; set; }
        
        /// <summary>
        /// Gets the payment per month.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public double Tax { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TariffPlan"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cost">The cost.</param>
        /// <param name="tax">The tax.</param>
        public TariffPlan(string name, double cost, double tax)
        {
            Name = name;
            Cost = cost;
            Tax = tax;
        }

        #endregion

        /// <summary>
        /// Calculates the call cost.
        /// </summary>
        /// <param name="historyRecords">The history records.</param>
        /// <returns></returns>
        public abstract IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords);

    }
}
