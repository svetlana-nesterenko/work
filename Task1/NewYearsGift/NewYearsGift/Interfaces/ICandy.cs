namespace NewYearsGift.Interfaces
{
    #region Usings
    using NewYearsGift.Enumerations;
    #endregion

    /// <summary>
    /// Interface used for any kind of sweets. 
    /// </summary>
    public interface ICandy
    {
        /// <summary>
        /// Gets or sets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        double SugarContent { get; set; }
        
        /// <summary>
        /// Gets or sets the category of sweets from enumeration CandyCategory.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        CandyCategory Category { get; set; }
        
        /// <summary>
        /// Gets or sets the flavor of sweets from enumeration CandyFlavor.
        /// </summary>
        /// <value>
        /// The flavor of candy.
        /// </value>
        CandyFlavor CandyFlavor { get; set; }
    }
}
