using System;
using SharpPunk;

namespace BlankProject
{
	public class BlankProject : Engine
	{
		public BlankProject()
			: base(800, 600)
		{
		}

		[STAThread]
		static void Main(string[] args)
		{
			Run(new BlankProject());
		}
	}
}
