namespace BillingSystem.Classes
{
    #region Usings

    using System;
    using Interfaces;
    
    #endregion

    public class TariffPlanRecord
    {
        public DateTime StartDate;
        public DateTime? EndDate;
        public ITariffPlan TariffPlan;
    }
}
