using System;

namespace Cross
{
	public interface IPhysicsObject3D : IPhysicsObject
	{
		Vec3 pos
		{
			get;
			set;
		}

		float x
		{
			get;
			set;
		}

		float y
		{
			get;
			set;
		}

		float z
		{
			get;
			set;
		}

		Vec3 rot
		{
			get;
			set;
		}

		float rotX
		{
			get;
			set;
		}

		float rotY
		{
			get;
			set;
		}

		float rotZ
		{
			get;
			set;
		}

		Vec3 scale
		{
			get;
			set;
		}

		float scaleX
		{
			get;
			set;
		}

		float scaleY
		{
			get;
			set;
		}

		float scaleZ
		{
			get;
			set;
		}

		Vec3 axisX
		{
			get;
		}

		Vec3 axisY
		{
			get;
		}

		Vec3 axisZ
		{
			get;
		}

		Vec3 rayCast(Vec3 origin, Vec3 dir);
	}
}
