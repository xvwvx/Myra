using System.Drawing;

namespace Myra.Graphics2D
{
	public interface IImage: IBrush
	{
		Size Size { get; }
	}
}
