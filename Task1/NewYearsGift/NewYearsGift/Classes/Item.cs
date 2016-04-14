namespace NewYearsGift.Classes
{
    #region Usings

    using System.Xml.Serialization;
    using NewYearsGift.Interfaces;

    #endregion

    /// <summary>
    /// Class used for representing candies, fruits, box and etc.
    /// There is an delegate which will be called when weight of element changes.
    /// </summary>
    /// <seealso cref="NewYearsGift.Interfaces.IItem" />
    public abstract class Item : IItem
    {
        /// <summary>
        /// The weight
        /// </summary>
        private double _Weight;


        /// <summary>
        /// The delegate when the weight is changed.
        /// </summary>
        public delegate void WeightChanged();


        /// <summary>
        /// Gets or sets the delegate which will be called on every weight changing.
        /// </summary>
        /// <value>
        /// The on weight changed.
        /// </value>
        [XmlIgnore]
        public WeightChanged OnWeightChanged { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            
        }

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
        public double Weight
        {
            get { return this._Weight; }
            set
            {
                if (value != this._Weight)
                {
                    this._Weight = value;
                    if (OnWeightChanged != null)
                    {
                        OnWeightChanged();
                    }
                }
            }
        }
    }
}
