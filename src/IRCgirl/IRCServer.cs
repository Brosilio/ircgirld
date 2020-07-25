using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libIRCgirl
{
	public class IRCServer
	{
		private readonly IRCServerOptions options;
		private CancellationToken cancel;
		private CancellationTokenSource cancelSource;

		private TcpListener listener;
		private bool isRunning;


		public IRCServer(IRCServerOptions options)
		{
			this.options = options;
			cancelSource = new CancellationTokenSource();
			cancel = cancelSource.Token;
		}

		public async Task Run()
		{
			if (isRunning)
				throw new InvalidOperationException("This IRCServer is already running.");

			isRunning = true;

			listener = new TcpListener(IPAddress.Any, 6667); // TODO: replace with shit from ircserveroptions
			listener.Start();

			try
			{
				while(isRunning)
				{
					IRCClient c = new IRCClient(await listener.AcceptTcpClientAsync());
				}
			}
			catch (ObjectDisposedException ode)
			{
				// someone probably told the server to fuck off
			}
		}

		public async Task FuckOff()
		{

		}
	}
}
