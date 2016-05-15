using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public interface IPort : IRemoteEndpoint
    {
        int Id { get; set; }

        PortState PortState { get; set; }

        //event EventHandler<string> OnOutgoingCallEvent;
        event EventHandler<string> OnPrepareOutgoingCallEvent;
        //event EventHandler IncomingCallAcceptedEvent;
        //event EventHandler DropCallEvent;
        event EventHandler FinishedEvent;
        event EventHandler<CallInfo> CallCompletedEvent;

        void OnIncomingCall(object sender, string number);
        //void OnIncomingCallAccepted(object sender, EventArgs e);

        //void OnRemoteDrop(object sender, EventArgs e);

        void WorkWithTerminal(ITerminal terminal, string number);

        void ClearAllEvents();
    }
}
