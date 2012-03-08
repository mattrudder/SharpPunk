using System;
using SharpDX;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using Device1 = SharpDX.Direct3D10.Device1;
using Resource = SharpDX.Direct3D10.Resource;

namespace SharpPunk.Platform.Windows
{
	internal class Direct3D10RenderContext : IRenderContext
	{
		public Device1 Device
		{
			get { return m_device; }
		}

		public Texture2D BackBuffer
		{
			get { return m_backBuffer; }
		}

		public RenderTargetView BackBufferView
		{
			get { return m_backBufferView; }
		}

		public Direct3D10RenderContext(Window window)
		{
			SwapChainDescription desc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(window.Width, window.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = window.DisplayHandle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};
			
			Device1.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, desc, FeatureLevel.Level_10_0, out m_device, out m_swapChain);

			Factory factory = m_swapChain.GetParent<Factory>();
			factory.MakeWindowAssociation(window.DisplayHandle, WindowAssociationFlags.IgnoreAll);

			m_backBuffer = Resource.FromSwapChain<Texture2D>(m_swapChain, 0);

			m_backBufferView = new RenderTargetView(m_device, m_backBuffer);
		}

		public void BeginDraw()
		{
			m_device.Rasterizer.SetViewports(new Viewport(0, 0, m_backBuffer.Description.Width, m_backBuffer.Description.Height));
			m_device.OutputMerger.SetTargets(m_backBufferView);
			m_device.ClearRenderTargetView(m_backBufferView, Colors.DeepPink);
		}

		public void EndDraw()
		{
			m_swapChain.Present(1, PresentFlags.None);
		}

		public void Dispose()
		{
			
		}

		SwapChain m_swapChain;
		Device1 m_device;
		Texture2D m_backBuffer;
		RenderTargetView m_backBufferView;
	}
}