namespace NewYearsGift.Classes
{
    #region Usings

    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class used to describe the gift packing (box).
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class Box: IItem
    {
        /// <summary>
        /// Gets or sets the name of box.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the boxes article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of box.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public double Weight { get; set; }
    }
}
