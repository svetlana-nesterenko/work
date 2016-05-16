namespace ATS.Interfaces
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    /// Interface used for ports or commutators wich provide connection between ports.
    /// </summary>
    public interface IRemoteEndpoint
    {
        /// <summary>
        /// Fires when port or commutator generates outgoing call with remote port.
        /// </summary>
        event EventHandler<string> OnOutgoingCallEvent;

        /// <summary>
        /// Fires when port or commutator get incoming call from terminal.
        /// </summary>
        event EventHandler IncomingCallAcceptedEvent;

        /// <summary>
        /// Fires when terminal drops phone.
        /// </summary>
        event EventHandler DropCallEvent;

        /// <summary>
        /// Gets incoming call.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="number">The number.</param>
        void OnIncomingCall(object sender, string number);

        /// <summary>
        /// Sets incoming connection.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnIncomingCallAccepted(object sender, EventArgs e);

        /// <summary>
        /// Reacts to drop phone by remote terminal.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnRemoteDrop(object sender, EventArgs e);
    }
}
