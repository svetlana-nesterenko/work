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
            _gift.Add(new Candy("Веселые ребята", "YR47583758", 15, CandyFlavor.Cherry, CandyCategory.FruitJelly, new DateTime(2016, 10, 1), 120));
            _gift.Add(new Candy("Коммунарка", "KP65454874", 100, CandyFlavor.DarkChocolate, CandyCategory.Chocolate, new DateTime(2016, 08, 15), 156.5));
            _gift.Add(new Candy("Юбилейный", "ET12454854", 36.3,CandyFlavor.Stroberry, CandyCategory.Biscuit,new DateTime(2016, 09, 21), 124));
            _gift.Add(new Candy("Барбарис", "XV15748787", 18, CandyFlavor.Orange, CandyCategory.Caramel, new DateTime(2016, 06, 30),239.6));
            _gift.Add(new Candy("Спартак", "LH25787878", 100, CandyFlavor.MilkChocolate, CandyCategory.Chocolate, new DateTime(2016, 12, 16),202.3));
            _gift.Add(new Candy("Чародейка", "ND41545448", 20.9, CandyFlavor.Stroberry, CandyCategory.Marshmallow, new DateTime(2016, 07, 5), 299.9));
            _gift.Add(new Fruit("Мандарин", "DH12154542", 35.1,new DateTime(2016, 06, 15)));
            _gift.Add(new Toy("Медвежонок Винни", "JE54544848", 260, new DateTime(2019, 12, 31), ToyColor.Blue));
            _gift.Add(new Toy("Машинка", "WT14545454",250,new DateTime(2019,12,31),ToyColor.Welloy));
            _gift.Add(new KinderSurprise("Смешарики", "KL454574878", 200, new DateTime(2016,10,01),ToyColor.Pink, 360));
            _gift.Add(new Box("Теремок", "ZT45485748", 12.5));
        }
    }
}
