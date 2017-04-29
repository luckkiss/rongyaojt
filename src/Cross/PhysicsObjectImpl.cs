using System;

namespace Cross
{
	public class PhysicsObjectImpl : IPhysicsObject
	{
		public virtual bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public virtual void dispose()
		{
		}
	}
}
