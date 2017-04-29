using System;

namespace Cross
{
	public interface IBitmap : IGraphObject2D, IGraphObject
	{
		IAssetBitmap asset
		{
			get;
			set;
		}
	}
}
