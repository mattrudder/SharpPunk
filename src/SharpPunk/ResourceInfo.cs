using System;
using System.IO;

namespace SharpPunk
{
	internal class ResourceInfo
	{
		public ResourceInfo(string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (!File.Exists(source))
				throw new ArgumentException(string.Format("The resource '{0}' does not exist.", source), "source");


		}
	}
}