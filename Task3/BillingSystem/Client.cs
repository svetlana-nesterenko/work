using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem
{
    public class Client
    {
        private string _passportId { get; set; }
        private string _name { get; set; }

        public ICollection<IContract> Contracts;

        public Client(string id, string name)
        {
            Contracts = new List<IContract>();
            _passportId = id;
            _name = name;
        }

        public void AddContract(string id, string phoneNumber, ITariffPlan tariffPlan)
        {
            IContract contract = new Contract(id, phoneNumber, tariffPlan);
            contract.ChangeTariff(tariffPlan);
            Contracts.Add(contract);
        }

        public IContract GetContractByNumber(string number)
        {
            return Contracts.FirstOrDefault(c => c.PhoneNumber.Equals(number));
        }
    }
}
