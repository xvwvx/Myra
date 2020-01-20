using Myra.Platform;
using System.Drawing;

namespace Myra.Graphics2D.Brushes
{
	public class SolidBrush : IBrush
	{
		private Color _color = Color.White;

		public Color Color
		{
			get
			{
				return _color;
			}

			set
			{
				_color = value;
			}
		}

		public SolidBrush(Color color)
		{
			Color = color;
		}

		public void Draw(IBackend backend, Rectangle dest, Color color)
		{
			var white = DefaultAssets.WhiteRegion;

			if (color == Color.White)
			{
				white.Draw(backend, dest, Color);
			}
			else
			{
				var c = new Color((int)(Color.R * color.R / 255.0f),
					(int)(Color.G * color.G / 255.0f),
					(int)(Color.B * color.B / 255.0f),
					(int)(Color.A * color.A / 255.0f));

				white.Draw(backend, dest, c);
			}
		}
	}
}