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
    public class Candy: Item, ICandy,IHasExpirationDate
    {
        /// <summary>
        /// Gets or sets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        public double SugarContent { get; set; }

        /// <summary>
        /// Gets or sets the category of sweets from enumeration CandyCategory.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public CandyCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the flavor of sweets from enumeration CandyFlavor.
        /// </summary>
        /// <value>
        /// The flavor of candy.
        /// </value>
        public CandyFlavor CandyFlavor { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        public Candy(double weight) : base(weight)
        {
            Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        public Candy()
        {
            
        }
    }
}
