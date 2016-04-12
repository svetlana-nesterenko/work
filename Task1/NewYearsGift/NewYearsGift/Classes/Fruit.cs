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

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        public Fruit(double weight): base(weight)
        {
            Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class.
        /// </summary>
        public Fruit()
        {
            
        }
    }
}
