namespace BillingSystem.Interfaces
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using BillingSystem.Classes;

    #endregion

    /// <summary>
    /// Interface used for defined contract that attachs some phone number to client.
    /// </summary>
    public interface IContract
    {
        /// <summary>
        /// Gets the identifier of the contract.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
       
        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string PhoneNumber { get; }

        /// <summary>
        /// Changes the tariff.
        /// </summary>
        /// <param name="newTariff">The new tariff.</param>
        /// <returns></returns>
        bool ChangeTariff(ITariffPlan newTariff);
        
        /// <summary>
        /// Adds the call record to history.
        /// </summary>
        /// <param name="incoming">if set to <c>true</c> [incoming].</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="number">The number.</param>
        void AddCallRecord(bool incoming, bool success, DateTime start, DateTime end, string number);
        
        /// <summary>
        /// Generates the invoice.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        string GenerateInvoice(DateTime start, DateTime end);

        /// <summary>
        /// Gets the current tariff.
        /// </summary>
        /// <returns></returns>
        ITariffPlan GetCurrentTariff();

        /// <summary>
        /// Gets the call history.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        IEnumerable<HistoryRecordWithSumm> GetCallHistory(DateTime start, DateTime end);
    }
}
