using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using SharpPunk.Platform;
using SharpPunk.Platform.Windows;

namespace SharpPunk
{
	public class Engine
	{
		public Engine(int width, int height, float frameRate = 60.0f, bool fixedFps = false)
		{
			if (Instance != null)
				throw new InvalidOperationException("Cannot have two instances of the engine in a single game.");

			Width = width;
			Height = height;
			FrameRate = frameRate;
			Fixed = fixedFps;
			Elapsed = 0.0f;

			m_world = null;

			Screen = new Screen();
		}

		public static Engine Instance { get; private set; }

		public float Width { get; private set; }
		public float Height { get; private set; }
		public float FrameRate { get; private set; }
		public bool Fixed { get; private set; }
		public float Elapsed { get; private set; }
		public float Rate { get; private set; }
		public Screen Screen { get; private set; }
		
		public RectangleF Bounds { get; private set; }
		public PointF Camera { get; private set; }

		public float HalfWidth { get { return Width / 2.0f; } }
		public float HalfHeight { get { return Height / 2.0f; } }

		public World World
		{
			get { return m_world; }
			set
			{
				if (m_world != value)
				{
					m_goto = value;
				}
			}
		}

		protected bool IsRunning { get; set; }

		public void SetCamera(float x = 0, float y = 0)
		{
			Camera = new PointF(x, y);
		}

		public void ResetCamera()
		{
			SetCamera();
		}

		protected virtual void OnInitialize()
		{
		}

		void EnterFrame()
		{
			m_renderContext.BeginDraw();

			// TODO: Draw stuff

			m_renderContext.EndDraw();
		}

		void Shutdown()
		{
			m_renderContext.Dispose();
		}

		void Initialize()
		{
			// Search entities for EmbedAttribute
			Type[] entityTypes = Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Entity))).ToArray();
			foreach (Type type in entityTypes)
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
				foreach (FieldInfo field in fields)
				{
					EmbedAttribute embed = (EmbedAttribute) field.GetCustomAttributes(typeof (EmbedAttribute), false).SingleOrDefault();
					if (embed != null)
					{
						Console.WriteLine("TODO: Embed attribute with source {0}", embed.Source);
						field.SetValue(null, new ResourceInfo(embed.Source));
					}
				}
			}


			m_window = s_platformFactory.CreateWindow((int) Width, (int) Height);
			m_renderContext = s_platformFactory.CreateRenderContext(m_window);

			IsRunning = true;

			OnInitialize();
		}

		protected static void Run(Engine instance)
		{
			Instance = instance;

			instance.Initialize();
			s_platformFactory.RunRenderLoop(instance.m_window, () =>
			{
				instance.EnterFrame();
				return instance.IsRunning;
			});
			instance.Shutdown();
		}

		static Engine()
		{
#if WINDOWS
			s_platformFactory = new WindowsPlatformFactory();
#endif
		}

		static readonly PlatformFactory s_platformFactory;
		
		Window m_window;
		
		World m_world;
		World m_goto;
		
		IRenderContext m_renderContext;
	}
}
