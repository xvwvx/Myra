using Myra.Platform;
using Myra.Utility;

namespace Myra.Assets
{
	internal class ITextureLoader : IAssetLoader<ITexture>
	{
		public ITexture Load(AssetLoaderContext context, string assetName)
		{
			using (var stream = context.Open(assetName))
			{
				var data = ITextureExtensions.FromStream(stream, true);

				return MyraEnvironment.Backend.CreateTexture(data.Width, data.Height, data.Data);
			}
		}
	}
}