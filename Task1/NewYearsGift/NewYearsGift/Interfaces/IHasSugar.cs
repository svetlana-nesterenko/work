namespace NewYearsGift.Interfaces
{
    /// <summary>
    /// Interface used for the gifts elements which have a sugar (candies, KinderSurprise).
    /// </summary>
    public interface IHasSugar
    {
        /// <summary>
        /// Gets the content of the sugar.
        /// </summary>
        /// <value>
        /// The content of the sugar.
        /// </value>
        double SugarContent { get; }
    }
}
