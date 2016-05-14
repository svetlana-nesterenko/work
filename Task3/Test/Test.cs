using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS;
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
        public void Test1()
        {
            Station station = new Station(10);

            List<ITerminal> terminals = new List<ITerminal>();
            for (int i = 0; i < 30; i++)
            {
                ITerminal terminal = new Terminal();
                terminal.Id = i;
                terminals.Add(terminal);
                station.AddTerminal(GenerateNumber(i), terminal);
            }


            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                ITerminal terminal1 = terminals[j];
                ITerminal terminal2 = terminals[j + 1];
                ITerminal terminal3 = terminals[j + 2];

                terminal1.Call(GenerateNumber(j + 1));
                terminal3.Call(GenerateNumber(j + 1));

                IPort port1 = station.GetPortByTerminal(terminal1);
                IPort port2 = station.GetPortByTerminal(terminal2);
                IPort port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Assert.AreEqual(PortState.Busy, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Drop();
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port2.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port3.PortState);

                terminal2.Answer();
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Busy, port2.PortState);
                Assert.AreEqual(PortState.Busy, port3.PortState);

                terminal2.Drop();
                Assert.AreEqual(0, station.GetPortCountInUse());
                //Assert.AreEqual(PortState.Free, port1.PortState);
                //Assert.AreEqual(PortState.Free, port2.PortState);
                //Assert.AreEqual(PortState.Free, port3.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                port1 = station.GetPortByTerminal(terminal1);
                port3 = station.GetPortByTerminal(terminal3);

                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);
                
                terminal1.Drop();
                Assert.AreEqual(0, station.GetPortCountInUse());
                Assert.AreEqual(PortState.Free, port1.PortState);
                Assert.AreEqual(PortState.Free, port2.PortState);

                terminal1.Call(GenerateNumber(j + 2));
                terminal2.Call(GenerateNumber(j + 2));
                Assert.AreEqual(3, station.GetPortCountInUse());

                port1 = station.GetPortByTerminal(terminal1);
                port2 = station.GetPortByTerminal(terminal2);
                port3 = station.GetPortByTerminal(terminal3);
                Assert.AreEqual(PortState.OutgoingCall, port1.PortState);
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal1.Drop();
                Assert.AreEqual(2, station.GetPortCountInUse());
                Assert.AreEqual(PortState.OutgoingCall, port2.PortState);
                Assert.AreEqual(PortState.IncomingCall, port3.PortState);

                terminal2.Drop();
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
