using System;

namespace Cross
{
	public interface IAssetMesh : IAsset
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
