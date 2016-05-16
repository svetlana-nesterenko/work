namespace ATS.Interfaces
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Interface used for terminal.
    /// </summary>
    public interface ITerminal
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the terminal is ringing.
        /// </summary>
        /// <value>
        /// <c>true</c> if the terminal is ringing; otherwise, <c>false</c>.
        /// </value>
        bool IsRinging { get; }

        /// <summary>
        /// Gets the identifier of terminal.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets a value indicating whether the terminal pick up phone.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pick up phone]; otherwise, <c>false</c>.
        /// </value>
        bool PickUpPhone { get; }

        /// <summary>
        /// Gets a value indicating whether the terminal is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if the terminal is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; }

        #endregion

        #region Events

        /// <summary>
        /// Fires when terminal tries to connect with the station.
        /// </summary>
        event EventHandler PingEvent;

        /// <summary>
        /// Fires when terminal connects with the station and prepares outgoing call.
        /// </summary>
        event EventHandler<string> PrepareOutgoingCallEvent;

        /// <summary>
        /// Fires when tries to make an outgoing call.
        /// </summary>
        event EventHandler<string> OutgoingCallEvent;

        /// <summary>
        /// Fires when terminal drops phone.
        /// </summary>
        event EventHandler DropEvent;

        /// <summary>
        /// Fires when terminal answers the incoming call.
        /// </summary>
        event EventHandler AnswerEvent;

        #endregion

        #region Methods

        /// <summary>
        /// Sets value indicating whether the terminal pick up phone.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        void SetPickUpPhone(bool flag);

        /// <summary>
        /// Plugs the terminal.
        /// </summary>
        void Plug();

        /// <summary>
        /// Unplugs the terminal.
        /// </summary>
        void UnPlug();

        /// <summary>
        /// Gets incoming call.
        /// </summary>
        /// <param name="number">The number.</param>
        void IncomingCall(string number);

        /// <summary>
        /// Calls the specified number.
        /// </summary>
        /// <param name="number">The number.</param>
        void Call(string number);

        /// <summary>
        /// Drops phone.
        /// </summary>
        void Drop();

        /// <summary>
        /// Answers the incoming call.
        /// </summary>
        void Answer();

        #endregion
    }
}
