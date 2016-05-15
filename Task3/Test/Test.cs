using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATS;
using BillingSystem;
using NUnit.Framework;

namespace Test
{
    public class Test
    {
        private string GenerateNumber(int i )
        {
            return String.Format("{0}{0}{0}", i);
        }

        [Test]
        public void BillingTest()
        {
            DateTime startDate = new DateTime(2016, 05, 01, 09, 00, 00);
            DateTime endDate = new DateTime(2016, 06, 30, 22, 00, 00);

            StaticTime.CurrentTime = startDate;

            TariffStandart tariffStandart = new TariffStandart("Standart", 1.5, 20);
            TariffLight tariffLight = new TariffLight("Discount 10", 1.1, 25, 10, 25);
            TariffSpecial tariffSpecial = new TariffSpecial("Talk more than 3", 1.2, 30, 3, 100);

            int pauseBetweenAnswer = 10; //sec
            int pauseBetweenDrop = 5; //min
            int pauseBetweenNextCall = 15; //day


            BillingSystem.BillingSystem billing = new BillingSystem.BillingSystem();
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
            billing.GenerateBlackListEvent += station.OnBlackListReceived;

            ITerminal terminal1 = new Terminal();
            terminal1.Id = 1;
            station.AddTerminal(client1_Contract1.PhoneNumber, terminal1);

            ITerminal terminal2 = new Terminal();
            terminal2.Id = 2;
            station.AddTerminal(client2_Contract1.PhoneNumber, terminal2);

            ITerminal terminal3 = new Terminal();
            terminal3.Id = 3;
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
            TariffStandart tariffStandart = new TariffStandart("Standart", 1.5, 20);
            TariffLight tariffLight = new TariffLight("Discount 10", 1.1, 25, 10, 25);
            TariffSpecial tariffSpecial = new TariffSpecial("Talk more than 3", 1.2, 30, 3, 100);

            

            BillingSystem.BillingSystem billing = new BillingSystem.BillingSystem();
            billing.TariffPlans.Add(tariffStandart);
            billing.TariffPlans.Add(tariffLight);
            billing.TariffPlans.Add(tariffSpecial);

            Client client1 = billing.AddClient("1", "Client 1");
            client1.AddContract("1", "000", tariffStandart);

            Client client2 = billing.AddClient("2", "Client 2");
            client2.AddContract("1", "111", tariffLight);

            Client client3 = billing.AddClient("3", "Client 3");
            client3.AddContract("1", "222", tariffSpecial);

            Station station = new Station(10);
            
            station.CallCompletedEvent += billing.OnCallComleted;
            billing.GenerateBlackListEvent += station.OnBlackListReceived;
            

            List<ITerminal> terminals = new List<ITerminal>();
            for (int i = 0; i < 30; i++)
            {
                ITerminal terminal = new Terminal();
                terminal.Id = i;
                terminals.Add(terminal);
                station.AddTerminal(GenerateNumber(i), terminal);
            }

            Random r = new Random();

            int j = 0;
            for (int i = 0; i < 1; i++)
            {
                ITerminal terminal1 = terminals[j];
                ITerminal terminal2 = terminals[j + 1];
                ITerminal terminal3 = terminals[j + 2];

                terminal1.Call(GenerateNumber(j + 1));
                Thread.Sleep(r.Next(100, 3000));
                terminal3.Call(GenerateNumber(j + 1));
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(3, station.GetPortCountInUse());

                IPort port1 = station.GetPortByTerminal(terminal1);
                IPort port2 = station.GetPortByTerminal(terminal2);
                IPort port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(PortState.Busy, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Drop();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.Busy, port3.PortState);

                terminal2.Drop();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(0, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Free, port2.PortState);
                Assert.AreEqual(PortState.Free, port3.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                Thread.Sleep(r.Next(100, 3000));
                port1 = station.GetPortByTerminal(terminal1);
                port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);
                
                terminal1.Drop();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(0, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Free, port2.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                Thread.Sleep(r.Next(100, 3000));
                terminal2.Call(GenerateNumber(j + 2));
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(3, station.GetPortCountInUse());

                port1 = station.GetPortByTerminal(terminal1);
                port2 = station.GetPortByTerminal(terminal2);
                port3 = station.GetPortByTerminal(terminal3);
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal1.Drop();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal2.Drop();
                Thread.Sleep(r.Next(100, 3000));
                Assert.AreEqual(0, station.GetPortCountInUse());
                j += 2;
            }

            //ITerminal terminal1 = terminals[0];
            //ITerminal terminal2 = terminals[1];
            //ITerminal terminal3 = terminals[2];
            //ITerminal terminal4 = terminals[3];
            //ITerminal terminal5 = terminals[4];
            //ITerminal terminal6 = terminals[5];
            //ITerminal terminal7 = terminals[6];
            //ITerminal terminal8 = terminals[7];
            //ITerminal terminal9 = terminals[8];

            //// 1 --> 2
            //terminal1.Call(GenerateNumber(2));
            //IPort port1 = station.GetPortByTerminal(terminal1);
            //IPort port2 = station.GetPortByTerminal(terminal2);

            //Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
            //Assert.AreEqual(PortState.IncomingCall, port2.PortState);

            //terminal2.Answer();

            //// 3 --> 2
            //terminal3.Call(GenerateNumber(2));
            //IPort port3 = station.GetPortByTerminal(terminal3);

            //Assert.AreEqual(PortState.Busy, port1.PortState);
            //Assert.AreEqual(PortState.Busy, port2.PortState);

            //terminal1.Drop();
            //Assert.AreEqual(PortState.Free, port1.PortState);
            //Assert.AreEqual(PortState.IncomingCall, port2.PortState);
            //Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

            //// 1 --> 3
            //terminal1.Call(GenerateNumber(3));
            //Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
            //Assert.AreEqual(PortState.IncomingCall, port2.PortState);
            //Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

            //terminal2.Answer();
            //Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
            //Assert.AreEqual(PortState.Busy, port2.PortState);
            //Assert.AreEqual(PortState.Busy, port3.PortState);
        }
    }
}
