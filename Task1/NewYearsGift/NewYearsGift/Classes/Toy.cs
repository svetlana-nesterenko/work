namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Text;
    using NewYearsGift.Enumerations;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class used to describe the nonfood elements of gift which have guarantee period.
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class Toy : Item, IToy
    {
        #region Public Properties

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public override string FullName
        {
            get { return String.Format("Child toy '{0}'", base.Name); }
        }

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

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        public Toy()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        public Toy(string name)
            : base(name)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        public Toy(string name, string productArticle)
            : base(name, productArticle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Toy(string name, string productArticle, double weight)
            : base(name, productArticle, weight)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="guaranteePeriod">The guarantee period.</param>
        public Toy(string name, string productArticle, double weight, DateTime guaranteePeriod)
            : base(name, productArticle, weight)
        {
            this.GuaranteePeriod = guaranteePeriod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="guaranteePeriod">The guarantee period.</param>
        /// <param name="color">The color.</param>
        public Toy(string name, string productArticle, double weight, DateTime guaranteePeriod, ToyColor color)
            : base(name, productArticle, weight)
        {
            this.GuaranteePeriod = guaranteePeriod;
            this.ToyColor = color;
        }
        #endregion

        #region Overridden methods

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.GetInfo());
            sb.AppendLine(String.Format("Guarantee period: {0:yyyy-MM-dd}", GuaranteePeriod));
            sb.AppendLine(String.Format("Toy color: {0}", ToyColor));
            return sb.ToString();
        }

        #endregion
    }
}
