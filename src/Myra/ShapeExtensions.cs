// This code had been borrowed from the MonoGame.Extended project: https://github.com/craftworkgames/MonoGame.Extended

using System;

#if !XENKO
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using ITexture = Xenko.Graphics.Texture;
#endif


namespace Myra
{
	/// <summary>
	/// Sprite batch extensions for drawing primitive shapes
	/// </summary>
	public static class ShapeExtensions
	{
		private static void Draw(this IBackend IBackend, ITexture texture, PointF offset, Color color, PointF scale, float rotation = 0.0f)
		{
			IBackend.Draw(texture, offset, null, color, rotation, PointF.Empty, scale, SpriteEffects.None, 0.0f);
		}

		/// <summary>
		///     Draws a closed polygon from an array of points
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// ///
		/// <param name="offset">Where to offset the points</param>
		/// <param name="points">The points to connect with lines</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the lines</param>
		public static void DrawPolygon(this IBackend IBackend, PointF offset, PointF[] points, Color color,
			float thickness = 1f)
		{
			if (points.Length == 0)
				return;

			if (points.Length == 1)
			{
				DrawPoint(IBackend, points[0], color, (int) thickness);
				return;
			}

			var texture = DefaultAssets.White;

			for (var i = 0; i < points.Length - 1; i++)
				DrawPolygonEdge(IBackend, texture, points[i] + offset, points[i + 1] + offset, color, thickness);

			DrawPolygonEdge(IBackend, texture, points[points.Length - 1] + offset, points[0] + offset, color,
				thickness);
		}

		private static void DrawPolygonEdge(IBackend IBackend, ITexture texture, PointF point1, PointF point2,
			Color color, float thickness)
		{
			var length = PointF.Distance(point1, point2);
			var angle = (float) Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
			var scale = new PointF(length, thickness);
			IBackend.Draw(texture, point1, color: color, rotation: angle, scale: scale);
		}

		/// <summary>
		///     Draws a filled rectangle
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="rectangle">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this IBackend IBackend, Rectangle rectangle, Color color)
		{
			FillRectangle(IBackend, new PointF(rectangle.X, rectangle.Y), new PointF(rectangle.Width, rectangle.Height), color);
		}

