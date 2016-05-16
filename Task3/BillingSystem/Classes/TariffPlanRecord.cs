namespace BillingSystem.Classes
{
    #region Usings

    using System;
    using Interfaces;
    
    #endregion

    /// <summary>
    /// Class used for representing validity of tariff plan.
    /// </summary>
    public class TariffPlanRecord
    {
        /// <summary>
        /// The start date of the period.
        /// </summary>
        public DateTime StartDate;
        /// <summary>
        /// The end date of the period.
        /// </summary>
        public DateTime? EndDate;
        /// <summary>
        /// The tariff plan.
        /// </summary>
        public ITariffPlan TariffPlan;
    }
}
