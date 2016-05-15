using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class Port : IPort
    {
        public int Id { get; set; }

        private ITerminal _terminal;
        private string _number;

        private PortState _portState;
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

        public event EventHandler<PortState> OnPortStateChangedEvent;
        public event EventHandler<string> OnOutgoingCallEvent;
        public event EventHandler DropCallEvent;
        public event EventHandler<string> OnPrepareOutgoingCallEvent;
        public event EventHandler IncomingCallAcceptedEvent;
        public event EventHandler FinishedEvent;
        public event EventHandler<CallInfo> CallCompletedEvent;


        private readonly ICollection<KeyValuePair<string, IPort>> _incomingNumbers;
        private string _currentRemoteNumber;
        private IRemoteEndpoint _currentRemoteEndpoint;

        private CallInfo _callInfo;

        public Port()
        {
            _portState = PortState.Free;
            _incomingNumbers = new List<KeyValuePair<string, IPort>>();
        }

        public void OnIncomingCall(object sender, string number)
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


        public void OnIncomingCallAccepted(object sender, EventArgs e)
        {
            _callInfo.StartDate = StaticTime.CurrentTime;
            PortState = PortState.Busy;
            IPort remotePort = sender as IPort;
            if (remotePort != null)
            {
                IncomingCallAcceptedEvent -= remotePort.OnIncomingCallAccepted;
                remotePort.DropCallEvent += OnRemoteDrop;
            }
        }

        public void OnRemoteDrop(object sender, EventArgs e)
        {
            IPort remotePort = sender as IPort;
            if (remotePort != null)
            {
                remotePort.DropCallEvent -= OnRemoteDrop;
                remotePort.OnOutgoingCallEvent -= OnIncomingCall;
            }

            if (OnPortStateChangedEvent == null)
            {
                OnPortStateChangedEvent += PortStateChanged;
            }

            _callInfo.EndDate = StaticTime.CurrentTime;
            _callInfo.CallResultType = PortState == PortState.Busy ? CallInfoResultType.Success : CallInfoResultType.Unanswered;
            if (CallCompletedEvent != null)
            {
                CallCompletedEvent(this, _callInfo);
            }

            PortState = PortState.Free;
            OnPortStateChangedEvent -= PortStateChanged;
        }

        protected void PortStateChanged(object sender, PortState state)
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

        public void OnTerminalPrepareOutgoingCall(object sender, string number)
        {
            PortState = PortState.OutgoingCall;
            if (OnPrepareOutgoingCallEvent != null)
            {
                OnPrepareOutgoingCallEvent(this, number);
            }
        }

        public void OnTerminalOutgoingCall(object sender, string number)
        {
            if (OnOutgoingCallEvent != null)
            {
                OnOutgoingCallEvent(this, _number);
            }
            _callInfo = new CallInfo(number, CallType.Outgoing);
        }

        public void OnTerminalDrop(object sender, EventArgs e)
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
            _callInfo.CallResultType = PortState == PortState.Busy ? CallInfoResultType.Success : CallInfoResultType.Unanswered;
            if (CallCompletedEvent != null)
            {
                CallCompletedEvent(this, _callInfo);
            }
            PortState = PortState.Free;
            OnPortStateChangedEvent -= PortStateChanged;
        }

        public void OnTerminalAnswer(object sender, EventArgs e)
        {
            _callInfo.StartDate = StaticTime.CurrentTime;
            PortState = PortState.Busy;
            if (IncomingCallAcceptedEvent != null)
            {
                _currentRemoteEndpoint.DropCallEvent += OnRemoteDrop;
                IncomingCallAcceptedEvent(this, null);
                IncomingCallAcceptedEvent -= _currentRemoteEndpoint.OnIncomingCallAccepted;
                DropCallEvent += _currentRemoteEndpoint.OnRemoteDrop;
            }
        }

        public void WorkWithTerminal(ITerminal terminal, string number)
        {
            _number = number;
            _terminal = terminal;
            _terminal.OutgoingCallEvent += OnTerminalOutgoingCall;
            _terminal.PrepareOutgoingCallEvent += OnTerminalPrepareOutgoingCall;
            _terminal.DropEvent += OnTerminalDrop;
            _terminal.AnswerEvent += OnTerminalAnswer;
        }

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
    }
}
