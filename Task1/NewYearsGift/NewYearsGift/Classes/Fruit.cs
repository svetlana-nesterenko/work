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
    public class Fruit : Item, IHasExpirationDate
    {
        /// <summary>
        /// Gets or sets the expiration date of fruit.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        public Fruit()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        public Fruit(string name)
            : base(name)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        public Fruit(string name, string productArticle)
            : base(name, productArticle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Fruit(string name, string productArticle, double weight)
            : base(name, productArticle, weight)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="expirationDate">The expiration date.</param>
        public Fruit(string name, string productArticle, double weight, DateTime expirationDate)
            : base(name, productArticle, weight)
        {
            this.ExpirationDate = expirationDate;
        }
        #endregion
    }
}
