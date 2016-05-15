namespace ATS.Interfaces
{
    #region Usings

    using System;

    #endregion

    public interface IRemoteEndpoint
    {
        event EventHandler<string> OnOutgoingCallEvent;
        event EventHandler IncomingCallAcceptedEvent;
        event EventHandler DropCallEvent;

        void OnIncomingCall(object sender, string number);
        void OnIncomingCallAccepted(object sender, EventArgs e);
        void OnRemoteDrop(object sender, EventArgs e);
    }
}
