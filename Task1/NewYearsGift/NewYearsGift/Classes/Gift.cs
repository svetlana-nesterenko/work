namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Linq;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// This class represents the New Year's gift which consists from IItems collection (toys, candies and etc.).
    /// </summary>
    [Serializable]
    public class Gift
    {
        #region Private Fields

        /// <summary>
        /// This flag indicates that the weight of the gift should be recalculated according to items change.
        /// </summary>
        private bool _SomethingChanged;

        /// <summary>
        /// The weight.
        /// </summary>
        private double _Weight;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the gift.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product article.
        /// </summary>
        /// <value>
        /// The product article.
        /// </value>
        public string ProductArticle { get; set; }

        /// <summary>
        /// Gets the weight of the gift.
        /// Counts the weight of the gift if any item was added, removed or changed.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [XmlIgnore]
        public double Weight
        {
            get
            {
                if (_SomethingChanged)
                {
                    _Weight = Items.Select(i => i.Weight).Sum();
                }
                return _Weight;
            }
        }

        /// <summary>
        /// Gets or sets the items (cadies, fruits and etc.).
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public CustomList<Item> Items { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Gift"/> class.
        /// </summary>
        public Gift()
        {
            _SomethingChanged = false;
            Items = new CustomList<Item>();
            Items.OnAdded += NewItemWasAdded;
            Items.OnRemoved += NewItemWasRemoved;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fires when new item was added.
        /// </summary>
        /// <param name="item">The item.</param>
        private void NewItemWasAdded(Item item)
        {
            _SomethingChanged = true;
            item.OnWeightChanged += ItemWasChanged;
        }

        /// <summary>
        /// Fires when some item was removed.
        /// </summary>
        private void NewItemWasRemoved()
        {
            _SomethingChanged = true;
        }

        /// <summary>
        /// Fires when some item was changed.
        /// </summary>
        private void ItemWasChanged()
        {
            _SomethingChanged = true;
        }

        #endregion


    }
}
