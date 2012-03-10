using System;

namespace SharpPunk
{
	public abstract class Graphic
	{


		public void Update()
		{
		}
	}

	public sealed class Image : Graphic
	{
		public Image(object resourceInfo)
		{
			if (resourceInfo == null)
				throw new ArgumentNullException("resourceInfo");

			ResourceInfo ri = resourceInfo as ResourceInfo;
			if (ri == null)
				throw new ArgumentException("resourceInfo does not contain valid resource information.");

			// TODO: Load resource info from ri.
		}
	}
}