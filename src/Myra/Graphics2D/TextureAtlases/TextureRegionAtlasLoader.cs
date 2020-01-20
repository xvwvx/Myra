using Myra.Assets;
using Myra.Platform;

namespace Myra.Graphics2D.TextureAtlases
{
	internal class TextureRegionAtlasLoader : IAssetLoader<TextureRegionAtlas>
	{
		public TextureRegionAtlas Load(AssetLoaderContext context, string assetName)
		{
			var xml = context.Load<string>(assetName);
			return TextureRegionAtlas.FromXml(xml, name => context.Load<ITexture>(name));
		}
	}
}