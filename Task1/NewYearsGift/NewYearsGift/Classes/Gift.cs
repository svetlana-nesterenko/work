namespace NewYearsGift.Classes
{
    #region Usings

    using System.Collections.Generic;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// This class represents the New Year's gift which consists from IItems collection (toys, candies and etc.).
    /// </summary>
    public class Gift
    {
        /// <summary>
        /// Gets or sets the items (gifts, cadies, fruits and etc.).
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ICollection<IItem> Items { get; set; }
    }
}
