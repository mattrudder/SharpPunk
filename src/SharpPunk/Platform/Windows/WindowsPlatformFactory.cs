using System;
using System.Windows.Forms;
using SharpDX.Windows;

namespace SharpPunk.Platform.Windows
{
	internal sealed class WindowsPlatformFactory : IPlatformFactory
	{
		public Window CreateWindow(int width, int height)
		{
			return new FormsWindow(width, height);
		}

		public IRenderContext CreateRenderContext(Window window)
		{
			return new Direct3D10RenderContext(window);
		}

		public void RunRenderLoop(Window window, Func<bool> fnFrame)
		{
			FormsWindow formWin = (FormsWindow) window;
			RenderLoop.Run(formWin.Form, () =>
			{
				if (!fnFrame())
					formWin.Form.Close();
			});
		}
	}
}
