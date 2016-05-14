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


        private readonly ICollection<KeyValuePair<string, IPort>> _incomingNumbers;
        private string _currentRemoteNumber;
        private IPort _currentRemotePort;

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
                    _currentRemotePort = remotePort;
                    _currentRemoteNumber = number;
                    PortState = PortState.IncomingCall;
                    _terminal.IncomingCall(number);
                    IncomingCallAcceptedEvent += _currentRemotePort.OnIncomingCallAccepted;
                    _currentRemotePort.DropCallEvent += OnRemoteDrop;
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
            PortState = PortState.Free;
            OnPortStateChangedEvent -= PortStateChanged;
        }

        protected void PortStateChanged(object sender, PortState state)
        {
            if (state == PortState.Free)
            {
                OnPortStateChangedEvent -= PortStateChanged;
                if (_currentRemotePort != null)
                {
                    _currentRemotePort.DropCallEvent -= OnRemoteDrop;
                    _currentRemotePort.OnOutgoingCallEvent -= OnIncomingCall;
                    IncomingCallAcceptedEvent -= _currentRemotePort.OnIncomingCallAccepted;
                }

                if (_incomingNumbers.Count > 0)
                {
                    KeyValuePair<string, IPort> keyValue = _incomingNumbers.FirstOrDefault();
                    _currentRemoteNumber = keyValue.Key;
                    _currentRemotePort = keyValue.Value;

                    _incomingNumbers.Remove(keyValue);

                    IncomingCallAcceptedEvent += _currentRemotePort.OnIncomingCallAccepted;
                    _currentRemotePort.DropCallEvent += OnRemoteDrop;

                    PortState = PortState.IncomingCall;
                    _terminal.IncomingCall(_currentRemoteNumber);
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
        }

        public void OnTerminalDrop(object sender, EventArgs e)
        {
            if (DropCallEvent != null)
            {
                //_currentRemotePort.OnOutgoingCallEvent -= OnIncomingCall;

                DropCallEvent(this, null);
                //DropCallEvent -= _currentRemotePort.OnRemoteDrop;

                if (OnPortStateChangedEvent == null)
                {
                    OnPortStateChangedEvent += PortStateChanged;
                }

                PortState = PortState.Free;
                OnPortStateChangedEvent -= PortStateChanged;
            }
        }

        public void OnTerminalAnswer(object sender, EventArgs e)
        {
            PortState = PortState.Busy;
            if (IncomingCallAcceptedEvent != null)
            {
                _currentRemotePort.DropCallEvent += OnRemoteDrop;
                IncomingCallAcceptedEvent(this, null);
                IncomingCallAcceptedEvent -= _currentRemotePort.OnIncomingCallAccepted;
                DropCallEvent += _currentRemotePort.OnRemoteDrop;
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

            if (_currentRemotePort != null)
            {
                _currentRemotePort.DropCallEvent -= OnRemoteDrop;
                _currentRemotePort.OnOutgoingCallEvent -= OnIncomingCall;
                IncomingCallAcceptedEvent -= _currentRemotePort.OnIncomingCallAccepted;
                DropCallEvent -= _currentRemotePort.OnRemoteDrop;
                _currentRemotePort = null;
            }

            OnPortStateChangedEvent -= PortStateChanged;
        }
    }
}
