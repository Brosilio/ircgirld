using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IRCgirld.API;

namespace IRCgirld.Services
{
	public class TextLogService : IService
	{
		private readonly StreamWriter writer;

		public TextLogService(string path)
		{
			writer = new StreamWriter(path, true);
		}

		public void WriteLine(object o)
		{
			writer.WriteLine(o);
		}

		public Task WriteLineAsync(string s)
		{
			return writer.WriteLineAsync(s);
		}
	}
}
