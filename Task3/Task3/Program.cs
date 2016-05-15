using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS;
using ATS.Classes;
using ATS.Interfaces;
using BillingSystem;
using BillingSystem.Classes;
using BillingSystem.Classes.Tariffs;
using BillingSystem.Interfaces;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2016, 05, 01, 09, 00, 00);
            DateTime endDate = new DateTime(2016, 06, 30, 22, 00, 00);

            StaticTime.CurrentTime = startDate;

            TariffStandart tariffStandart = new TariffStandart("Standart", 1.5, 20);
            TariffLight tariffLight = new TariffLight("Discount 10", 1.1, 25, 10, 25);
            TariffSpecial tariffSpecial = new TariffSpecial("Talk more than 3", 1.2, 30, 3, 100);

            BillingSystem.Classes.BillingSystem billing = new BillingSystem.Classes.BillingSystem();
            billing.TariffPlans.Add(tariffStandart);
            billing.TariffPlans.Add(tariffLight);
            billing.TariffPlans.Add(tariffSpecial);

            Client client1 = billing.AddClient("1", "Client 1");
            string client1_number = "111111";
            client1.AddContract("1", client1_number, tariffStandart);
            IContract client1_Contract1 = client1.GetContractByNumber(client1_number);

            Client client2 = billing.AddClient("2", "Client 2");
            string client2_number = "222222";
            client2.AddContract("1", client2_number, tariffLight);
            IContract client2_Contract1 = client2.GetContractByNumber(client2_number);

            Station station = new Station(10);
            station.CallCompletedEvent += billing.OnCallComleted;

            ITerminal terminal1 = new Terminal(1);
            terminal1.Plug();
            station.AddTerminal(client1_Contract1.PhoneNumber, terminal1);

            ITerminal terminal2 = new Terminal(2);
            terminal2.Plug();
            station.AddTerminal(client2_Contract1.PhoneNumber, terminal2);

            terminal1.Call(client2_number);
            StaticTime.AddSeconds(5);
            terminal2.Answer();
            StaticTime.AddMinutes(5);
            terminal2.Drop();

            StaticTime.CurrentTime = startDate.AddDays(1);
            terminal2.Call(client1_number);
            terminal1.Answer();
            StaticTime.AddMinutes(5);
            terminal1.Drop();

            StaticTime.CurrentTime = startDate.AddDays(2);
            terminal2.Call(client1_number);
            terminal1.Answer();
            StaticTime.AddMinutes(10);
            terminal2.Drop();

            Console.WriteLine(client1_Contract1.GenerateInvoice(startDate, endDate));
            Console.WriteLine("**************");
            Console.WriteLine(client2_Contract1.GenerateInvoice(startDate, endDate));

            Console.ReadKey();
        }
    }
}
