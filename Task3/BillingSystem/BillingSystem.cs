﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS;

namespace BillingSystem
{
    public class BillingSystem
    {
        private readonly ICollection<Client> _clientsCollection;

        public ICollection<ITariffPlan> TariffPlans;


        public BillingSystem()
        {
            _clientsCollection = new List<Client>();
            TariffPlans = new List<ITariffPlan>();
        }


        public void OnCallComleted(object sender, CallInfo callInfo)
        {
            IContract contract = _clientsCollection.SelectMany(c => c.Contracts).FirstOrDefault(c => c.PhoneNumber.Equals(callInfo.MyNumber));
            if (contract != null)
            {
                contract.AddCallRecord(callInfo.CallType == CallType.Ingoing,
                    callInfo.CallResultType == CallInfoResultType.Success, callInfo.StartDate ?? callInfo.CallDate, callInfo.EndDate,
                    callInfo.Number);
            }
        }

        public Client AddClient(string id, string name)
        {
            Client client = new Client(id, name);
            _clientsCollection.Add(client);
            return client;
        }

        public IContract FindContractByNumber(string number)
        {
            IContract contract = _clientsCollection.SelectMany(c => c.Contracts).FirstOrDefault(c => c.PhoneNumber.Equals(number));
            return contract;
        }
    }
}
