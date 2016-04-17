namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Text;
    using NewYearsGift.Enumerations;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// The class represents the any kind of sweets.
    /// </summary>
    /// <seealso cref="NewYearsGift.Classes.Item" />
    /// <seealso cref="NewYearsGift.Interfaces.IHasSugar" />
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    /// <seealso cref="NewYearsGift.Interfaces.ICandy" />
    /// <seealso cref="NewYearsGift.Interfaces.IHasExpirationDate" />
    public class Candy : Item, ICandy, IHasExpirationDate, IHasSugar
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
            get { return String.Format("{0} '{1}'", this.Category, base.Name); }
        }

        /// <summary>
        /// Gets or sets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        public double SugarContent { get; set; }

        /// <summary>
        /// Gets or sets the category of sweets from enumeration CandyCategory.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public CandyCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the flavor of sweets from enumeration CandyFlavor.
        /// </summary>
        /// <value>
        /// The flavor of candy.
        /// </value>
        public CandyFlavor CandyFlavor { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>
        /// The expiration date.
        /// </value>
        public DateTime ExpirationDate { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        public Candy()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name"></param>
        public Candy(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        public Candy(string name, string productArticle)
            : base(name, productArticle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Candy(string name, string productArticle, double weight)
            : base(name, productArticle, weight)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Candy(string name, string productArticle, double weight, CandyFlavor candyFlavor)
            : base(name, productArticle, weight)
        {
            this.CandyFlavor = candyFlavor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Candy(string name, string productArticle, double weight, CandyFlavor candyFlavor, CandyCategory category)
            : base(name, productArticle, weight)
        {
            this.CandyFlavor = candyFlavor;
            this.Category = category;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Candy(string name, string productArticle, double weight, CandyFlavor candyFlavor, CandyCategory category, DateTime expirationDate)
            : base(name, productArticle, weight)
        {
            this.CandyFlavor = candyFlavor;
            this.Category = category;
            this.ExpirationDate = expirationDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="productArticle">The product article.</param>
        /// <param name="weight">The weight.</param>
        public Candy(string name, string productArticle, double weight, CandyFlavor candyFlavor, CandyCategory category, DateTime expirationDate, double sugar)
            : base(name, productArticle, weight)
        {
            this.CandyFlavor = candyFlavor;
            this.Category = category;
            this.ExpirationDate = expirationDate;
            this.SugarContent = sugar;
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
            sb.AppendLine(String.Format("Category: {0}", Category));
            sb.AppendLine(String.Format("CandyFlavor: {0}", CandyFlavor));
            sb.AppendLine(String.Format("ExpirationDate: {0:yyyy-MM-dd}", ExpirationDate));
            sb.AppendLine(String.Format("SugarContent: {0}", SugarContent));
            return sb.ToString();
        }

        #endregion
    }
}
