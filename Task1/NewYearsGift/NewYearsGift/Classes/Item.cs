namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Text;
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

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the gifts element (candy, fruit, box and etc.).
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public virtual string FullName
        {
            get { return this.Name; }
        }

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

        #endregion

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <returns></returns>
        public virtual string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Name: {0}", this.FullName));
            sb.AppendLine(String.Format("ProductArticle: {0}", this.ProductArticle));
            sb.AppendLine(String.Format("Weight: {0}", this.Weight));
            return sb.ToString();
        }
    }
}
