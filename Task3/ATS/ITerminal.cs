using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public interface ITerminal
    {
        bool IsRinging { get; }
        int Id { get; set; }
        bool PickUpPhone { get; set; }
        bool IsEnabled { get; }

        event EventHandler PingEvent;
        event EventHandler<string> PrepareOutgoingCallEvent;
        event EventHandler<string> OutgoingCallEvent;
        event EventHandler DropEvent;
        event EventHandler AnswerEvent;

        void Plug();
        void UnPlug();
        void IncomingCall(string number);
        void Call(string number);
        void Drop();
        void Answer();
    }
}
