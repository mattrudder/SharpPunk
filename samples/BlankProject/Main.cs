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

		protected override void OnInitialize()
		{
			Console.WriteLine("SharpPunk has started successfully!");
			var world = new MyWorld();
		}

		[STAThread]
		static void Main(string[] args)
		{
			Run(new BlankProject());
		}
	}

	public class MyWorld : World
	{
		public MyWorld()
		{
			Add(new MyEntity());
		}
	}

	public class MyEntity : Entity
	{
		public MyEntity()
		{
			Graphic = new Graphic(Player);
		}

		[Embed("assets/player.png")]
		static readonly object Player = null;
	}
}
