namespace BillingSystem.Interfaces
{
    #region Usings

    using System.Collections.Generic;
    using BillingSystem.Classes;

    #endregion

    /// <summary>
    /// Interface used for different tariff plans.
    /// </summary>
    public interface ITariffPlan
    {
        /// <summary>
        /// Gets the tariff plan name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        
        /// <summary>
        /// Gets the tariff plan cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        double Cost { get; }
        
        /// <summary>
        /// Gets the payment per month.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        double Tax { get; }

        /// <summary>
        /// Calculates the call cost.
        /// </summary>
        /// <param name="historyRecords">The history records.</param>
        /// <returns></returns>
        IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords);
    }
}
