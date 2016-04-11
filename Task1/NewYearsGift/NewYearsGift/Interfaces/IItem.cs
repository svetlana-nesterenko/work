namespace NewYearsGift.Interfaces
{
    #region Usings

    #endregion

    /// <summary>
    /// Interface used for representing the elements of gift (candies, fruits, box and etc.)
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets or sets the name of gifts element.
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
        /// Gets or sets the weight of gifts element.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        double Weight { get; set; }
    }
}