		/// <summary>
		///     Draws a filled rectangle
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this IBackend IBackend, PointF location, PointF size, Color color)
		{
			IBackend.Draw(DefaultAssets.White, location, null, color, 0, PointF.Empty, size, SpriteEffects.None, 0);
		}

		/// <summary>
		///     Draws a filled rectangle
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="x">The X coord of the left side</param>
		/// <param name="y">The Y coord of the upper side</param>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		/// <param name="color">The color to draw the rectangle in</param>
		public static void FillRectangle(this IBackend IBackend, float x, float y, float width, float height,
			Color color)
		{
			FillRectangle(IBackend, new PointF(x, y), new PointF(width, height), color);
		}

		/// <summary>
		///     Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="rectangle">The rectangle to draw</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the lines</param>
		public static void DrawRectangle(this IBackend IBackend, Rectangle rectangle, Color color,
			float thickness = 1f)
		{
			var texture = DefaultAssets.White;
			var topLeft = new PointF(rectangle.X, rectangle.Y);
			var topRight = new PointF(rectangle.Right - thickness, rectangle.Y);
			var bottomLeft = new PointF(rectangle.X, rectangle.Bottom - thickness);
			var horizontalScale = new PointF(rectangle.Width, thickness);
			var verticalScale = new PointF(thickness, rectangle.Height);

			IBackend.Draw(texture, topLeft, scale: horizontalScale, color: color);
			IBackend.Draw(texture, topLeft, scale: verticalScale, color: color);
			IBackend.Draw(texture, topRight, scale: verticalScale, color: color);
			IBackend.Draw(texture, bottomLeft, scale: horizontalScale, color: color);
		}

		/// <summary>
		///     Draws a rectangle with the thickness provided
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="location">Where to draw</param>
		/// <param name="size">The size of the rectangle</param>
		/// <param name="color">The color to draw the rectangle in</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawRectangle(this IBackend IBackend, PointF location, PointF size, Color color,
			float thickness = 1f)
		{
			DrawRectangle(IBackend, new Rectangle((int) location.X, (int) location.Y, (int) size.X, (int) size.Y), color,
				thickness);
		}

		/// <summary>
		///     Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="x1">The X coord of the first point</param>
		/// <param name="y1">The Y coord of the first point</param>
		/// <param name="x2">The X coord of the second point</param>
		/// <param name="y2">The Y coord of the second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawLine(this IBackend IBackend, float x1, float y1, float x2, float y2, Color color,
			float thickness = 1f)
		{
			DrawLine(IBackend, new PointF(x1, y1), new PointF(x2, y2), color, thickness);
		}

		/// <summary>
		///     Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="point1">The first point</param>
		/// <param name="point2">The second point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawLine(this IBackend IBackend, PointF point1, PointF point2, Color color,
			float thickness = 1f)
		{
			// calculate the distance between the two vectors
			var distance = PointF.Distance(point1, point2);

			// calculate the angle between the two vectors
			var angle = (float) Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

			DrawLine(IBackend, point1, distance, angle, color, thickness);
		}

		/// <summary>
		///     Draws a line from point1 to point2 with an offset
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="point">The starting point</param>
		/// <param name="length">The length of the line</param>
		/// <param name="angle">The angle of this line from the starting point</param>
		/// <param name="color">The color to use</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawLine(this IBackend IBackend, PointF point, float length, float angle, Color color,
			float thickness = 1f)
		{
			var origin = new PointF(0f, 0.5f);
			var scale = new PointF(length, thickness);
			IBackend.Draw(DefaultAssets.White, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
		}

		/// <summary>
		///     Draws a point at the specified x, y position. The center of the point will be at the position.
		/// </summary>
		public static void DrawPoint(this IBackend IBackend, float x, float y, Color color, float size = 1f)
		{
			DrawPoint(IBackend, new PointF(x, y), color, size);
		}

		/// <summary>
		///     Draws a point at the specified position. The center of the point will be at the position.
		/// </summary>
		public static void DrawPoint(this IBackend IBackend, PointF position, Color color, float size = 1f)
		{
			var scale = PointF.One * size;
			var offset = new PointF(0.5f) - new PointF(size * 0.5f);
			IBackend.Draw(DefaultAssets.White, position + offset, color: color, scale: scale);
		}

		/// <summary>
		///     Draw a circle
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="center">The center of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		/// <param name="thickness">The thickness of the lines used</param>
		public static void DrawCircle(this IBackend IBackend, PointF center, float radius, int sides, Color color,
			float thickness = 1f)
		{
			DrawPolygon(IBackend, center, CreateCircle(radius, sides), color, thickness);
		}

		/// <summary>
		///     Draw a circle
		/// </summary>
		/// <param name="IBackend">The destination drawing surface</param>
		/// <param name="x">The center X of the circle</param>
		/// <param name="y">The center Y of the circle</param>
		/// <param name="radius">The radius of the circle</param>
		/// <param name="sides">The number of sides to generate</param>
		/// <param name="color">The color of the circle</param>
		/// <param name="thickness">The thickness of the line</param>
		public static void DrawCircle(this IBackend IBackend, float x, float y, float radius, int sides,
			Color color, float thickness = 1f)
		{
			DrawPolygon(IBackend, new PointF(x, y), CreateCircle(radius, sides), color, thickness);
		}

		private static PointF[] CreateCircle(double radius, int sides)
		{
			const double max = 2.0 * Math.PI;
			var points = new PointF[sides];
			var step = max / sides;
			var theta = 0.0;

			for (var i = 0; i < sides; i++)
			{
				points[i] = new PointF((float) (radius * Math.Cos(theta)), (float) (radius * Math.Sin(theta)));
				theta += step;
			}

			return points;
		}
	}
}