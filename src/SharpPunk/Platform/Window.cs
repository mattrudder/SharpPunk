using System;

namespace SharpPunk.Platform
{
	internal abstract class Window : IDisposable
	{
		public abstract IntPtr DisplayHandle { get; }

		public abstract int Width { get; }
		public abstract int Height { get; }

		protected Window()
		{
		}

		~Window()
		{
			if (!m_disposed)
			{
				Dispose(false);
				m_disposed = true;
			}
		}

		public void Dispose()
		{
			if (!m_disposed)
			{
				Dispose(true);
				m_disposed = true;
			}

			GC.SuppressFinalize(this);
		}

		protected abstract void Dispose(bool disposeManagedResources);

		bool m_disposed;
	}
}