namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using NewYearsGift.Enumerations;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class used to describe the nonfood elements of gift which have guarantee period.
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class Toy: IItem, IToy
    {
        /// <summary>
        /// Gets or sets the name of toy.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get ; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of toy.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the guarantee period.
        /// </summary>
        /// <value>
        /// The guarantee period.
        /// </value>
        public DateTime GuaranteePeriod { get; set; }

        /// <summary>
        /// Gets or sets the color of the toy.
        /// </summary>
        /// <value>
        /// The color of the toy.
        /// </value>
        public ToyColor ToyColor { get; set; }
    }
}
