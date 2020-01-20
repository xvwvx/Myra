using System;
using System.Drawing;
using Myra.Assets;
using Myra.Platform;

namespace Myra.Graphics2D.TextureAtlases
{
	[AssetLoader(typeof(TextureRegionLoader))]
	public class TextureRegion: IImage
	{
		private readonly ITexture _texture;
		private readonly Rectangle _bounds;

		public ITexture Texture
		{
			get { return _texture; }
		}

		public Rectangle Bounds
		{
			get { return _bounds; }
		}

		public Size Size
		{
			get
			{
				return Bounds.Size;
			}
		}

		/// <summary>
		/// Covers the whole texture
		/// </summary>
		/// <param name="texture"></param>
		public TextureRegion(ITexture texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height))
		{
		}

		public TextureRegion(ITexture texture, Rectangle bounds)
		{
			if (texture == null)
			{
				throw new ArgumentNullException("texture");
			}

			_texture = texture;
			_bounds = bounds;
		}

		public TextureRegion(TextureRegion region, Rectangle bounds)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}

			_texture = region.Texture;
			bounds.Offset(region.Bounds.Location);
			_bounds = bounds;
		}

		public virtual void Draw(IBackend batch, Rectangle dest, Color color)
		{
#if !XENKO
			batch.Draw(Texture,
				dest,
				Bounds,
				color);
#else
			batch.Draw(Texture,
				new RectangleF(dest.X, dest.Y, dest.Width, dest.Height),
				new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height),
				color,
				0.0f,
				PointF.Empty);
#endif
		}
	}
}