namespace ATS.Classes
{
    #region Usings

    using System;
    using Interfaces;

    #endregion

    public class Terminal : ITerminal
    {
        private bool _pickUpPhone { get; set; }
        private bool _isRinging { get; set; }
        private bool _isEnabled { get; set; }

        public Terminal(int id)
        {
            Id = id;
        }
        public bool IsRinging
        {
            get { return _isRinging; }
        }

        public int Id { get; private set; }

        public bool PickUpPhone
        {
            get { return _pickUpPhone; }
            set
            {
                _pickUpPhone = value;
                _isRinging = false;
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
        }

        public event EventHandler PingEvent;
        public event EventHandler<string> PrepareOutgoingCallEvent;
        public event EventHandler<string> OutgoingCallEvent;
        public event EventHandler DropEvent;
        public event EventHandler AnswerEvent;
        
        public void UnPlug()
        {
            _isEnabled = false;
            if (_pickUpPhone)
            {
                Drop();
            }
        }

        public void SetPickUpPhone(bool flag)
        {
            PickUpPhone = flag;
        }

        public void Plug()
        {
            _isEnabled = true;
        }

        public void IncomingCall(string number)
        {
            if (!PickUpPhone)
            {
                _isRinging = true;
            }
        }

        public void Call(string number)
        {
            if (_isEnabled && PingEvent != null && !PickUpPhone)
            {
                PingEvent(this, null);

                if (PrepareOutgoingCallEvent != null)
                {
                    PrepareOutgoingCallEvent(this, number);

                    if (OutgoingCallEvent != null)
                    {
                        OutgoingCallEvent(this, number);
                    }
                }
                PickUpPhone = true;
            }
        }

        public void Drop()
        {
            if (_isEnabled && DropEvent != null && PickUpPhone)
            {
                PickUpPhone = false;
                DropEvent(this, null);
            }
        }

        public void Answer()
        {
            if (AnswerEvent != null && !PickUpPhone)
            {
                AnswerEvent(this, null);
                PickUpPhone = true;
                _isRinging = false;
            }
        }
    }
}
