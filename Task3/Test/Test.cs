namespace Test
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using ATS.Classes;
    using ATS.Enum;
    using ATS.Interfaces;
    using BillingSystem.Classes;
    using BillingSystem.Classes.Tariffs;
    using BillingSystem.Interfaces;
    using NUnit.Framework;

    #endregion

    public class Test
    {
        private string GenerateNumber(int i )
        {
            return String.Format("{0}{0}{0}", i);
        }

        [Test]
        public void BillingTest_2_clients()
        {
            DateTime startDate = new DateTime(2016, 05, 01, 09, 00, 00);
            DateTime endDate = new DateTime(2016, 06, 30, 22, 00, 00);

            StaticTime.CurrentTime = startDate;

            TariffStandart tariffStandart = new TariffStandart("Standart", 1.5, 20);
            TariffLight tariffLight = new TariffLight("Discount 10", 1.1, 25, 10, 25);
            TariffSpecial tariffSpecial = new TariffSpecial("Talk more than 3", 1.2, 30, 3, 100);

            BillingSystem billing = new BillingSystem();
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


            IEnumerable<HistoryRecordWithSumm> history_client1 = client1_Contract1.GetCallHistory(startDate, startDate.AddDays(1));
            Assert.AreEqual(tariffStandart.Cost * 5, history_client1.Sum(h => h.Cost));

            IEnumerable<HistoryRecordWithSumm> history1_client2 = client2_Contract1.GetCallHistory(startDate.AddDays(1), startDate.AddDays(2));
            Assert.AreEqual((tariffLight.Cost - tariffLight.Cost / 100 * tariffLight.Discount) * 5, history1_client2.Sum(h => h.Cost));

            IEnumerable<HistoryRecordWithSumm> history2_client2 = client2_Contract1.GetCallHistory(startDate.AddDays(2), startDate.AddDays(3));
            Assert.AreEqual(tariffLight.Cost * 5 + (tariffLight.Cost - tariffLight.Cost/100*tariffLight.Discount) * 5, history2_client2.Sum(h => h.Cost));
        }

        [Test]
        public void BillingTest_3_clients()
        {
            DateTime startDate = new DateTime(2016, 05, 01, 09, 00, 00);
            DateTime endDate = new DateTime(2016, 06, 30, 22, 00, 00);

            StaticTime.CurrentTime = startDate;

            TariffStandart tariffStandart = new TariffStandart("Standart", 1.5, 20);
            TariffLight tariffLight = new TariffLight("Discount 10", 1.1, 25, 10, 25);
            TariffSpecial tariffSpecial = new TariffSpecial("Talk more than 3", 1.2, 30, 3, 100);

            BillingSystem billing = new BillingSystem();
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

            Client client3 = billing.AddClient("3", "Client 3");
            string client3_number = "333333";
            client3.AddContract("1", client3_number, tariffSpecial);
            IContract client3_Contract1 = client3.GetContractByNumber(client3_number);

            Station station = new Station(10);
            station.CallCompletedEvent += billing.OnCallComleted;

            ITerminal terminal1 = new Terminal(1);
            terminal1.Plug();
            station.AddTerminal(client1_Contract1.PhoneNumber, terminal1);

            ITerminal terminal2 = new Terminal(2);
            terminal2.Plug();
            station.AddTerminal(client2_Contract1.PhoneNumber, terminal2);

            ITerminal terminal3 = new Terminal(3);
            terminal3.Plug();
            station.AddTerminal(client3_Contract1.PhoneNumber, terminal3);

            terminal1.Call(client2_number);

            StaticTime.AddSeconds(5);
            terminal3.Call(client2_number);

            StaticTime.AddSeconds(10);
            terminal2.Answer();

            StaticTime.AddMinutes(5);
            terminal2.Drop();
            
            StaticTime.AddSeconds(20);
            terminal2.Answer();
            StaticTime.AddMinutes(15);
            terminal2.Drop();

            StaticTime.AddDays(10);
            terminal1.Call(client3_number);
            StaticTime.AddMinutes(2);
            terminal1.Drop();

            StaticTime.AddDays(10);
            terminal1.Call(client3_number);
            StaticTime.AddMinutes(1);
            terminal2.Call(client3_number);

            StaticTime.AddMinutes(2);
            terminal1.Drop();
            StaticTime.AddSeconds(30);
            terminal2.Drop();

            Assert.AreEqual(3, client1_Contract1.GetCallHistory(startDate, endDate).Count());
            Assert.AreEqual(3, client2_Contract1.GetCallHistory(startDate, endDate).Count());
            Assert.AreEqual(4, client3_Contract1.GetCallHistory(startDate, endDate).Count());

            string client1_invoice = client1_Contract1.GenerateInvoice(startDate, endDate);
            string client2_invoice = client2_Contract1.GenerateInvoice(startDate, endDate);
            string client3_invoice = client3_Contract1.GenerateInvoice(startDate, endDate);

        }


        [Test]
        public void TestATS()
        {
            Station station = new Station(10);
            
            List<ITerminal> terminals = new List<ITerminal>();
            for (int i = 0; i < 30; i++)
            {
                ITerminal terminal = new Terminal(i);
                terminal.Plug();
                terminals.Add(terminal);
                station.AddTerminal(GenerateNumber(i), terminal);
            }

            Random r = new Random();

            int j = 0;
            for (int i = 0; i < 5; i++)
            {
                ITerminal terminal1 = terminals[j];
                ITerminal terminal2 = terminals[j + 1];
                ITerminal terminal3 = terminals[j + 2];

                terminal1.Call(GenerateNumber(j + 1));
                Assert.AreEqual(true, terminal2.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                terminal3.Call(GenerateNumber(j + 1));
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(3, station.GetPortCountInUse());

                IPort port1 = station.GetPortByTerminal(terminal1);
                IPort port2 = station.GetPortByTerminal(terminal2);
                IPort port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Assert.AreEqual(false, terminal2.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(PortState.Busy, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Drop();
                Assert.AreEqual(true, terminal2.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Assert.AreEqual(false, terminal2.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.Busy, port3.PortState);

                terminal2.Drop();
                Assert.AreEqual(false, terminal2.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(0, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Free, port2.PortState);
                Assert.AreEqual(PortState.Free, port3.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                Assert.AreEqual(true, terminal3.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                port1 = station.GetPortByTerminal(terminal1);
                port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);
                
                terminal1.Drop();
                Assert.AreEqual(false, terminal3.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(0, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Free, port2.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                Assert.AreEqual(true, terminal3.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                terminal2.Call(GenerateNumber(j + 2));
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(3, station.GetPortCountInUse());

                port1 = station.GetPortByTerminal(terminal1);
                port2 = station.GetPortByTerminal(terminal2);
                port3 = station.GetPortByTerminal(terminal3);
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal1.Drop();
                Assert.AreEqual(true, terminal3.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal2.Drop();
                Assert.AreEqual(false, terminal3.IsRinging);
                Thread.Sleep(r.Next(100, 300));
                Assert.AreEqual(0, station.GetPortCountInUse());
                j += 2;
            }
        }
    }
}
