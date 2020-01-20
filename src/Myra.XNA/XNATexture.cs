using Microsoft.Xna.Framework.Graphics;
using Myra.Platform;
using System;

namespace Myra
{
	internal sealed class XNATexture: ITexture
	{
		public Texture2D Texture { get; private set; }

		public XNATexture(Texture2D texture)
		{
			Texture = texture ?? throw new ArgumentNullException(nameof(texture));
		}
	}
}
