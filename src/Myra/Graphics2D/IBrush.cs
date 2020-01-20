using Myra.Assets;
using Myra.Graphics2D.Brushes;
using Myra.Platform;
using System.Drawing;

namespace Myra.Graphics2D
{
	[AssetLoader(typeof(BrushLoader))]
	public interface IBrush
	{
		void Draw(IBackend batch, Rectangle dest, Color color);
	}
}
