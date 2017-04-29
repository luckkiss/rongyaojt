using System;

namespace Cross
{
	public interface IAssetTexture : IAsset
	{
		int width
		{
			get;
		}

		int height
		{
			get;
		}
	}
}
