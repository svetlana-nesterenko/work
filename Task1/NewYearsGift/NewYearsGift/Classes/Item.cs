namespace NewYearsGift.Classes
{
    #region Usings

    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// Class used for representing the gift or his elements (candies, fruits, box and etc.).
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public abstract class Item : IItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        protected Item(double weight)
        {
            Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            
        }
        /// <summary>
        /// Gets or sets the name of the gift or his elements.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets the weight of the gift or his elements.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }
    }
}
