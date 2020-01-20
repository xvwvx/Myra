using Myra.Platform;
using System;
using System.Drawing;

namespace Myra.Graphics2D.UI
{
	public class RenderContext
	{
		public IBackend Batch { get; set; }
		public Rectangle View { get; set; }
		public float Opacity { get; set; }

#if XENKO
		internal RenderContext()
		{
			var rs = new RasterizerStateDescription();
			rs.SetDefault();
			rs.ScissorTestEnable = true;

			_IBackendBeginParams.RasterizerState = rs;
		}
#endif

		/// <summary>
		/// Draws texture region taking into account the context transformations
		/// </summary>
		/// <param name="brush"></param>
		/// <param name="rectangle"></param>
		/// <param name="color"></param>
		public void Draw(IBrush brush, Rectangle rectangle, Color? color = null)
		{
			var c = color != null ? color.Value : Color.White;
			brush.Draw(Batch, rectangle, c * Opacity);
		}

		/// <summary>
		/// Draws texture region taking into account the context transformations
		/// </summary>
		/// <param name="image"></param>
		/// <param name="rectangle"></param>
		/// <param name="color"></param>
		public void Draw(IImage image, Point location, Color? color = null)
		{
			var c = color != null ? color.Value : Color.White;
			image.Draw(Batch, new Rectangle(location.X, location.Y, image.Size.X, image.Size.Y), c * Opacity);
		}

		/// <summary>
		/// Draws rectangle taking into account the context transformations
		/// </summary>
		/// <param name="rectangle"></param>
		/// <param name="color"></param>
		public void DrawRectangle(Rectangle rectangle, Color color)
		{
			Batch.DrawRectangle(rectangle, color);
		}

		/// <summary>
		/// Draws a filled rectangle taking into account the context transformations
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="rectangle">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public void FillRectangle(Rectangle rectangle, Color color)
		{
			Batch.FillRectangle(rectangle, color * Opacity);
		}

		internal void Begin()
		{
#if MONOGAME
			Batch.Begin(IBackendBeginParams.SpriteSortMode,
				IBackendBeginParams.BlendState,
				IBackendBeginParams.SamplerState,
				IBackendBeginParams.DepthStencilState,
				IBackendBeginParams.RasterizerState,
				IBackendBeginParams.Effect,
				IBackendBeginParams.TransformMatrix);
#elif FNA
			Batch.Begin(IBackendBeginParams.SpriteSortMode,
				IBackendBeginParams.BlendState,
				IBackendBeginParams.SamplerState,
				IBackendBeginParams.DepthStencilState,
				IBackendBeginParams.RasterizerState,
				IBackendBeginParams.Effect,
				IBackendBeginParams.TransformMatrix != null ? IBackendBeginParams.TransformMatrix.Value : Matrix.Identity);
#elif XENKO
			Batch.Begin(MyraEnvironment.Game.GraphicsContext,
				IBackendBeginParams.SpriteSortMode,
				IBackendBeginParams.BlendState,
				IBackendBeginParams.SamplerState,
				IBackendBeginParams.DepthStencilState,
				IBackendBeginParams.RasterizerState);
#endif
		}

		internal void End()
		{
			Batch.End();
		}

		internal void Flush()
		{
			End();
			Begin();
		}
	}
}