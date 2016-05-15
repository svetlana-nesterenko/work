namespace BillingSystem.Interfaces
{
    #region Usings

    using System.Collections.Generic;
    using BillingSystem.Classes;

    #endregion

    public interface ITariffPlan
    {
        string Name { get; }
        double Cost { get; }
        double Tax { get; }

        IEnumerable<HistoryRecordWithSumm> CalculateCost(IEnumerable<HistoryRecord> historyRecords);
    }
}
