namespace NewYearsGift.Interfaces
{
    /// <summary>
    /// Defines the base fields for different classes (candies, fruits, box and etc.).
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets the name of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the product article (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        string ProductArticle { get; }

        /// <summary>
        /// Gets the weight of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        double Weight { get; }

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <returns></returns>
        string GetInfo();
    }
}
