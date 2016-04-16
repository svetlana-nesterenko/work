namespace NewYearsGift.Interfaces
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Interface used for the gifts elements which have an expiration date (candies, fruits).
    /// </summary>
    interface IHasExpirationDate
    {
        /// <summary>
        /// Gets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        DateTime ExpirationDate { get; }
    }
}
