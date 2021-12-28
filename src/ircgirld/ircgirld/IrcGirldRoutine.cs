using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libircgirl;
using libircgirl.Server;

namespace ircgirld
{
    internal class IrcGirldRoutine : IrcServerRoutine
    {
        public override Task OnConnect()
        {
            return base.OnConnect();
        }

        public override Task OnDisconnect()
        {
            return base.OnDisconnect();
        }
    }
}
