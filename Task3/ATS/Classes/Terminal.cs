namespace ATS.Classes
{
    #region Usings

    using System;
    using Interfaces;

    #endregion

    /// <summary>
    /// Class represents the terminal.
    /// </summary>
    /// <seealso cref="ATS.Interfaces.ITerminal" />
    public class Terminal : ITerminal
    {
        #region Private Fields

        /// <summary>
        /// Gets or sets a value indicating whether the terminal pick up phone.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [_pick up phone]; otherwise, <c>false</c>.
        /// </value>
        private bool _pickUpPhone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the terminal is ringing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [_is ringing]; otherwise, <c>false</c>.
        /// </value>
        private bool _isRinging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the terminal is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [_is enabled]; otherwise, <c>false</c>.
        /// </value>
        private bool _isEnabled { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the terminal is ringing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the terminal is ringing; otherwise, <c>false</c>.
        /// </value>
        public bool IsRinging
        {
            get { return _isRinging; }
        }

        /// <summary>
        /// Gets the identifier of terminal.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the terminal pick up phone.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pick up phone]; otherwise, <c>false</c>.
        /// </value>
        public bool PickUpPhone
        {
            get { return _pickUpPhone; }
            set
            {
                _pickUpPhone = value;
                _isRinging = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the terminal is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the terminal is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return _isEnabled; }
        }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Terminal"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Terminal(int id)
        {
            Id = id;
        }

        #endregion

        #region Events

        /// <summary>
        /// Fires when terminal tries to connect with the station.
        /// </summary>
        public event EventHandler PingEvent;

        /// <summary>
        /// Fires when terminal connects with the station and prepares outgoing call.
        /// </summary>
        public event EventHandler<string> PrepareOutgoingCallEvent;

        /// <summary>
        /// Fires when tries to make an outgoing call.
        /// </summary>
        public event EventHandler<string> OutgoingCallEvent;

        /// <summary>
        /// Fires when terminal drops phone.
        /// </summary>
        public event EventHandler DropEvent;

        /// <summary>
        /// Fires when terminal answers the incoming call.
        /// </summary>
        public event EventHandler AnswerEvent;

        #endregion

        #region Public Methods

        /// <summary>
        /// Unplugs the terminal.
        /// </summary>
        public void UnPlug()
        {
            _isEnabled = false;
            if (_pickUpPhone)
            {
                Drop();
            }
        }

        /// <summary>
        /// Sets value indicating whether the terminal pick up phone.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        public void SetPickUpPhone(bool flag)
        {
            PickUpPhone = flag;
        }

        /// <summary>
        /// Plugs the terminal.
        /// </summary>
        public void Plug()
        {
            _isEnabled = true;
        }

        /// <summary>
        /// Gets incoming call.
        /// </summary>
        /// <param name="number">The number.</param>
        public void IncomingCall(string number)
        {
            if (!PickUpPhone)
            {
                _isRinging = true;
            }
        }

        /// <summary>
        /// Calls the specified number.
        /// </summary>
        /// <param name="number">The number.</param>
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

        /// <summary>
        /// Drops phone.
        /// </summary>
        public void Drop()
        {
            if (_isEnabled && DropEvent != null && PickUpPhone)
            {
                PickUpPhone = false;
                DropEvent(this, null);
            }
        }

        /// <summary>
        /// Answers the incoming call.
        /// </summary>
        public void Answer()
        {
            if (AnswerEvent != null && !PickUpPhone)
            {
                AnswerEvent(this, null);
                PickUpPhone = true;
                _isRinging = false;
            }
        }

        #endregion

    }
}
