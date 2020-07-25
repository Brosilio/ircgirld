using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace libIRCgirl
{
	public class IRCReader
	{
		public delegate void OnCommandDelegate(IRCCommand command);

		private Stream stream;
		private CancellationToken cancel;
		private OnCommandDelegate OnCommand;


		public IRCReader(Stream stream, OnCommandDelegate onCommandCallback, CancellationToken cancel)
		{
			this.stream = stream;
			this.OnCommand = onCommandCallback;
			this.cancel = cancel;
		}


	}
}
