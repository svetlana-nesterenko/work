namespace ATS.Classes
{
    #region Usings

    using System;
    using ATS.Enum;

    #endregion
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

