namespace BillingSystem.Interfaces
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using BillingSystem.Classes;

    #endregion

    public interface IContract
    {
        string Id { get; }
        string PhoneNumber { get; }

        bool ChangeTariff(ITariffPlan newTariff);
        void AddCallRecord(bool incoming, bool success, DateTime start, DateTime end, string number);
        string GenerateInvoice(DateTime start, DateTime end);

        ITariffPlan GetCurrentTariff();

        IEnumerable<HistoryRecordWithSumm> GetCallHistory(DateTime start, DateTime end);
    }
}
