using System;

namespace SharpPunk.Platform
{
	internal abstract class PlatformFactory
	{
		public Window CreateWindow(int width, int height)
		{
			return CreateWindow(width, height, null, null);
		}

		public abstract Window CreateWindow(int width, int height, string title, string iconFile);
		public abstract IRenderContext CreateRenderContext(Window window);
		public abstract void RunRenderLoop(Window window, Func<bool> fnFrame);
	}
}
