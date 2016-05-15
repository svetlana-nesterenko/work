namespace BillingSystem.Classes.Tariffs
{
    #region Usings

    using System.Collections.Generic;
    using Interfaces;

    #endregion

    public abstract class TariffPlan : ITariffPlan
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public double Tax { get; set; }

        public abstract IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords);

        public TariffPlan(string name, double cost, double tax)
        {
            Name = name;
            Cost = cost;
            Tax = tax;
        }
    }
}
