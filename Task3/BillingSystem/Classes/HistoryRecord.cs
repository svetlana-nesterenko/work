namespace BillingSystem.Classes
{
    #region Usings

    using System;
   
    #endregion

    /// <summary>
    /// Class used for representing call in history.
    /// </summary>
    public class HistoryRecord
    {
        /// <summary>
        /// True if incoming call, false if outgoing call.
        /// </summary>
        public bool incoming;
        /// <summary>
        /// The success: true if call is answered/accepted.
        /// </summary>
        public bool success;
        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime start;
        /// <summary>
        /// The end time.
        /// </summary>
        public DateTime end;
        /// <summary>
        /// The number: remote phone number.
        /// </summary>
        public string number;


        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryRecord"/> class.
        /// </summary>
        /// <param name="incoming">if set to <c>true</c> [incoming].</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="number">The number.</param>
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
