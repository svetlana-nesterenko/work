namespace NewYearsGift.Interfaces
{
    #region Usings

    using System;
    using NewYearsGift.Enumerations;

    #endregion

    /// <summary>
    /// Interface used for nonfood elements of gift which have guarantee period and color.
    /// </summary>
    public interface IToy
    {
        /// <summary>
        /// Gets or sets the guarantee period.
        /// </summary>
        /// <value>
        /// The guarantee period.
        /// </value>
        DateTime GuaranteePeriod { get; set; }


        /// <summary>
        /// Gets or sets the color of the toys from enumeration ToyColor.
        /// </summary>
        /// <value>
        /// The color of the toys.
        /// </value>
        ToyColor ToyColor { get; set; }
    }
}
