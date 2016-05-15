namespace ATS.Interfaces
{
    #region Usings

    using System;
    using ATS.Classes;
    using ATS.Enum;

    #endregion

    public interface IPort : IRemoteEndpoint
    {
        int Id { get; }

        PortState PortState { get; }

        event EventHandler<string> OnPrepareOutgoingCallEvent;
        event EventHandler FinishedEvent;
        event EventHandler<CallInfo> CallCompletedEvent;

        void WorkWithTerminal(ITerminal terminal, string number);

        void ClearAllEvents();
    }
}
