using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class CallInfo
    {
        public DateTime CallDate;
        public DateTime? StartDate;
        public DateTime EndDate;
        public string MyNumber;
        public string Number;
        public CallType CallType;
        public CallInfoResultType CallResultType;

        public CallInfo(string number, CallType callType)
        {
            Number = number;
            CallType = callType;
            CallDate = StaticTime.CurrentTime;
        }
    }
}

