namespace NewYearsGift.Classes
{
    #region Usings

    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    #endregion Usings

    /// <summary>
    /// Class used for representing the gifts elements. 
    /// Calls delegate on every collection changing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    [Serializable]
    public class CustomList<T> : List<T>
    {

        /// <summary>
        /// The delegate when adding an element
        /// </summary>
        /// <param name="item">The item.</param>
        public delegate void CollectionAddedDelegate( T item);

        /// <summary>
        /// Delegate when removing an element.
        /// </summary>
        public delegate void CollectionRemovedDelegate();

        /// <summary>
        /// Gets or sets the delegate which will be called when a new item is added to the collection.
        /// </summary>
        /// <value>
        /// The on added.
        /// </value>
        [XmlIgnore]
        public CollectionAddedDelegate OnAdded { get; set; }

        /// <summary>
        /// Gets or sets the delegate when an item is removed from the collection.
        /// </summary>
        /// <value>
        /// The on removed.
        /// </value>
        [XmlIgnore]
        public CollectionRemovedDelegate OnRemoved { get; set; }

        #region Overrided Methods

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(T item)
        {
            base.Add(item);
            if (OnAdded != null)
            {
                OnAdded(item);
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public new bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (OnRemoved != null)
            {
                OnRemoved();
            }
            return result;
        }

        /// <summary>
        /// Removes item at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (OnRemoved != null)
            {
                OnRemoved();
            }
        }

        #endregion
    }
}
