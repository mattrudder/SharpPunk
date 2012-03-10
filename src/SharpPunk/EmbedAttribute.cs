using System;

namespace SharpPunk
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class EmbedAttribute : Attribute
	{
		public string Source { get; private set; }

		public EmbedAttribute(string source)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			Source = source;
		}
	}
}