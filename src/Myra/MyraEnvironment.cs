using Myra.Platform;
using System;
using System.Reflection;

namespace Myra
{

	public static class MyraEnvironment
	{
		private static IBackend _backend;

		public static event EventHandler GameDisposed;

		public static IBackend Backend
		{
			get
			{
				if (_backend == null)
				{
					throw new Exception("MyraEnvironment.Game is null. Please, set it to the Game instance before using Myra.");
				}

				return _backend;
			}

			set
			{
				if (_backend == value)
				{
					return;
				}

#if !XENKO
				if (_backend != null)
				{
					_backend.Disposed -= GameOnDisposed;
				}
#endif

				_backend = value;

#if !XENKO
				if (_backend != null)
				{
					_backend.Disposed += GameOnDisposed;
				}
#endif
			}
		}

		public static bool DrawWidgetsFrames { get; set; }
		public static bool DrawKeyboardFocusedWidgetFrame { get; set; }
		public static bool DrawMouseWheelFocusedWidgetFrame { get; set; }
		public static bool DrawTextGlyphsFrames { get; set; }
		public static bool DisableClipping { get; set; }

		private static void GameOnDisposed(object sender, EventArgs eventArgs)
		{
			DefaultAssets.Dispose();

			var ev = GameDisposed;
			if (ev != null)
			{
				ev(null, EventArgs.Empty);
			}
		}

		public static string Version
		{
			get
			{
				var assembly = typeof(MyraEnvironment).Assembly;
				var name = new AssemblyName(assembly.FullName);

				return name.Version.ToString();
			}
		}

		internal static string InternalClipboard;
	}
}