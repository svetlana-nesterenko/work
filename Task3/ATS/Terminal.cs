using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public class Terminal : ITerminal
    {
        public int Id { get; set; }

        public event EventHandler PingEvent;
        public event EventHandler<string> PrepareOutgoingCallEvent;
        public event EventHandler<string> OutgoingCallEvent;
        public event EventHandler DropEvent;
        public event EventHandler AnswerEvent;

        public void IncomingCall(string number)
        {
            
        }

        public void Call(string number)
        {
            if (PingEvent != null)
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
            }
        }

        public void Drop()
        {
            if (DropEvent != null)
            {
                DropEvent(this, null);
            }
        }

        public void Answer()
        {
            if (AnswerEvent != null)
            {
                AnswerEvent(this, null);
            }
        }
    }
}
