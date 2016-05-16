namespace BillingSystem.Classes
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    #endregion

    /// <summary>
    /// Class used for representing some person who can get one or more phone numbers.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the passport identifier.
        /// </summary>
        /// <value>
        /// The _passport identifier.
        /// </value>
        private string _passportId { get; set; }
        
        /// <summary>
        /// Gets or sets the _name.
        /// </summary>
        /// <value>
        /// The _name.
        /// </value>
        private string _name { get; set; }

        /// <summary>
        /// The contracts.
        /// </summary>
        public ICollection<IContract> Contracts;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Client(string id, string name)
        {
            Contracts = new List<IContract>();
            _passportId = id;
            _name = name;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the contract.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="tariffPlan">The tariff plan.</param>
        public void AddContract(string id, string phoneNumber, ITariffPlan tariffPlan)
        {
            IContract contract = new Contract(id, phoneNumber);
            contract.ChangeTariff(tariffPlan);
            Contracts.Add(contract);
        }

        /// <summary>
        /// Gets the contract by number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public IContract GetContractByNumber(string number)
        {
            return Contracts.FirstOrDefault(c => c.PhoneNumber.Equals(number));
        }

        #endregion
    }
}
