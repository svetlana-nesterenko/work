namespace ATS.Classes
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ATS.Interfaces;

    #endregion

    public class Station
    {
        private readonly ICollection<IPort> _portCollection;
        private int _portCount;
        private readonly IDictionary<ITerminal, IPort> _portMapping;
        private readonly IDictionary<string, ITerminal> _terminalMapping;

        public event EventHandler<CallInfo> CallCompletedEvent;


        public Station(int portCount)
        {
            _terminalMapping = new Dictionary<string, ITerminal>();
            _portCount = portCount;
            _portCollection = new List<IPort>();
            for (int i = 0; i < portCount; i++)
            {
                _portCollection.Add(new Port(i));
            }
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

        public bool RemoveTerminal(ITerminal terminal)
        {
            if (terminal != null)
            {
                string number = GetNumberForTerminal(terminal);
                if (number != null)
                {
                    terminal.Drop();
                    _terminalMapping.Remove(number);
                    return true;
                }
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

        protected virtual void OnPortFinished(object sender, EventArgs e)
        {
            IPort port = sender as IPort;
            if (port != null)
            {
                port.FinishedEvent -= OnPortFinished;
                port.CallCompletedEvent -= OnCallCompleted;
                port.OnPrepareOutgoingCallEvent -= OnPrepareOutgoingCallEvent;
                port.ClearAllEvents();
                KeyValuePair<ITerminal, IPort> keyValue = _portMapping.FirstOrDefault(kv => kv.Value == port);
                _portMapping.Remove(keyValue);
            }
        }

        protected virtual void OnPingEvent(object sender, EventArgs e)
        {
            ITerminal terminal = sender as ITerminal;
            if (terminal != null)
            {
                if (!_portMapping.ContainsKey(terminal))
                {
                    IPort freePort = GetFreePort();
                    if (freePort != null)
                    {
                        freePort.OnPrepareOutgoingCallEvent += OnPrepareOutgoingCallEvent;
                        freePort.WorkWithTerminal(terminal, GetNumberForTerminal(terminal));
                        freePort.FinishedEvent += OnPortFinished;
                        freePort.CallCompletedEvent += OnCallCompleted;

                        _portMapping.Add(terminal, freePort);
                    }
                }
            }
        }

        protected virtual void OnPrepareOutgoingCallEvent(object sender, string number)
        {
            IPort sourcePort = sender as IPort;
            if (sourcePort != null)
            {
                if (_terminalMapping.ContainsKey(number))
                {
                    ITerminal terminal = _terminalMapping[number];
                    if (!terminal.IsEnabled)
                    {
                        return;
                    }
                    IPort port = null;
                    if (_portMapping.ContainsKey(terminal))
                    {
                        port = _portMapping[terminal];
                    }
                    else
                    {
                        port = GetFreePort();
                        if (port == null)
                        {
                            return;
                        }
                        port.CallCompletedEvent += OnCallCompleted;
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


        protected virtual void OnCallCompleted(object sender, CallInfo callInfo)
        {
            IPort port = sender as IPort;
            if (port != null)
            {
                KeyValuePair<ITerminal, IPort> keyValue = _portMapping.FirstOrDefault(kv => kv.Value == port);
                string sourceNumber = GetNumberForTerminal(keyValue.Key);
                callInfo.MyNumber = sourceNumber;

                if (CallCompletedEvent != null)
                {
                    CallCompletedEvent(this, callInfo);
                }
            }
        }
    
        protected IPort GetFreePort()
        {
            return _portCollection.Except(_portMapping.Values).FirstOrDefault();
        }
    }
}
