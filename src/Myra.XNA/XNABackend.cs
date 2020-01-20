using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Platform;
using Myra.Utility;

namespace Myra
{
	public class XNABackend : IBackend
	{
		public Game Game { get; private set; }

		public GraphicsDevice Device
		{
			get
			{
				return Game.GraphicsDevice;
			}
		}

		public XNABackend(Game game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
		}


		public ITexture CreateTexture(int width, int height, byte[] data)
		{
			Texture2D texture = null;
			texture = CrossEngineStuff.CreateTexture2D(Device, width, height);
			CrossEngineStuff.SetData(Game, texture, data);

			return new XNATexture(texture);
		}
	}
}
