namespace NewYearsGift.Classes
{
    #region Usings

    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class used to describe the gift packing (box).
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class Box : Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        public Box(double weight): base(weight)
        {
            Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        public Box()
        {
            
        }

    }
}
