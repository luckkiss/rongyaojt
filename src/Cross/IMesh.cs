using System;

namespace Cross
{
	public interface IMesh : IGraphObject3D, IGraphObject
	{
		IAssetMesh asset
		{
			get;
			set;
		}
	}
}
