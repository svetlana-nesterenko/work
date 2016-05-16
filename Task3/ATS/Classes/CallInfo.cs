namespace ATS.Classes
{
    #region Usings

    using System;
    using ATS.Enum;

    #endregion

    /// <summary>
    /// Class used for representing the information about call.
    /// </summary>
    public class CallInfo
    {
        /// <summary>
        /// The call date.
        /// </summary>
        public DateTime CallDate;

        /// <summary>
        /// The start date of conversation.
        /// </summary>
        public DateTime? StartDate;

        /// <summary>
        /// The end date of conversation.
        /// </summary>
        public DateTime EndDate;

        /// <summary>
        /// My phone number.
        /// </summary>
        public string MyNumber;

        /// <summary>
        /// The remote phone number.
        /// </summary>
        public string Number;

        /// <summary>
        /// The call type.
        /// </summary>
        public CallType CallType;

        /// <summary>
        /// The call result type.
        /// </summary>
        public CallInfoResultType CallResultType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfo"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="callType">Type of the call.</param>
        public CallInfo(string number, CallType callType)
        {
            Number = number;
            CallType = callType;
            CallDate = StaticTime.CurrentTime;
        }
    }
}

