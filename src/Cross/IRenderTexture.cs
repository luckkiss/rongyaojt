using System;

namespace Cross
{
	public interface IRenderTexture : IGraphObject2D, IGraphObject
	{
		IAssetRenderTexture asset
		{
			get;
			set;
		}
	}
}
