namespace ATS.Classes
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ATS.Interfaces;

    #endregion

    /// <summary>
    /// Class represents the automatic telephone station.
    /// </summary>
    public class Station
    {
        #region Private Fields

        /// <summary>
        /// The port collection.
        /// </summary>
        private readonly ICollection<IPort> _portCollection;

        /// <summary>
        /// The port count.
        /// </summary>
        private int _portCount;

        /// <summary>
        /// The port mapping.
        /// </summary>
        private readonly IDictionary<ITerminal, IPort> _portMapping;

        /// <summary>
        /// The terminal mapping.
        /// </summary>
        private readonly IDictionary<string, ITerminal> _terminalMapping;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Station"/> class.
        /// </summary>
        /// <param name="portCount">The port count.</param>
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

        #endregion

        #region Events

        /// <summary>
        /// Fires when port finished work with current connection.
        /// </summary>
        public event EventHandler<CallInfo> CallCompletedEvent;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the count of used port.
        /// </summary>
        /// <returns></returns>
        public int GetPortCountInUse()
        {
            return _portMapping.Count;
        }

        /// <summary>
        /// Adds the terminal.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="terminal">The terminal.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes the terminal.
        /// </summary>
        /// <param name="terminal">The terminal.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the port by terminal.
        /// </summary>
        /// <param name="terminal">The terminal.</param>
        /// <returns></returns>
        public IPort GetPortByTerminal(ITerminal terminal)
        {
            if (_portMapping.ContainsKey(terminal))
            {
                return _portMapping[terminal];
            }
            return null;
        }

        /// <summary>
        /// Gets the number for terminal.
        /// </summary>
        /// <param name="terminal">The terminal.</param>
        /// <returns></returns>
        public string GetNumberForTerminal(ITerminal terminal)
        {
            KeyValuePair<string, ITerminal> keyValue = _terminalMapping.FirstOrDefault(kv => kv.Value == terminal);
            return keyValue.Key;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Reacts when port finished work with current connection.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Attach port to terminal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Prepares outgoing call.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="number">The number.</param>
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


        /// <summary>
        /// Called when call completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="callInfo">The call information.</param>
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

        /// <summary>
        /// Gets the free port.
        /// </summary>
        /// <returns></returns>
        protected IPort GetFreePort()
        {
            return _portCollection.Except(_portMapping.Values).FirstOrDefault();
        }

        #endregion

    }
}
