namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    /// This class represents the New Year's gift which consists from IItems collection (toys, candies and etc.).
    /// </summary>
    [Serializable]
    public class Gift: Item
    {
        #region Public Properties

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
            Items = new CustomList<Item>();
            Items.OnChanged += ItemsChanged;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Counts the weight of the gift if any item was added or removed.
        /// </summary>
        private void ItemsChanged()
        {
            Weight = Items.Select(i => i.Weight).Sum();
        }
        #endregion
    }
}
