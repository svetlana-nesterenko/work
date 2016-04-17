using System.Text;

namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using NewYearsGift.Enumerations;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class used to describe the elements of gift which combine candy and toy. 
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public class KinderSurprise : Item, IToy, IHasSugar
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
            get { return String.Format("Kinder surprise '{0}'", base.Name); }
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

        /// <summary>
        /// Gets or sets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        public double SugarContent { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        public KinderSurprise()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        public KinderSurprise(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        public KinderSurprise(string name, string productArticle)
            : base(name, productArticle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public KinderSurprise(string name, string productArticle, double weight)
            : base(name, productArticle, weight)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="guaranteePeriod">The guarantee period.</param>
        public KinderSurprise(string name, string productArticle, double weight, DateTime guaranteePeriod)
            : base(name, productArticle, weight)
        {
            this.GuaranteePeriod = guaranteePeriod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="guaranteePeriod">The guarantee period.</param>
        /// <param name="toyColor">Color of the toy.</param>
        public KinderSurprise(string name, string productArticle, double weight, DateTime guaranteePeriod, ToyColor toyColor)
            : base(name, productArticle, weight)
        {
            this.GuaranteePeriod = guaranteePeriod;
            this.ToyColor = toyColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinderSurprise"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="guaranteePeriod">The guarantee period.</param>
        /// <param name="toyColor">Color of the toy.</param>
        /// <param name="sugarContent">Content of the sugar.</param>
        public KinderSurprise(string name, string productArticle, double weight, DateTime guaranteePeriod, ToyColor toyColor, double sugarContent)
            : base(name, productArticle, weight)
        {
            this.GuaranteePeriod = guaranteePeriod;
            this.ToyColor = toyColor;
            this.SugarContent = sugarContent;
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
            sb.AppendLine(String.Format("Sugar: {0}", SugarContent));
            return sb.ToString();
        }

        #endregion
    }
}
