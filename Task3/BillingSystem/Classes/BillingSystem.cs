namespace BillingSystem.Classes
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using ATS.Classes;
    using ATS.Enum;
    using Interfaces;

    #endregion

    /// <summary>
    /// This class represents the billing system.
    /// </summary>
    public class BillingSystem
    {
        /// <summary>
        /// The collection of clients.
        /// </summary>
        private readonly ICollection<Client> _clientsCollection;

        /// <summary>
        /// The collection of tariff plans.
        /// </summary>
        public ICollection<ITariffPlan> TariffPlans;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingSystem"/> class.
        /// </summary>
        public BillingSystem()
        {
            _clientsCollection = new List<Client>();
            TariffPlans = new List<ITariffPlan>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when call comleted.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="callInfo">The call information.</param>
        public virtual void OnCallComleted(object sender, CallInfo callInfo)
        {
            IContract contract = _clientsCollection.SelectMany(c => c.Contracts).FirstOrDefault(c => c.PhoneNumber.Equals(callInfo.MyNumber));
            if (contract != null)
            {
                contract.AddCallRecord(callInfo.CallType == CallType.Ingoing,
                    callInfo.CallResultType == CallInfoResultType.Success, callInfo.StartDate ?? callInfo.CallDate, callInfo.EndDate,
                    callInfo.Number);
            }
        }

        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Client AddClient(string id, string name)
        {
            Client client = new Client(id, name);
            _clientsCollection.Add(client);
            return client;
        }

        /// <summary>
        /// Finds the contract by number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public IContract FindContractByNumber(string number)
        {
            IContract contract = _clientsCollection.SelectMany(c => c.Contracts).FirstOrDefault(c => c.PhoneNumber.Equals(number));
            return contract;
        }
        
        #endregion
    }
}
