namespace Demo
{
    #region Usings

    using System;
    using NewYearsGift.Classes;
    using NewYearsGift.Enumerations;    

    #endregion

    /// <summary>
    /// Class used to create a gift for demonstration. 
    /// </summary>
    public class GiftBilder
    {
        private Gift _gift;

        /// <summary>
        /// Initializes a new instance of the <see cref="GiftBilder"/> class.
        /// </summary>
        /// <param name="gift">The gift.</param>
        public GiftBilder(Gift gift)
        {
            _gift = gift;
        }

        /// <summary>
        /// Builds the gift.
        /// </summary>
        public void Build()
        {
            CreatGift();
            AddItems();
        }

        /// <summary>
        /// Fills the field of the gift.
        /// </summary>
        protected void CreatGift()
        {
            _gift.Name = "Happy New Year!";
            _gift.ProductArticle = "AD45485481";
        }

        /// <summary>
        /// Adds the items of the gift.
        /// </summary>
        protected void AddItems()
        {
            _gift.Items.Add(new Candy(15) { CandyFlavor = CandyFlavor.Cherry, Category = CandyCategory.FruitJelly, ExpirationDate = new DateTime(2016, 10, 1), Name = "Веселые ребята", ProductArticle = "YR47583758", SugarContent = 100.2 });
            _gift.Items.Add(new Candy(100) { CandyFlavor = CandyFlavor.DarkChocolate, Category = CandyCategory.Chocolate, ExpirationDate = new DateTime(2016, 08, 15), Name = "Коммунарка", ProductArticle = "KP65454874", SugarContent = 156.5 });
            _gift.Items.Add(new Candy(36.3) { CandyFlavor = CandyFlavor.Stroberry, Category = CandyCategory.Biscuit, ExpirationDate = new DateTime(2016, 09, 21), Name = "Юбилейный", ProductArticle = "ET12454854", SugarContent = 124 });
            _gift.Items.Add(new Candy(18) { CandyFlavor = CandyFlavor.Orange, Category = CandyCategory.Caramel, ExpirationDate = new DateTime(2016, 06, 30), Name = "Баобарис", ProductArticle = "XV15748787", SugarContent = 239.6 });
            _gift.Items.Add(new Candy(100) { CandyFlavor = CandyFlavor.MilkChocolate, Category = CandyCategory.Chocolate, ExpirationDate = new DateTime(2016, 12, 16), Name = "Спартак", ProductArticle = "LH25787878", SugarContent = 202.3 });
            _gift.Items.Add(new Candy(20.9) { CandyFlavor = CandyFlavor.Stroberry, Category = CandyCategory.Marshmallow, ExpirationDate = new DateTime(2016, 07, 5), Name = "Чародейка", ProductArticle = "ND41545448", SugarContent = 299.9 });
            _gift.Items.Add(new Fruit(35.1) { ExpirationDate = new DateTime(2016, 06, 15), Name = "Мандарин", ProductArticle = "DH12154542" });
            _gift.Items.Add(new Toy(260) { GuaranteePeriod = new DateTime(2019, 12, 31), Name = "Медвежонок Винни", ProductArticle = "JE54544848", ToyColor = ToyColor.Blue });
            _gift.Items.Add(new Box(12.5) { Name = "Теремок", ProductArticle = "ZT45485748" });
        }
    }
}
