namespace ATS.Interfaces
{
    #region Usings

    using System;
    using ATS.Classes;
    using ATS.Enum;

    #endregion

    /// <summary>
    /// Interface used for ports.
    /// </summary>
    /// <seealso cref="ATS.Interfaces.IRemoteEndpoint" />
    public interface IPort : IRemoteEndpoint
    {
        /// <summary>
        /// Gets the identifier of port.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets the state of the port.
        /// </summary>
        /// <value>
        /// The state of the port.
        /// </value>
        PortState PortState { get; }

        /// <summary>
        /// Fires when port get outgoing call.
        /// </summary>
        event EventHandler<string> OnPrepareOutgoingCallEvent;

        /// <summary>
        /// Fires when port gets free state.
        /// </summary>
        event EventHandler FinishedEvent;

        /// <summary>
        /// Fires when current or remote terminal drop phone.
        /// </summary>
        event EventHandler<CallInfo> CallCompletedEvent;

        /// <summary>
        /// Attach terminal.
        /// </summary>
        /// <param name="terminal">The terminal.</param>
        /// <param name="number">The number.</param>
        void WorkWithTerminal(ITerminal terminal, string number);

        /// <summary>
        /// Clears all events.
        /// </summary>
        void ClearAllEvents();
    }
}
