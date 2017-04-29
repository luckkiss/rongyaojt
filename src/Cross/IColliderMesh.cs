using System;

namespace Cross
{
	public interface IColliderMesh : IPhysicsObject3D, IPhysicsObject
	{
		IAssetMesh asset
		{
			get;
			set;
		}
	}
}
