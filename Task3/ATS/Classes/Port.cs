namespace ATS.Classes
{
    #region Usings
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ATS.Enum;
    using ATS.Interfaces;

    #endregion

    /// <summary>
    /// Class represents the port.
    /// </summary>
    /// <seealso cref="ATS.Interfaces.IPort" />
    public class Port : IPort
    {
        #region Private Fields

        /// <summary>
        /// The terminal.
        /// </summary>
        private ITerminal _terminal;

        /// <summary>
        /// The phone number.
        /// </summary>
        private string _number;

        /// <summary>
        /// The mapping collection of incoming number and port.
        /// </summary>
        private readonly ICollection<KeyValuePair<string, IPort>> _incomingNumbers;

        /// <summary>
        /// The current remote phone number.
        /// </summary>
        private string _currentRemoteNumber;

        /// <summary>
        /// The current remote port.
        /// </summary>
        private IRemoteEndpoint _currentRemoteEndpoint;

        /// <summary>
        /// The call information.
        /// </summary>
        private CallInfo _callInfo;

        /// <summary>
        /// The port state.
        /// </summary>
        private PortState _portState;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the identifier of port.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the state of the port.
        /// </summary>
        /// <value>
        /// The state of the port.
        /// </value>
        public PortState PortState
        {
            get { return _portState; }
            set
            {
                _portState = value;
                if (OnPortStateChangedEvent != null)
                {
                    OnPortStateChangedEvent(this, value);
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Fires when port changed state.
        /// </summary>
        public event EventHandler<PortState> OnPortStateChangedEvent;

        /// <summary>
        /// Fires when port generates outgoing call with remote port.
        /// </summary>
        public event EventHandler<string> OnOutgoingCallEvent;

        /// <summary>
        /// Fires when terminal drops phone.
        /// </summary>
        public event EventHandler DropCallEvent;

        /// <summary>
        /// Fires when port get outgoing call.
        /// </summary>
        public event EventHandler<string> OnPrepareOutgoingCallEvent;

        /// <summary>
        /// Fires when port or commutator get incoming call from terminal.
        /// </summary>
        public event EventHandler IncomingCallAcceptedEvent;

        /// <summary>
        /// Fires when port gets free state.
        /// </summary>
        public event EventHandler FinishedEvent;

        /// <summary>
        /// Fires when current or remote terminal drop phone.
        /// </summary>
        public event EventHandler<CallInfo> CallCompletedEvent;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Port"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Port(int id)
        {
            Id = id;
            _portState = PortState.Free;
            _incomingNumbers = new List<KeyValuePair<string, IPort>>();
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets incoming call.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="number">The number.</param>
        public virtual void OnIncomingCall(object sender, string number)
        {
            if (sender is IPort)
            {
                IPort remotePort = sender as IPort;
                remotePort.OnOutgoingCallEvent -= OnIncomingCall;
                if (PortState == PortState.Free)
                {
                    _currentRemoteEndpoint = remotePort;
                    _currentRemoteNumber = number;
                    PortState = PortState.IncomingCall;
                    _terminal.IncomingCall(number);
                    IncomingCallAcceptedEvent += _currentRemoteEndpoint.OnIncomingCallAccepted;
                    _currentRemoteEndpoint.DropCallEvent += OnRemoteDrop;
                    _callInfo = new CallInfo(number, CallType.Ingoing);
                }
                else
                {
                    OnPortStateChangedEvent += PortStateChanged;
                    _incomingNumbers.Add(new KeyValuePair<string, IPort>(number, sender as IPort));
                }
            }
        }


        /// <summary>
        /// Sets incoming connection.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public virtual void OnIncomingCallAccepted(object sender, EventArgs e)
        {
            _callInfo.StartDate = StaticTime.CurrentTime;
            PortState = PortState.Busy;
            IPort remotePort = sender as IPort;
            if (remotePort != null)
            {
                _currentRemoteEndpoint = remotePort;
                IncomingCallAcceptedEvent -= remotePort.OnIncomingCallAccepted;
            }
        }

        /// <summary>
        /// Reacts to the remote terminal drop.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        public virtual void OnRemoteDrop(object sender, EventArgs e)
        {
            IPort remotePort = sender as IPort;
            if (remotePort != null)
            {
                remotePort.DropCallEvent -= OnRemoteDrop;
                remotePort.OnOutgoingCallEvent -= OnIncomingCall;
                _terminal.SetPickUpPhone(false);
            }

            if (OnPortStateChangedEvent == null)
            {
                OnPortStateChangedEvent += PortStateChanged;
            }

            _callInfo.EndDate = StaticTime.CurrentTime;
            _callInfo.CallResultType = PortState == PortState.Busy
                ? CallInfoResultType.Success
                : CallInfoResultType.Unanswered;
            if (CallCompletedEvent != null)
            {
                CallCompletedEvent(this, _callInfo);
            }

            PortState = PortState.Free;
            OnPortStateChangedEvent -= PortStateChanged;
        }

        /// <summary>
        /// Attach terminal.
        /// </summary>
        /// <param name="terminal">The terminal.</param>
        /// <param name="number">The number.</param>
        public void WorkWithTerminal(ITerminal terminal, string number)
        {
            _number = number;
            _terminal = terminal;
            _terminal.OutgoingCallEvent += OnTerminalOutgoingCall;
            _terminal.PrepareOutgoingCallEvent += OnTerminalPrepareOutgoingCall;
            _terminal.DropEvent += OnTerminalDrop;
            _terminal.AnswerEvent += OnTerminalAnswer;
        }

        /// <summary>
        /// Clears all events.
        /// </summary>
        public void ClearAllEvents()
        {
            if (_terminal != null)
            {
                _terminal.OutgoingCallEvent -= OnTerminalOutgoingCall;
                _terminal.PrepareOutgoingCallEvent -= OnTerminalPrepareOutgoingCall;
                _terminal.DropEvent -= OnTerminalDrop;
                _terminal.AnswerEvent -= OnTerminalAnswer;
                _terminal = null;
            }

            if (_currentRemoteEndpoint != null)
            {
                _currentRemoteEndpoint.DropCallEvent -= OnRemoteDrop;
                _currentRemoteEndpoint.OnOutgoingCallEvent -= OnIncomingCall;
                IncomingCallAcceptedEvent -= _currentRemoteEndpoint.OnIncomingCallAccepted;
                DropCallEvent -= _currentRemoteEndpoint.OnRemoteDrop;
                _currentRemoteEndpoint = null;
            }

            OnPortStateChangedEvent -= PortStateChanged;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Changes state.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="state">The state.</param>
        protected virtual void PortStateChanged(object sender, PortState state)
        {
            if (state == PortState.Free)
            {
                OnPortStateChangedEvent -= PortStateChanged;
                if (_currentRemoteEndpoint != null)
                {
                    _currentRemoteEndpoint.DropCallEvent -= OnRemoteDrop;
                    _currentRemoteEndpoint.OnOutgoingCallEvent -= OnIncomingCall;
                    IncomingCallAcceptedEvent -= _currentRemoteEndpoint.OnIncomingCallAccepted;
                }

                if (_incomingNumbers.Count > 0)
                {
                    KeyValuePair<string, IPort> keyValue = _incomingNumbers.FirstOrDefault();
                    _currentRemoteNumber = keyValue.Key;
                    _currentRemoteEndpoint = keyValue.Value;

                    _incomingNumbers.Remove(keyValue);

                    IncomingCallAcceptedEvent += _currentRemoteEndpoint.OnIncomingCallAccepted;
                    _currentRemoteEndpoint.DropCallEvent += OnRemoteDrop;

                    PortState = PortState.IncomingCall;
                    _terminal.IncomingCall(_currentRemoteNumber);
                    _callInfo = new CallInfo(_currentRemoteNumber, CallType.Ingoing);
                }
                else
                {
                    if (FinishedEvent != null)
                    {
                        FinishedEvent(this, null);
                    }
                }
            }
        }

        /// <summary>
        /// Gets outgoing call.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="number">The number.</param>
        protected virtual void OnTerminalPrepareOutgoingCall(object sender, string number)
        {
            PortState = PortState.OutgoingCall;
            if (OnPrepareOutgoingCallEvent != null)
            {
                OnPrepareOutgoingCallEvent(this, number);
            }
        }

        /// <summary>
        /// Generates outgoing connection with remote port.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="number">The number.</param>
        protected virtual void OnTerminalOutgoingCall(object sender, string number)
        {
            _callInfo = new CallInfo(number, CallType.Outgoing);
            if (OnOutgoingCallEvent != null)
            {
                OnOutgoingCallEvent(this, _number);
            }
            else
            {
                _terminal.Drop();
            }
        }

        /// <summary>
        /// Reacts to drop phone by terminal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnTerminalDrop(object sender, EventArgs e)
        {
            if (DropCallEvent != null)
            {
                DropCallEvent(this, null);
            }

            if (OnPortStateChangedEvent == null)
            {
                OnPortStateChangedEvent += PortStateChanged;
            }

            _callInfo.EndDate = StaticTime.CurrentTime;
            _callInfo.CallResultType = PortState == PortState.Busy
                ? CallInfoResultType.Success
                : CallInfoResultType.Unanswered;
            if (CallCompletedEvent != null)
            {
                CallCompletedEvent(this, _callInfo);
            }
            PortState = PortState.Free;
            OnPortStateChangedEvent -= PortStateChanged;
        }

        /// <summary>
        /// Generates connection with port.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnTerminalAnswer(object sender, EventArgs e)
        {
            _callInfo.StartDate = StaticTime.CurrentTime;
            PortState = PortState.Busy;
            if (IncomingCallAcceptedEvent != null)
            {
                IncomingCallAcceptedEvent(this, null);
                IncomingCallAcceptedEvent -= _currentRemoteEndpoint.OnIncomingCallAccepted;
                DropCallEvent += _currentRemoteEndpoint.OnRemoteDrop;
            }
        }

        #endregion
    }
}
