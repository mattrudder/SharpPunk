using System;

namespace SharpPunk.Platform
{
	internal interface IRenderContext : IDisposable
	{
		void BeginDraw();
		void EndDraw();
	}
}