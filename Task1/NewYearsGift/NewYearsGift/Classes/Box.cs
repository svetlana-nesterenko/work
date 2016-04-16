namespace NewYearsGift.Classes
{
    /// <summary>
    /// The class used to describe the gift packing (box).
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class Box : Item
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        public Box()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="name"></param>
        public Box(string name)
            : base(name)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        public Box(string name, string productArticle)
            : base(name, productArticle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Box(string name, string productArticle, double weight)
            : base(name, productArticle, weight)
        {

        }
        #endregion
    }
}