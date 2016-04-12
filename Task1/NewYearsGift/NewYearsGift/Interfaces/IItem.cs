namespace NewYearsGift.Interfaces
{
    /// <summary>
    /// Defines the base fields for different classes (gift, candies, fruits, box and etc.)
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets or sets the name of the gift or his element.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of the gift or his element.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        double Weight { get; }
    }
}
