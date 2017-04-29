using System;

namespace Cross
{
	public interface IPhysicsObject
	{
		bool enabled
		{
			get;
			set;
		}

		void dispose();
	}
}
