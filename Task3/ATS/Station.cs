using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class Station
    {
        private ICollection<ITerminal> _terminalCollection;
        private ICollection<IPort> _portCollection;
        private int _portCount;
        private IDictionary<ITerminal, IPort> _portMapping;
        private IDictionary<string, ITerminal> _terminalMapping;

        public Station(int portCount)
        {
            _terminalMapping = new Dictionary<string, ITerminal>();
            _portCount = portCount;
            _portCollection = new List<IPort>();
            for (int i = 0; i < portCount; i++)
            {
                _portCollection.Add(new Port());
            }
            _terminalCollection = new List<ITerminal>();
            _portMapping = new Dictionary<ITerminal, IPort>();
        }

        public int GetPortCountInUse()
        {
            return _portMapping.Count;
        }

        public bool AddTerminal(string number, ITerminal terminal)
        {
            if (!_terminalMapping.ContainsKey(number))
            {
                terminal.PingEvent += OnPingEvent;
                _terminalMapping.Add(number, terminal);
                return true;
            }
            return false;
        }

        public IPort GetPortByTerminal(ITerminal terminal)
        {
            if (_portMapping.ContainsKey(terminal))
            {
                return _portMapping[terminal];
            }
            return null;
        }

        public string GetNumberForTerminal(ITerminal terminal)
        {
            KeyValuePair<string, ITerminal> keyValue = _terminalMapping.FirstOrDefault(kv => kv.Value == terminal);
            return keyValue.Key;
        }

        protected void OnPortFinished(object sender, EventArgs e)
        {
            IPort port = sender as IPort;
            if (port != null)
            {
                port.FinishedEvent -= OnPortFinished;
                port.ClearAllEvents();
                KeyValuePair<ITerminal, IPort> keyValue = _portMapping.FirstOrDefault(kv => kv.Value == port);
                _portMapping.Remove(keyValue);
            }
        }

        protected void OnPingEvent(object sender, EventArgs e)
        {
            ITerminal terminal = sender as ITerminal;
            if (terminal != null)
            {
                if (!_portMapping.ContainsKey(terminal))
                {
                    IPort freePort = GetFreePort();
                    if (freePort != null)
                    {
                        freePort.Id = terminal.Id;
                        freePort.OnPrepareOutgoingCallEvent += OnPrepareOutgoingCallEvent;
                        freePort.WorkWithTerminal(terminal, GetNumberForTerminal(terminal));
                        freePort.FinishedEvent += OnPortFinished;
                        _portMapping.Add(terminal, freePort);
                    }
                }
            }
        }

        protected void OnPrepareOutgoingCallEvent(object sender, string number)
        {
            IPort sourcePort = sender as IPort;
            if (sourcePort != null)
            {
                if (_terminalMapping.ContainsKey(number))
                {
                    ITerminal terminal = _terminalMapping[number];
                    IPort port = null;
                    if (_portMapping.ContainsKey(terminal))
                    {
                        port = _portMapping[terminal];
                    }
                    else
                    {
                        port = GetFreePort();
                        port.Id = terminal.Id;
                    }
                    
                    sourcePort.OnOutgoingCallEvent += port.OnIncomingCall;
                    if (!_portMapping.ContainsKey(terminal))
                    {
                        port.FinishedEvent += OnPortFinished;
                        _portMapping.Add(terminal, port);
                        port.WorkWithTerminal(terminal, number);
                    }
                }
            }
        }
    

        protected IPort GetFreePort()
        {
            return _portCollection.Except(_portMapping.Values).FirstOrDefault();
        }
    }
}
