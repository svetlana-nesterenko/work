namespace ATS.Interfaces
{
    #region Usings

    using System;

    #endregion

    public interface ITerminal
    {
        bool IsRinging { get; }
        int Id { get; }
        bool PickUpPhone { get; }
        bool IsEnabled { get; }

        event EventHandler PingEvent;
        event EventHandler<string> PrepareOutgoingCallEvent;
        event EventHandler<string> OutgoingCallEvent;
        event EventHandler DropEvent;
        event EventHandler AnswerEvent;

        void SetPickUpPhone(bool flag);
        void Plug();
        void UnPlug();
        void IncomingCall(string number);
        void Call(string number);
        void Drop();
        void Answer();
    }
}
