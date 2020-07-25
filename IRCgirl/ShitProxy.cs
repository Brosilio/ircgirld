using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRCgirld
{
	public class ShitProxy
	{
		private int _localPort;
		private IPEndPoint _remote;
		private List<CopyContext> ctx = new List<CopyContext>();

		public ShitProxy(int localPort, IPEndPoint remote)
		{
			this._localPort = localPort;
			this._remote = remote;
		}

		public void Run()
		{
			try
			{
				TcpListener tl = new TcpListener(new IPEndPoint(IPAddress.Any, _localPort));
				tl.Start();
				while (true)
				{
					HandleClient(tl.AcceptTcpClient());
					Console.WriteLine($"[PRX] got someone on {_localPort}.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void HandleClient(TcpClient input)
		{
			try
			{
				Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
				s.Connect(_remote);
				TcpClient c = input;
				ctx.Add(new CopyContext(c, s));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				try
				{
					input.Dispose();
				}
				catch (Exception ex2)
				{
					Console.WriteLine(ex2.Message);
				}
			}
		}
	}

	public class CopyContext
	{
		private NetworkStream local, remote;

		private byte[] ibuf, obuf;
		private TcpClient c;
		private Socket s;

		public CopyContext(TcpClient local, Socket remote)
		{
			this.local = local.GetStream();
			this.remote = new NetworkStream(remote);
			this.c = local;
			this.s = remote;

			ibuf = new byte[8192];
			obuf = new byte[8192];

			new Thread(CopyLR).Start();
			new Thread(CopyRL).Start();
		}

		private void CopyLR()
		{
			int read = 0;
			do
			{
				read = local.Read(ibuf, 0, 8192);
				if (read == 0)
				{
					remote.Dispose();
					local.Dispose();
					break;
				}

				Console.WriteLine($"[L>R] ---------------------------");
				Console.WriteLine(Encoding.ASCII.GetString(ibuf, 0, read));
				Console.WriteLine();

				remote.Write(ibuf, 0, read);

			} while (read > 0);
		}

		private void CopyRL()
		{
			int read = 0;
			do
			{
				read = remote.Read(obuf, 0, 8192);
				if (read == 0)
				{
					local.Dispose();
					remote.Dispose();
					break;
				}

				Console.WriteLine($"[R>L] ---------------------------");
				Console.WriteLine(Encoding.ASCII.GetString(obuf, 0, read));
				Console.WriteLine();

				local.Write(obuf, 0, read);

			} while (read > 0);
		}
	}
}
