using System;

namespace Cross
{
	public interface IAssetSkAniMesh : IAsset
	{
		int numVertices
		{
			get;
		}

		int numTriangles
		{
			get;
		}
	}
}
