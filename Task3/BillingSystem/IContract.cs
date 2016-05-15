﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS;

namespace BillingSystem
{
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
