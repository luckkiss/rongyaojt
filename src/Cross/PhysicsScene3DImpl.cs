using System;
using UnityEngine;

namespace Cross
{
	public class PhysicsScene3DImpl : PhysicsSceneImpl, IPhysicsScene3D, IPhysicsScene
	{
		public GameObject m_u3dObj = new GameObject();

		public IHeightMap createHeightMap()
		{
			return new HeightMapImpl
			{
				u3dObject = 
				{
					transform = 
					{
						parent = this.m_u3dObj.transform
					}
				}
			};
		}

		public IColliderMesh createColliderMesh()
		{
			return new ColliderMeshImpl
			{
				u3dObject = 
				{
					transform = 
					{
						parent = this.m_u3dObj.transform
					}
				}
			};
		}
	}
}
