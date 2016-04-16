namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// Class used for representing candies, fruits, box and etc.
    /// There is an delegate which will be called when weight of element changes.
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public abstract class Item : IItem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        protected Item()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        protected Item(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        protected Item(string name, string productArticle)
        {
            this.Name = name;
            this.ProductArticle = productArticle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        protected Item(string name, string productArticle, double weight)
        {
            this.Name = name;
            this.ProductArticle = productArticle;
            this.Weight = weight;
        }

        #endregion


        /// <summary>
        /// Gets or sets the name of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the product article (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets or sets the weight of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return String.Format("{0} {1} вес - {2}", ProductArticle, Name, Weight);
        }
    }
}
