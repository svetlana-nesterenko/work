namespace BillingSystem.Classes
{
    #region Usings

    using System;
    
    #endregion

    /// <summary>
    /// Class used for representing call in history and contains call cost.
    /// </summary>
    /// <seealso cref="BillingSystem.Classes.HistoryRecord" />
    public class HistoryRecordWithSumm : HistoryRecord
    {
        /// <summary>
        /// The cost of call.
        /// </summary>
        public double Cost;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryRecordWithSumm"/> class.
        /// </summary>
        /// <param name="incoming">if set to <c>true</c> [incoming].</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="number">The number.</param>
        public HistoryRecordWithSumm(bool incoming, bool success, DateTime start, DateTime end, string number) : base(incoming, success, start, end, number)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryRecordWithSumm"/> class.
        /// </summary>
        /// <param name="incoming">if set to <c>true</c> [incoming].</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="number">The number.</param>
        /// <param name="cost">The cost.</param>
        public HistoryRecordWithSumm(bool incoming, bool success, DateTime start, DateTime end, string number, double cost)
            : base(incoming, success, start, end, number)
        {
            Cost = cost;
        }
    }
}
