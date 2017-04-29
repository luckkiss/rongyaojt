using System;

namespace Cross
{
	public class PhysicsInterfaceImpl : IPhysicsInterface
	{
		public IPhysicsScene3D createScene3D()
		{
			return new PhysicsScene3DImpl();
		}
	}
}
