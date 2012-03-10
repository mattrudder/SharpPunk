using System;
using System.Windows.Forms;
using SharpDX.Windows;

namespace SharpPunk.Platform.Windows
{
	internal sealed class WindowsPlatformFactory : PlatformFactory
	{
		public override Window CreateWindow(int width, int height, string title, string iconFile)
		{
			return new FormsWindow(width, height, title, iconFile);
		}

		public override IRenderContext CreateRenderContext(Window window)
		{
			return new Direct3D10RenderContext(window);
		}

		public override void RunRenderLoop(Window window, Func<bool> fnFrame)
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
