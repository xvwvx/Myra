namespace Myra.Platform
{
	public interface IBackend
	{
		/// <summary>
		/// Creates a texture from data in RGBA format
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		ITexture CreateTexture(int width, int height, byte[] data);
	}
}
