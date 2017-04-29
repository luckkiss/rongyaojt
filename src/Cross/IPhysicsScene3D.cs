using System;

namespace Cross
{
	public interface IPhysicsScene3D : IPhysicsScene
	{
		IHeightMap createHeightMap();

		IColliderMesh createColliderMesh();
	}
}
