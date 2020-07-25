using System;
using System.Net;
using System.Threading.Tasks;

namespace IRCgirld
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			ShitProxy sp = new ShitProxy(6667, new IPEndPoint(Dns.GetHostAddresses("irc.restrictedaddress.space")[0], 6667));

			sp.Run();

		}
	}
}
