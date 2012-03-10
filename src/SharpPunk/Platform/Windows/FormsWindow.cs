using System;
using System.Drawing;
using System.Windows.Forms;
using SharpDX.Windows;

namespace SharpPunk.Platform.Windows
{
	internal class FormsWindow : Window
	{
		public FormsWindow(int width, int height, string title, string iconFile)
		{
			Icon icon = null;
			try
			{
				icon = new Icon(iconFile);
			}
			catch (ArgumentException) { }
			
			m_form = new RenderForm(title ?? "SharpPunk")
			{
				ClientSize = new Size(width, height),
				MaximizeBox = false,
				FormBorderStyle = FormBorderStyle.Fixed3D,
				Icon = icon ?? SystemIcons.Application,
			};
			m_form.Show();
		}

		public override IntPtr DisplayHandle
		{
			get { return m_form.Handle; }
		}

		public override int Width
		{
			get { return m_form.Width; }
		}

		public override int Height
		{
			get { return m_form.Height; }
		}

		public Form Form
		{
			get { return m_form; }
		}

		protected override void Dispose(bool disposeManagedResources)
		{
			if (disposeManagedResources)
			{
				if (m_form != null)
				{
					m_form.Dispose();
					m_form = null;
				}
			}
		}

		Form m_form;
	}
}