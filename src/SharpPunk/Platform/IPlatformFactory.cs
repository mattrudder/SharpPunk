using System;

namespace SharpPunk.Platform
{
	internal interface IPlatformFactory
	{
		Window CreateWindow(int width, int height);
		IRenderContext CreateRenderContext(Window window);
		void RunRenderLoop(Window window, Func<bool> fnFrame);
	}
}
