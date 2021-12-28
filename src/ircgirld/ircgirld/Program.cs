using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using libircgirl;
using libircgirl.Server;

namespace ircgirld
{
    static class Program
    {
        static void Main(string[] args)
        {
            IrcServer server = new IrcServer();
            server.Start<IrcGirldRoutine>(IPAddress.Any, 6667);

            server.WaitForExit();
        }
    }
}
