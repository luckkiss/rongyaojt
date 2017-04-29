using System;
using UnityEngine;

namespace Cross
{
	public class PhysicsObject3DImpl : PhysicsObjectImpl, IPhysicsObject3D, IPhysicsObject
	{
		protected GameObject m_u3dObj = null;

		protected Vector3 m_u3dPos = default(Vector3);

		protected Vector3 m_u3dRot = default(Vector3);

		protected Vector3 m_u3dScale = new Vector3(1f, 1f, 1f);

		public override bool enabled
		{
			get
			{
				return this.m_u3dObj.activeSelf;
			}
			set
			{
				this.m_u3dObj.SetActive(value);
			}
		}

		public Vec3 pos
		{
			get
			{
				return new Vec3(this.m_u3dPos.x, this.m_u3dPos.y, this.m_u3dPos.z);
			}
			set
			{
				this.m_u3dPos.Set(value.x, value.y, value.z);
				this.m_u3dObj.transform.localPosition = this.m_u3dPos;
			}
		}

		public float x
		{
			get
			{
				return this.m_u3dPos.x;
			}
			set
			{
				this.m_u3dPos.x = value;
				this.m_u3dObj.transform.localPosition = this.m_u3dPos;
			}
		}

		public float y
		{
			get
			{
				return this.m_u3dPos.y;
			}
			set
			{
				this.m_u3dPos.y = value;
				this.m_u3dObj.transform.localPosition = this.m_u3dPos;
			}
		}

		public float z
		{
			get
			{
				return this.m_u3dPos.z;
			}
			set
			{
				this.m_u3dPos.z = value;
				this.m_u3dObj.transform.localPosition = this.m_u3dPos;
			}
		}

		public Vec3 rot
		{
			get
			{
				return new Vec3(this.m_u3dRot.x, this.m_u3dRot.y, this.m_u3dRot.z);
			}
			set
			{
				this.m_u3dRot.Set(value.x, value.y, value.z);
				this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
			}
		}

		public float rotX
		{
			get
			{
				return this.m_u3dRot.x;
			}
			set
			{
				this.m_u3dRot.x = value;
				this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
			}
		}

		public float rotY
		{
			get
			{
				return this.m_u3dRot.y;
			}
			set
			{
				this.m_u3dRot.y = value;
				this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
			}
		}

		public float rotZ
		{
			get
			{
				return this.m_u3dRot.z;
			}
			set
			{
				this.m_u3dRot.z = value;
				this.m_u3dObj.transform.localEulerAngles = this.m_u3dRot;
			}
		}

		public Vec3 scale
		{
			get
			{
				return new Vec3(this.m_u3dScale.x, this.m_u3dScale.y, this.m_u3dScale.z);
			}
			set
			{
				this.m_u3dScale.Set(value.x, value.y, value.z);
				this.m_u3dObj.transform.localScale = this.m_u3dScale;
			}
		}

		public float scaleX
		{
			get
			{
				return this.m_u3dScale.x;
			}
			set
			{
				this.m_u3dScale.x = value;
				this.m_u3dObj.transform.localScale = this.m_u3dScale;
			}
		}

		public float scaleY
		{
			get
			{
				return this.m_u3dScale.y;
			}
			set
			{
				this.m_u3dScale.y = value;
				this.m_u3dObj.transform.localScale = this.m_u3dScale;
			}
		}

		public float scaleZ
		{
			get
			{
				return this.m_u3dScale.z;
			}
			set
			{
				this.m_u3dScale.z = value;
				this.m_u3dObj.transform.localScale = this.m_u3dScale;
			}
		}

		public Vec3 axisX
		{
			get
			{
				Vector3 right = this.m_u3dObj.transform.right;
				return new Vec3(right.x, right.y, right.z);
			}
		}

		public Vec3 axisY
		{
			get
			{
				Vector3 up = this.m_u3dObj.transform.up;
				return new Vec3(up.x, up.y, up.z);
			}
		}

		public Vec3 axisZ
		{
			get
			{
				Vector3 forward = this.m_u3dObj.transform.forward;
				return new Vec3(forward.x, forward.y, forward.z);
			}
		}

		public GameObject u3dObject
		{
			get
			{
				return this.m_u3dObj;
			}
		}

		public PhysicsObject3DImpl()
		{
			this.m_u3dObj = new GameObject();
			this.m_u3dPos = this.m_u3dObj.transform.localPosition;
			this.m_u3dRot = this.m_u3dObj.transform.localEulerAngles;
			this.m_u3dScale = this.m_u3dObj.transform.localScale;
		}

		public virtual Vec3 rayCast(Vec3 origin, Vec3 dir)
		{
			return null;
		}
	}
}
