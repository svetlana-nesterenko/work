namespace NewYearsGift.Interfaces
{
    /// <summary>
    /// Defines the base fields for different classes (candies, fruits, box and etc.).
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets or sets the name of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        double Weight { get; set; }
    }
}
