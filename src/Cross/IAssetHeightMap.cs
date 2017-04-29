using System;

namespace Cross
{
	public interface IAssetHeightMap : IAsset
	{
		float width
		{
			get;
		}

		float height
		{
			get;
		}

		int pixelWidth
		{
			get;
		}

		int pixelHeight
		{
			get;
		}

		float heightMin
		{
			get;
		}

		float heightMax
		{
			get;
		}
	}
}
