using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class HistoryRecord
    {
        public bool incoming;
        public bool success;
        public DateTime start;
        public DateTime end;
        public string number;

        public HistoryRecord(bool incoming, bool success, DateTime start, DateTime end, string number)
        {
            this.incoming = incoming;
            this.success = success;
            this.start = start;
            this.end = end;
            this.number = number;
        }
    }
}
