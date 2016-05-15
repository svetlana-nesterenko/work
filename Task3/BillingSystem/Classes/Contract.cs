namespace BillingSystem.Classes
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ATS.Classes;
    using Interfaces;

    #endregion

    public class Contract : IContract
    {
        public string Id { get; set; }
       
        public string PhoneNumber { get; set; }

        private readonly ICollection<TariffPlanRecord> _tariffPlanHistory;
        private readonly ICollection<HistoryRecord> _historyRecords;

        public Contract(string id, string phoneNumber)
        {
            _tariffPlanHistory = new List<TariffPlanRecord>();
            _historyRecords = new List<HistoryRecord>();
            Id = id;
            PhoneNumber = phoneNumber;
        }

        public bool ChangeTariff(ITariffPlan newTariff)
        {
            TariffPlanRecord currentTariffRecord = _tariffPlanHistory.LastOrDefault();
            if (currentTariffRecord != null)
            {
                if (currentTariffRecord.StartDate.Year == StaticTime.CurrentTime.Year &&
                    currentTariffRecord.StartDate.Month == StaticTime.CurrentTime.Month)
                {
                    return false;
                }
                currentTariffRecord.EndDate = StaticTime.CurrentTime;
            }

            TariffPlanRecord newTariffRecord = new TariffPlanRecord();
            newTariffRecord.TariffPlan = newTariff;
            newTariffRecord.StartDate = StaticTime.CurrentTime;
            _tariffPlanHistory.Add(newTariffRecord);
            return true;
        }

        public ITariffPlan GetCurrentTariff()
        {
            TariffPlanRecord currentTariffRecord = _tariffPlanHistory.LastOrDefault();
            if (currentTariffRecord != null)
            {
                return currentTariffRecord.TariffPlan;
            }
            return null;
        }

        public void AddCallRecord(bool incoming, bool success, DateTime start, DateTime end, string number)
        {
            _historyRecords.Add(new HistoryRecord(incoming, success, start, end, number));
        }

        public IEnumerable<HistoryRecordWithSumm> GetCallHistoryByNumber(DateTime start, DateTime end, string number)
        {
            return GetCallHistory(start, end).Where(h => h.number.Equals(number));
        }

        public IEnumerable<HistoryRecordWithSumm> GetCallHistoryBySumm(DateTime start, DateTime end, double minSumm, double maxSumm)
        {
            return GetCallHistory(start, end).Where(h => h.Cost >= minSumm && h.Cost <= maxSumm);
        }

        public IEnumerable<HistoryRecordWithSumm> GetCallHistory(DateTime start, DateTime end)
        {
            TariffPlanRecord[] tariffPlanRecords = _tariffPlanHistory.Where(t => t.StartDate <= start && (t.EndDate == null || t.EndDate > end)).ToArray();

            DateTime monthBegin = new DateTime(start.Year, start.Month, 1);
            List<HistoryRecordWithSumm> list = new List<HistoryRecordWithSumm>();
            foreach (TariffPlanRecord tariffPlanRecord in tariffPlanRecords)
            {
                var record = tariffPlanRecord;
                list.AddRange(
                    tariffPlanRecord.TariffPlan.CalculateCost(
                        _historyRecords.Where(r => r.start >= monthBegin && r.end <= end
                                                   && r.start >= record.StartDate &&
                                                   (r.end < record.EndDate || record.EndDate == null))));
            }

            return list.Where(r => r.start >= start && r.end <= end);
        }

        public string GenerateDetailedHistoryByNumber(DateTime start, DateTime end, string number)
        {
            IEnumerable<HistoryRecordWithSumm> list = GetCallHistoryByNumber(start, end, number);
            return GenerateReport(list, start, end);
        }

        public string GenerateDetailedHistoryBySumm(DateTime start, DateTime end, double minSumm, double maxSumm)
        {
            IEnumerable<HistoryRecordWithSumm> list = GetCallHistoryBySumm(start, end, maxSumm, minSumm);
            return GenerateReport(list, start, end);
        }

        public string GenerateInvoice(DateTime start, DateTime end)
        {
            IEnumerable<HistoryRecordWithSumm> list = GetCallHistory(start, end);
            return GenerateReport(list, start, end);
        }

        private string GenerateReport(IEnumerable<HistoryRecordWithSumm> list, DateTime start, DateTime end)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Number: {0}", PhoneNumber));
            sb.AppendLine(String.Format("Date range: {0:yyyy-MM-dd} - {1:yyyy-MM-dd}", start, end));
            sb.AppendLine();
            sb.AppendLine("TIME\tTYPE\tNUMBER\tDURATION\tCOST");
            foreach (HistoryRecordWithSumm s in list)
            {
                sb.AppendLine(String.Format("{0:yyyy-MM-dd HH:mm:ss}\t{1}\t{2}\t{3}\t{4}", s.start, s.incoming ? "IN" : "OUT", s.number,
                    s.end.Subtract(s.start).TotalSeconds, s.Cost));
            }

            sb.AppendLine();
            double callsSumm = list.Sum(h => h.Cost);
            double taxSumm = GetCurrentTariff().Tax;
            sb.AppendLine(String.Format("Calls price: {0}", callsSumm));
            sb.AppendLine(String.Format("Tax: {0}", taxSumm));
            sb.AppendLine();
            sb.AppendLine(String.Format("Total price: {0}", callsSumm + taxSumm));
            return sb.ToString();
        }
    }
}
