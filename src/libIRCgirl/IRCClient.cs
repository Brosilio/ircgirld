using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace libIRCgirl
{
	public class IRCClient
	{
		private TcpClient client;

		public IRCClient(TcpClient client)
		{
			this.client = client;
		}
	}
}
