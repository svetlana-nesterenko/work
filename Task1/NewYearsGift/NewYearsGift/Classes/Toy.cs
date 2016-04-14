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
    public class Toy : Item, IToy
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        public Toy()
        {
            
        }
    }
}
