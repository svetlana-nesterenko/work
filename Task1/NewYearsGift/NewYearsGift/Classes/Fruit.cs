namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class describes some fruits which can be part of the gift (mandarin, orange and etc). 
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    /// <seealso cref="NewYearsGift.Interfaces.IHasExpirationDate" />
    public class Fruit: IItem, IHasExpirationDate
    {
        /// <summary>
        /// Gets or sets the name of fruit.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of fruit.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of fruit.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public DateTime ExpirationDate { get; set; }
    }
}
