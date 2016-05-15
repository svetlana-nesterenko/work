using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS
{
    public interface IRemoteEndpoint
    {
        event EventHandler<string> OnOutgoingCallEvent;
        event EventHandler IncomingCallAcceptedEvent;
        event EventHandler DropCallEvent;

        void OnIncomingCallAccepted(object sender, EventArgs e);
        void OnRemoteDrop(object sender, EventArgs e);
    }
}
