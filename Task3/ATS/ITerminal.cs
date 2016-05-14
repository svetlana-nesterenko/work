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
        int Id { get; set; }

        event EventHandler PingEvent;
        event EventHandler<string> PrepareOutgoingCallEvent;
        event EventHandler<string> OutgoingCallEvent;
        event EventHandler DropEvent;
        event EventHandler AnswerEvent;

        void IncomingCall(string number);
        void Call(string number);
        void Drop();

        void Answer();
    }
}
