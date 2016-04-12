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
        public delegate void CollectionChangedDelegate();

        [XmlIgnore]
        public CollectionChangedDelegate OnChanged { get; set; }

        #region Overrided Methods
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(T item)
        {
            base.Add(item);
            if (OnChanged != null)
            {
                OnChanged();
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
            if (OnChanged != null)
            {
                OnChanged();
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
            if (OnChanged != null)
            {
                OnChanged();
            }
        }
        #endregion
    }
}
