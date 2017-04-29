using System;

namespace Cross
{
	public interface IAssetBitmap : IAsset
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
