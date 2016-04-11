namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using NewYearsGift.Enumerations;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class represents the any kind of sweets.
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    /// <seealso cref="NewYearsGift.Interfaces.ICandy" />
    /// <seealso cref="NewYearsGift.Interfaces.IHasExpirationDate" />
    public class Candy: IItem, ICandy,IHasExpirationDate
    {
        /// <summary>
        /// Gets or sets the name of candy.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of candy.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public double SugarContent { get; set; }

        /// <summary>
        /// Gets or sets the category of sweets from enumeration CandyCategory.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public CandyCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the flavor of sweets from enumeration CandyFlavor.
        /// </summary>
        /// <value>
        /// The flavor of candy.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public CandyFlavor CandyFlavor { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public DateTime ExpirationDate { get; set; }
    }
}
